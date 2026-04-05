using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using YeniKitapKirtasiyeWebApp.Data.ViewModel;
using YeniKitapKirtasiyeWebApp.Models;

namespace YeniKitapKirtasiyeWebApp.Controllers
{
    public class UrunController : Controller
    {
        YeniKitapKirtasiyeDBModel db = new YeniKitapKirtasiyeDBModel();
        // GET: Urun
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CookieSepeteEkle(int id)
        {
            Product p = db.Products.Find(id);
            if (p == null)
            {
                RedirectToAction("UrunNotFound");
            }

            if (Request.Cookies["Sepet"] != null)
            {
                HttpCookie kurabiye = Request.Cookies["Sepet"];
                List<SepetItem> items = JsonConvert.DeserializeObject<List<SepetItem>>(kurabiye.Value);
                var sayi = items.Where(x => x.UrunId == id).Count();
                if (sayi > 0)
                {
                    items.First(x => x.UrunId == id).Adet++;
                }
                else
                {
                    items.Add(new SepetItem() { UrunId = p.ID, UrunAdi = p.Name, Fiyat = p.Price, Adet = 1 });
                }
                var settings = new JsonSerializerSettings { StringEscapeHandling = StringEscapeHandling.EscapeNonAscii };
                string jsoncart = JsonConvert.SerializeObject(items, Formatting.None, settings);
                kurabiye.Value = jsoncart;
                kurabiye.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Add(kurabiye); //Response == Browser(Hangi browser kullanıyok ise)
            }
            else
            {
                HttpCookie kurabiye = new HttpCookie("Sepet");
                List<SepetItem> items = new List<SepetItem>();
                items.Add(new SepetItem() { UrunId = p.ID, UrunAdi = p.Name, Fiyat = p.Price, Adet = 1 });
                var settings = new JsonSerializerSettings { StringEscapeHandling = StringEscapeHandling.EscapeNonAscii };
                string jsoncart = JsonConvert.SerializeObject(items, Formatting.None, settings);
                kurabiye.Value = jsoncart;
                kurabiye.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Add(kurabiye); //Response == Browser(Hangi browser kullanıyok ise)
            }
            return RedirectToAction("index", "Urun"); // sepete'de yönlendirme yapılabilir
        }

        public ActionResult CookieSepetTemizle()
        {
            if (Request.Cookies["Sepet"] != null)
            {
                Response.Cookies.Remove("Sepet");
                HttpCookie kurabiye = new HttpCookie("Sepet");
                kurabiye.Value = null;
                kurabiye.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(kurabiye);
            }
            return RedirectToAction("Sepet");
        }

        public ActionResult CookieSepet()
        {
            List<SepetItem> item = new List<SepetItem>();

            if (Request.Cookies["Sepet"] != null)
            {
                HttpCookie kurabiye = Request.Cookies["Sepet"];
                item = JsonConvert.DeserializeObject<List<SepetItem>>(kurabiye.Value);
            }
            return View(item);
        }

        public ActionResult OnaylaCookie(string kartIsim, string kartNo, string sonKullanma, string cvv)
        {
            try
            {
                List<SepetItem> sepet = null;
                HttpCookie sepetCookie = Request.Cookies["Sepet"];
                if (sepetCookie != null && !string.IsNullOrEmpty(sepetCookie.Value))
                {
                    sepet = JsonConvert.DeserializeObject<List<SepetItem>>(sepetCookie.Value);
                }
                if (sepet == null || !sepet.Any())
                {
                    TempData["SatinAlmaDurum"] = "başarısız";
                    return RedirectToAction("Index", "Sepet");
                }

                int userId = Convert.ToInt32(Session["UserID"]);
                decimal toplamTutar = sepet.Sum(x => x.Toplam);

                var order = new Orders
                {
                    CustomerID = userId,
                    OrderDateTime = DateTime.Now,
                    SumPrice = toplamTutar,
                    KartIsim = kartIsim,
                    KartNo = kartNo,
                    SonKullanmaTarihi = sonKullanma,
                    CVV = cvv
                };
                db.Orders.Add(order);
                db.SaveChanges();

                foreach (var item in sepet)
                {
                    var detail = new OrderDetails
                    {
                        OrderID = order.ID,
                        ProductID = item.UrunId,
                        Price = item.Fiyat,
                        Quantity = item.Adet
                    };
                    db.OrderDetails.Add(detail);
                }
                db.SaveChanges();

                if (sepetCookie != null)
                {
                    sepetCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(sepetCookie);
                }

                TempData["SatinAlmaDurum"] = "başarılı";
                return RedirectToAction("Index", "Sepet");
            }
            catch
            {
                TempData["SatinAlmaDurum"] = "başarısız";
                return RedirectToAction("Index", "Sepet");
            }
        }

        [HttpGet]
        public ActionResult Ode()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ode(OdeViewModel model)
        {
            if (ModelState.IsValid)
            {
                string[] tarih = model.Expiry.Split('/');
                List<SepetItem> item = new List<SepetItem>();
                if (Request.Cookies["sepet"] != null)
                {
                    HttpCookie kurabiye = Request.Cookies["sepet"];
                    item = JsonConvert.DeserializeObject<List<SepetItem>>(kurabiye.Value);
                }
                decimal price = item.Sum(x => x.Fiyat * x.Adet);
                string fiyatstr = price.ToString().Replace(",", ".");
                string apiurl = "https://localhost:44326/API/PayAPI?merchandID=1597536789&merchandPassword=12346789&price=" + fiyatstr + "&CardNumber=" + model.CardNumber + "&Cvv=" + model.CVV + "&month=" + tarih[0] + "&year=" + tarih[1];
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.GetAsync(apiurl).Result;
                var strinResp = response.Content.ReadAsStringAsync();

                //Switch Case yapısı ile yazılabilir

                if (strinResp.Result == "\'900\'" || strinResp.Result == "\'901\'" || strinResp.Result == "\'800\'" || strinResp.Result == "\'801\'" || strinResp.Result == "\'902\'" || strinResp.Result == "\'903\'")
                {
                    ViewBag.Durum = "Ödeme Sırasında geçici bir hata oluştu. Lütfen Daha Sonra Tekrar Deneyiniz.";
                }
                if (strinResp.Result == "\'600\'")
                {
                    ViewBag.Durum = "Kart Numarası Hatalı ! ";
                }
                if (strinResp.Result == "\'625\'")
                {
                    ViewBag.Durum = "Son Kullanma Tarihi Hatalı ! ";
                }
                if (strinResp.Result == "\'602\'")
                {
                    ViewBag.Durum = "Cvv Hatalı ! ";
                }
                if (strinResp.Result == "\'603\'")
                {
                    ViewBag.Durum = "Tutar Hatası ! ";
                }
                if (strinResp.Result == "\'313\'")
                {
                    ViewBag.Durum = "Kart Bakiyesi Yetersiz ! ";
                }
                if (strinResp.Result == "\'101\'")
                {
                    //Cookie'yi boşaltmayı Unutma !!!! ve order details gibi bir veri tabanına kaydet !!!! (NORTWND veritabanındaki order ve order details gibi aynısını yap)
                    Response.Cookies.Remove("Sepet");
                    HttpCookie kurabiye = new HttpCookie("Sepet");
                    kurabiye.Value = null;
                    kurabiye.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(kurabiye);
                    return RedirectToAction("Index", "Sepet"); //Ödeme Başarılı Sayfası Yapılabilir
                }
            }
            else
            {
                ViewBag.Durum = "Lütfen İşaretli Alanları Doldurun!";
            }
            return View(model);
        }
    }
}
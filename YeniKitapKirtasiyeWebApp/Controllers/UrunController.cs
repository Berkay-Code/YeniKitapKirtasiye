using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                    // sepete'de yönlendirme yapılabilir
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
                HttpCookie kurabiye = new HttpCookie("sepet");
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
            if (Request.Cookies["sepet"] != null)
            {
                Response.Cookies.Remove("sepet");
                HttpCookie kurabiye = new HttpCookie("sepet");
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
    }
}
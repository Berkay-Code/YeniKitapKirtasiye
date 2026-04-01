using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YeniKitapKirtasiyeWebApp.Data.Helper;
using YeniKitapKirtasiyeWebApp.Data.ViewModel;
using YeniKitapKirtasiyeWebApp.Filters;
using YeniKitapKirtasiyeWebApp.Models;

namespace YeniKitapKirtasiyeWebApp.Controllers
{
    [UserAuthenticationFilter]
    public class SepetController : Controller
    {
        YeniKitapKirtasiyeDBModel db = new YeniKitapKirtasiyeDBModel();
        // GET: Sepet
        public ActionResult Index()
        {
            List<SepetItem> sepet = SepetHelper.SepetAl(Session);
            return View(sepet);
        }
        public ActionResult Cikar(int urunId)
        {
            SepetHelper.UrunCikar(Session, urunId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Onayla()
        {
            List<SepetItem> sepet = SepetHelper.SepetAl(Session);
            if (sepet == null)
            {
                return RedirectToAction("Index");
            }

            return View(sepet);
        }

        [HttpPost]
        public ActionResult Onayla(string kartIsim, string kartNo, string sonKullanma, string cvv)
        {
            try
            {
                var sepet = SepetHelper.SepetAl(Session);
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

                SepetHelper.SepetTemizle(Session);
                TempData["SatinAlmaDurum"] = "başarılı";

                return RedirectToAction("Index", "Sepet");
            }
            catch
            {
                TempData["SatinAlmaDurum"] = "başarısız";
                return RedirectToAction("Index", "Sepet");
            }
        }

        public ActionResult AdetGuncelle(int urunId, string islem)
        {
            List<SepetItem> sepet = SepetHelper.SepetAl(Session);
            SepetItem item = sepet.FirstOrDefault(x => x.UrunId == urunId);

            if (item != null)
            {
                int yeniAdet = islem == "artir" ? item.Adet + 1 : item.Adet - 1;
                SepetHelper.AdetGuncelle(Session, urunId, yeniAdet);
            }

            return RedirectToAction("Index");
        }
    }
}
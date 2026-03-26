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
    public class UserProductController : Controller
    {
        YeniKitapKirtasiyeDBModel db = new YeniKitapKirtasiyeDBModel();
        // GET: UserProduct
        public ActionResult Find(int? id)
        {
            if (id != null)
            {
                Product urun = db.Products.Find(id);
                return View(urun);
            }
            else
            {
                return RedirectToAction("Index", "UserHome");
            }
        }

        [UserAuthenticationFilter]
        public ActionResult SepeteEkle(int id)
        {
            Product urun = db.Products.FirstOrDefault(u => u.ID == id);

            if (urun == null)
            {
                return HttpNotFound();
            }

            var item = new SepetItem
            {
                UrunId = urun.ID,
                UrunAdi = urun.Name,
                Fotograf = urun.ImagePath,
                Fiyat = urun.Price,
                Adet = 1
            };

            SepetHelper.UrunEkle(Session, item);

            return RedirectToAction("Index", "Sepet");
        }
    }
}
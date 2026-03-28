using System;
using System.Collections.Generic;
using System.Linq;
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
            Product urun = db.Products.Find(id);
            if (urun == null)
            {
                return RedirectToAction("UrunNotFound");
            }
            List<SepetItem> items = new List<SepetItem>();

            if (Request.Cookies["Sepet"] != null)
            {

            }
            else
            {
                HttpCookie kurabiye = new HttpCookie("Sepet");
            }

            return View();
        }
    }
}
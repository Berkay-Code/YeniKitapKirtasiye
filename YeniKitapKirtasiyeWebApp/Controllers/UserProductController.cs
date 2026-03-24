using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YeniKitapKirtasiyeWebApp.Models;

namespace YeniKitapKirtasiyeWebApp.Controllers
{
    public class UserProductController : Controller
    {
        YeniKitapKirtasiyeDBModel db = new YeniKitapKirtasiyeDBModel();
        // GET: UserProduct
        public ActionResult Find(int? id)
        {
            Product urun = db.Products.Find(id);
            return View(urun);
        }
    }
}
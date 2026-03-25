using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YeniKitapKirtasiyeWebApp.Filters;
using YeniKitapKirtasiyeWebApp.Models;

namespace YeniKitapKirtasiyeWebApp.Controllers
{
    [UserAuthenticationFilter]
    public class UserHomeController : Controller
    {
        YeniKitapKirtasiyeDBModel db = new YeniKitapKirtasiyeDBModel();
        // GET: UserHome
        public ActionResult Index()
        {
            return View(db.Products.ToList().Where(item => item.IsActive == true));
        }
    }
}
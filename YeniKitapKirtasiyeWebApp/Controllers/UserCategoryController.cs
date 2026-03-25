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
    public class UserCategoryController : Controller
    {
        YeniKitapKirtasiyeDBModel db = new YeniKitapKirtasiyeDBModel();
        // GET: UserCategory
        public ActionResult Find(int? kategoriID)
        {
            if (kategoriID != null)
            {
                return View(db.Products.Where(item => item.Category_ID == kategoriID && item.IsActive == true).ToList());
            }
            else
            {
                return RedirectToAction("Index", "UserHome");
            }
        }
    }
}
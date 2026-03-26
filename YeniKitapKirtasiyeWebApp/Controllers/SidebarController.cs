using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YeniKitapKirtasiyeWebApp.Models;

namespace YeniKitapKirtasiyeWebApp.Controllers
{
    public class SidebarController : BaseController
    {
        [ChildActionOnly] // Sadece html.action ile çağrılabilir
        public ActionResult Categories()
        {
            List<Category> kategoriler = db.Categories.ToList();
            return PartialView("_CategorySidebar", kategoriler);
        }
    }
}
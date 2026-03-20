using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YeniKitapKirtasiyeWebApp.Controllers
{
    public class SidebarController : BaseController
    {
        [ChildActionOnly] // Sadece html.action ile çağrılabilir
        public ActionResult Categories()
        {
            var kategoriler = db.Categories.ToList();
            return PartialView("_CategorySidebar", kategoriler);
        }
    }
}
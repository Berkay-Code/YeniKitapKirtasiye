using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YeniKitapKirtasiyeWebApp.Models;

namespace YeniKitapKirtasiyeWebApp.Controllers
{
    public class BaseController : Controller
    {
        protected YeniKitapKirtasiyeDBModel db = new YeniKitapKirtasiyeDBModel(); // Protected yazılmaz ise yandan ulaşılamaz
    }
}
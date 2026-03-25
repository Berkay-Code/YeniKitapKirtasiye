using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YeniKitapKirtasiyeWebApp.Data.Helper;
using YeniKitapKirtasiyeWebApp.Data.ViewModel;
using YeniKitapKirtasiyeWebApp.Filters;

namespace YeniKitapKirtasiyeWebApp.Controllers
{
    [UserAuthenticationFilter]
    public class SepetController : Controller
    {
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

        public ActionResult Onayla()
        {
            List<SepetItem> sepet = SepetHelper.SepetAl(Session);
            if (sepet == null)
            {
                return RedirectToAction("Index");
            }

            return View(sepet);
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
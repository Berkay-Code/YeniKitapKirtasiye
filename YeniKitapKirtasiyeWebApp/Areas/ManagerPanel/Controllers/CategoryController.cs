using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using YeniKitapKirtasiyeWebApp.Filters;
using YeniKitapKirtasiyeWebApp.Models;

namespace YeniKitapKirtasiyeWebApp.Areas.ManagerPanel.Controllers
{
    [ManagerAuthenticationFilter]
    public class CategoryController : Controller
    {
        private YeniKitapKirtasiyeDBModel db = new YeniKitapKirtasiyeDBModel();

        // GET: ManagerPanel/Category
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        // GET: ManagerPanel/Category/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid) //Sınıfın içine gider bakar ve eğer veri var ise içindekini çalıştırır(Sadece Attribute'ları kontrol eder!!!!!)
            {
                try
                {
                    db.Categories.Add(category);
                    db.SaveChanges();
                    TempData["basarili"] = "Kategori Ekleme Başarılı"; // bu herhangi bir view'a giderken taşınabilir
                }
                catch (Exception)
                {
                    TempData["basarisiz"] = "Kategori Eklenirken Bi Hata Oluştu";
                }

                return RedirectToAction("Index");
            }

            return View(category); //Eğer burda view'ın içini boş bırakır isek validation error'ların görünmez!!!!
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            return View(category);
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

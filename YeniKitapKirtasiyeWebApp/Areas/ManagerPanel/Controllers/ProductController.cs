using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YeniKitapKirtasiyeWebApp.Filters;
using YeniKitapKirtasiyeWebApp.Models;

namespace YeniKitapKirtasiyeWebApp.Areas.ManagerPanel.Controllers
{
    [ManagerAuthenticationFilter]
    public class ProductController : Controller
    {
        YeniKitapKirtasiyeDBModel db = new YeniKitapKirtasiyeDBModel();
        // GET: ManagerPanel/Product
        public ActionResult Index()
        {
            List<Product> urunler = db.Products.Where(x => x.IsDeleted == false).ToList();
            return View(urunler);
        }
        public ActionResult AllIndex()
        {
            List<Product> urunler = db.Products.ToList();
            return View(urunler);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Category_ID = new SelectList(db.Categories, "ID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product model, HttpPostedFileBase urunResim)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (urunResim != null)
                    {
                        FileInfo fi = new FileInfo(urunResim.FileName);
                        string uzanti = fi.Extension;
                        string isim = Guid.NewGuid().ToString();
                        string tamIsim = isim + uzanti;
                        urunResim.SaveAs(Server.MapPath("~/Assets/ProductImages/" + tamIsim));
                        model.ImagePath = tamIsim;
                    }
                    else
                    {
                        model.ImagePath = "none.png";
                    }
                    db.Products.Add(model);
                    db.SaveChanges();
                    TempData["basarili"] = "Ürün Ekleme İşlemi Başarılı";
                }
                catch
                {
                    TempData["basarisiz"] = "Ürün Ekleme İşlemi Başarısız";
                }
            }
            ViewBag.Category_ID = new SelectList(db.Categories, "ID", "Name", model.Category_ID);
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Product");
            }
            Product p = db.Products.Find(id);
            if (p == null)
            {
                return RedirectToAction("Index", "Product");
            }
            ViewBag.Category_ID = new SelectList(db.Categories, "ID", "Name", p.category.ID);
            return View(p);
        }

        [HttpPost]
        public ActionResult Edit(Product model, HttpPostedFileBase urunResim)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    if (urunResim != null)
                    {
                        FileInfo fi = new FileInfo(urunResim.FileName);
                        string uzanti = fi.Extension;
                        string isim = Guid.NewGuid().ToString();
                        string tamIsim = isim + uzanti;
                        urunResim.SaveAs(Server.MapPath("~/Assets/ProductImages/" + tamIsim));
                        model.ImagePath = tamIsim;
                    }
                    TempData["basarili"] = "Güncelleme İşlemi Başarılı";
                    db.SaveChanges();
                }
                catch
                {
                    TempData["basarisiz"] = "Güncelleme İşlemi Başarısız";
                }
            }
            ViewBag.Category_ID = new SelectList(db.Categories, "ID", "Name", model.category.ID);
            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Product product = db.Products.Find(id);
            product.IsDeleted = true;
            product.IsActive = false;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult BackUp(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Product product = db.Products.Find(id);
            product.IsDeleted = false;
            product.IsActive = false;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
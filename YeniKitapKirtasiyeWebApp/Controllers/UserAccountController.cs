using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YeniKitapKirtasiyeWebApp.Data.ViewModel;
using YeniKitapKirtasiyeWebApp.Models;

namespace YeniKitapKirtasiyeWebApp.Controllers
{
    public class UserAccountController : Controller
    {
        YeniKitapKirtasiyeDBModel db = new YeniKitapKirtasiyeDBModel();
        // GET: UserAccount
        [HttpGet]
        public ActionResult Login()
        {
            if (Session["UserName"] != null)
            {
                return RedirectToAction("Index", "UserHome");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            User kullanici = db.Users.FirstOrDefault(item => item.Mail == model.Mail && item.Password == model.Password);
            if (kullanici == null)
            {
                ViewBag.Hata = "Email veya şifre hatalı.";
                return View(model);
            }
            Session["UserName"] = kullanici.Name + " " + kullanici.Surname;

            return RedirectToAction("Index", "UserHome");
        }

        [HttpGet]
        public ActionResult Register()
        {
            if (Session["UserName"] != null)
            {
                return RedirectToAction("Index", "UserHome");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (model.Password != model.PasswordAgain)
            {
                ViewBag.Hata = "Şifreler eşleşmiyor.";
                return View(model);
            }

            User mevcutKullanici = db.Users.FirstOrDefault(item => item.Mail == model.Mail);

            if (mevcutKullanici != null)
            {
                ViewBag.Hata = "Bu email zaten kayıtlı.";
                return View(model);
            }
            User kullanici = new User
            {
                Name = model.Name,
                Surname = model.Surname,
                Mail = model.Mail,
                Password = model.Password,
                CreationTime = DateTime.Now,
                IsActive = true
            };

            db.Users.Add(kullanici);
            db.SaveChanges();
            Session["UserName"] = kullanici.Name + " " + kullanici.Surname;

            return RedirectToAction("Index", "UserHome");
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "UserHome");
        }
    }
}
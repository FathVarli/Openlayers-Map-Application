using BasarSoftStajMVC.DataAccess.Concrete;
using BasarSoftStajMVC.DataBase;
using BasarSoftStajMVC.Model;
using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using System.Web.Security;

namespace BasarSoftStajMVC.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult NewAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewAccount(NewAccountModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var user = new User
            {
                Name = model.Name,
                EMail = model.EMail,
                Password = model.Password
            };

            EfUserDal efUserDal = new EfUserDal();
            efUserDal.Add(user);
            return RedirectToAction("RegisterSuccess");
        }

        public ActionResult RegisterSuccess()
        {
            return View();
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LogInModel model)
        {
            EfUserDal efUserDal = new EfUserDal();
            var user = efUserDal.Get(u => u.Name == model.UserName && u.Password == model.Password);
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.Name, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.LoginError = "Kullanıcı adı veya şifre yanlış";
                return View();
            }           
        }

        [Authorize]
        public ActionResult Update()
        {
            return View();
        }

        [Authorize]
        [HttpPut]
        public ActionResult Update(UserUpdateModel userUpdateModel)
        {
            EfUserDal efUserDal = new EfUserDal();
            var userid = (int)Session["uyeId"];
            var user = efUserDal.Get(u => u.UserId == userid);
            if (user != null)
            {
                user.Name = userUpdateModel.Name;
                user.EMail = userUpdateModel.Email;
                user.Password = userUpdateModel.Password;
                efUserDal.Update(user);
                return View();
            }
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogIn");
        }
    }
}
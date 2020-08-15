using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using System.Web.Security;
using Business.Abstract;
using Core.Models;

namespace MVCWebUI.Controllers
{
    public class AccountController : Controller
    {
        private IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = _authService.Register(model);
            if (result.Success)
            {
                FormsAuthentication.SetAuthCookie(result.Data.UserName, false);
                return RedirectToAction("GetMap","Map");
            }

            ViewBag.RegisterError = result.Message;
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            var result = _authService.Login(model);
            if (result.Success)
            {
                FormsAuthentication.SetAuthCookie(result.Data.UserName, false);
                return RedirectToAction("GetMap", "Map");
            }
            else
            {
                ViewBag.LoginError = result.Message;
                return View();
            }
        }

        [Authorize]
        public ActionResult Update()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Update(UserUpdateModel userUpdateModel)
        {
            var userName = HttpContext.User.Identity.Name;
            var result = _authService.Update(userName, userUpdateModel);
            if (result.Success)
            {
                ViewBag.SuccesUpdate = result.Message;
                return View();
            }

            ViewBag.UpdateError = result.Message;
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
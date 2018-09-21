using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using RMS.App.Models;
using RMS.Models.EntityModels.Identity;
using RMS.Models.Identity.IdentityConfig;


namespace RMS.App.Controllers
{
    public class AccountController : Controller
    {
        private AppUserManager _userManager;
        private AppSignInManager _signInManager;

        private AppUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().Get<AppUserManager>(); }
        }

        private AppSignInManager SignInManager
        {
            get { return _signInManager ?? HttpContext.GetOwinContext().Get<AppSignInManager>(); }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            var user = new AppUser()
            {
                Email = model.Email,
                UserName = model.Email
            };
            //var userRole = new AppUserRole()
            //{
            //    UserId = user.Id,
            //    RoleId = 2
            //};
            //user.Roles.Add(userRole);
            var result = UserManager.Create(user, model.Password);
            if (result.Succeeded)
            {
                //SignInManager.SignIn(user, false, false);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = SignInManager.PasswordSignIn(model.Email, model.Password, model.RememberMe, false);
                    if (result == SignInStatus.Success)
                    {
                        return RedirectToLocal(returnUrl);
                    }
                }
                ViewBag.ReturnUrl = returnUrl;
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex ,"Account", "Login"));
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        //
        // POST: /Account/LogOff

        [HttpPost]

        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");

        }
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateController()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateController(RegisterViewModel model)
        {
            var user = new AppUser()
            {
                Email = model.Email,
                UserName = model.Email
            };
            var result = UserManager.Create(user,model.Password);
            if (result.Succeeded)
            {
                var roleAdded = UserManager.AddToRole(user.Id, "Controller");
                
            }
            {
                //SignInManager.SignIn(user, false, false);
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
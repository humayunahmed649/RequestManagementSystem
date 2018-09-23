using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using IdentitySample.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using RMS.App.Models;
using RMS.App.ViewModels;
using RMS.BLL.Contracts;
using RMS.Models.EntityModels;
using RMS.Models.EntityModels.Identity;
using RMS.Models.Identity.IdentityConfig;


namespace RMS.App.Controllers
{
    public class AccountController : Controller
    {
        private AppUserManager _userManager;
        private AppSignInManager _signInManager;
        private IEmployeeManager _employeeManager;

        public AccountController(IEmployeeManager employeeManager)
        {
            this._employeeManager = employeeManager;
        }

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

                        if (User.IsInRole("Administrator") || User.IsInRole("Controller"))
                        {
                            return RedirectToAction("Index", "Queue");
                        }

                        if (User.IsInRole("User"))
                        {
                            return RedirectToAction("Create", "Requisitions");
                        }

                        return RedirectToLocal(returnUrl);
                    }else
                    {
                        ModelState.AddModelError("", "Invalid email or password!");
                        ViewBag.ReturnUrl = returnUrl;
                        return View(model);
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

        // GET: /Manage/Index  Account/Change Password Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(Convert.ToInt32(userId)),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(Convert.ToInt32(userId)),
                Logins = await UserManager.GetLoginsAsync(Convert.ToInt32(userId)),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _employeeManager.FindByLoginId(Convert.ToInt32(userId));
            if (employee == null)
            {
                return HttpNotFound();
            }
            EmployeeViewModel employeeViewModel = Mapper.Map<EmployeeViewModel>(employee);
            model.Model = employeeViewModel;
            return View(model);
        }


        public ActionResult ChangePassword()
        {
            
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(Convert.ToInt32(User.Identity.GetUserId()), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(Convert.ToInt32(User.Identity.GetUserId()));
                
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }
        private bool HasPassword()
        {
            var user = UserManager.FindById<AppUser,int>(Convert.ToInt32(User.Identity.GetUserId()));
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}
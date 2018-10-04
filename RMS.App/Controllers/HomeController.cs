using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using RMS.App.ViewModels;
using RMS.BLL.Contracts;
using RMS.Models.EntityModels;

namespace RMS.App.Controllers
{
    public class HomeController : Controller
    {
        private IContactManager _contactManager;

        public HomeController(IContactManager contactManager)
        {
            this._contactManager = contactManager;
        }
        public ActionResult Index()
        {
            try
            {

                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Home", "Index"));
            }
        }

        public ActionResult About()
        {
            try
            {

                ViewBag.Message = "Your application description page.";

                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Home", "About"));
            }
        }

        public ActionResult Contact()
        {
            try
            {
                ViewBag.MessageCount = _contactManager.GetAll().Count;
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Home", "Contact"));
            }
        }

        [HttpPost]
        public ActionResult Contact([Bind(Include = "Email,PhoneNo,Message")] ContactViewModel contactViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ContactModel contactm = Mapper.Map<ContactModel>(contactViewModel);

                    _contactManager.Add(contactm);
                    TempData["msg"] = "Message has been send successfully";
                    return RedirectToAction("Contact");
                }
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Home", "Contact"));
            }
        }

        public ActionResult AboutDeveloper()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Home", "AboutDeveloper"));
            }
        }

        [Authorize(Roles= "User,Administrator,Controller")]
        public ActionResult Help()
        {
            try
            {

                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Home", "AboutDeveloper"));
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMS.App.Controllers
{
    public class HomeController : Controller
    {
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

                ViewBag.Message = "Your contact page.";

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

                ViewBag.Message = "Developer page.";

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

                ViewBag.Message = "Help page.";

                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Home", "AboutDeveloper"));
            }
        }
    }
}
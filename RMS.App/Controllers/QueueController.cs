using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RMS.App.Controllers
{
    [Authorize(Roles = "Controller,Administrator")]

    public class QueueController : Controller
    {
        // GET: SetupAll
        public ActionResult Index()
        {
            return View();
        }
    }
}
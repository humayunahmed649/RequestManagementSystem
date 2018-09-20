using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RMS.BLL.Contracts;

namespace RMS.App.Controllers
{
    [Authorize(Roles = "Controller,Administrator")]

    public class QueueController : Controller
    {
        private INotificationManager _notificationManager;

        public QueueController(INotificationManager notificationManager)
        {
            this._notificationManager = notificationManager;
        }

        // GET: SetupAll
        public ActionResult Index()
        {
            var notification = _notificationManager.GetNotificationsForController("Unseen");
            var notificationCount = notification.Count;
            if (notification!=null)
            {
                ViewBag.Notification = notification;
                ViewBag.count = notificationCount;
            }
             
            return View();
        }
    }
}
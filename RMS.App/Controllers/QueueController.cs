using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using RMS.App.ViewModels;
using RMS.BLL.Contracts;
using RMS.Models.EntityModels;

namespace RMS.App.Controllers
{
    [Authorize(Roles = "Controller,Administrator")]

    public class QueueController : Controller
    {
        private INotificationManager _notificationManager;
        private IRequisitionManager _requisitionManager;
        private IRequisitionStatusManager _requisitionStatusManager;
        private IEmployeeManager _employeeManager;
        private IVehicleManager _vehicleManager;
        private IVehicleTypeManager _vehicleTypeManager;
        private IContactManager _contactManager;

        public QueueController(INotificationManager notificationManager,IRequisitionManager requisitionManager,IRequisitionStatusManager requisitionStatusManager,IEmployeeManager employeeManager,IVehicleManager vehicleManager,IVehicleTypeManager vehicleTypeManager, IContactManager contactManager)
        {
            this._notificationManager = notificationManager;
            this._requisitionManager = requisitionManager;
            this._requisitionStatusManager = requisitionStatusManager;
            this._employeeManager = employeeManager;
            this._vehicleManager = vehicleManager;
            this._vehicleTypeManager = vehicleTypeManager;
            this._contactManager = contactManager;
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

        public ActionResult RequisitionQueue()
        {
            //Notification Area
            var notification = _notificationManager.GetNotificationsForController("Unseen");
            var notificationCount = notification.Count;
            if (notification != null)
            {
                ViewBag.Notification = notification;
                ViewBag.count = notificationCount;
            }

            //Dash Board Area
            ViewBag.requisitionCount = _requisitionManager.GetAll().Count;
            ViewBag.RequisitionStatusCount = _requisitionStatusManager.GetAllStatusNew().Count;

            ViewBag.CompleteRequisition =
                _requisitionStatusManager.GetAll().Count(c => c.StatusType == "Completed");
            ViewBag.CancelRequisition =
    _requisitionStatusManager.GetAll().Count(c => c.StatusType == "Cancelled");

            ViewBag.OnProcessRequest = _requisitionStatusManager.GetAllStatusExecute().Count;
            ViewBag.EmployeeCount = _employeeManager.GetAllEmployees().Count;
            ViewBag.DriverCount = _employeeManager.GetAllDriver().Count;
            ViewBag.VehicleCount = _vehicleManager.GetAll().Count;
            ViewBag.VehicleTypeCount = _vehicleTypeManager.GetAll().Count;
            return View();
        }


        [HttpGet]
        public ActionResult GetMessage()
        {
            try
            {
                ICollection<ContactModel> contact = _contactManager.GetAll();
                IEnumerable<ContactViewModel> contactViewModels = Mapper.Map<IEnumerable<ContactViewModel>>(contact);
                return View(contactViewModels);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Queue", "GetMessage"));
            }
        }


        public ActionResult ReplyMail(string email)
        {
            try
            {
                if (!email.IsNullOrWhiteSpace())
                {
                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                    smtpClient.Credentials = new NetworkCredential("demowork9999@gmail.com", "~Aa123456");
                    smtpClient.EnableSsl = true;


                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("demowork9999@gmail.com");
                    mailMessage.To.Add(new MailAddress(email));
                    mailMessage.Subject = "Thanks for your message";
                    mailMessage.Body = "We will try to improve our service";
                    smtpClient.Send(mailMessage);

                    TempData["msg"] = "Mail has been send successfully";
                    return RedirectToAction("GetMessage");
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Queue", "GetMessage"));
            }
            return View();
        }
    }
}
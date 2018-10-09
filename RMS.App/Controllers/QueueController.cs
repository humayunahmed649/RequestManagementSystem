using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
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
        private IOrganizationManager _organizationManager;
        private IDepartmentManager _departmentManager;
        private IDesignationManager _designationManager;
        private IEmployeeTypeManager _employeeTypeManager;

        public QueueController(INotificationManager notificationManager,IRequisitionManager requisitionManager,IRequisitionStatusManager requisitionStatusManager,
            IEmployeeManager employeeManager,IVehicleManager vehicleManager,IVehicleTypeManager vehicleTypeManager, IContactManager contactManager,
            IOrganizationManager organizationManager, IDepartmentManager departmentManager, IDesignationManager designationManager, IEmployeeTypeManager employeeTypeManager)
        {
            this._notificationManager = notificationManager;
            this._requisitionManager = requisitionManager;
            this._requisitionStatusManager = requisitionStatusManager;
            this._employeeManager = employeeManager;
            this._vehicleManager = vehicleManager;
            this._vehicleTypeManager = vehicleTypeManager;
            this._contactManager = contactManager;
            this._organizationManager = organizationManager;
            this._departmentManager = departmentManager;
            this._designationManager = designationManager;
            this._employeeTypeManager = employeeTypeManager;

        }

        // GET: Setup All Queue

        public ActionResult SetupQueue()
        {
            try
            {
                ViewBag.OrganizationCount = _organizationManager.GetAll().Count;
                ViewBag.DepartmentCount = _departmentManager.GetAll().Count;
                ViewBag.DesignationCount = _designationManager.GetAll().Count;
                ViewBag.EmployeeTypeCount = _employeeTypeManager.GetAll().Count;
                ViewBag.EmployeeCount = _employeeManager.GetAllEmployees().Count;
                ViewBag.DriverCount = _employeeManager.GetAllDriver().Count;
                ViewBag.VehicleCount = _vehicleManager.GetAll().Count;
                ViewBag.VehicleTypeCount = _vehicleTypeManager.GetAll().Count;
                return View();
            }
            catch (Exception ex)
            {

                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "Queue", "SetupQueue"));
            }
            
        }

        // GET: Requisition Queue
        public ActionResult RequisitionQueue()
        {
            try
            {
                //Notification Area
                //var notification = _notificationManager.GetNotificationsForController("Unseen");
                //var notificationCount = notification.Count;
                //if (notification != null)
                //{
                //    ViewBag.Notification = notification;
                //    ViewBag.count = notificationCount;
                //}

                //Dash Board Area
                ViewBag.RequisitionStatusCount = _requisitionStatusManager.GetAllStatusNew().Count;
                ViewBag.AssignedRequisition = _requisitionStatusManager.GetAllAssignRequisitions().Count();
                ViewBag.AllRequisitionCount = _requisitionManager.GetAll().Count;
                ViewBag.OnProcessRequest = _requisitionStatusManager.GetAllStatusExecute().Count;
                ViewBag.CompleteRequisition = _requisitionStatusManager.GetAll().Count(c => c.StatusType == "Completed");
                ViewBag.CancelRequisition = _requisitionStatusManager.GetAll().Count(c => c.StatusType == "Cancelled");

                ViewBag.EmployeeCount = _employeeManager.GetAllEmployees().Count;
                ViewBag.DriverCount = _employeeManager.GetAllDriver().Count;
                ViewBag.VehicleCount = _vehicleManager.GetAll().Count;
                ViewBag.VehicleTypeCount = _vehicleTypeManager.GetAll().Count;

                return View();
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "Queue", "RequisitionQueue"));
            }
            
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
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "Queue", "GetMessage"));
            }
        }

        [HttpGet]
        public ActionResult ReplyMail(string email)
        {
            try
            {

                MailServiceViewModel mailModel = new MailServiceViewModel();
                ViewBag.Email = email;
                return View(mailModel);


            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "Queue", "GetMessage"));
            }
        }

        [HttpPost]
        public ActionResult ReplyMail(MailServiceViewModel mailModel,string email)
        {
            try
            {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new NetworkCredential("demowork9999@gmail.com", "~Aa123456");
            smtpClient.EnableSsl = true;


            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(mailModel.From);
            mailMessage.To.Add(new MailAddress(email));
            mailMessage.Subject = mailModel.Subject;
            mailMessage.Body = mailModel.Body;
            smtpClient.Send(mailMessage);

            TempData["msg"] = "Mail has been send successfully";
            return RedirectToAction("GetMessage");
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "Queue", "ReplyMail"));
            }

        }
        private void ExceptionMessage(Exception ex)
        {
            ViewBag.ErrorMsg = ex.Message;

            if (ex.InnerException != null)
            {
                ViewBag.ErrorMsg = ex.InnerException.Message;
            }
            if (ex.InnerException?.InnerException != null)
            {
                ViewBag.ErrorMsg = ex.InnerException.InnerException.Message;
            }
            if (ex.InnerException?.InnerException?.InnerException != null)
            {
                ViewBag.ErrorMsg = ex.InnerException.InnerException.InnerException.Message;
            }
        }
    }
}
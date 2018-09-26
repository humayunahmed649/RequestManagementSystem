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
        private IRequisitionManager _requisitionManager;
        private IRequisitionStatusManager _requisitionStatusManager;
        private IEmployeeManager _employeeManager;
        private IVehicleManager _vehicleManager;
        private IVehicleTypeManager _vehicleTypeManager;

        public QueueController(INotificationManager notificationManager,IRequisitionManager requisitionManager,IRequisitionStatusManager requisitionStatusManager,IEmployeeManager employeeManager,IVehicleManager vehicleManager,IVehicleTypeManager vehicleTypeManager)
        {
            this._notificationManager = notificationManager;
            this._requisitionManager = requisitionManager;
            this._requisitionStatusManager = requisitionStatusManager;
            this._employeeManager = employeeManager;
            this._vehicleManager = vehicleManager;
            this._vehicleTypeManager = vehicleTypeManager;
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
            ViewBag.requisitionCount = _requisitionManager.GetAll().Count;
            ViewBag.RequisitionStatusCount = _requisitionStatusManager.GetAllStatusNew().Count;
            ViewBag.CompleteRequisition =
                _requisitionStatusManager.GetAll().Where(c => c.StatusType == "Completed").Count();
            ViewBag.OnProcessRequest = _requisitionStatusManager.GetAllStatusExecute().Count;
            ViewBag.EmployeeCount = _employeeManager.GetAllEmployees().Count;
            ViewBag.DriverCount = _employeeManager.GetAllDriver().Count;
            ViewBag.VehicleCount = _vehicleManager.GetAll().Count;
            ViewBag.VehicleTypeCount = _vehicleTypeManager.GetAll().Count;
            return View();
        }
    }
}
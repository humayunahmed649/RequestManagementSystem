using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using RMS.App.ViewModels;
using RMS.BLL.Contracts;
using RMS.Models.DatabaseContext;
using RMS.Models.EntityModels;

namespace RMS.App.Controllers
{
    [Authorize(Roles = "Controller,Administrator,User")]
    public class NotificationsController : Controller
    {
        private INotificationManager _notificationManager; 
        private IEmployeeManager _employeeManager;
        private IRequisitionManager _requisitionManager;

        public NotificationsController(INotificationManager notificationManager, IEmployeeManager employeeManager, IRequisitionManager requisitionManager)
        {
            this._notificationManager = notificationManager;
            this._employeeManager = employeeManager;
            this._requisitionManager = requisitionManager;

        }
        // GET: Notifications
        public ActionResult Index()
        {
            try
            {
                //Get employee Id by user login id
                var loginUserId = Convert.ToInt32(User.Identity.GetUserId());
                var empId = _employeeManager.FindByLoginId(loginUserId);

                ICollection<Notification> notifications = _notificationManager.GetAllNotificationByEmployeeId(empId.Id);
                IEnumerable<NotificationViewModel> notificationViewModels = Mapper.Map<IEnumerable<NotificationViewModel>>(notifications);
                return View(notificationViewModels);
            }
            catch (Exception ex)
            {

                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "Notifications", "Index"));
            }
            
        }
        //GET: Notifications all For Employee
        public ActionResult AllNotification()
        {
            try
            {

                ICollection<Notification> notifications = _notificationManager.GetAllNotificationForController();
                IEnumerable<NotificationViewModel> notificationViewModels = Mapper.Map<IEnumerable<NotificationViewModel>>(notifications);
                return View(notificationViewModels);
            }
            catch (Exception ex)
            {

                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "Notifications", "AllNotification"));
            }
        }

        // GET: Notifications/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Notification notification = _notificationManager.FindById((int)id);
                if (notification == null)
                {
                    return HttpNotFound();
                }
                NotificationViewModel notificationViewModel = Mapper.Map<NotificationViewModel>(notification);
                return View(notificationViewModel);
            }
            catch (Exception ex)
            {

                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "Notifications", "Details"));
            }
            
        }


        //Load Notification for controller
        public ActionResult ControllerNotify()
        {
            var notification = _notificationManager.GetNotificationsForController("Unseen");
            var notificationCount = notification.Count;
            if (notification != null)
            {
                ViewBag.Notification = notification;
                ViewBag.count = notificationCount;
            }
            return View("_ControllerNotification");
        }


        //Load Notification for Employee
        public ActionResult EmployeeNotify()
        {
            //Get employee Id by user login id
            var loginUserId = Convert.ToInt32(User.Identity.GetUserId());
            var empId = _employeeManager.FindByLoginId(loginUserId);
            
            //Notification for employee
            var notification = _notificationManager.GetNotificationsForSender("Unseen", empId.Id);
            var notificationCount = notification.Count;
            if (notification != null)
            {
                ViewBag.Notification = notification;
                ViewBag.count = notificationCount;
            }
            return View("_EmployeeNotification");
        }


        // GET: Notifications/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.EmployeeId = new SelectList(_employeeManager.GetAll(), "Id", "FullName");
                ViewBag.RequisitionId = new SelectList(_requisitionManager.GetAll(), "Id", "RequisitionNumber");
                return View();
            }
            catch (Exception ex)
            {

                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "Notifications", "Create"));
            }
            
        }

        // POST: Notifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Text,EmployeeId,RequisitionId,NotifyDateTime,SenderViewStatus,ControllerViewStatus")] NotificationViewModel notificationViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Notification notification = Mapper.Map<Notification>(notificationViewModel);
                    _notificationManager.Add(notification);
                    return RedirectToAction("Index");
                }

                ViewBag.EmployeeId = new SelectList(_employeeManager.GetAll(), "Id", "FullName", notificationViewModel.EmployeeId);
                ViewBag.RequisitionId = new SelectList(_requisitionManager.GetAll(), "Id", "RequisitionNumber", notificationViewModel.RequisitionId);
                return View(notificationViewModel);
            }
            catch (Exception ex)
            {

                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "Notifications", "Create"));
            }
            
        }

        // GET: Notifications/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Notification notification = _notificationManager.FindById((int)id);
                if (notification == null)
                {
                    return HttpNotFound();
                }
                NotificationViewModel notificationViewModel = Mapper.Map<NotificationViewModel>(notification);
                return View(notificationViewModel);
            }
            catch (Exception ex)
            {

                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "Notifications", "Edit"));
            }
            
        }

        // POST: Notifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Text,EmployeeId,RequisitionId,NotifyDateTime,SenderViewStatus,ControllerViewStatus")] NotificationViewModel notificationViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Notification notification = Mapper.Map<Notification>(notificationViewModel);
                    _notificationManager.Update(notification);
                    return RedirectToAction("Index");
                }

                ViewBag.EmployeeId = new SelectList(_employeeManager.GetAll(), "Id", "FullName", notificationViewModel.EmployeeId);
                ViewBag.RequisitionId = new SelectList(_requisitionManager.GetAll(), "Id", "RequisitionNumber", notificationViewModel.RequisitionId);
                return View(notificationViewModel);
            }
            catch (Exception ex)
            {

                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "Notifications", "Edit"));
            }
            
        }

        // GET: Notifications/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Notification notification = _notificationManager.FindById((int)id);
                if (notification == null)
                {
                    return HttpNotFound();
                }
                NotificationViewModel notificationViewModel = Mapper.Map<NotificationViewModel>(notification);
                return View(notificationViewModel);
            }
            catch (Exception ex)
            {

                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "Notifications", "Delete"));
            }
            
        }

        // POST: Notifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Notification notification = _notificationManager.FindById((int)id);
                _notificationManager.Remove(notification);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "Notifications", "Delete"));
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
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _notificationManager.Dispose();
                _employeeManager.Dispose();
                _requisitionManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

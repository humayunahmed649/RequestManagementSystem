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
            ICollection<Notification> notifications = _notificationManager.GetAll();
            IEnumerable< NotificationViewModel > notificationViewModels = Mapper.Map<IEnumerable<NotificationViewModel>>(notifications);
            return View(notificationViewModels);
        }

        // GET: Notifications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = _notificationManager.FindById((int) id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            NotificationViewModel notificationViewModel = Mapper.Map<NotificationViewModel>(notification);
            return View(notificationViewModel);
        }

        // GET: Notifications/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(_employeeManager.GetAll(), "Id", "FullName");
            ViewBag.RequisitionId = new SelectList(_requisitionManager.GetAll(), "Id", "RequisitionNumber");
            return View();
        }

        // POST: Notifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Text,EmployeeId,RequisitionId,NotifyDateTime,SenderViewStatus,ControllerViewStatus")] NotificationViewModel notificationViewModel)
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

        // GET: Notifications/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Notifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Text,EmployeeId,RequisitionId,NotifyDateTime,SenderViewStatus,ControllerViewStatus")] NotificationViewModel notificationViewModel)
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

        // GET: Notifications/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Notifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Notification notification = _notificationManager.FindById((int) id);
            _notificationManager.Remove(notification);
            return RedirectToAction("Index");
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

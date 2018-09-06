﻿using System;
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
using RMS.Repositories.Contracts;

namespace RMS.App.Controllers
{
    public class AssignRequisitionsController : Controller
    {
        private IRequisitionManager _requisitionManager;
        private IVehicleManager _vehicleManager;
        private IEmployeeManager _employeeManager;
        private IAssignRequisitionManager _assignRequisitionManager;
        private IRequisitionStatusManager _requisitionStatusManager;

        public AssignRequisitionsController(IRequisitionManager requisitionManager,IVehicleManager vehicleManager,IEmployeeManager employeeManager,IAssignRequisitionManager assignRequisitionManager,IRequisitionStatusManager requisitionStatusManager)
        {
            this._requisitionManager = requisitionManager;
            this._employeeManager = employeeManager;
            this._vehicleManager = vehicleManager;
            this._assignRequisitionManager = assignRequisitionManager;
            this._requisitionStatusManager = requisitionStatusManager;
        }

        // GET: AssignRequisitions
        public ActionResult Index(string searchText)
        {

            ICollection<AssignRequisition> requisitions = _assignRequisitionManager.GetAll();
            IEnumerable<AssignRequisitionViewModel> assignRequisitionViewModels =
                Mapper.Map<IEnumerable<AssignRequisitionViewModel>>(requisitions);
            return View(assignRequisitionViewModels);
        }

        // GET: AssignRequisitions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignRequisition assignRequisition = _assignRequisitionManager.FindById((int)id);
            if (assignRequisition == null)
            {
                return HttpNotFound();
            }
            if (assignRequisition != null)
            {
                var requestDetails = _assignRequisitionManager.GetAllWithInformation();
                AssignRequisitionViewModel assignRequisitionViewModel = Mapper.Map<AssignRequisitionViewModel>(assignRequisition);

                return View(assignRequisitionViewModel);
            }
            return View("Error");
        }

        
        //Get
        public ActionResult Create(int requisitionId)
        {
            if (requisitionId == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requisition requisition = _requisitionManager.FindById(requisitionId);
            if (requisition == null)
            {
                return HttpNotFound();
            }
            AssignRequisitionViewModel assignRequisitionViewModel=new AssignRequisitionViewModel();
            assignRequisitionViewModel.RequisitionId = requisitionId;
            ViewBag.RequisitionNumber = requisition.RequisitionNumber;

            ViewBag.EmployeeId = new SelectList(_employeeManager.GetAllDriver(), "Id", "FullName");
            ViewBag.VehicleId = new SelectList(_vehicleManager.GetAll(), "Id", "RegNo");
            ViewBag.RequisitionStatusId = new SelectList(_requisitionStatusManager.GetAllStatusNew(), "Id", "StatusType");
            return View();
        }
        
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RequisitionId,RequisitionStatusId,RequisitionNumber,VehicleId,EmployeeId")] AssignRequisitionViewModel assignRequisitionViewModel)
        {
            if (ModelState.IsValid)
            {

                AssignRequisition assignRequisition = Mapper.Map<AssignRequisition>(assignRequisitionViewModel);
                _assignRequisitionManager.Add(assignRequisition);
                RequisitionStatus status = new RequisitionStatus();
                status.Id = assignRequisition.RequisitionStatusId;
                status.RequisitionId = assignRequisition.RequisitionId;
                status.RequisitionNumber = assignRequisition.RequisitionNumber;
                status.StatusType = "Assigned";
                _requisitionStatusManager.Update(status);
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(_employeeManager.GetAllDriver(), "Id", "FullName");
            ViewBag.RequisitionId = new SelectList(_requisitionManager.GetAll(), "Id", "DestinationPlace");
            ViewBag.VehicleId = new SelectList(_vehicleManager.GetAll(), "Id", "RegNo");
            return View(assignRequisitionViewModel);
        }

        [HttpGet]
        public ActionResult Cancel(int statusId)
        {
            if (statusId == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequisitionStatus requisition = _requisitionStatusManager.FindById(statusId);

            if (requisition == null)
            {
                return HttpNotFound();
            }
            RequisitionStatusViewModel requisitionStatusViewModel = Mapper.Map<RequisitionStatusViewModel>(requisition);
            return View(requisitionStatusViewModel);
        }

        [HttpPost]
        public ActionResult Cancel([Bind(Include = "Id,RequisitionId,RequisitionStatusId,RequisitionNumber")]RequisitionStatusViewModel model)
        {
            if (ModelState.IsValid)
            {
                RequisitionStatus requisitionStatus = Mapper.Map<RequisitionStatus>(model);
                requisitionStatus.Id = requisitionStatus.Id;
                requisitionStatus.RequisitionId = requisitionStatus.RequisitionId;
                requisitionStatus.StatusType = "Cancelled";
                _requisitionStatusManager.Update(requisitionStatus);
                return RedirectToAction("Index");
            }
            return View();

        }


        // GET: AssignRequisitions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignRequisition assignRequisition = _assignRequisitionManager.FindById((int)id);
            if (assignRequisition == null)
            {
                return HttpNotFound();
            }
            AssignRequisitionViewModel assignRequisitionViewModel =
                Mapper.Map<AssignRequisitionViewModel>(assignRequisition);
            ViewBag.EmployeeId = new SelectList(_employeeManager.GetAllDriver(), "Id", "FullName");
            ViewBag.VehicleId = new SelectList(_vehicleManager.GetAll(), "Id", "RegNo");
            ViewBag.RequisitionStatusId = new SelectList(_requisitionStatusManager.GetAll(), "Id", "StatusType");
            return View(assignRequisitionViewModel);
        }

        // POST: AssignRequisitions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RequisitionId,RequisitionStatusId,RequisitionNumber,VehicleId,DriverId,EmployeeId")] AssignRequisitionViewModel assignRequisitionViewModel)
        {
            if (ModelState.IsValid)
            {
                AssignRequisition requisition = Mapper.Map<AssignRequisition>(assignRequisitionViewModel);
                _assignRequisitionManager.Update(requisition);
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(_employeeManager.GetAllDriver(), "Id", "FullName", assignRequisitionViewModel.EmployeeId);
            ViewBag.RequisitionId = new SelectList(_requisitionManager.GetAll(), "Id", "DestinationPlace", assignRequisitionViewModel.RequisitionId);
            ViewBag.VehicleId = new SelectList(_vehicleManager.GetAll(), "Id", "RegNo", assignRequisitionViewModel.VehicleId);
            return View(assignRequisitionViewModel);
        }

        // GET: AssignRequisitions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignRequisition assignRequisition = _assignRequisitionManager.FindById((int)id);
            if (assignRequisition == null)
            {
                return HttpNotFound();
            }
            AssignRequisitionViewModel assignRequisitionViewModel =
                Mapper.Map<AssignRequisitionViewModel>(assignRequisition);
            return View(assignRequisitionViewModel);
        }

        // POST: AssignRequisitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssignRequisition assignRequisition = _assignRequisitionManager.FindById((int)id);
            _assignRequisitionManager.Remove(assignRequisition);
            return RedirectToAction("Index");
        }

       

        public ActionResult Requests(string searchText)
        {
            if (searchText != null)
            {
                IEnumerable<RequisitionStatus> requisition = _requisitionStatusManager.SearchByRequisitionId(searchText);
                IEnumerable<RequisitionStatusViewModel> requisitionStatusViewModels =
                Mapper.Map<IEnumerable<RequisitionStatusViewModel>>(requisition);
                return View(requisitionStatusViewModels);
            }

            ICollection<RequisitionStatus> requisitions = _requisitionStatusManager.GetAllStatusNew();
            IEnumerable<RequisitionStatusViewModel> requisitionViewModels =
                Mapper.Map<IEnumerable<RequisitionStatusViewModel>>(requisitions);
            return View(requisitionViewModels);
        }
        public ActionResult ViewDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requisition requisition = _requisitionManager.FindById((int)id);
            if (requisition == null)
            {
                return HttpNotFound();
            }
            if (requisition != null)
            {
                var requisitions = _requisitionManager.GetAllWithEmployee();
                RequisitionViewModel requisitionViewModel = Mapper.Map<RequisitionViewModel>(requisition);

                return View(requisitionViewModel);
            }
            return View("Error");
        }
        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _assignRequisitionManager.Dispose();
                _employeeManager.Dispose();
                _requisitionManager.Dispose();
                _vehicleManager.Dispose();
            }
            base.Dispose(disposing);
        }

        
    }
}

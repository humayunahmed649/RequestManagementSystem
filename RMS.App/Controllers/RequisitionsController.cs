using System;
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
    public class RequisitionsController : Controller
    {
        private IRequisitionManager _requisitionManager;
        private IEmployeeManager _employeeManager;
        private IVehicleManager _vehicleManager;

        public RequisitionsController(IRequisitionManager requisitionManager,IEmployeeManager employeeManager,IVehicleManager vehicleManager)
        {
            this._requisitionManager = requisitionManager;
            this._employeeManager = employeeManager;
            this._vehicleManager = vehicleManager;
        }

        // GET: Requisitions
        public ActionResult Index()
        {
            ICollection<Requisition> requisitions = _requisitionManager.GetAll();
            IEnumerable<RequisitionViewModel> requisitionViewModels =
                Mapper.Map<IEnumerable<RequisitionViewModel>>(requisitions);
            return View(requisitionViewModels);
        }

        public ActionResult RequisitionList()
        {

            ICollection<Requisition> requisitions = _requisitionManager.GetAll();
            IEnumerable<RequisitionViewModel> requisitionViewModels =
                Mapper.Map<IEnumerable<RequisitionViewModel>>(requisitions);
            return View(requisitionViewModels);
        }

        // GET: Requisitions/Details/5
        public ActionResult Details(int? id)
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
            RequisitionViewModel requisitionViewModel = Mapper.Map<RequisitionViewModel>(requisition);
            return View(requisitionViewModel);
        }

        // GET: Requisitions/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(_employeeManager.GetAll(), "Id", "FullName");
            return View();
        }
        

        // POST: Requisitions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FromPlace,DestinationPlace,StartDateTime,EndDateTime,Description,EmployeeId")] RequisitionViewModel requisitionViewModel)
        {
            if (ModelState.IsValid)
            {
                requisitionViewModel.RequestFor = "Own";
                requisitionViewModel.RequisitionNumber = requisitionViewModel.GetRequisitionNumber();
                Requisition requisition = Mapper.Map<Requisition>(requisitionViewModel);
                _requisitionManager.Add(requisition);
                TempData["msg"] = "Information has been saved successfully";
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(_employeeManager.GetAll(), "Id", "FullName", requisitionViewModel.EmployeeId);
            return View(requisitionViewModel);
        }
        [HttpGet]
        public ActionResult CreateRequestForOther()
        {
            ViewBag.EmployeeId = new SelectList(_employeeManager.GetAll(), "Id", "FullName");
            return View();
        }
        [HttpPost]
        public ActionResult CreateRequestForOther([Bind(Include = "Id,FromPlace,DestinationPlace,StartDateTime,EndDateTime,Description,RequestFor,EmployeeId")] RequisitonForAnotherViewModel requisitonForAnother)
        {
            if (ModelState.IsValid)
            {
                requisitonForAnother.RequisitionNumber = requisitonForAnother.GetRequisitionNumber();
                Requisition requisition = Mapper.Map<Requisition>(requisitonForAnother);
                _requisitionManager.Add(requisition);
                TempData["msg"] = "Requisition has been Send successfully....! Please Wait For Response..........Thanks";
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(_employeeManager.GetAll(), "Id", "FullName", requisitonForAnother.EmployeeId);
            return View(requisitonForAnother);
        }

        // GET: Requisitions/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.EmployeeId = new SelectList(_employeeManager.GetAll(), "Id", "FullName", requisition.EmployeeId);
            RequisitionViewModel requisitionViewModel = Mapper.Map<RequisitionViewModel>(requisition);
            return View(requisitionViewModel);
        }

        // POST: Requisitions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FromPlace,DestinationPlace,StartDateTime,EndDateTime,Description,RequestFor,EmployeeId")] RequisitionViewModel requisitionViewModel)
        {
            if (ModelState.IsValid)
            {
                Requisition requisition = Mapper.Map<Requisition>(requisitionViewModel);
                _requisitionManager.Update(requisition);
                TempData["msg"] = "Information has been updated successfully";
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(_employeeManager.GetAll(), "Id", "FullName", requisitionViewModel.EmployeeId);
            return View(requisitionViewModel);
        }

        // GET: Requisitions/Delete/5
        public ActionResult Delete(int? id)
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
            RequisitionViewModel requisitionViewModel = Mapper.Map<RequisitionViewModel>(requisition);
            return View(requisitionViewModel);
        }

        // POST: Requisitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Requisition requisition = _requisitionManager.FindById((int)id);
            _requisitionManager.Remove(requisition);
            TempData["msg"] = "Information has been deleted successfully";
            return RedirectToAction("Index");
        }
        
        


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _requisitionManager.Dispose();
                _employeeManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

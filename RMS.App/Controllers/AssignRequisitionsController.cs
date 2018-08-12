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
using RMS.Repositories.Contracts;

namespace RMS.App.Controllers
{
    public class AssignRequisitionsController : Controller
    {
        private IRequisitionManager _requisitionManager;
        private IVehicleManager _vehicleManager;
        private IEmployeeManager _employeeManager;
        private IAssignRequisitionManager _assignRequisitionManager;

        public AssignRequisitionsController(IRequisitionManager requisitionManager,IVehicleManager vehicleManager,IEmployeeManager employeeManager,IAssignRequisitionManager assignRequisitionManager)
        {
            this._requisitionManager = requisitionManager;
            this._employeeManager = employeeManager;
            this._vehicleManager = vehicleManager;
            this._assignRequisitionManager = assignRequisitionManager;
        }

        // GET: AssignRequisitions
        public ActionResult Index()
        {

            ICollection<AssignRequisition> requisitions = _assignRequisitionManager.GetAll();
            IEnumerable<AssignRequisitionViewModel> assignRequisitionViewModels =
                Mapper.Map<IEnumerable<AssignRequisitionViewModel>>(requisitions);
            return View((IEnumerable<AssignRequisitionViewModel>) assignRequisitionViewModels);
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
            AssignRequisitionViewModel assignRequisitionViewModel =
                Mapper.Map<AssignRequisitionViewModel>(assignRequisition);
            return View(assignRequisitionViewModel);
        }

        // GET: AssignRequisitions/Create
        public ActionResult Create(int id)
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
            ViewBag.RequisitionId = requisition.Id;
            ViewBag.EmployeeId = new SelectList(_employeeManager.GetAllDriver(), "Id", "FullName");
            ViewBag.VehicleId = new SelectList(_vehicleManager.GetAll(), "Id", "RegNo");
            return View();
        }

        // POST: AssignRequisitions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RequisitionId,VehicleId,DriverId,EmployeeId")] AssignRequisitionViewModel assignRequisitionViewModel)
        {
            if (ModelState.IsValid)
            {
                AssignRequisition assignRequisition = Mapper.Map<AssignRequisition>(assignRequisitionViewModel);
                _assignRequisitionManager.Add(assignRequisition);
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(_employeeManager.GetAllDriver(), "Id", "FullName");
            ViewBag.RequisitionId = new SelectList(_requisitionManager.GetAll(), "Id", "DestinationPlace");
            ViewBag.VehicleId = new SelectList(_vehicleManager.GetAll(), "Id", "RegNo");
            return View(assignRequisitionViewModel);
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
            ViewBag.EmployeeId = new SelectList(_employeeManager.GetAllDriver(), "Id", "FullName",assignRequisition.EmployeeId);
            ViewBag.RequisitionId = new SelectList(_requisitionManager.GetAll(), "Id", "DestinationPlace",assignRequisition.RequisitionId);
            ViewBag.VehicleId = new SelectList(_vehicleManager.GetAll(), "Id", "RegNo",assignRequisition.VehicleId);
            AssignRequisitionViewModel assignRequisitionViewModel =
                Mapper.Map<AssignRequisitionViewModel>(assignRequisition);
            return View(assignRequisitionViewModel);
        }

        // POST: AssignRequisitions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RequisitionId,VehicleId,DriverId,EmployeeId")] AssignRequisitionViewModel assignRequisitionViewModel)
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

        public ActionResult Requests()
        {
            ICollection<Requisition> requisitions = _requisitionManager.GetAll();
            IEnumerable<RequisitionViewModel> requisitionViewModels =
                Mapper.Map<IEnumerable<RequisitionViewModel>>(requisitions);
            return View(requisitionViewModels);
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

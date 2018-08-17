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
        public ActionResult Index(string searchByText)
        {
            if (searchByText != null)
            {
                ////IEnumerable<AssignRequisition> requisition = _assignRequisitionManager.SearchByName(searchByText);
                //IEnumerable<AssignRequisitionViewModel> assignRequisitionViewModel =
                //Mapper.Map<IEnumerable<AssignRequisitionViewModel>>(requisition);
                //return View(assignRequisitionViewModel);
            }

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
            if (assignRequisition != null)
            {
                var requestDetails = _assignRequisitionManager.GetAllWithInformation();
                AssignRequisitionViewModel assignRequisitionViewModel = Mapper.Map<AssignRequisitionViewModel>(assignRequisition);

                return View(assignRequisitionViewModel);
            }
            return View("Error");
        }

        // GET: AssignRequisitions/Create
        public ActionResult Create(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requisition requisition = _requisitionManager.FindById(id);
            
            if (requisition == null)
            {
                return HttpNotFound();
            }
            RequisitionViewModel requisitionViewModel = Mapper.Map<RequisitionViewModel>(requisition);
            ViewBag.RequisitionId = requisition.Id;
            ViewBag.RequisitionStatusId = new SelectList(_requisitionStatusManager.GetAll(), "Id", "StatusType");
            ViewBag.RequisitionNumber = requisition.RequisitionNumber;
            ViewBag.EmployeeId = new SelectList(_employeeManager.GetAllDriver(), "Id", "FullName");
            ViewBag.VehicleId = new SelectList(_vehicleManager.GetAll(), "Id", "RegNo");
            return View();
        }

        // POST: AssignRequisitions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RequisitionStatusId,RequisitionId,RequisitionNumber,VehicleId,DriverId,EmployeeId")] AssignRequisitionViewModel assignRequisitionViewModel)
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
                var requestDetails = _requisitionManager.GetAllWithEmployee();
                RequisitionViewModel requisitionViewModel = Mapper.Map<RequisitionViewModel>(requisition);

                return View(requisitionViewModel);
            }
            return View("Error");
        }

        public ActionResult EditRequisition(int? id)
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
        public ActionResult EditRequisition([Bind(Include = "Id,FromPlace,DestinationPlace,StartDateTime,EndDateTime,Description,RequestFor,EmployeeId")] RequisitionViewModel requisitionViewModel)
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

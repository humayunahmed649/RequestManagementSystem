using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
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
        private IRequisitionStatusManager _requisitionStatusManager;
        private IFeedbackManager _feedbackManager;

        public RequisitionsController(IRequisitionManager requisitionManager,IEmployeeManager employeeManager,IVehicleManager vehicleManager,IRequisitionStatusManager requisitionStatusManager,IFeedbackManager feedbackManager)
        {
            this._requisitionManager = requisitionManager;
            this._employeeManager = employeeManager;
            this._vehicleManager = vehicleManager;
            this._requisitionStatusManager = requisitionStatusManager;
            this._feedbackManager = feedbackManager;
        }

        // GET: Requisitions
        public ActionResult Index()
        {
            try
            {

                ICollection<Requisition> requisitions = _requisitionManager.GetAllWithEmployee();
                IEnumerable<RequisitionViewModel> requisitionViewModels =
                    Mapper.Map<IEnumerable<RequisitionViewModel>>(requisitions);
                return View(requisitionViewModels);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Requisitions", "Index"));

            }
        }

        public ActionResult RequisitionList()
        {
            try
            {

                ICollection<Requisition> requisitions = _requisitionManager.GetAll();
                IEnumerable<RequisitionViewModel> requisitionViewModels =
                    Mapper.Map<IEnumerable<RequisitionViewModel>>(requisitions);
                return View(requisitionViewModels);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Requisitions", "RequisitionList"));
            }
        }

        // GET: Requisitions/Details/5
        public ActionResult Details(int? id)
        {
            try
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
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Requisitions", "Details"));
            }
            
        }

        // GET: Requisitions/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.EmployeeId = new SelectList(_employeeManager.GetAllEmployees(), "Id", "FullName");
                ICollection<Requisition> requisitions = _requisitionManager.GetAll();
                IEnumerable<RequisitionViewModel> requisitionViewModels =
                    Mapper.Map<IEnumerable<RequisitionViewModel>>(requisitions);
                ViewBag.Requisition = requisitionViewModels;
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Requisitions", "Create"));
            }
            
        }
        

        // POST: Requisitions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FromPlace,DestinationPlace,StartDateTime,StartTime,EndDateTime,EndTime,Description,EmployeeId")] RequisitionViewModel requisitionViewModel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var startDate = requisitionViewModel.StartDateTime.ToString("MM/dd/yyyy");
                    var startTime = requisitionViewModel.StartTime;
                    DateTime startDateTime = Convert.ToDateTime(startDate + " " + startTime);
                    requisitionViewModel.StartDateTime = startDateTime;

                    var endDate = requisitionViewModel.EndDateTime.ToString("MM/dd/yyyy");
                    var endTime = requisitionViewModel.EndTime;
                    DateTime endDateTime = Convert.ToDateTime(endDate + " " + endTime);
                    requisitionViewModel.EndDateTime = endDateTime;

                    requisitionViewModel.RequestFor = "Own";
                    requisitionViewModel.RequisitionNumber = requisitionViewModel.GetRequisitionNumber();

                    Requisition requisition = Mapper.Map<Requisition>(requisitionViewModel);
                    bool isSaved = _requisitionManager.Add(requisition);

                    //Requisition Status Save
                    if (isSaved == true)
                    {
                        RequisitionStatus status = new RequisitionStatus();
                        status.RequisitionNumber = requisition.RequisitionNumber;

                        status.RequisitionId = requisition.Id;
                        status.StatusType = "New";
                        _requisitionStatusManager.Add(status);
                        TempData["msg"] = "Requisition has been Send successfully....! Please Wait For Response..........Thanks";
                        return RedirectToAction("Index");
                    }
                }

                ViewBag.EmployeeId = new SelectList(_employeeManager.GetAll(), "Id", "FullName", requisitionViewModel.EmployeeId);
                return View(requisitionViewModel);
            }
            catch (Exception ex)
            {
                return View("", new HandleErrorInfo(ex, "Requisitions", "Create"));
            }
        }

        // GET: Requisitions/CreateRequestForOther
        [HttpGet]
        public ActionResult CreateRequestForOther()
        {
            try
            {

                ViewBag.EmployeeId = new SelectList(_employeeManager.GetAllEmployees(), "Id", "FullName");
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Requisitions", "CreateRequestForOther"));
            }
        }

        // POST: Requisitions/CreateRequestForOther
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRequestForOther([Bind(Include = "Id,FromPlace,DestinationPlace,StartDateTime,StartTime,EndDateTime,EndTime,Description,RequestFor,EmployeeId")] RequisitonForAnotherViewModel requisitonForAnother)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var startDate = requisitonForAnother.StartDateTime.ToString("MM/dd/yyyy");
                    var startTime = requisitonForAnother.StartTime;
                    DateTime startDateTime = Convert.ToDateTime(startDate + " " + startTime);
                    requisitonForAnother.StartDateTime = startDateTime;

                    var endDate = requisitonForAnother.EndDateTime.ToString("MM/dd/yyyy");
                    var endTime = requisitonForAnother.EndTime;
                    DateTime endDateTime = Convert.ToDateTime(endDate + " " + endTime);
                    requisitonForAnother.EndDateTime = endDateTime;

                    requisitonForAnother.RequisitionNumber = requisitonForAnother.GetRequisitionNumber();
                    Requisition requisition = Mapper.Map<Requisition>(requisitonForAnother);

                    bool isSaved = _requisitionManager.Add(requisition);
                    //Requisition Status Save
                    if (isSaved == true)
                    {
                        RequisitionStatus status = new RequisitionStatus();
                        status.RequisitionNumber = requisition.RequisitionNumber;

                        status.RequisitionId = requisition.Id;
                        status.StatusType = "New";
                        _requisitionStatusManager.Add(status);
                        TempData["msg"] = "Requisition has been Send successfully....! Please Wait For Response..........Thanks";
                        return RedirectToAction("Index");
                    }
                }

                ViewBag.EmployeeId = new SelectList(_employeeManager.GetAllEmployees(), "Id", "FullName", requisitonForAnother.EmployeeId);
                return View(requisitonForAnother);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Requisitions", "CreateRequestForOther"));
            }
        }

        // GET: Requisitions/Edit/5
        public ActionResult Edit(int? id)
        {
            try
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
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Requisitions", "Edit"));
            }        }

        // POST: Requisitions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FromPlace,DestinationPlace,StartDateTime,StartTime,EndDateTime,EndTime,Description,RequestFor,EmployeeId,RequisitionNumber")] RequisitionViewModel requisitionViewModel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var startDate = requisitionViewModel.StartDateTime.ToString("MM/dd/yyyy");
                    var startTime = requisitionViewModel.StartTime;
                    DateTime startDateTime = Convert.ToDateTime(startDate + " " + startTime);
                    requisitionViewModel.StartDateTime = startDateTime;

                    var endDate = requisitionViewModel.EndDateTime.ToString("MM/dd/yyyy");
                    var endTime = requisitionViewModel.EndTime;
                    DateTime endDateTime = Convert.ToDateTime(endDate + " " + endTime);
                    requisitionViewModel.EndDateTime = endDateTime;

                    Requisition requisition = Mapper.Map<Requisition>(requisitionViewModel);

                    _requisitionManager.Update(requisition);
                    TempData["msg"] = "Information has been updated successfully";
                    return RedirectToAction("Index");
                }
                ViewBag.EmployeeId = new SelectList(_employeeManager.GetAll(), "Id", "FullName", requisitionViewModel.EmployeeId);
                return View(requisitionViewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Requisitions", "Edit"));
            }
        }

        // GET: Requisitions/Delete/5
        public ActionResult Delete(int? id)
        {
            try
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
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Requisitions", "Delete"));
            }
            
        }

        // POST: Requisitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {

                Requisition requisition = _requisitionManager.FindById((int)id);
                _requisitionManager.Remove(requisition);
                TempData["msg"] = "Information has been deleted successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Requisitions", "Delete"));
            }
        }
        [HttpGet]
        public ActionResult Feedback(int requisitionId)
        {
            try
            {
                Requisition requisition = _requisitionManager.FindById((int)requisitionId);
                if (requisition == null)
                {
                    return HttpNotFound();
                }
                RequisitionViewModel requisitionViewModel = Mapper.Map<RequisitionViewModel>(requisition);
                FeedbackViewModel feedbackViewModel = new FeedbackViewModel();
                feedbackViewModel.Requisition = requisition;
                ViewBag.Feedback = _feedbackManager.GetAll().Where(c => c.RequisitionId == requisitionId);
                return View(feedbackViewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Requisitions", "Feedback"));
            }
            
            
        }

        [HttpPost]
        public ActionResult Feedback([Bind(Include = "Id,RequisitionId,CommentText")]FeedbackViewModel feedbackViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Feedback feedback = Mapper.Map<Feedback>(feedbackViewModel);
                    _feedbackManager.Add(feedback);
                    ViewBag.Feedback = _feedbackManager.GetAll();
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Requisitions", "Feedback"));

            }
            return View();
        }

        [HttpGet]
        public ActionResult Reply(int feedbackId)
        {
            try
            {
                Feedback feedback = _feedbackManager.FindById((int)feedbackId);
                if (feedback == null)
                {
                    return HttpNotFound();
                }
                FeedbackViewModel feedbackViewModel = Mapper.Map<FeedbackViewModel>(feedback);

                return View(feedbackViewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Requisitions", "Reply"));
            }

        }

        [HttpPost]
        public ActionResult Reply([Bind(Include = "Id,RequisitionId,CommentText,FeedbackId")]FeedbackViewModel feedbackViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Feedback feedback = Mapper.Map<Feedback>(feedbackViewModel);

                    _feedbackManager.Add(feedback);
                    ViewBag.Feedback = _feedbackManager.GetAll();
                    return RedirectToAction("Feedback","Requisitions");
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Requisitions", "Feedback"));

            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _requisitionManager.Dispose();
                _employeeManager.Dispose();
                _requisitionStatusManager.Dispose();
                _vehicleManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

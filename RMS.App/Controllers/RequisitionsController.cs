using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using AutoMapper;
using Microsoft.AspNet.Identity;
using RMS.App.ViewModels;
using RMS.BLL.Contracts;
using RMS.Models.DatabaseContext;
using RMS.Models.EntityModels;

namespace RMS.App.Controllers
{
    [Authorize(Roles = "User,Administrator,Controller")]
    public class RequisitionsController : Controller
    {
        private IRequisitionManager _requisitionManager;
        private IEmployeeManager _employeeManager;
        private IVehicleManager _vehicleManager;
        private IRequisitionStatusManager _requisitionStatusManager;
        private IFeedbackManager _feedbackManager;
        private IAssignRequisitionManager _assignRequisitionManager;
        private INotificationManager _notificationManager;

        public RequisitionsController(IRequisitionManager requisitionManager,IEmployeeManager employeeManager,IVehicleManager vehicleManager,IRequisitionStatusManager requisitionStatusManager,IFeedbackManager feedbackManager,
            IAssignRequisitionManager assignRequisitionManager, INotificationManager notificationManager)
        {
            this._requisitionManager = requisitionManager;
            this._employeeManager = employeeManager;
            this._vehicleManager = vehicleManager;
            this._requisitionStatusManager = requisitionStatusManager;
            this._feedbackManager = feedbackManager;
            this._assignRequisitionManager = assignRequisitionManager;
            this._notificationManager = notificationManager;
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

        //GET: Requisition List
        public ActionResult RequisitionList()
        {
            try
            {
                //Get employee Id by user login id
                var loginUserId = Convert.ToInt32(User.Identity.GetUserId());
                var empId = _employeeManager.FindByLoginId(loginUserId);
                if (empId != null)
                {
                    ICollection<RequisitionStatus> requisitions = _requisitionStatusManager.GetAllById(empId.Id);
                    IEnumerable<RequisitionStatusViewModel> requisitionStatusViewModels =
                        Mapper.Map<IEnumerable<RequisitionStatusViewModel>>(requisitions);
                    return View(requisitionStatusViewModels);
                }
                IEnumerable<RequisitionStatusViewModel> requisitionViewModel =new List<RequisitionStatusViewModel>();
                TempData["msg"] = "You have not sent or assigned requisition!";
                return View(requisitionViewModel);

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

                //ViewBag.Data = _assignRequisitionManager.GetAll().FirstOrDefault(c => c.RequisitionId == id);

                RequisitionStatus requisition = _requisitionStatusManager.FindByRequisitionId((int)id);
                if (requisition.StatusType != "New")
                {
                    ViewBag.Data = _assignRequisitionManager.GetAll().FirstOrDefault(c => c.RequisitionId == id);
                }
                        if (requisition == null)
                        {
                            return HttpNotFound();
                        }
                      RequisitionStatusViewModel requisitionStatusViewModel = Mapper.Map<RequisitionStatusViewModel>(requisition);
                    return View(requisitionStatusViewModel);
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

                //ViewBag.EmployeeId = new SelectList(_employeeManager.GetAllEmployees(), "Id", "FullName");
                ICollection<Requisition> requisitions = _requisitionManager.GetAll();
                IEnumerable<RequisitionViewModel> requisitionViewModels =
                    Mapper.Map<IEnumerable<RequisitionViewModel>>(requisitions);

                //Get employee Id by user login id
                var loginUserId = Convert.ToInt32(User.Identity.GetUserId());
                var empId = _employeeManager.FindByLoginId(loginUserId);

                //Notifications for assignd vehicle from controller
                var notification = _notificationManager.GetNotificationsForSender("Unseen",empId.Id);
                var notificationCount = notification.Count;
                if (notification != null)
                {
                    ViewBag.Notification = notification;
                    ViewBag.count = notificationCount;
                }
                ViewBag.Requisition = _requisitionManager.GetAllRequisitionByEmployeeId(empId.Id);

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
        public ActionResult Create([Bind(Include = "Id,FromPlace,DestinationPlace,StartDateTime,StartTime,EndDateTime,EndTime,PassengerQty,Description,RequisitionType")] RequisitionViewModel requisitionViewModel)
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

                    //Get employee Id by user login id
                    var loginUserId= Convert.ToInt32(User.Identity.GetUserId());
                    var empId=_employeeManager.FindByLoginId(loginUserId);
                    requisitionViewModel.EmployeeId = empId.Id;

                    Requisition requisition = Mapper.Map<Requisition>(requisitionViewModel);
                    requisition.SubmitDateTime=DateTime.Now;

                    bool isSaved = _requisitionManager.Add(requisition);

                    //Requisition Status Save
                    if (isSaved == true)
                    {
                        RequisitionStatus status = new RequisitionStatus();
                        status.RequisitionNumber = requisition.RequisitionNumber;

                        status.RequisitionId = requisition.Id;
                        status.StatusType = "New";
                        _requisitionStatusManager.Add(status);

                        // Notification Status Save
                        Notification notification = new Notification();
                        notification.Text = "Request for a vehicle";
                        notification.EmployeeId = empId.Id;
                        notification.ControllerViewStatus = "Unseen";
                        notification.RequisitionId = requisition.Id;
                        notification.NotifyDateTime=DateTime.Now;
                        _notificationManager.Add(notification);
                        TempData["msg"] = "Requisition has been Send successfully....! Please Wait For Response..........Thanks";
                        return RedirectToAction("Create");
                    }

                }

                ViewBag.EmployeeId = new SelectList(_employeeManager.GetAll(), "Id", "FullName", requisitionViewModel.EmployeeId);
                ICollection<Requisition> requisitions = _requisitionManager.GetAll();
                IEnumerable<RequisitionViewModel> requisitionViewModels =
                    Mapper.Map<IEnumerable<RequisitionViewModel>>(requisitions);
                ViewBag.Requisition = requisitionViewModels;

                //Get employee Id by user login id
                var loginUserId1 = Convert.ToInt32(User.Identity.GetUserId());
                var empId1 = _employeeManager.FindByLoginId(loginUserId1);

                //Notifications for assignd vehicle from controller
                var notification1 = _notificationManager.GetNotificationsForSender("Unseen", empId1.Id);
                var notificationCount = notification1.Count;

                if (notification1 != null)
                {
                    ViewBag.Notification = notification1;
                    ViewBag.count = notificationCount;
                }

                ViewBag.Requisition = _requisitionManager.GetAllRequisitionByEmployeeId(empId1.Id);
                TempData["msg"] = "Requisition send failed! You are missing to input proper value. Please check and try again!";

                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Requisitions", "Create"));
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
        public ActionResult CreateRequestForOther([Bind(Include = "Id,FromPlace,DestinationPlace,StartDateTime,StartTime,EndDateTime,EndTime,PassengerQty,Description,RequestFor")] RequisitonForAnotherViewModel requisitonForAnother)
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

                    //Get employee Id by user login id
                    var loginUserId = Convert.ToInt32(User.Identity.GetUserId());
                    var empId = _employeeManager.FindByLoginId(loginUserId);
                    requisitonForAnother.EmployeeId = empId.Id;

                    Requisition requisition = Mapper.Map<Requisition>(requisitonForAnother);
                    requisition.SubmitDateTime=DateTime.Now;

                    bool isSaved = _requisitionManager.Add(requisition);
                    //Requisition Status Save
                    if (isSaved)
                    {
                        RequisitionStatus status = new RequisitionStatus();
                        status.RequisitionNumber = requisition.RequisitionNumber;

                        status.RequisitionId = requisition.Id;
                        status.StatusType = "New";
                        _requisitionStatusManager.Add(status);

                        // Notification Status Save
                        Notification notification = new Notification();
                        notification.Text = "Request for a vehicle";
                        notification.EmployeeId = empId.Id;
                        notification.ControllerViewStatus = "Unseen";
                        notification.RequisitionId = requisition.Id;
                        notification.NotifyDateTime = DateTime.Now;
                        _notificationManager.Add(notification);

                        TempData["msg"] = "Requisition has been Send successfully....! Please Wait For Response..........Thanks";
                        return RedirectToAction("Create");
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
        public ActionResult Edit([Bind(Include = "Id,FromPlace,DestinationPlace,StartDateTime,StartTime,EndDateTime,EndTime,PassengerQty,Description,RequestFor,EmployeeId,RequisitionNumber,SubmitDateTime")] RequisitionViewModel requisitionViewModel)
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

                    DateTime submitedDateTime=requisitionViewModel.SubmitDateTime;

                    //Get employee Id by user login id
                    var loginUserId = Convert.ToInt32(User.Identity.GetUserId());
                    var empId = _employeeManager.FindByLoginId(loginUserId);
                    requisitionViewModel.EmployeeId = empId.Id;

                    Requisition requisition = Mapper.Map<Requisition>(requisitionViewModel);
                    requisition.SubmitDateTime = submitedDateTime;

                    _requisitionManager.Update(requisition);
                    TempData["msg"] = "Information has been updated successfully";
                    return RedirectToAction("Create");
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
        public ActionResult Feedback(int? requisitionId)
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
                    ViewBag.Msg = "Comment Save successfully";


                    Requisition requisition = _requisitionManager.FindById(feedbackViewModel.RequisitionId);
                    RequisitionViewModel requisitionViewModel = Mapper.Map<RequisitionViewModel>(requisition);
                    feedbackViewModel.Requisition = requisition;
                    ViewBag.Feedback = _feedbackManager.GetAll().Where(c => c.RequisitionId == feedbackViewModel.RequisitionId);

                }
                return View(feedbackViewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Requisitions", "Feedback"));

            }
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
                    ViewBag.Msg = "Reply Has been saved successfully";
                }
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Requisitions", "Feedback"));

            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _requisitionManager.Dispose();
                _employeeManager.Dispose();
                _requisitionStatusManager.Dispose();
                _vehicleManager.Dispose();
                _notificationManager.Dispose();
                _assignRequisitionManager.Dispose();
                _feedbackManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

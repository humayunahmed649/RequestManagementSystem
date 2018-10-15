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
        private ICancelRequisitionManager _cancelRequisitionManager;
        private IRequisitionHistoryManager _requisitionHistoryManager;

        public RequisitionsController(IRequisitionManager requisitionManager,IEmployeeManager employeeManager,IVehicleManager vehicleManager,IRequisitionStatusManager requisitionStatusManager,IFeedbackManager feedbackManager,
            IAssignRequisitionManager assignRequisitionManager, INotificationManager notificationManager,ICancelRequisitionManager cancelRequisitionManager,IRequisitionHistoryManager requisitionHistoryManager)
        {
            this._requisitionManager = requisitionManager;
            this._employeeManager = employeeManager;
            this._vehicleManager = vehicleManager;
            this._requisitionStatusManager = requisitionStatusManager;
            this._feedbackManager = feedbackManager;
            this._assignRequisitionManager = assignRequisitionManager;
            this._notificationManager = notificationManager;
            this._cancelRequisitionManager = cancelRequisitionManager;
            this._requisitionHistoryManager = requisitionHistoryManager;
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
                ExceptionMessage(ex);
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
                ExceptionMessage(ex);
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

                if (requisition == null)
                {
                    return HttpNotFound();
                }

                if (requisition.StatusType != "New")
                {
                    ViewBag.Data = _assignRequisitionManager.GetAll().FirstOrDefault(c => c.RequisitionId == id);

                    if (requisition.StatusType == "Cancelled")
                    {
                        ViewBag.CancelInfo = _cancelRequisitionManager.GetAll().FirstOrDefault(c=>c.RequisitionId==id);
                    }
                }

                //Employee notification status updated.
                Notification notificationUpdate = _notificationManager.FindByRequisitionId(requisition.RequisitionId);
                if (notificationUpdate!=null) 
                {
                    notificationUpdate.SenderViewStatus = "Seen";
                    _notificationManager.Update(notificationUpdate);
                }
                

                RequisitionStatusViewModel requisitionStatusViewModel = Mapper.Map<RequisitionStatusViewModel>(requisition);

                return View(requisitionStatusViewModel);
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
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

                //Get employee Id by user login id
                var loginUserId = Convert.ToInt32(User.Identity.GetUserId());
                var empId = _employeeManager.FindByLoginId(loginUserId);

                //Notifications for assignd vehicle from controller
                //var notification = _notificationManager.GetNotificationsForSender("Unseen",empId.Id);
                //var notificationCount = notification.Count;
                //if (notification != null)
                //{
                //    ViewBag.Notification = notification;
                //    ViewBag.count = notificationCount;
                //}
                ViewBag.Requisition = _requisitionStatusManager.GetAllById(empId.Id);

                return View();
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
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
                        bool Saved=_requisitionStatusManager.Add(status);
                        if (Saved)
                        {
                            RequisitionHistory history=new RequisitionHistory();
                            history.Status = "New";
                            history.RequisitionId = requisition.Id;
                            history.SubmitDateTime=DateTime.Now;
                            _requisitionHistoryManager.Add(history);
                        }


                        // Notification Status Save
                        Notification notification = new Notification();

                        notification.RequisitionId = requisition.Id;
                        notification.EmployeeId = empId.Id;
                        notification.ControllerText = "Request for a vehicle";
                        notification.ControllerViewStatus = "Unseen";
                        notification.ControllerNotifyDateTime=DateTime.Now;
                        notification.SenderNotifyDateTime=DateTime.Now;
                        _notificationManager.Add(notification);

                        TempData["msg"] = "Requisition has been Send successfully! Please wait for Response! Thanks";
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

                //Notifications for previous assigned vehicle By controller
                var notification1 = _notificationManager.GetNotificationsForSender("Unseen", empId1.Id);
                var notificationCount = notification1.Count;

                if (notification1 != null)
                {
                    ViewBag.Notification = notification1;
                    ViewBag.count = notificationCount;
                }
                ViewBag.Requisition =_requisitionStatusManager.GetAllById(empId1.Id);
                //ViewBag.Requisition = _requisitionManager.GetAllRequisitionByEmployeeId(empId1.Id);

                TempData["msg1"] = " Requisition send failed! You are missing to input proper value. Please check and try again! ";

                return View();
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "Requisitions", "Create"));
            }
        }

        // GET: Requisitions/CreateRequestForOther
        [HttpGet]
        public ActionResult CreateRequestForOther()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "Requisitions", "CreateRequestForOther"));
            }
        }

        // POST: Requisitions/CreateRequestForOther
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRequestForOther([Bind(Include = "Id,FromPlace,DestinationPlace,StartDateTime,StartTime,EndDateTime,EndTime,PassengerQty,Description,RequisitionType,EmployeeId")] RequisitonForAnotherViewModel requisitonForAnother)
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

                    if (requisitonForAnother.EmployeeId>0)
                    {
                        var emp = _employeeManager.FindById(requisitonForAnother.EmployeeId);
                        requisitonForAnother.RequestFor = emp.FullName;
                    }
                   
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
                        bool Saved=_requisitionStatusManager.Add(status);
                        if (Saved)
                        {
                            RequisitionHistory history = new RequisitionHistory();
                            history.Status = "New";
                            history.RequisitionId = requisition.Id;
                            history.SubmitDateTime = DateTime.Now;
                            _requisitionHistoryManager.Add(history);
                        }
                        // Notification Status Save
                        Notification notification = new Notification();
                        notification.RequisitionId = requisition.Id;
                        notification.EmployeeId = empId.Id;
                        notification.ControllerText = "Request for a vehicle";
                        notification.ControllerViewStatus = "Unseen";
                        notification.ControllerNotifyDateTime = DateTime.Now;
                        notification.SenderNotifyDateTime=DateTime.Now;
                        _notificationManager.Add(notification);

                        TempData["msg"] = " Requisition has been Send successfully! Please wait for Response! Thanks ";
                        return RedirectToAction("Create");
                    }
                }

                ViewBag.EmployeeId = new SelectList(_employeeManager.GetAllEmployees(), "Id", "FullName", requisitonForAnother.EmployeeId);

                TempData["msg1"] = " Requisition send failed! You are missing to input proper value. Please check and try again! ";

                return RedirectToAction("Create");
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "Requisitions", "CreateRequestForOther"));
            }
        }

        // GET: Requisitions/Edit/5
        public ActionResult Edit(int? id)
         {
            try
            {
                RequisitionViewModel requisitionView=new RequisitionViewModel();
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var status = _requisitionStatusManager.FindByRequisitionId((int) id);
                if (status.StatusType == "New")
                {
                    Requisition requisition = _requisitionManager.FindById((int)id);

                    if (requisition == null)
                    {
                        return HttpNotFound();
                    }
                    ViewBag.EmployeeId = new SelectList(_employeeManager.GetAll(), "Id", "FullName", requisition.EmployeeId);
                    RequisitionViewModel requisitionViewModel = Mapper.Map<RequisitionViewModel>(requisition);
                    return View(requisitionViewModel);
                }

                TempData["EditMsg"] = " Your requisition already assigned or completed. You Cant't Edid or Update. ";
                return RedirectToAction("Create");

            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "Requisitions", "Edit"));
            }
   }

        // POST: Requisitions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "Id,FromPlace,DestinationPlace,StartDateTime,StartTime,EndDateTime,EndTime,PassengerQty,Description,RequestFor,EmployeeId,RequisitionNumber,RequisitionType,SubmitDateTime")] RequisitionViewModel requisitionViewModel)

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

                    //DateTime submitedDateTime=Convert.ToDateTime(requisitionViewModel.StringSubmitDateTime);
                    if (requisitionViewModel.RequestFor!="Own" && requisitionViewModel.EmployeeId!=null && requisitionViewModel.EmployeeId>0)
                    {
                        var emp = _employeeManager.FindById((int)requisitionViewModel.EmployeeId);
                        requisitionViewModel.RequestFor = emp.FullName;
                    }
                    //Get employee Id by user login id
                    var loginUserId = Convert.ToInt32(User.Identity.GetUserId());
                    var empId = _employeeManager.FindByLoginId(loginUserId);
                    requisitionViewModel.EmployeeId = empId.Id;

                    Requisition requisition = Mapper.Map<Requisition>(requisitionViewModel);

                    bool IsUpdated=_requisitionManager.Update(requisition);
                    if (IsUpdated)
                    {

                        var historyId = _requisitionHistoryManager.FindByRequisitionId(requisition.Id);
                        
                        historyId.Id = historyId.Id;
                        historyId.Status = "New";
                        historyId.RequisitionId = requisition.Id;
                        historyId.UpdateDateTime = DateTime.Now;
                        _requisitionHistoryManager.Update(historyId);
                    }
                    TempData["msg"] = " Information has been updated successfully! ";

                    return RedirectToAction("Create");
                }

                ViewBag.EmployeeId = new SelectList(_employeeManager.GetAll(), "Id", "FullName", requisitionViewModel.EmployeeId);

                TempData["msg1"] = "Requisition send failed! You are missing to input proper value. Please check and try again!";

                return View(requisitionViewModel);
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
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
                ExceptionMessage(ex);
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

                Requisition requisition = _requisitionManager.FindById(id);
                bool IsDeleted=_requisitionManager.Remove(requisition);
                if (IsDeleted)
                {
                    RequisitionHistory history = new RequisitionHistory();
                    history.Status = "New";
                    history.RequisitionId = requisition.Id;
                    history.DeletedDateTime = DateTime.Now;
                    _requisitionHistoryManager.Update(history);
                }
                TempData["msg"] = "Information has been deleted successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "Requisitions", "Delete"));
            }
        }

        //Get: Requisitin Feedback
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
                //RequisitionViewModel requisitionViewModel = Mapper.Map<RequisitionViewModel>(requisition);
                FeedbackViewModel feedbackViewModel = new FeedbackViewModel();
                feedbackViewModel.Requisition = requisition;
                ViewBag.Feedback = _feedbackManager.GetAll().Where(c => c.RequisitionId == requisitionId);
                return View(feedbackViewModel);
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "Requisitions", "Feedback"));
            }
        }

        //Post: Requisitin Feedback
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
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "Requisitions", "Feedback"));

            }
        }

        [Authorize(Roles = "Controller,Admin")]
        //Get: Requisitin Reply
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

                //Get employee Id by user login id
                var loginUserId = Convert.ToInt32(User.Identity.GetUserId());
                var empId = _employeeManager.FindByLoginId(loginUserId);

                FeedbackViewModel feedbackViewModel = Mapper.Map<FeedbackViewModel>(feedback);

                feedbackViewModel.EmployeeId = Convert.ToInt32(empId.Id);

                return View(feedbackViewModel);
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "Requisitions", "Reply"));
            }

        }


        //Post: Requisition Reply
        [HttpPost]
        public ActionResult Reply([Bind(Include = "Id,RequisitionId,CommentText,FeedbackId,EmployeeId")]FeedbackViewModel feedbackViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Get employee Id by user login id
                    var loginUserId = Convert.ToInt32(User.Identity.GetUserId());
                    var empId = _employeeManager.FindByLoginId(loginUserId);
                    feedbackViewModel.EmployeeId = Convert.ToInt32(feedbackViewModel.EmployeeId);
                    Feedback feedback = Mapper.Map<Feedback>(feedbackViewModel);

                    bool IsSave = _feedbackManager.Add(feedback);
                    if (IsSave)
                    {
                        ViewBag.Msg = "Reply Has been saved successfully";
                        return RedirectToAction("Feedback", new { requisitionId = feedback.RequisitionId });
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "Requisitions", "Feedback"));

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

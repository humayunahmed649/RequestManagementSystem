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
using RMS.Models.EntityModels;

namespace RMS.App.Controllers
{
    public class FeedbackController : Controller
    {
        private IRequisitionManager _requisitionManager;
        private IFeedbackManager _feedbackManager;
        private IReplyManager _replyManager;

        public FeedbackController(IRequisitionManager requisitionManager, IReplyManager replyManager, IFeedbackManager feedbackManager,
            IAssignRequisitionManager assignRequisitionManager, INotificationManager notificationManager, IRequisitionHistoryManager requisitionHistoryManager)
        {
            this._requisitionManager = requisitionManager;
            this._feedbackManager = feedbackManager;
            this._replyManager = replyManager;
        }

        // GET: Feedback
        //public ActionResult Index()
        //{
        //    var feedbackViewModels = db.FeedbackViewModels.Include(f => f.Employee).Include(f => f.Requisition);
        //    return View(feedbackViewModels.ToList());
        //}

        // GET: Feedback/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    FeedbackViewModel feedbackViewModel = db.FeedbackViewModels.Find(id);
        //    if (feedbackViewModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(feedbackViewModel);
        //}

        // GET: Feedback/Create
        public ActionResult Create(int requisitionId)
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



                ViewBag.Feedback = _feedbackManager.GetAllByRequisitionId(requisitionId);
                
                //ViewBag.FeedbackWithReplies = _feedbackManager.GetAllFeedbackWithReply(requisitionId);
                
                return View(feedbackViewModel);
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "Feedback", "Create"));
            }
        }

        // POST: Feedback/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CommentText,RequisitionId")] FeedbackViewModel feedbackViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var requisitionByEmployee = _requisitionManager.FindById(feedbackViewModel.RequisitionId);
                    var empId = requisitionByEmployee.EmployeeId;
                    Feedback feedback = Mapper.Map<Feedback>(feedbackViewModel);
                    feedback.EmployeeId = empId;
                    feedback.CreatedOn = DateTime.Now;
                    _feedbackManager.Add(feedback);
                    TempData["Msg"] = "Comment Save successfully";


                    Requisition requisition = _requisitionManager.FindById(feedbackViewModel.RequisitionId);
                    RequisitionViewModel requisitionViewModel = Mapper.Map<RequisitionViewModel>(requisition);
                    feedbackViewModel.Requisition = requisition;
                    ViewBag.Feedback = _feedbackManager.GetAllByRequisitionId(feedbackViewModel.RequisitionId);

                }
                return View(feedbackViewModel);
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "Requisitions", "Feedback"));

            }
        }

        // GET: Feedback/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = _feedbackManager.FindById((int)id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            FeedbackViewModel feedbackViewModel = Mapper.Map<FeedbackViewModel>(feedback);
            return View(feedbackViewModel);
        }

        //// POST: Feedback/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CommentText")] FeedbackViewModel feedbackViewModel)
        {
            if (ModelState.IsValid)
            {
                var updateFeedback = _feedbackManager.FindById(feedbackViewModel.Id);
                Feedback feedback = Mapper.Map<Feedback>(feedbackViewModel);
                feedback.EmployeeId = updateFeedback.EmployeeId;
                feedback.RequisitionId = updateFeedback.RequisitionId;
                feedback.CreatedOn=updateFeedback.CreatedOn;
                feedback.CommentText = feedbackViewModel.CommentText;
                bool IsUpdate = _feedbackManager.Update(feedback);
                if (IsUpdate)
                {
                    TempData["UpdateMsg"] = "Updated Successfully";
                    return RedirectToAction("Create", new {requisitionId = updateFeedback.RequisitionId});
                }
            }
            return View(feedbackViewModel);
        }

        //// GET: Feedback/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    FeedbackViewModel feedbackViewModel = db.FeedbackViewModels.Find(id);
        //    if (feedbackViewModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(feedbackViewModel);
        //}

        //// POST: Feedback/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    FeedbackViewModel feedbackViewModel = db.FeedbackViewModels.Find(id);
        //    db.FeedbackViewModels.Remove(feedbackViewModel);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
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
                _feedbackManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

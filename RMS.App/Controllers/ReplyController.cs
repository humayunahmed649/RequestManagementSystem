using System;
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
    public class ReplyController : Controller
    {

        private IRequisitionManager _requisitionManager;
        private IFeedbackManager _feedbackManager;
        private IEmployeeManager _employeeManager;
        private IReplyManager _replyManager;

        public ReplyController(IRequisitionManager requisitionManager, IEmployeeManager employeeManager, IFeedbackManager feedbackManager,
            IAssignRequisitionManager assignRequisitionManager, IReplyManager replyManager, IRequisitionHistoryManager requisitionHistoryManager)
        {
            this._requisitionManager = requisitionManager;
            this._feedbackManager = feedbackManager;
            this._employeeManager = employeeManager;
            this._replyManager = replyManager;
        }
        //private RmsDbContext db = new RmsDbContext();

        // GET: Reply
        //public ActionResult Index()
        //{
        //    var replyViewModels = db.ReplyViewModels.Include(r => r.Employee).Include(r => r.Feedback);
        //    return View(replyViewModels.ToList());
        //}

        // GET: Reply/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ReplyViewModel replyViewModel = db.ReplyViewModels.Find(id);
        //    if (replyViewModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(replyViewModel);
        //}

        // GET: Reply/Create
        public ActionResult Create(int commentId)
        {
            if (commentId == 0 || commentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = _feedbackManager.FindById(commentId);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            ViewBag.Feedback = feedback;
            ReplyViewModel replyViewModel = new ReplyViewModel();
            replyViewModel.FeedbackId = feedback.Id;
            
            //ReplyViewModel replyViewModel = new ReplyViewModel();
            //replyViewModel.Feedback = Mapper.Map<Feedback>(feedback);
            return View(replyViewModel);
        }

        // POST: Reply/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ReplyText,FeedbackId,CreatedOn")] ReplyViewModel replyViewModel)
        {
            if (ModelState.IsValid)
            {
                Reply reply = Mapper.Map<Reply>(replyViewModel);
                //Get employee Id by user login id
                var loginUserId = Convert.ToInt32(User.Identity.GetUserId());
                var empId = _employeeManager.FindByLoginId(loginUserId);
                reply.EmployeeId = empId.Id;
                reply.CreatedOn=DateTime.Now;

                bool IsSave = _replyManager.Add(reply);
                if (IsSave)
                {
                    TempData["msg"] = "reply save successfully";
                }
            }
            return View(replyViewModel);
        }

        // GET: Reply/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ReplyViewModel replyViewModel = db.ReplyViewModels.Find(id);
        //    if (replyViewModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FullName", replyViewModel.EmployeeId);
        //    ViewBag.FeedbackId = new SelectList(db.Feedbacks, "Id", "CommentText", replyViewModel.FeedbackId);
        //    return View(replyViewModel);
        //}

        //// POST: Reply/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,ReplyText,FeedbackId,EmployeeId,CreatedOn")] ReplyViewModel replyViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(replyViewModel).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FullName", replyViewModel.EmployeeId);
        //    ViewBag.FeedbackId = new SelectList(db.Feedbacks, "Id", "CommentText", replyViewModel.FeedbackId);
        //    return View(replyViewModel);
        //}

        //// GET: Reply/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ReplyViewModel replyViewModel = db.ReplyViewModels.Find(id);
        //    if (replyViewModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(replyViewModel);
        //}

        //// POST: Reply/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    ReplyViewModel replyViewModel = db.ReplyViewModels.Find(id);
        //    db.ReplyViewModels.Remove(replyViewModel);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _feedbackManager.Dispose();
                _requisitionManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

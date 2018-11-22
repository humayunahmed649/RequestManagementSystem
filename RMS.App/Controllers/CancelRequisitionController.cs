using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using RMS.App.ViewModels;
using RMS.BLL.Contracts;
using RMS.Models.DatabaseContext;
using RMS.Models.EntityModels;

namespace RMS.App.Controllers
{
 
    public class CancelRequisitionController : Controller
    {
        private ICancelRequisitionManager _cancelRequisitionManager;
        private IRequisitionStatusManager _requisitionStatusManager;
        private INotificationManager _notificationManager;
        private IRequisitionManager _requisitionManager;
        public CancelRequisitionController(ICancelRequisitionManager cancelRequisitionManager,IRequisitionStatusManager requisitionStatusManager, INotificationManager notificationManager,IRequisitionManager requisitionManager)
        {
            this._cancelRequisitionManager = cancelRequisitionManager;
            this._requisitionStatusManager = requisitionStatusManager;
            this._notificationManager = notificationManager;
            this._requisitionManager = requisitionManager;
        }
        // GET: CancelRequisitionViewModels
        [Authorize(Roles = "Controller,Administrator")]

        public ActionResult Index()
        {
            try
            {
                ICollection<CancelRequisition> cancelRequisitions = _cancelRequisitionManager.GetAll();
                IEnumerable<CancelRequisitionViewModel> cancelRequisitionViewModels =
                    Mapper.Map<IEnumerable<CancelRequisitionViewModel>>(cancelRequisitions);
                return View(cancelRequisitionViewModels);
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "CancelRequisition", "Index"));
            }
        }
        [Authorize(Roles = "Controller,Administrator")]

        // GET: CancelRequisitionViewModels/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CancelRequisition cancelRequisition = _cancelRequisitionManager.FindById((int)id);

                if (cancelRequisition == null)
                {
                    return HttpNotFound();
                }
                CancelRequisitionViewModel cancelRequisitionViewModel =
                   Mapper.Map<CancelRequisitionViewModel>(cancelRequisition);
                return View(cancelRequisitionViewModel);
            }
            catch (Exception ex)
            {

                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "CancelRequisition", "Details"));
            }
            
        }
        [Authorize(Roles = "Controller,Administrator,User")]

        // GET: CancelRequisitionViewModels/Create
        [HttpGet]
        public ActionResult Create(int requisitionId)
        {
            try
            {
                if (requisitionId == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RequisitionStatus requisition = _requisitionStatusManager.FindById(requisitionId);

                if (requisition == null)
                {
                    return HttpNotFound();
                }
                CancelRequisitionViewModel cancelRequisitionViewModel=new CancelRequisitionViewModel();
                cancelRequisitionViewModel.RequisitionStatus=Mapper.Map<RequisitionStatus>(requisition);
                return View(cancelRequisitionViewModel);
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "CancelRequisition", "Create"));
            }
        }

        // POST: CancelRequisitionViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Controller,Administrator,User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RequisitionId,Reason")]CancelRequisitionViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (model.Reason != null)
                    {

                        
                        var status = _requisitionStatusManager.FindByRequisitionId((int)model.RequisitionId);
                        model.RequisitionStatusId = status.Id;
                        model.RequisitionNumber = status.RequisitionNumber;

                        status.StatusType = "Cancelled";
                        bool IsSaved = _requisitionStatusManager.Update(status);
                        if (IsSaved)
                        {
                            CancelRequisition cancelRequisition = Mapper.Map<CancelRequisition>(model);
                            _cancelRequisitionManager.Add(cancelRequisition);
                        }
                        //Notifaication status change after assign requisition
                        Notification notificationUpdate = _notificationManager.FindByRequisitionId(status.RequisitionId);
                        if (notificationUpdate != null)
                        {
                            notificationUpdate.ControllerViewStatus = "Seen";
                            notificationUpdate.SenderViewStatus = "Unseen";
                            notificationUpdate.SenderText = "Your requisition has been canceled.";
                            notificationUpdate.SenderNotifyDateTime = DateTime.Now;
                            var updateResult = _notificationManager.Update(notificationUpdate);
                        }
                        return RedirectToAction("Index");
                    }
                    if (model.Reason.IsNullOrWhiteSpace())
                    {
                        return View("Error");
                    }
                    
                }
                return View();
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "AssignRequisitions", "Requests"));
            }

        }

        //// GET: CancelRequisitionViewModels/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CancelRequisition cancelRequisition = _cancelRequisitionManager.FindById((int)id);
        //    if (cancelRequisition == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    CancelRequisitionViewModel cancelRequisitionViewModel =
        //        Mapper.Map<CancelRequisitionViewModel>(cancelRequisition);
        //    ViewBag.RequisitionId = new SelectList(_cancelRequisitionManager.GetAll(), "Id", "RequisitionId", cancelRequisition.RequisitionId);
        //    ViewBag.RequisitionStatusId = new SelectList(_requisitionStatusManager.GetAll(), "Id", "StatusType", cancelRequisition.RequisitionStatusId);
        //    return View(cancelRequisitionViewModel);
        //}

        //// POST: CancelRequisitionViewModels/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Reason,RequisitionStatusId,RequisitionId")] CancelRequisitionViewModel cancelRequisitionViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        CancelRequisition cancelRequisition = Mapper.Map<CancelRequisition>(cancelRequisitionViewModel);
        //        _cancelRequisitionManager.Update(cancelRequisition);
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.RequisitionId = new SelectList(_requisitionStatusManager.GetAll(), "Id", "RequisitionNumber", cancelRequisition.RequisitionId);
        //    ViewBag.RequisitionStatusId = new SelectList(_requisitionStatusManager.GetAll(), "Id", "StatusType", cancelRequisition.RequisitionStatusId);
        //    return View(cancelRequisitionViewModel);
        //}

        // GET: CancelRequisitionViewModels/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CancelRequisitionViewModel cancelRequisitionViewModel = db.CancelRequisitionViewModels.Find(id);
        //    if (cancelRequisitionViewModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(cancelRequisitionViewModel);
        //}

        //// POST: CancelRequisitionViewModels/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    CancelRequisitionViewModel cancelRequisitionViewModel = db.CancelRequisitionViewModels.Find(id);
        //    db.CancelRequisitionViewModels.Remove(cancelRequisitionViewModel);
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
                _requisitionStatusManager.Dispose();
                _cancelRequisitionManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

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
    [Authorize(Roles = "Administrator,Controller")]
    public class RequisitionStatusController : Controller
    {
        private IRequisitionStatusManager _requisitionStatusManager;
        private IRequisitionManager _requisitionManager;

        public RequisitionStatusController(IRequisitionStatusManager requisitionStatusManager,IRequisitionManager requisitionManager)
        {
            this._requisitionStatusManager = requisitionStatusManager;
            this._requisitionManager = requisitionManager;
        }
        public ActionResult Index()
        {
            try
            {
                var requisitionStatuses = _requisitionStatusManager.GetAll();
                IEnumerable<RequisitionStatusViewModel> requisitionStatusViewModels =
                    Mapper.Map<IEnumerable<RequisitionStatusViewModel>>(requisitionStatuses);
                return View(requisitionStatusViewModels);
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "RequisitionStatus", "Index"));
            }
        }

        // GET: RequisitionStatus/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RequisitionStatus requisitionStatus = _requisitionStatusManager.FindById((int)id);
                if (requisitionStatus == null)
                {
                    return HttpNotFound();
                }
                RequisitionStatusViewModel requisitionStatusViewModel =
                    Mapper.Map<RequisitionStatusViewModel>(requisitionStatus);
                return View(requisitionStatusViewModel);
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "RequisitionStatus", "Details"));
            }

        }

        // GET: RequisitionStatus/Create
        //public ActionResult Create()
        //{
        //    ViewBag.RequisitionId = new SelectList(_requisitionStatusManager.Requisitions, "Id", "RequisitionNumber");
        //    return View();
        //}

        // POST: RequisitionStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        //public ActionResult Create([Bind(Include = "Id,StatusType,RequisitionId")] RequisitionStatus requisitionStatus)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.RequisitionStatuses.Add(requisitionStatus);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.RequisitionId = new SelectList(db.Requisitions, "Id", "RequisitionNumber", requisitionStatus.RequisitionId);
        //    return View(requisitionStatus);
        //}

        // GET: RequisitionStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RequisitionStatus requisitionStatus = _requisitionStatusManager.FindById((int)id);
                if (requisitionStatus == null)
                {
                    return HttpNotFound();
                }
                RequisitionStatusViewModel requisitionStatusViewModel =
                    Mapper.Map<RequisitionStatusViewModel>(requisitionStatus);
                ViewBag.RequisitionId = new SelectList(_requisitionManager.GetAll(), "Id", "RequisitionNumber", requisitionStatus.RequisitionId);
                return View(requisitionStatusViewModel);
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "RequisitionStatus", "Edit"));
            }
        }

        // POST: RequisitionStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StatusType,RequisitionId")] RequisitionStatusViewModel requisitionStatusViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RequisitionStatus requisitionStatus = Mapper.Map<RequisitionStatus>(requisitionStatusViewModel);
                    _requisitionStatusManager.Update(requisitionStatus);
                    return RedirectToAction("Index");
                }
                ViewBag.RequisitionId = new SelectList(_requisitionManager.GetAll(), "Id", "RequisitionNumber", requisitionStatusViewModel.RequisitionId);
                return View(requisitionStatusViewModel);
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "RequisitionStatus", "Edit"));
            }
        }

        // GET: RequisitionStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RequisitionStatus requisitionStatus = _requisitionStatusManager.FindById((int)id);
                if (requisitionStatus == null)
                {
                    return HttpNotFound();
                }
                RequisitionStatusViewModel requisitionStatusViewModel =
                    Mapper.Map<RequisitionStatusViewModel>(requisitionStatus);
                return View(requisitionStatusViewModel);
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "RequisitionStatus", "Delete"));
            }
        }

        // POST: RequisitionStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {

                RequisitionStatus requisitionStatus = _requisitionStatusManager.FindById((int)id);
                _requisitionStatusManager.Remove(requisitionStatus);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "RequisitionStatus", "Delete"));
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
                _requisitionStatusManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

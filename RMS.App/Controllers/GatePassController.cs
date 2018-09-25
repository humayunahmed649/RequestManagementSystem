using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using RMS.App.ViewModels;
using RMS.BLL.Contracts;
using RMS.Models.EntityModels;

namespace RMS.App.Controllers
{
    [Authorize(Roles = "Controller,Administrator")]
    public class GatePassController : Controller
    {
        private IAssignRequisitionManager _assignRequisitionManager;
        private IRequisitionManager _requisitionManager;
        private IRequisitionStatusManager _requisitionStatusManager;
        public GatePassController(IAssignRequisitionManager assignRequisitionManager,IRequisitionManager requisitionManager,IRequisitionStatusManager statusManager)
        {
            this._assignRequisitionManager = assignRequisitionManager;
            this._requisitionManager = requisitionManager;
            this._requisitionStatusManager = statusManager;
        }
        // GET: GatePass
        public ActionResult Index()
        {
            try
            {

                ICollection<RequisitionStatus> requisitionStatus = _requisitionStatusManager.GetAllWithRequisitionDetails();
                IEnumerable<RequisitionStatusViewModel> assignRequisitionView = Mapper.Map<IEnumerable<RequisitionStatusViewModel>>(requisitionStatus);
                return View(assignRequisitionView);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "GatePass", "Index"));
            }
        }
        [HttpGet]
        public ActionResult CheckOut(int? id)
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
                if (requisitionStatus != null)
                {
                    RequisitionStatusViewModel requisitionStatusViewModel =
                        Mapper.Map<RequisitionStatusViewModel>(requisitionStatus);
                    return View(requisitionStatusViewModel);
                }
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "GatePass", "CheckOut"));
            }

        }
        [HttpPost]
        public ActionResult CheckOut([Bind(Include = "Id,StatusType,RequisitionNumber,RequisitionId")] RequisitionStatusViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RequisitionStatus requisitionStatus = Mapper.Map<RequisitionStatus>(model);
                    requisitionStatus.StatusType = "OnExecute";
                    _requisitionStatusManager.Update(requisitionStatus);
                    return RedirectToAction("Index");
                }

                return View();

            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "GatePass", "CheckIn"));
            }

        }
        [HttpGet]
        public ActionResult CheckIn(int? id)
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
                if (requisitionStatus != null)
                {
                    RequisitionStatusViewModel requisitionStatusViewModel =
                        Mapper.Map<RequisitionStatusViewModel>(requisitionStatus);
                    return View(requisitionStatusViewModel);
                }
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "GatePass", "CheckIn"));
            }

        }
        

        [HttpPost]
        public ActionResult CheckIn([Bind(Include = "Id,StatusType,RequisitionNumber,RequisitionId")] RequisitionStatusViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RequisitionStatus requisitionStatus = Mapper.Map<RequisitionStatus>(model);
                    requisitionStatus.StatusType = "Completed";
                    _requisitionStatusManager.Update(requisitionStatus);
                    return RedirectToAction("Index","Queue");
                }

                return View();

            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "GatePass", "CheckIn"));
            }

        }
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,StatusType,RequisitionNumber,RequisitionId")] RequisitionStatusViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RequisitionStatus request = Mapper.Map<RequisitionStatus>(model);
                    request.StatusType = "Completed";
                    _requisitionStatusManager.Update(request);
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "GatePass", "Edit"));
            }

        }

    }
}
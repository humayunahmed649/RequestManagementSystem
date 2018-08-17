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
            ICollection<RequisitionStatus> requisitionStatus = _requisitionStatusManager.GetAllWithRequisitionDetails();
            IEnumerable<RequisitionStatusViewModel> assignRequisitionView = Mapper.Map<IEnumerable<RequisitionStatusViewModel>>(requisitionStatus);
            return View(assignRequisitionView);
        }
        [HttpGet]
        public ActionResult CheckOut(int? id)
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
                
                RequisitionStatusViewModel assignRequisitionViewModel =
                    Mapper.Map<RequisitionStatusViewModel>(requisitionStatus);

                //ViewBag.StatusId = _assignRequisitionManager.FindByRequisitionId((int)id);
                return View(assignRequisitionViewModel);
            }
            return View("Error");
        }
        [HttpPost]
        public ActionResult CheckOut([Bind(Include = "Id,StatusType,RequisitionNumber,RequisitionId")] RequisitionStatusViewModel model)
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
       
    }
}
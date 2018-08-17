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
            ICollection<AssignRequisition> assignRequisitions = _assignRequisitionManager.GetAllWithInformation();
            IEnumerable<AssignRequisitionViewModel> assignRequisitionView = Mapper.Map<IEnumerable<AssignRequisitionViewModel>>(assignRequisitions);
            return View(assignRequisitionView);
        }
        [HttpGet]
        public ActionResult CheckOut(int? id)
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
                
                AssignRequisitionViewModel assignRequisitionViewModel =
                    Mapper.Map<AssignRequisitionViewModel>(assignRequisition);

                //ViewBag.StatusId = _assignRequisitionManager.FindByRequisitionId((int)id);
                return View(assignRequisitionViewModel);
            }
            return View("Error");
        }
        [HttpPost]
        public ActionResult CheckOut([Bind(Include = "Id,RequisitionId,RequisitionNumber,VehicleId,DriverId,EmployeeId")] AssignRequisitionViewModel model)
        {
            if (ModelState.IsValid)
            {
                AssignRequisition assignRequisition = Mapper.Map<AssignRequisition>(model);
                _assignRequisitionManager.Update(assignRequisition);

                //RequisitionStatus status=new RequisitionStatus();
                ////status.Id = _assignRequisitionManager.FindByRequisitionId(model.RequisitionId);
                //status.StatusType = "OnExecute";
                //status.RequisitionNumber = model.RequisitionNumber;
                //status.RequisitionId = model.RequisitionId;
                //_requisitionStatusManager.Update(status);
                //return RedirectToAction("Index");
            }
            
            return View();
        }
        //[HttpGet]
        //public ActionResult CheckIn()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult CheckIn()
        //{
        //    return View();
        //}
    }
}
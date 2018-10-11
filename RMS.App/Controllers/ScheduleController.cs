using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using RMS.App.ViewModels;
using RMS.BLL.Contracts;
using RMS.Models.EntityModels;

namespace RMS.App.Controllers
{
    [Authorize(Roles = "Controller,Administrator")]
    public class ScheduleController : Controller
    {
        private IAssignRequisitionManager _assignRequisitionManager;
        private IRequisitionStatusManager _requisitionStatusManager;

        public ScheduleController(IAssignRequisitionManager assignRequisitionManager,IRequisitionStatusManager requisitionStatusManager)
        {
            this._assignRequisitionManager = assignRequisitionManager;
            this._requisitionStatusManager = requisitionStatusManager;
        }

        // GET: Schedule
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllRequisition()
        {
            var requisitions = _requisitionStatusManager.GetAllRequisitions();
            return new JsonResult() {Data = requisitions,JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }
        
    }
}
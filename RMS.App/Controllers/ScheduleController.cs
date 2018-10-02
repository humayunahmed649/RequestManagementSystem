using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.BLL.Contracts;

namespace RMS.App.Controllers
{
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

        public JsonResult GetAssignRequisition()
        {
            var requisitions = _requisitionStatusManager.GetAllAssignRequisitions();
            return new JsonResult() {Data = requisitions,JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }
    }
}
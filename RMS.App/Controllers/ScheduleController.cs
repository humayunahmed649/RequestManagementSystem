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

        public ScheduleController(IAssignRequisitionManager assignRequisitionManager)
        {
            this._assignRequisitionManager = assignRequisitionManager;
        }

        // GET: Schedule
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAssignRequisition()
        {
            var requisitions = _assignRequisitionManager.GetAll().ToList();
            return new JsonResult() {Data = requisitions,JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }
    }
}
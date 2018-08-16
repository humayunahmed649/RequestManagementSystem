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
    public class GatePassController : Controller
    {
        private IAssignRequisitionManager _assignRequisitionManager;
        private IRequisitionManager _requisitionManager;
        public GatePassController(IAssignRequisitionManager assignRequisitionManager,IRequisitionManager requisitionManager)
        {
            this._assignRequisitionManager = assignRequisitionManager;
            this._requisitionManager = requisitionManager;
        }
        // GET: GatePass
        public ActionResult Index()
        {
            ICollection<AssignRequisition> assignRequisitions = _assignRequisitionManager.GetAllWithInformation();
            IEnumerable<AssignRequisitionViewModel> assignRequisitionView = Mapper.Map<IEnumerable<AssignRequisitionViewModel>>(assignRequisitions);
            return View(assignRequisitionView);
        }
        //[HttpGet]
        //public ActionResult CheckOut()
        //{
        //    AssignRequisitionViewModel model=new AssignRequisitionViewModel();
        //    model.Requisition.RequisitionNumber
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult CheckOut()
        //{
        //    return View();
        //}
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
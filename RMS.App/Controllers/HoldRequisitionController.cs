using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using RMS.App.ViewModels;
using RMS.BLL.Contracts;
using RMS.Models.EntityModels;
using WebGrease.Css.Extensions;

namespace RMS.App.Controllers
{
    public class HoldRequisitionController : Controller
    {
        private IRequisitionManager _requisitionManager;
        private IVehicleManager _vehicleManager;
        private IEmployeeManager _employeeManager;
        private IAssignRequisitionManager _assignRequisitionManager;
        private IRequisitionStatusManager _requisitionStatusManager;
        private IVehicleTypeManager _vehicleTypeManager;
        private INotificationManager _notificationManager;
        private IMailServiceManager _mailServiceManager;
        private ICancelRequisitionManager _cancelRequisitionManager;
        private IRequisitionHistoryManager _requisitionHistoryManager;

        public HoldRequisitionController(IRequisitionManager requisitionManager, IVehicleManager vehicleManager, IEmployeeManager employeeManager, IAssignRequisitionManager assignRequisitionManager, IRequisitionStatusManager requisitionStatusManager,
            IVehicleTypeManager vehicleTypeManager, INotificationManager notificationManager, IMailServiceManager mailServiceManager, ICancelRequisitionManager cancelRequisitionManager, IRequisitionHistoryManager requisitionHistoryManager)
        {
            this._requisitionManager = requisitionManager;
            this._employeeManager = employeeManager;
            this._vehicleManager = vehicleManager;
            this._assignRequisitionManager = assignRequisitionManager;
            this._requisitionStatusManager = requisitionStatusManager;
            this._vehicleTypeManager = vehicleTypeManager;
            this._notificationManager = notificationManager;
            this._mailServiceManager = mailServiceManager;
            this._cancelRequisitionManager = cancelRequisitionManager;
            this._requisitionHistoryManager = requisitionHistoryManager;

        }
        // GET: HoldRequisition
        public ActionResult Index()
        {
            try
            {

                ICollection<RequisitionStatus> requisitionStatus = _requisitionStatusManager.GetAll().Where(c => c.StatusType == "Hold").ToList();
                IEnumerable<RequisitionStatusViewModel> requisitionStatusViewModels =
                    Mapper.Map<IEnumerable<RequisitionStatusViewModel>>(requisitionStatus);
                return View(requisitionStatusViewModels);
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "AssignRequisitions", "Index"));
            }
        }

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
                RequisitionStatusViewModel requisitionStatusViewModel = Mapper.Map<RequisitionStatusViewModel>(requisition);
                return View(requisitionStatusViewModel);
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "HoldRequisition", "Create"));
            }
        }

        public ActionResult Create(RequisitionStatusViewModel statusViewModel)
        {
            try
            {
                var requisitionStatus= _requisitionStatusManager.FindByRequisitionId(statusViewModel.Id); ;
                requisitionStatus.StatusType = "Hold";
                bool IsUpdate = _requisitionStatusManager.Update(requisitionStatus);
                if (IsUpdate)
                {
                    return RedirectToAction("Index", "AssignRequisitions");
                }
                return View(statusViewModel);
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "HoldRequisition", "Create"));
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
    }
}
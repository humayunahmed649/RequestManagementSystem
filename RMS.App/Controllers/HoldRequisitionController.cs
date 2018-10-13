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
        public ActionResult Hold(int statusId)
        {
            try
            {

                if (statusId == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RequisitionStatus requisition = _requisitionStatusManager.FindById(statusId);

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
                return View("Error", new HandleErrorInfo(ex, "CancelRequisition", "Create"));
            }
        }

        public ActionResult Hold(RequisitionStatusViewModel statusViewModel)
        {
            RequisitionStatus requisitionStatus = Mapper.Map<RequisitionStatus>(statusViewModel);
            //var status =_requisitionStatusManager.FindByRequisitionId(statusViewModel.RequisitionId);
            //requisitionStatus.Id = statusViewModel.Id;
            //requisitionStatus.RequisitionId = statusViewModel.RequisitionId;
            //requisitionStatus.RequisitionNumber = statusViewModel.RequisitionNumber;
            requisitionStatus.StatusType = "Hold";
            bool IsUpdate = _requisitionStatusManager.Update(requisitionStatus);
            if (IsUpdate)
            {
                return RedirectToAction("Index", "AssignRequisitions");
            }
            return View(statusViewModel);
        }

        public ActionResult HoldRequestCreate(int statusId)
        {
            try
            {
                if (statusId == 0)
                {
                    return View("Error");
                }
                RequisitionStatus requisition = _requisitionStatusManager.FindById(statusId);
                if (requisition == null)
                {
                    return HttpNotFound();
                }

                ViewBag.Requisition = requisition;

                AssignRequisitionViewModel assignRequisitionViewModel = new AssignRequisitionViewModel();
                assignRequisitionViewModel.RequisitionId = requisition.RequisitionId;
                assignRequisitionViewModel.VehicleTypes = _vehicleTypeManager.GetAll().ToList();
                assignRequisitionViewModel.RequisitionNumber = requisition.RequisitionNumber;

                ViewBag.EmployeeId = new SelectList(_employeeManager.GetAllDriver(), "Id", "FullName");
                ViewBag.VehicleId = new SelectListItem[] { new SelectListItem() { Value = "", Text = "Select Vehicle" } };
                return View(assignRequisitionViewModel);
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "AssignRequisitions", "Create"));
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
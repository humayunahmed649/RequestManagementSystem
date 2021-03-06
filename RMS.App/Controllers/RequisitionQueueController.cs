﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using RMS.App.ViewModels;
using RMS.BLL.Contracts;

namespace RMS.App.Controllers
{
    [Authorize(Roles = "Controller,Administrator")]
    public class RequisitionQueueController : Controller
    {
        private IRequisitionStatusManager _requisitionStatusManager;
        private IRequisitionManager _requisitionManager;

        public RequisitionQueueController(IRequisitionStatusManager requisitionStatusManager, IRequisitionManager requisitionManager)
        {
            this._requisitionStatusManager = requisitionStatusManager;
            this._requisitionManager = requisitionManager;
        }
        // GET: RequisitionQueue
        public ActionResult Queue()
        {
            try
            {
                var requisitionStatuses = _requisitionStatusManager.GetAllStatusNew();
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
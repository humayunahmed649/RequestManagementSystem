using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AutoMapper;
using Microsoft.AspNet.Identity;
using RMS.App.ViewModels;
using RMS.BLL.Contracts;
using RMS.Models.DatabaseContext;
using RMS.Models.EntityModels;
using RMS.Models.Identity.IdentityConfig;
using RMS.Repositories.Contracts;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Reporting.WebForms;
using WebGrease.Css.Extensions;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace RMS.App.Controllers
{
    [Authorize(Roles = "Controller,Administrator")]
    public class AssignRequisitionsController : Controller
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

        public AssignRequisitionsController(IRequisitionManager requisitionManager,IVehicleManager vehicleManager,IEmployeeManager employeeManager,IAssignRequisitionManager assignRequisitionManager,IRequisitionStatusManager requisitionStatusManager,
            IVehicleTypeManager vehicleTypeManager, INotificationManager notificationManager, IMailServiceManager mailServiceManager,ICancelRequisitionManager cancelRequisitionManager,IRequisitionHistoryManager requisitionHistoryManager)
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
        
        // GET: AssignRequisitions
        public ActionResult Index(string searchText)
        {
            try
            {

                ICollection<RequisitionStatus> requisitionStatus = _requisitionStatusManager.GetAll().Where(c=>c.StatusType=="Assigned").ToList();
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

        // GET: AssignRequisitions/Details/5
        public ActionResult Details(int? id)
        {
            try
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

                    var requestDetails = _assignRequisitionManager.GetAllWithInformation();
                    AssignRequisitionViewModel assignRequisitionViewModel = Mapper.Map<AssignRequisitionViewModel>(assignRequisition);

                    return View(assignRequisitionViewModel);

            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "AssignRequisitions", "Details"));
            }
        }

        
        //Get
        public ActionResult Create(int requisitionId)
        {
            try
            {
                if (requisitionId == 0)
                {
                    return View("Error");
                }
                Requisition requisition = _requisitionManager.FindById(requisitionId);
                if (requisition == null)
                {
                    return HttpNotFound();
                }

                RequisitionViewModel requisitionViewModel = Mapper.Map<RequisitionViewModel>(ViewBag.Requisition = requisition);

                AssignRequisitionViewModel assignRequisitionViewModel = new AssignRequisitionViewModel();
                assignRequisitionViewModel.RequisitionId = requisitionId;
                assignRequisitionViewModel.VehicleTypes = _vehicleTypeManager.GetAll().ToList();

                ViewBag.RequisitionNumber = requisition.RequisitionNumber;
                ViewBag.EmployeeId = new SelectList(_employeeManager.GetAllDriver(), "Id", "FullName");
                ViewBag.VehicleId = new SelectListItem[] { new SelectListItem() { Value = "", Text = "Select Vehicle" } };
                ViewBag.RequisitionStatusId = new SelectList(_requisitionStatusManager.GetAllStatusNew(), "Id", "StatusType");
                return View(assignRequisitionViewModel);
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "AssignRequisitions", "Create"));
            }
        }
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RequisitionId,RequisitionStatusId,RequisitionNumber,VehicleId,EmployeeId")] AssignRequisitionViewModel assignRequisitionViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var requestStatus = _requisitionStatusManager.FindByRequisitionId(assignRequisitionViewModel.RequisitionId);
                    if(requestStatus.StatusType== "Cancelled")
                    {
                        TempData["StatusMsg"] = "This is Cancelled Request try another one...Thanks";
                        return  RedirectToAction("Create", new { requisitionId = assignRequisitionViewModel.RequisitionId });
                    }
                    
                    var vehicleStatus = _assignRequisitionManager.GetVehicleStatus(assignRequisitionViewModel.VehicleId);
                    if (vehicleStatus.Id > 0)
                    {
                        if (requestStatus.Requisition.StartDateTime > vehicleStatus.Requisition.EndDateTime)
                        {
                            SaveRequisition(assignRequisitionViewModel);
                            return RedirectToAction("PrintDetails", "AssignRequisitions", new { id = assignRequisitionViewModel.RequisitionId });
                        }

                        TempData["StatusMsg"] = "You Cannot Assigned a vehicle/Driver which is not Available...Select Another Vehicle";
                        return RedirectToAction("Create", new { requisitionId = assignRequisitionViewModel.RequisitionId });
                    }
                    if (vehicleStatus.Id <= 0)
                    {
                        assignRequisitionViewModel.RequisitionStatusId = requestStatus.Id;
                        SaveRequisition(assignRequisitionViewModel);
                        return RedirectToAction("PrintDetails", "AssignRequisitions", new { id = assignRequisitionViewModel.RequisitionId });
                    }
                    else
                    {
                        
                            ViewBag.RequisitionStatusId = new SelectList(_requisitionStatusManager.GetAllStatusNew(), "Id", "StatusType");
                            ViewBag.VehicleId = new SelectListItem[] { new SelectListItem() { Value = "", Text = "Select Vehicle" } };
                            ViewBag.RequisitionNumber = assignRequisitionViewModel.RequisitionNumber;
                            ViewBag.EmployeeId = new SelectList(_employeeManager.GetAllDriver(), "Id", "FullName");
                            TempData["StatusMsg"] = "You Cannot Assigned a vehicle/Driver which is not Available...Select Another Vehicle";
                            return RedirectToAction("Create", new { requisitionId = assignRequisitionViewModel.RequisitionId });
                        
                    }
                    
                }
                return View(assignRequisitionViewModel);

                //Requisition requisition = _requisitionManager.FindById(assignRequisitionViewModel.RequisitionId);

                //RequisitionViewModel requisitionViewModel = Mapper.Map<RequisitionViewModel>(ViewBag.Requisition = requisition);
                //assignRequisitionViewModel.RequisitionId = assignRequisitionViewModel.RequisitionId;
                //assignRequisitionViewModel.VehicleTypes = _vehicleTypeManager.GetAll().ToList();


                //ViewBag.RequisitionNumber = assignRequisitionViewModel.RequisitionNumber;
                //ViewBag.EmployeeId = new SelectList(_employeeManager.GetAllDriver(), "Id", "FullName");
                //ViewBag.VehicleId = new SelectListItem[] { new SelectListItem() { Value = "", Text = "Select Vehicle" } };
                //ViewBag.RequisitionStatusId = new SelectList(_requisitionStatusManager.GetAllStatusNew(), "Id", "StatusType");


                //TempData["msg"] = "Assign faield! You have missed to select all field.";

                //return View(assignRequisitionViewModel);
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "AssignRequisitions", "Create"));
            }
        }

        //Hold Request Assign info get by requisitionId //Get
       
        private void SaveRequisition(AssignRequisitionViewModel assignRequisitionViewModel)
        {
            AssignRequisition assignRequisition = Mapper.Map<AssignRequisition>(assignRequisitionViewModel);
            bool isSave = _assignRequisitionManager.Add(assignRequisition);

            if (isSave)
            {
                //Requisition History Save
                RequisitionHistory history = new RequisitionHistory();
                history.Status = "Assigned";
                history.RequisitionId = assignRequisition.RequisitionId;
                history.SubmitDateTime = DateTime.Now;
                _requisitionHistoryManager.Add(history);

                //Requisition status information
                var status =
                    _requisitionStatusManager.FindByRequisitionId(assignRequisition.RequisitionId);
                status.Id = assignRequisition.RequisitionStatusId;
                status.RequisitionId = assignRequisition.RequisitionId;
                status.RequisitionNumber = assignRequisition.RequisitionNumber;
                status.StatusType = "Assigned";

                bool Saved = _requisitionStatusManager.Update(status);

                if (Saved)
                {
                    //Notifaication status change after assign requisition
                    Notification notificationUpdate =
                        _notificationManager.FindByRequisitionId(assignRequisition.RequisitionId);

                    //Get Driver by id
                    var driver = _employeeManager.FindById(assignRequisition.EmployeeId);

                    //Get employee by requisition Id
                    var req = _requisitionManager.FindById(assignRequisition.RequisitionId);

                    if (notificationUpdate != null)
                    {
                        notificationUpdate.ControllerViewStatus = "Seen";
                        notificationUpdate.SenderViewStatus = "Unseen";
                        notificationUpdate.SenderText = "Your vehicle has been assigned";
                        notificationUpdate.SenderNotifyDateTime = DateTime.Now;
                        var updateResult = _notificationManager.Update(notificationUpdate);

                        //Sending mail to employee for assigned vehicle
                        if (updateResult)
                        {

                            //Get Vehicle Type by id
                            var vehicle = _vehicleManager.FindById(assignRequisition.VehicleId);

                            //Get controller info
                            var loginUserId = Convert.ToInt32(User.Identity.GetUserId());
                            var controller = _employeeManager.FindByLoginId(loginUserId);

                            //Mail service section
                            var subject = "Assign a vehicle on your requisition no : " +
                                          assignRequisition.RequisitionNumber;

                            var msgBody = "Dear " + req.Employee.FullName + "," + Environment.NewLine +
                                          "On the basis of your request, assigned a vehicle." +
                                          Environment.NewLine + "Your driver is :" +
                                          driver.FullName + " Contect No : " + driver.ContactNo +
                                          Environment.NewLine + "Vehicle :" + vehicle.VehicleType.Name +
                                          " and Reg No : " + vehicle.RegNo + Environment.NewLine +
                                          "Regards, " + Environment.NewLine + controller.FullName;


                            MailService mailService = new MailService();
                            mailService.To = req.Employee.Email;
                            mailService.From = "demowork9999@gmail.com";
                            mailService.Subject = subject;
                            mailService.Body = msgBody;
                            mailService.MailSendingDateTime = DateTime.Now;
                            mailService.RequisitionId = req.Id;

                            var result = _mailServiceManager.Add(mailService);

                            if (result && req.Employee.Email != null)
                            {

                                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                                smtpClient.Credentials =
                                    new NetworkCredential("demowork9999@gmail.com", "~Aa123456");
                                smtpClient.EnableSsl = true;


                                MailMessage mailMessage = new MailMessage();
                                mailMessage.From = new MailAddress("demowork9999@gmail.com");
                                mailMessage.To.Add(mailService.To);
                                mailMessage.Subject = mailService.Subject;
                                mailMessage.Body = mailService.Body;
                                smtpClient.Send(mailMessage);

                                TempData["msg"] = "Vehicle assigned and mail send successfully";


                                //catch (Exception ex)
                                //{
                                //    TempData["msg1"] =
                                //        "Vehicle assigned and notification send successfully. Mail send failed. The error message is -" +
                                //        "<br/>" + " [ " + ex.Message + " Helpline" + " ] ";

                                //    return RedirectToAction("Index");
                                //}

                            }

                            

                        }
                    }
                    try
                    {
                        //SmS to the driver and user

                        const string accountSid = "AC092d5f38c0ba5e8b384528c662c3209e";
                        const string authToken = "a6bdd845a5552a4df57cc59c097223d1";
                        TwilioClient.Init(accountSid, authToken);
                        var to = new PhoneNumber("+88" + driver.ContactNo);
                        var driverMsg = "Dear " + driver.FullName + "," + " You are assigned by the Requisition No " +
                                        req.RequisitionNumber + ", and Employee Contact Number is" +
                                        req.Employee.ContactNo;
                        var message = MessageResource.Create(
                            to,
                            from: new PhoneNumber("+18504035959"), //  From number, must be an SMS-enabled Twilio number ( This will send sms from ur "To" numbers ).
                            body: driverMsg);
                    }
                    catch (Exception ex)
                    {

                        var failMessage = ex.ToString();
                        TempData["msg"] = "Vehicle assigned and mail send successfully but fail to send message for invalid mobile number";
                    }
                    
                }
                
            }
        }

        // GET: AssignRequisitions/Edit/5
        public ActionResult ReAssign(int id)
        {
            try
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
                //AssignRequisitionViewModel assignRequisitionViewModel =Mapper.Map<AssignRequisitionViewModel>(assignRequisition);
                ReAssignRequisitionViewModel requisitionViewModel=new ReAssignRequisitionViewModel();
                requisitionViewModel.AssignRequisitionViewModel = Mapper.Map<AssignRequisitionViewModel>(assignRequisition);


                requisitionViewModel.VehicleTypes = _vehicleTypeManager.GetAll().ToList();
                ViewBag.EmployeeId = new SelectList(_employeeManager.GetAllDriver(), "Id", "FullName");
                ViewBag.VehicleId = new SelectListItem[] { new SelectListItem() { Value = "", Text = "Select Vehicle" } };
                return View(requisitionViewModel);
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "AssignRequisitions", "ReAssign"));
            }
        }

        // POST: AssignRequisitions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReAssign([Bind(Include = "Id,RequisitionId,RequisitionStatusId,RequisitionNumber,VehicleId,EmployeeId,StartDateTime,EndDateTime")] ReAssignRequisitionViewModel reAssignRequisitionViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   
                    var assignRequisition = _assignRequisitionManager.FindById(reAssignRequisitionViewModel.Id);
                    

                    assignRequisition.VehicleId = reAssignRequisitionViewModel.VehicleId;
                    assignRequisition.EmployeeId = reAssignRequisitionViewModel.EmployeeId;
                    assignRequisition.Employee = null;
                    assignRequisition.Vehicle = null;
                    bool IsUpdated=_assignRequisitionManager.Update(assignRequisition);
                    if (IsUpdated)
                    {
                        var requisitionUpdate = _requisitionManager.FindById(assignRequisition.RequisitionId);


                        requisitionUpdate.StartDateTime = reAssignRequisitionViewModel.StartDateTime;
                        requisitionUpdate.EndDateTime = reAssignRequisitionViewModel.EndDateTime;
                        _requisitionManager.Update(requisitionUpdate);
                    }
                    return RedirectToAction("Index");
                }
                AssignRequisitionViewModel assignRequisitionViewModel = Mapper.Map<AssignRequisitionViewModel>(reAssignRequisitionViewModel);
                ReAssignRequisitionViewModel requisitionViewModel = new ReAssignRequisitionViewModel();
                requisitionViewModel.AssignRequisitionViewModel = assignRequisitionViewModel;


                assignRequisitionViewModel.VehicleTypes = _vehicleTypeManager.GetAll().ToList();
                ViewBag.EmployeeId = new SelectList(_employeeManager.GetAllDriver(), "Id", "FullName");
                ViewBag.VehicleId = new SelectListItem[] { new SelectListItem() { Value = "", Text = "Select Vehicle" } };
                ViewBag.RequisitionStatusId = new SelectList(_requisitionStatusManager.GetAllStatusNew(), "Id", "StatusType");
                return View(requisitionViewModel);
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "AssignRequisitions", "ReAssign"));
            }
        }

        // GET: AssignRequisitions/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var reqId = _assignRequisitionManager.FindById((int) id);
                
                AssignRequisition assignRequisition = _assignRequisitionManager.FindById((int)id);
                if (assignRequisition == null)
                {
                    return HttpNotFound();
                }
                AssignRequisitionViewModel assignRequisitionViewModel =
                    Mapper.Map<AssignRequisitionViewModel>(assignRequisition);
                return View(assignRequisitionViewModel);
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "AssignRequisitions", "Delete"));
            }
        }

        // POST: AssignRequisitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                AssignRequisition assignRequisition = _assignRequisitionManager.FindById((int)id);
                _assignRequisitionManager.Remove(assignRequisition);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "AssignRequisitions", "Delete"));
            }
        }

        public ActionResult Requests(string searchText)
        {
            try
            {
                if (searchText != null)
                {
                    IEnumerable<RequisitionStatus> requisition = _requisitionStatusManager.SearchByRequisitionId(searchText);
                    IEnumerable<RequisitionStatusViewModel> requisitionStatusViewModels =
                    Mapper.Map<IEnumerable<RequisitionStatusViewModel>>(requisition);
                    return View(requisitionStatusViewModels);
                }

                ICollection<RequisitionStatus> requisitions = _requisitionStatusManager.GetAllStatusNew();
                IEnumerable<RequisitionStatusViewModel> requisitionViewModels =
                    Mapper.Map<IEnumerable<RequisitionStatusViewModel>>(requisitions);
                return View(requisitionViewModels);
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "AssignRequisitions", "Requests"));
            }
        }
        public ActionResult ViewDetails(int? id)
        {
            try
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Requisition requisition = _requisitionManager.FindById((int)id);
                if (requisition == null)
                {
                    return HttpNotFound();
                }

                 RequisitionViewModel requisitionViewModel = Mapper.Map<RequisitionViewModel>(requisition);

                 return View(requisitionViewModel);

            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "AssignRequisitions", "ViewDetails"));
            }
            
        }

        public ActionResult CompleteRequest()
        {
            try
            {
                ICollection<RequisitionStatus> requisitions = _requisitionStatusManager.GetAll().Where(c => c.StatusType == "Completed").ToList();
                IEnumerable<RequisitionStatusViewModel> requisitionStatusViewModels =
                    Mapper.Map<IEnumerable<RequisitionStatusViewModel>>(requisitions);
                return View(requisitionStatusViewModels);

            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "AssignRequisitions", "CompleteRequest"));
            }
        }

        public ActionResult OnProcess()
        {
            try
            {
                ICollection<RequisitionStatus> requisitions = _requisitionStatusManager.GetAll().Where(c => c.StatusType == "OnExecute").ToList();
                IEnumerable<RequisitionStatusViewModel> requisitionStatusViewModels =
                    Mapper.Map<IEnumerable<RequisitionStatusViewModel>>(requisitions);
                return View(requisitionStatusViewModels);

            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "AssignRequisitions", "OnProcess"));
            }
        }

        public ActionResult ReportIndex()
        {
            try
            {
                var reportData = _assignRequisitionManager.GetRequisitionSummaryReport();
                var reportPath = Request.MapPath(Request.ApplicationPath) + @"\Report\AssignRequisition\AssignRequisitionReportRdlc.rdlc";
                ReportViewer reportViewer = new ReportViewer()
                {
                    KeepSessionAlive = true,
                    SizeToReportContent = true,
                    Width = Unit.Percentage(100),
                    ProcessingMode = ProcessingMode.Local
                };

                reportViewer.LocalReport.ReportPath = reportPath;

                ReportDataSource rds = new ReportDataSource("DS_AssignRequisitionSummary", reportData);

                reportViewer.LocalReport.DataSources.Add(rds);
                ViewBag.ReportViewer = reportViewer;
                return View();
            }
            catch (Exception ex)
            {

                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "AssignRequisitions", "ReportIndex"));
            }

        }
        
        public ActionResult PrintAssignRequisition(int assignRequisitionId)
        {
            try
            {
                var reportData = _assignRequisitionManager.GetAssignRequisition(assignRequisitionId);
                var reportPath = Request.MapPath(Request.ApplicationPath) + @"\Report\AssignRequisition\AssignReportRdlc.rdlc";
                ReportViewer reportViewer = new ReportViewer()
                {
                    KeepSessionAlive = true,
                    SizeToReportContent = true,
                    Width = Unit.Percentage(100),
                    ProcessingMode = ProcessingMode.Local
                };

                reportViewer.LocalReport.ReportPath = reportPath;

                ReportDataSource rds = new ReportDataSource("DS_AssignReport", reportData);

                reportViewer.LocalReport.DataSources.Add(rds);
                ViewBag.ReportViewer = reportViewer;
                return View();
            }
            catch (Exception ex)
            {

                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "AssignRequisitions", "ReportIndex"));
            }
        }
        public ActionResult PrintDetails(int? id)
        {
            try
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

                //var requestDetails = _assignRequisitionManager.GetAllWithInformation();
                AssignRequisitionViewModel assignRequisitionViewModel = Mapper.Map<AssignRequisitionViewModel>(assignRequisition);

                return View(assignRequisitionViewModel);

            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "AssignRequisitions", "PrintDetails"));
            }
        }
        
        //Get Vehicle Status By Json Result
        public JsonResult GetVehicleStatusByVehicleId(int? vehicleId)
        {
            if (vehicleId == null)
            {
                return null;
            }
            var vehicles = _assignRequisitionManager.GetVehicleStatus((int)vehicleId);


            return Json(vehicles, JsonRequestBehavior.AllowGet);

        }

        //Get Driver Status By Json Result
        public JsonResult GetDriverStatusByDriverId(int? driverId)
        {
            if (driverId == null)
            {
                return null;
            }
            var drivers = _assignRequisitionManager.GetDriverStatus((int)driverId);


            return Json(drivers, JsonRequestBehavior.AllowGet);

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
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _assignRequisitionManager.Dispose();
                _employeeManager.Dispose();
                _requisitionManager.Dispose();
                _vehicleManager.Dispose();
                _vehicleTypeManager.Dispose();
                _requisitionStatusManager.Dispose();
                _mailServiceManager.Dispose();
                _notificationManager.Dispose();
            }
            base.Dispose(disposing);
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

        public AssignRequisitionsController(IRequisitionManager requisitionManager,IVehicleManager vehicleManager,IEmployeeManager employeeManager,IAssignRequisitionManager assignRequisitionManager,IRequisitionStatusManager requisitionStatusManager,
            IVehicleTypeManager vehicleTypeManager, INotificationManager notificationManager, IMailServiceManager mailServiceManager,ICancelRequisitionManager cancelRequisitionManager)
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

                    AssignRequisition assignRequisition = Mapper.Map<AssignRequisition>(assignRequisitionViewModel);
                    bool isSave=_assignRequisitionManager.Add(assignRequisition);

                    if (isSave)
                    {
                        //Requisition status information
                        RequisitionStatus status = new RequisitionStatus();
                        status.Id = assignRequisition.RequisitionStatusId;
                        status.RequisitionId = assignRequisition.RequisitionId;
                        status.RequisitionNumber = assignRequisition.RequisitionNumber;
                        status.StatusType = "Assigned";
                        _requisitionStatusManager.Update(status);
                    }

                    //Notifaication status change after assign requisition
                    Notification notificationUpdate=_notificationManager.FindByRequisitionId(assignRequisition.RequisitionId);
                    if (notificationUpdate!=null) 
                    {
                        notificationUpdate.ControllerViewStatus = "Seen";
                        notificationUpdate.SenderViewStatus = "Unseen";
                        notificationUpdate.SenderText = "Your vehicle has been assigned";
                        notificationUpdate.SenderNotifyDateTime = DateTime.Now;
                        var updateResult = _notificationManager.Update(notificationUpdate);

                        //Sending mail to employee for assigned vehicle
                        if (updateResult)
                        {
                            //Get employee by requisition Id
                            var req = _requisitionManager.FindById(assignRequisition.RequisitionId);

                            //Get Driver by id
                            var driver = _employeeManager.FindById(assignRequisition.EmployeeId);

                            //Get Vehicle Type by id
                            var vehicle = _vehicleManager.FindById(assignRequisition.VehicleId);

                            //Get controller info
                            var loginUserId = Convert.ToInt32(User.Identity.GetUserId());
                            var controller = _employeeManager.FindByLoginId(loginUserId);

                            //Mail service section
                            var subject = "Assign a vehicle on your requisition no : " + assignRequisition.RequisitionNumber;

                            var msgBody = "Dear " + req.Employee.FullName + "," + Environment.NewLine + "On the basis of your request, assigned a vehicle." + Environment.NewLine + "Your driver is :" +
                                driver.FullName + " Contect No : " + driver.ContactNo + Environment.NewLine + "Vehicle :" + vehicle.VehicleType.Name +
                                " and Reg No : " + vehicle.RegNo + Environment.NewLine + "Regards, " + Environment.NewLine + controller.FullName;

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
                                try
                                {
                                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                                    smtpClient.Credentials = new NetworkCredential("demowork9999@gmail.com", "~Aa123456");
                                    smtpClient.EnableSsl = true;


                                    MailMessage mailMessage = new MailMessage();
                                    mailMessage.From = new MailAddress("demowork9999@gmail.com");
                                    mailMessage.To.Add(mailService.To);
                                    mailMessage.Subject = mailService.Subject;
                                    mailMessage.Body = mailService.Body;
                                    smtpClient.Send(mailMessage);

                                    TempData["msg"] = "Vehicle assigned and mail send successfully";

                                    return RedirectToAction("PrintDetails", "AssignRequisitions", new { id = assignRequisition.Id });
                                }
                                catch (Exception ex)
                                {
                                    TempData["msg1"] = "Vehicle assigned and notification send successfully. Mail send failed. The error message is -" + "<br/>" + " [ " + ex.Message + " Helpline" + " ] ";

                                    return RedirectToAction("Index");
                                }

                            }
                        }
                    }
                       
                        TempData["msg1"] = "Vehicle assigned but mail and notification send failed. Please contact with your service provider or developer! ";

                        return RedirectToAction("Index");
                    
                    
                }

                Requisition requisition = _requisitionManager.FindById(assignRequisitionViewModel.RequisitionId);

                RequisitionViewModel requisitionViewModel = Mapper.Map<RequisitionViewModel>(ViewBag.Requisition = requisition);
                assignRequisitionViewModel.RequisitionId = assignRequisitionViewModel.RequisitionId;
                assignRequisitionViewModel.VehicleTypes = _vehicleTypeManager.GetAll().ToList();


                ViewBag.RequisitionNumber = assignRequisitionViewModel.RequisitionNumber;
                ViewBag.EmployeeId = new SelectList(_employeeManager.GetAllDriver(), "Id", "FullName");
                ViewBag.VehicleId = new SelectListItem[] { new SelectListItem() { Value = "", Text = "Select Vehicle" } };
                ViewBag.RequisitionStatusId = new SelectList(_requisitionStatusManager.GetAllStatusNew(), "Id", "StatusType");
                

                TempData["msg"] = "Assign faield! You have missed to select all field.";

                return View(assignRequisitionViewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "AssignRequisitions", "Create"));
            }
        }

        // GET: AssignRequisitions/Edit/5
        public ActionResult Edit(int? id)
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
                AssignRequisitionViewModel assignRequisitionViewModel =
                    Mapper.Map<AssignRequisitionViewModel>(assignRequisition);
                ViewBag.EmployeeId = new SelectList(_employeeManager.GetAllDriver(), "Id", "FullName");
                ViewBag.VehicleId = new SelectList(_vehicleManager.GetAll(), "Id", "RegNo");
                ViewBag.RequisitionStatusId = new SelectList(_requisitionStatusManager.GetAll(), "Id", "StatusType");
                return View(assignRequisitionViewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "AssignRequisitions", "Edit"));
            }
        }

        // POST: AssignRequisitions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RequisitionId,RequisitionStatusId,RequisitionNumber,VehicleId,DriverId,EmployeeId")] AssignRequisitionViewModel assignRequisitionViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AssignRequisition requisition = Mapper.Map<AssignRequisition>(assignRequisitionViewModel);
                    _assignRequisitionManager.Update(requisition);
                    return RedirectToAction("Index");
                }
                ViewBag.EmployeeId = new SelectList(_employeeManager.GetAllDriver(), "Id", "FullName", assignRequisitionViewModel.EmployeeId);
                ViewBag.RequisitionId = new SelectList(_requisitionManager.GetAll(), "Id", "DestinationPlace", assignRequisitionViewModel.RequisitionId);
                ViewBag.VehicleId = new SelectList(_vehicleManager.GetAll(), "Id", "RegNo", assignRequisitionViewModel.VehicleId);
                return View(assignRequisitionViewModel);
            }
            catch (Exception ex)
            {

                return View("Error", new HandleErrorInfo(ex, "AssignRequisitions", "Edit"));
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
                if (requisition != null)
                {
                    RequisitionViewModel requisitionViewModel = Mapper.Map<RequisitionViewModel>(requisition);

                    return View(requisitionViewModel);
                }
                return View("Error");
            }
            catch (Exception ex)
            {
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
                return View("Error", new HandleErrorInfo(ex, "AssignRequisitions", "OnProcess"));
            }
        }

       

        public ActionResult ReportIndex()
        {
            var reportData = _assignRequisitionManager.GetRequisitionSummaryReport();
            var reportPath=Request.MapPath(Request.ApplicationPath)+ @"\Report\AssignRequisition\AssignRequisitionReportRdlc.rdlc";
            ReportViewer reportViewer=new ReportViewer()
            {
                KeepSessionAlive    = true,
                SizeToReportContent = true,
                Width = Unit.Percentage(100),
                ProcessingMode = ProcessingMode.Local
            };
            
            reportViewer.LocalReport.ReportPath = reportPath;

            ReportDataSource rds=new ReportDataSource("DS_AssignRequisitionSummary", reportData);

            reportViewer.LocalReport.DataSources.Add(rds);
            ViewBag.ReportViewer = reportViewer;
            return View();

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
                return View("Error", new HandleErrorInfo(ex, "AssignRequisitions", "PrintDetails"));
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

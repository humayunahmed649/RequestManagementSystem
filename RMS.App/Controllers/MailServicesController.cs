using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using RMS.App.ViewModels;
using RMS.BLL.Contracts;
using RMS.Models.DatabaseContext;
using RMS.Models.EntityModels;

namespace RMS.App.Controllers
{
    public class MailServicesController : Controller
    {
        private IMailServiceManager _mailServiceManager;
        private IRequisitionManager _requisitionManager;
        private IAssignRequisitionManager _assignRequisitionManager;

        public MailServicesController(IMailServiceManager mailServiceManager,IRequisitionManager requisitionManager,IAssignRequisitionManager assignRequisitionManager)
        {
            this._mailServiceManager = mailServiceManager;
            this._requisitionManager = requisitionManager;
            this._assignRequisitionManager = assignRequisitionManager;
        }

        // GET: MailServices
        public ActionResult Index()
        {
            ICollection<MailService> mailServices = _mailServiceManager.GetAll();
            IEnumerable<MailServiceViewModel> mailServiceViewModels =
                Mapper.Map<IEnumerable<MailServiceViewModel>>(mailServices);
            return View(mailServiceViewModels);
        }

        // GET: MailServices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MailService mailService = _mailServiceManager.FindById((int)id);
            if (mailService == null)
            {
                return HttpNotFound();
            }
            MailServiceViewModel mailServiceViewModel = Mapper.Map<MailServiceViewModel>(mailService);
            return View(mailServiceViewModel);
        }

        // GET: MailServices/Create
        public ActionResult Create()
        {
            ViewBag.RequisitionId = new SelectList(_requisitionManager.GetAllWithEmployee(), "Id", "RequisitionNumber");
            return View();
        }

        // POST: MailServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,To,From,Subject,Body,MailSendingDateTime,RequisitionId")] MailServiceViewModel mailServiceViewModel)
        {
            if (ModelState.IsValid)
            {
                MailService mailService = Mapper.Map<MailService>(mailServiceViewModel);
                _mailServiceManager.Add(mailService);
                return RedirectToAction("Index");
            }

            ViewBag.RequisitionId = new SelectList(_requisitionManager.GetAllWithEmployee(), "Id", "RequisitionNumber", mailServiceViewModel.RequisitionId);
            return View(mailServiceViewModel);
        }

        // GET: MailServices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MailService mailService = _mailServiceManager.FindById((int) id);
            if (mailService == null)
            {
                return HttpNotFound();
            }
            MailServiceViewModel mailServiceViewModel = Mapper.Map<MailServiceViewModel>(mailService);
            ViewBag.RequisitionId = new SelectList(_requisitionManager.GetAllWithEmployee(), "Id", "RequisitionNumber", mailServiceViewModel.RequisitionId);
            return View(mailServiceViewModel);
        }

        // POST: MailServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,To,From,Subject,Body,MailSendingDateTime,RequisitionId")] MailServiceViewModel mailServiceViewModel)
        {
            if (ModelState.IsValid)
            {
                MailService mailService = Mapper.Map<MailService>(mailServiceViewModel);
                _mailServiceManager.Update(mailService);
                return RedirectToAction("Index");
            }
            ViewBag.RequisitionId = new SelectList(_requisitionManager.GetAllWithEmployee(), "Id", "RequisitionNumber", mailServiceViewModel.RequisitionId);
            return View(mailServiceViewModel);
        }

        // GET: MailServices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MailService mailService = _mailServiceManager.FindById((int) id);
            if (mailService == null)
            {
                return HttpNotFound();
            }
            MailServiceViewModel mailServiceViewMode = Mapper.Map<MailServiceViewModel>(mailService);
            return View(mailServiceViewMode);
        }

        // POST: MailServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MailService mailService = _mailServiceManager.FindById(id);
            _mailServiceManager.Remove(mailService);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _mailServiceManager.Dispose();
                _requisitionManager.Dispose();
                _assignRequisitionManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

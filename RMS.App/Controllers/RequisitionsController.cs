using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RMS.BLL.Contracts;
using RMS.Models.DatabaseContext;
using RMS.Models.EntityModels;

namespace RMS.App.Controllers
{
    public class RequisitionsController : Controller
    {
        private IRequisitionManager _requisitionManager;
        private IEmployeeManager _employeeManager;

        public RequisitionsController(IRequisitionManager requisitionManager,IEmployeeManager employeeManager)
        {
            this._requisitionManager = requisitionManager;
            this._employeeManager = employeeManager;
        }

        // GET: Requisitions
        public ActionResult Index()
        {
            var requisitions = _requisitionManager.GetAll();
            return View(requisitions.ToList());
        }

        // GET: Requisitions/Details/5
        public ActionResult Details(int? id)
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
            return View(requisition);
        }

        // GET: Requisitions/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(_employeeManager.GetAll(), "Id", "FullName");
            return View();
        }

        // POST: Requisitions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FromPlace,DestinationPlace,StartDateTime,EndDateTime,Description,RequestFor,EmployeeId")] Requisition requisition)
        {
            if (ModelState.IsValid)
            {
                _requisitionManager.Add(requisition);
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(_employeeManager.GetAll(), "Id", "FullName", requisition.EmployeeId);
            return View(requisition);
        }

        // GET: Requisitions/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.EmployeeId = new SelectList(_employeeManager.GetAll(), "Id", "FullName", requisition.EmployeeId);
            return View(requisition);
        }

        // POST: Requisitions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FromPlace,DestinationPlace,StartDateTime,EndDateTime,Description,RequestFor,EmployeeId")] Requisition requisition)
        {
            if (ModelState.IsValid)
            {
                _requisitionManager.Update(requisition);
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(_employeeManager.GetAll(), "Id", "FullName", requisition.EmployeeId);
            return View(requisition);
        }

        // GET: Requisitions/Delete/5
        public ActionResult Delete(int? id)
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
            return View(requisition);
        }

        // POST: Requisitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Requisition requisition = _requisitionManager.FindById((int)id);
            _requisitionManager.Remove(requisition);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _requisitionManager.Dispose();
                _employeeManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

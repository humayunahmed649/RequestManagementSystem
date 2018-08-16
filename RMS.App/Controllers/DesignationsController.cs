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
    public class DesignationsController : Controller
    {
        private IDesignationManager _designationManager;

        public DesignationsController(IDesignationManager manager)
        {
            this._designationManager = manager;
        }

        // GET: Designations
        public ActionResult Index(string searchText)
        {
            if (searchText != null)
            {
                ICollection<Designation> designations =_designationManager.SearchByText(searchText);
                IEnumerable<DesignationViewModel> designationViewModel = Mapper.Map<IEnumerable<DesignationViewModel>>(designations);
                return View(designationViewModel);
            }
            else
            {

                ICollection<Designation> designation = _designationManager.GetAll();
                IEnumerable<DesignationViewModel> designationViewModels = Mapper.Map<IEnumerable<DesignationViewModel>>(designation);
                return View(designationViewModels);
            }
        }

        // GET: Designations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Designation designation = _designationManager.FindById((int) id);
            if (designation == null)
            {
                return HttpNotFound();
            }
            DesignationViewModel designationViewModel = Mapper.Map<DesignationViewModel>(designation);
            return View(designationViewModel);
        }

        // GET: Designations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Designations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title")] DesignationViewModel designationViewModel)
        {
            if (ModelState.IsValid)
            {
                Designation designation = Mapper.Map<Designation>(designationViewModel);
                _designationManager.Add(designation);
                TempData["msg"] = "Information has been saved successfully";
                return RedirectToAction("Index");
            }

            return View(designationViewModel);
        }

        // GET: Designations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Designation designation = _designationManager.FindById((int)id);
            if (designation == null)
            {
                return HttpNotFound();
            }
            DesignationViewModel designationViewModel = Mapper.Map<DesignationViewModel>(designation);
            return View(designationViewModel);
        }

        // POST: Designations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title")] DesignationViewModel designationViewModel)
        {
            if (ModelState.IsValid)
            {
                Designation designation = Mapper.Map<Designation>(designationViewModel);
                _designationManager.Update(designation);
                TempData["msg"] = "Information has been updated successfully";
                return RedirectToAction("Index");
            }
            return View(designationViewModel);
        }

        // GET: Designations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Designation designation = _designationManager.FindById((int)id);
            if (designation == null)
            {
                return HttpNotFound();
            }
            DesignationViewModel designationViewModel = Mapper.Map<DesignationViewModel>(designation);
            return View(designationViewModel);
        }

        // POST: Designations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Designation designation = _designationManager.FindById((int)id);
            _designationManager.Remove(designation);
            TempData["msg"] = "Information has been deleted successfully";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _designationManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

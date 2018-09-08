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
            try
            {
                if (searchText != null)
                {
                    ICollection<Designation> designations = _designationManager.SearchByText(searchText);
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
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Designations", "Index"));
            }
        }

        // GET: Designations/Details/5
        public ActionResult Details(int? id)
        {
            try
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
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Designations", "Details"));
            }
        }

        // GET: Designations/Create
        public ActionResult Create()
        {
            try
            {

                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Designations", "Create"));
            }
        }

        // POST: Designations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title")] DesignationViewModel designationViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Designation designation = Mapper.Map<Designation>(designationViewModel);

                    var title = designation.Title.Trim();
                    if (_designationManager.GetAll().Count(o => o.Title == title) > 0)
                    {
                        ViewBag.Message = "Designation title already exist.";
                    }
                    if (ViewBag.Message == null)
                    {
                        _designationManager.Add(designation);
                        TempData["msg"] = "Information has been saved successfully";
                        return RedirectToAction("Index");
                    }
                }

                return View(designationViewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Designations", "Create"));
            }
        }

        // GET: Designations/Edit/5
        public ActionResult Edit(int? id)
        {
            try
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
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Designations", "Edit"));
            }
        }

        // POST: Designations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title")] DesignationViewModel designationViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Designation designation = Mapper.Map<Designation>(designationViewModel);
                    var title = designation.Title.Trim();
                    if (_designationManager.GetAll().Count(o => o.Title == title && o.Id != designation.Id) > 0)
                    {
                        ViewBag.Message = "Designation title already exist.";
                    }
                    if (ViewBag.Message == null)
                    {
                        _designationManager.Update(designation);
                        TempData["msg"] = "Information has been updated successfully";
                        return RedirectToAction("Index");
                    }
                }
                return View(designationViewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Designations", "Edit"));
            }
        }

        // GET: Designations/Delete/5
        public ActionResult Delete(int? id)
        {
            try
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
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Designations", "Delete"));
            }
        }

        // POST: Designations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Designation designation = _designationManager.FindById((int)id);
                _designationManager.Remove(designation);
                TempData["msg"] = "Information has been deleted successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Designations", "Delete"));
            }
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

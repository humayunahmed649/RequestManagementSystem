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
    public class EmployeeTypesController : Controller
    {
        private IEmployeeTypeManager _employeeTypeManager;

        public EmployeeTypesController(IEmployeeTypeManager employeeTypeManager)
        {
            this._employeeTypeManager = employeeTypeManager;
        }
        // GET: EmployeeTypes
        public ActionResult Index(string searchText)
        {
            if (searchText != null)
            {
                ICollection<EmployeeType> employeeTypes = _employeeTypeManager.SearchByText(searchText);
                IEnumerable<EmployeeTypeViewModel> employeeTypeViewModels = Mapper.Map<IEnumerable<EmployeeTypeViewModel>>(employeeTypes);
                return View(employeeTypeViewModels);
            }
            else
            {
                ICollection<EmployeeType> employeeTypes = _employeeTypeManager.GetAll();
                IEnumerable<EmployeeTypeViewModel> employeeTypeViewModels = Mapper.Map<IEnumerable<EmployeeTypeViewModel>>(employeeTypes);
                return View(employeeTypeViewModels);
            }
        }

        // GET: EmployeeTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeType employeeType = _employeeTypeManager.FindById((int)id);
            if (employeeType == null)
            {
                return HttpNotFound();
            }
            EmployeeTypeViewModel employeeTypeViewModel = Mapper.Map<EmployeeTypeViewModel>(employeeType);
            return View(employeeTypeViewModel);
        }

        // GET: EmployeeTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Type")] EmployeeTypeViewModel employeeTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                EmployeeType employeeType = Mapper.Map<EmployeeType>(employeeTypeViewModel);

                if (_employeeTypeManager.GetAll().Count(o => o.Type == employeeType.Type) > 0)
                {
                    ViewBag.Message = "Employee type name already exist.";
                }
                if (ViewBag.Message==null) 
                {
                    _employeeTypeManager.Add(employeeType);
                    TempData["msg"] = "Information has been saved successfully";
                    return RedirectToAction("Index");
                }
            }

            return View(employeeTypeViewModel);
        }

        // GET: EmployeeTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeType employeeType = _employeeTypeManager.FindById((int) id);
            if (employeeType == null)
            {
                return HttpNotFound();
            }
            EmployeeTypeViewModel employeeTypeViewModel = Mapper.Map<EmployeeTypeViewModel>(employeeType);
            return View(employeeTypeViewModel);
        }

        // POST: EmployeeTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Type")] EmployeeTypeViewModel employeeTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                EmployeeType employeeType = Mapper.Map<EmployeeType>(employeeTypeViewModel);

                if (_employeeTypeManager.GetAll().Count(o => o.Type == employeeType.Type && o.Id != employeeType.Id) > 0)
                {
                    ViewBag.Message = "Employee type name already exist.";
                }
                if (ViewBag.Message==null)
                {
                    _employeeTypeManager.Update(employeeType);
                    TempData["msg"] = "Information has been updated successfully";
                    return RedirectToAction("Index");
                }
            }
            return View(employeeTypeViewModel);
        }

        // GET: EmployeeTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeType employeeType = _employeeTypeManager.FindById((int) id);
            if (employeeType == null)
            {
                return HttpNotFound();
            }
            EmployeeTypeViewModel employeeTypeViewModel = Mapper.Map<EmployeeTypeViewModel>(employeeType);
            return View(employeeTypeViewModel);
        }

        // POST: EmployeeTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeType employeeType = _employeeTypeManager.FindById(id);
            _employeeTypeManager.Remove(employeeType);
            TempData["msg"] = "Information has been deleted successfully";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
               _employeeTypeManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

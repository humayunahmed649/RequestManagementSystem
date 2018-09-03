using System;
using System.Collections;
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
    public class DepartmentsController : Controller
    {
        private IOrganizationManager _organizationManager;
        private IDepartmentManager _departmentManager;

        public DepartmentsController(IOrganizationManager organizationManager, IDepartmentManager departmentManager)
        {
            this._organizationManager = organizationManager;
            this._departmentManager = departmentManager;
        }

        // GET: Departments
        public ActionResult Index(string searchText)
        {
            if (searchText != null)
            {
                ICollection<Department> departments = _departmentManager.SearchByText(searchText);
                IEnumerable<DepartmentViewModel> departmentViewModels= Mapper.Map<IEnumerable<DepartmentViewModel>>(departments);
                return View(departmentViewModels);
            }
            else
            {
                ICollection<Department> department = (ICollection<Department>) _departmentManager.GetAll();
                IEnumerable<DepartmentViewModel> departmentViewModes = Mapper.Map<IEnumerable<DepartmentViewModel>>(department);
                return View(departmentViewModes);
            }
            
            
        }

        // GET: Departments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = _departmentManager.FindById((int)id);
            if (department == null)
            {
                return HttpNotFound();
            }
            DepartmentViewModel departmentViewMode = Mapper.Map<DepartmentViewModel>(department);
            return View(departmentViewMode);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            ViewBag.OrganizationId = new SelectList(_organizationManager.GetAll(), "Id", "Name");
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Code,OrganizationId")] DepartmentViewModel departmentViewMode)
        {
            if (ModelState.IsValid)
            {
                Department department = Mapper.Map<Department>(departmentViewMode);

                var name = department.Name.Trim();
                var code = department.Code.Trim();

                if (_departmentManager.GetAll().Count(o => o.Name ==name) > 0)
                {
                    ViewBag.Message1 = "Department name already exist.";
                }
                
                if (_departmentManager.GetAll().Count(o => o.Code == code) > 0)
                {
                    ViewBag.Message2 = "Department code already exist.";
                }
                if (ViewBag.Message1==null && ViewBag.Message2==null) 
                {
                    _departmentManager.Add(department);
                    TempData["msg"] = "Information has been saved successfully";
                    return RedirectToAction("Index");
                }
            }

            ViewBag.OrganizationId = new SelectList(_organizationManager.GetAll(), "Id", "Name", departmentViewMode.OrganizationId);
            return View(departmentViewMode);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = _departmentManager.FindById((int)id);
            if (department == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrganizationId = new SelectList(_organizationManager.GetAll(), "Id", "Name", department.OrganizationId);
            DepartmentViewModel departmentViewMode = Mapper.Map<DepartmentViewModel>(department);
            return View(departmentViewMode);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Code,OrganizationId")] DepartmentViewModel departmentViewModel)
        {
            if (ModelState.IsValid)
            {
                Department department = Mapper.Map<Department>(departmentViewModel);

                var name = department.Name.Trim();
                var code = department.Code.Trim();
                if (_departmentManager.GetAll().Count(o => o.Name == name && o.Id != department.Id) > 0)
                {
                    ViewBag.Message1 = "Department name already exist.";
                }
                if (_departmentManager.GetAll().Count(o => o.Code == code && o.Id != department.Id) > 0)
                {
                    ViewBag.Message2 = "Department code already exist.";
                }
                if (ViewBag.Message1==null && ViewBag.Message2==null) 
                {
                    _departmentManager.Update(department);
                    TempData["msg"] = "Information has been updated successfully";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.OrganizationId = new SelectList(_organizationManager.GetAll(), "Id", "Name", departmentViewModel.OrganizationId);
            return View(departmentViewModel);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = _departmentManager.FindById((int)id);
            if (department == null)
            {
                return HttpNotFound();
            }
            DepartmentViewModel departmentViewMode = Mapper.Map<DepartmentViewModel>(department);
            return View(departmentViewMode);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department department = _departmentManager.FindById((int)id);
            _departmentManager.Remove(department);
            TempData["msg"] = "Information has been deleted successfully";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _departmentManager.Dispose();
                _organizationManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

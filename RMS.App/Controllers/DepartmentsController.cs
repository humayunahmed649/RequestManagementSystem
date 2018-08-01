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
        public ActionResult Index()
        {
            //var departments = db.Departments.Include(d => d.Organization);
            return View(_departmentManager.GetAll());
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
            return View(department);
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
        public ActionResult Create([Bind(Include = "Id,Name,Code,OrganizationId")] Department department)
        {
            if (ModelState.IsValid)
            {
                _departmentManager.Add(department);
                return RedirectToAction("Index");
            }

            ViewBag.OrganizationId = new SelectList(_organizationManager.GetAll(), "Id", "Name", department.OrganizationId);
            return View(department);
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
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Code,OrganizationId")] Department department)
        {
            if (ModelState.IsValid)
            {
                _departmentManager.Update(department);
                return RedirectToAction("Index");
            }
            ViewBag.OrganizationId = new SelectList(_organizationManager.GetAll(), "Id", "Name", department.OrganizationId);
            return View(department);
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
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department department = _departmentManager.FindById((int)id);
            _departmentManager.Remove(department);
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

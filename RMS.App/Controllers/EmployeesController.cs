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
    public class EmployeesController : Controller
    {
        private IEmployeeManager _employeeManager;
        private IDesignationManager _designationManager;
        private IOrganizationManager _organizationManager;
        private IDepartmentManager _departmentManager;

        public EmployeesController(IEmployeeManager employeeManager, IDepartmentManager departmentManager,
            IDesignationManager designationManager, IOrganizationManager organizationManager)
        {
            this._employeeManager = employeeManager;
            this._departmentManager = departmentManager;
            this._designationManager = designationManager;
            this._organizationManager = organizationManager;
        }

        // GET: Employees
        public ActionResult Index()
        {
            //var employees = db.Employees.Include(e => e.Department).Include(e => e.Designation).Include(e => e.Organization);
            return View(_employeeManager.GetAll());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _employeeManager.FindById((int)id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(_departmentManager.GetAll(), "Id", "Name");
            ViewBag.DesignationId = new SelectList(_designationManager.GetAll(), "Id", "Title");
            ViewBag.OrganizationId = new SelectList(_organizationManager.GetAll(), "Id", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FullName,Email,ContactNo,NID,BloodGroup,OrganizationId,DepartmentId,DesignationId,HouseNo,RoadNo,FloorNo,PostOffice,District,Division")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeManager.Add(employee);
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(_departmentManager.GetAll(), "Id", "Name", employee.DepartmentId);
            ViewBag.DesignationId = new SelectList(_designationManager.GetAll(), "Id", "Title", employee.DesignationId);
            ViewBag.OrganizationId = new SelectList(_organizationManager.GetAll(), "Id", "Name", employee.OrganizationId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _employeeManager.FindById((int)id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(_departmentManager.GetAll(), "Id", "Name", employee.DepartmentId);
            ViewBag.DesignationId = new SelectList(_designationManager.GetAll(), "Id", "Title", employee.DesignationId);
            ViewBag.OrganizationId = new SelectList(_organizationManager.GetAll(), "Id", "Name", employee.OrganizationId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FullName,Email,ContactNo,NID,BloodGroup,OrganizationId,DepartmentId,DesignationId,HouseNo,RoadNo,FloorNo,PostOffice,District,Division")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeManager.Update(employee);
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(_departmentManager.GetAll(), "Id", "Name", employee.DepartmentId);
            ViewBag.DesignationId = new SelectList(_designationManager.GetAll(), "Id", "Title", employee.DesignationId);
            ViewBag.OrganizationId = new SelectList(_organizationManager.GetAll(), "Id", "Name", employee.OrganizationId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _employeeManager.FindById((int)id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = _employeeManager.FindById((int)id);
            _employeeManager.Remove(employee);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _employeeManager.Dispose();
                _departmentManager.Dispose();
                _designationManager.Dispose();
                _organizationManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

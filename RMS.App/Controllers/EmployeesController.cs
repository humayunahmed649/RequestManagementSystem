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
        public ActionResult Index(string searchTextEmpName)
        {
            //if(searchTextEmpName !=null)
            //{
            //    return View(_employeeManager.SearchByName(searchTextEmpName));
            //}
            //else
            //{

            //    return View(_employeeManager.GetAll());
            //}
            //var employees = db.Employees.Include(e => e.Department).Include(e => e.Designation).Include(e => e.Organization);
            ICollection<Employee> employee = _employeeManager.GetAll();
            IEnumerable<EmployeeViewModel> employeeViewModels = Mapper.Map<IEnumerable<EmployeeViewModel>>(employee);
            return View(employeeViewModels);
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
            EmployeeViewModel employeeViewModel = Mapper.Map<EmployeeViewModel>(employee);
            return View(employeeViewModel);
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
        public ActionResult Create([Bind(Include = "Id,FullName,Email,ContactNo,NID,BloodGroup,OrganizationId,DepartmentId,DesignationId,HouseNo,RoadNo,FloorNo,PostOffice,District,Division")] EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                Employee employee = Mapper.Map<Employee>(employeeViewModel);
                _employeeManager.Add(employee);
                TempData["msg"] = "Information has been saved successfully";
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(_departmentManager.GetAll(), "Id", "Name", employeeViewModel.DepartmentId);
            ViewBag.DesignationId = new SelectList(_designationManager.GetAll(), "Id", "Title", employeeViewModel.DesignationId);
            ViewBag.OrganizationId = new SelectList(_organizationManager.GetAll(), "Id", "Name", employeeViewModel.OrganizationId);
            return View(employeeViewModel);
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
            EmployeeViewModel employeeViewModel = Mapper.Map<EmployeeViewModel>(employee);
            return View(employeeViewModel);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FullName,Email,ContactNo,NID,BloodGroup,OrganizationId,DepartmentId,DesignationId,HouseNo,RoadNo,FloorNo,PostOffice,District,Division")] EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                Employee employee = Mapper.Map<Employee>(employeeViewModel);
                _employeeManager.Update(employee);
                TempData["msg"] = "Information has been updated successfully";
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(_departmentManager.GetAll(), "Id", "Name", employeeViewModel.DepartmentId);
            ViewBag.DesignationId = new SelectList(_designationManager.GetAll(), "Id", "Title", employeeViewModel.DesignationId);
            ViewBag.OrganizationId = new SelectList(_organizationManager.GetAll(), "Id", "Name", employeeViewModel.OrganizationId);
            return View(employeeViewModel);
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
            EmployeeViewModel employeeViewModel = Mapper.Map<EmployeeViewModel>(employee);
            return View(employeeViewModel);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = _employeeManager.FindById((int)id);
            _employeeManager.Remove(employee);
            TempData["msg"] = "Information has been deleted successfully";
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

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
    public class EmployeesController : Controller
    {
        private IEmployeeManager _employeeManager;
        private IDesignationManager _designationManager;
        private IOrganizationManager _organizationManager;
        private IDepartmentManager _departmentManager;
        private IEmployeeTypeManager _employeeTypeManager;
        private IDivisionManager _divisionManager;
        private IDistrictManager _districtManager;
        private IUpazilaManager _upazilaManager;

        public EmployeesController(IEmployeeManager employeeManager, IDepartmentManager departmentManager,
            IDesignationManager designationManager, IOrganizationManager organizationManager,IEmployeeTypeManager employeeTypeManager ,
            IDivisionManager divisionManager, IDistrictManager districtManager, IUpazilaManager upazilaManager)
        {
            this._employeeManager = employeeManager;
            this._departmentManager = departmentManager;
            this._designationManager = designationManager;
            this._organizationManager = organizationManager;
            this._employeeTypeManager = employeeTypeManager;
            this._divisionManager = divisionManager;
            this._districtManager = districtManager;
            this._upazilaManager = upazilaManager;
        }

        // GET: Employees
        public ActionResult Index(string searchText)
        {
            if (searchText != null)
            {
                ICollection<Employee> employees = _employeeManager.SearchByText(searchText);
                IEnumerable<EmployeeViewModel> employeeViewModel = Mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
                return View(employeeViewModel);
            }
            else
            {
                ICollection<Employee> employee = _employeeManager.GetAll();
                IEnumerable<EmployeeViewModel> employeeViewModels = Mapper.Map<IEnumerable<EmployeeViewModel>>(employee);
                return View(employeeViewModels);
            }
            
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
            ViewBag.EmployeeTypeId=new SelectList(_employeeTypeManager.GetAll(),"Id","Type");
            EmployeeViewModel employeeViewModel=new EmployeeViewModel();
            employeeViewModel.DivisionList = (List<Division>) _divisionManager.GetAllDivisions();
            ViewBag.DistrictDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select District" } }, "Value", "Text");
            ViewBag.UpazilaDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select Upazila" } }, "Value", "Text");
            return View(employeeViewModel);
        }
        

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FullName,Email,ContactNo,NID,BloodGroup,OrganizationId,DepartmentId,DesignationId,DrivingLicence,EmployeeTypeId,Addresses")] EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                Employee employee = Mapper.Map<Employee>(employeeViewModel);

                var email = employee.Email.Trim();
                if (_employeeManager.GetAll().Count(o => o.Email == email) > 0)
                {
                    ViewBag.Message1 = "Employee email already exist.";
                    ViewBag.DepartmentId = new SelectList(_departmentManager.GetAll(), "Id", "Name");
                    ViewBag.DesignationId = new SelectList(_designationManager.GetAll(), "Id", "Title");
                    ViewBag.OrganizationId = new SelectList(_organizationManager.GetAll(), "Id", "Name");
                    ViewBag.EmployeeTypeId = new SelectList(_employeeTypeManager.GetAll(), "Id", "Type");
                    employeeViewModel.DivisionList = (List<Division>)_divisionManager.GetAllDivisions();
                    ViewBag.DistrictDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select District" } }, "Value", "Text");
                    ViewBag.UpazilaDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select Upazila" } }, "Value", "Text");
                    return View(employeeViewModel);
                }
                var contactNo = employee.ContactNo.Trim();
                if (_employeeManager.GetAll().Count(o => o.ContactNo == contactNo) > 0)
                {
                    ViewBag.Message2 = "Employee contact no already exist.";
                    ViewBag.DepartmentId = new SelectList(_departmentManager.GetAll(), "Id", "Name");
                    ViewBag.DesignationId = new SelectList(_designationManager.GetAll(), "Id", "Title");
                    ViewBag.OrganizationId = new SelectList(_organizationManager.GetAll(), "Id", "Name");
                    ViewBag.EmployeeTypeId = new SelectList(_employeeTypeManager.GetAll(), "Id", "Type");
                    employeeViewModel.DivisionList = (List<Division>)_divisionManager.GetAllDivisions();
                    ViewBag.DistrictDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select District" } }, "Value", "Text");
                    ViewBag.UpazilaDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select Upazila" } }, "Value", "Text");
                    return View(employeeViewModel);
                }
                var nid = employee.NID.Trim();
                if (_employeeManager.GetAll().Count(o => o.NID == nid) > 0)
                {
                    ViewBag.Message3 = "Employee NID already exist.";
                    ViewBag.DepartmentId = new SelectList(_departmentManager.GetAll(), "Id", "Name");
                    ViewBag.DesignationId = new SelectList(_designationManager.GetAll(), "Id", "Title");
                    ViewBag.OrganizationId = new SelectList(_organizationManager.GetAll(), "Id", "Name");
                    ViewBag.EmployeeTypeId = new SelectList(_employeeTypeManager.GetAll(), "Id", "Type");
                    employeeViewModel.DivisionList = (List<Division>)_divisionManager.GetAllDivisions();
                    ViewBag.DistrictDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select District" } }, "Value", "Text");
                    ViewBag.UpazilaDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select Upazila" } }, "Value", "Text");
                    return View(employeeViewModel);
                }
                var drivingLicence = employee.DrivingLicence.Trim();
                if (_employeeManager.GetAll().Count(o=>o.DrivingLicence==drivingLicence)>0) 
                {
                    ViewBag.Message4 = "Employee driving licence no already exist.";
                    ViewBag.DepartmentId = new SelectList(_departmentManager.GetAll(), "Id", "Name");
                    ViewBag.DesignationId = new SelectList(_designationManager.GetAll(), "Id", "Title");
                    ViewBag.OrganizationId = new SelectList(_organizationManager.GetAll(), "Id", "Name");
                    ViewBag.EmployeeTypeId = new SelectList(_employeeTypeManager.GetAll(), "Id", "Type");
                    employeeViewModel.DivisionList = (List<Division>)_divisionManager.GetAllDivisions();
                    ViewBag.DistrictDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select District" } }, "Value", "Text");
                    ViewBag.UpazilaDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select Upazila" } }, "Value", "Text");
                    return View(employeeViewModel);
                }

                _employeeManager.Add(employee);
                TempData["msg"] = "Information has been saved successfully";
                return RedirectToAction("Index");
            }

            TempData["msg"] = "Please Check Your Information! You have missed to give some information.";
            ViewBag.DepartmentId = new SelectList(_departmentManager.GetAll(), "Id", "Name", employeeViewModel.DepartmentId);
            ViewBag.DesignationId = new SelectList(_designationManager.GetAll(), "Id", "Title", employeeViewModel.DesignationId);
            ViewBag.OrganizationId = new SelectList(_organizationManager.GetAll(), "Id", "Name", employeeViewModel.OrganizationId);
            ViewBag.EmployeeTypeId = new SelectList(_employeeTypeManager.GetAll(), "Id", "Type",employeeViewModel.EmployeeTypeId);
            employeeViewModel.DivisionList = (List<Division>)_divisionManager.GetAllDivisions();
            ViewBag.DistrictDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select District" } }, "Value", "Text");
            ViewBag.UpazilaDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select Upazila" } }, "Value", "Text");
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
            ViewBag.EmployeeTypeId = new SelectList(_employeeTypeManager.GetAll(), "Id", "Type", employee.EmployeeTypeId);
            EmployeeViewModel employeeViewModel = Mapper.Map<EmployeeViewModel>(employee);
            employeeViewModel.DivisionList = (List<Division>)_divisionManager.GetAllDivisions();
            ViewBag.DistrictDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select District" } }, "Value", "Text");
            ViewBag.UpazilaDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select Upazila" } }, "Value", "Text");
            return View(employeeViewModel);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FullName,Email,ContactNo,NID,BloodGroup,OrganizationId,DepartmentId,DesignationId,DrivingLicence,EmployeeTypeId,Addresses")] EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                Employee employee = Mapper.Map<Employee>(employeeViewModel);

                var email = employee.Email.Trim();
                if (_employeeManager.GetAll().Count(o => o.Email == email && o.Id!=employee.Id) > 0)
                {
                    ViewBag.Message1 = "Employee email already exist.";
                    ViewBag.DepartmentId = new SelectList(_departmentManager.GetAll(), "Id", "Name", employeeViewModel.DepartmentId);
                    ViewBag.DesignationId = new SelectList(_designationManager.GetAll(), "Id", "Title", employeeViewModel.DesignationId);
                    ViewBag.OrganizationId = new SelectList(_organizationManager.GetAll(), "Id", "Name", employeeViewModel.OrganizationId);
                    ViewBag.EmployeeTypeId = new SelectList(_employeeTypeManager.GetAll(), "Id", "Type", employeeViewModel.EmployeeTypeId);
                    employeeViewModel.DivisionList = (List<Division>)_divisionManager.GetAllDivisions();
                    ViewBag.DistrictDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select District" } }, "Value", "Text");
                    ViewBag.UpazilaDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select Upazila" } }, "Value", "Text");
                    return View(employeeViewModel);
                }
                var contactNo = employee.ContactNo.Trim();
                if (_employeeManager.GetAll().Count(o => o.ContactNo == contactNo && o.Id != employee.Id) > 0)
                {
                    ViewBag.Message2 = "Employee contact no already exist.";
                    ViewBag.DepartmentId = new SelectList(_departmentManager.GetAll(), "Id", "Name", employeeViewModel.DepartmentId);
                    ViewBag.DesignationId = new SelectList(_designationManager.GetAll(), "Id", "Title", employeeViewModel.DesignationId);
                    ViewBag.OrganizationId = new SelectList(_organizationManager.GetAll(), "Id", "Name", employeeViewModel.OrganizationId);
                    ViewBag.EmployeeTypeId = new SelectList(_employeeTypeManager.GetAll(), "Id", "Type", employeeViewModel.EmployeeTypeId);
                    employeeViewModel.DivisionList = (List<Division>)_divisionManager.GetAllDivisions();
                    ViewBag.DistrictDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select District" } }, "Value", "Text");
                    ViewBag.UpazilaDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select Upazila" } }, "Value", "Text");
                    return View(employeeViewModel);
                }
                var nid = employee.NID.Trim();
                if (_employeeManager.GetAll().Count(o => o.NID == nid && o.Id != employee.Id) > 0)
                {
                    ViewBag.Message3 = "Employee NID already exist.";
                    ViewBag.DepartmentId = new SelectList(_departmentManager.GetAll(), "Id", "Name", employeeViewModel.DepartmentId);
                    ViewBag.DesignationId = new SelectList(_designationManager.GetAll(), "Id", "Title", employeeViewModel.DesignationId);
                    ViewBag.OrganizationId = new SelectList(_organizationManager.GetAll(), "Id", "Name", employeeViewModel.OrganizationId);
                    ViewBag.EmployeeTypeId = new SelectList(_employeeTypeManager.GetAll(), "Id", "Type", employeeViewModel.EmployeeTypeId);
                    employeeViewModel.DivisionList = (List<Division>)_divisionManager.GetAllDivisions();
                    ViewBag.DistrictDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select District" } }, "Value", "Text");
                    ViewBag.UpazilaDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select Upazila" } }, "Value", "Text");
                    return View(employeeViewModel);
                }
                var drivingLicence = employee.DrivingLicence.Trim();
                if (_employeeManager.GetAll().Count(o => o.DrivingLicence == drivingLicence && o.Id != employee.Id) > 0)
                {
                    ViewBag.Message4 = "Employee driving licence no already exist.";
                    ViewBag.DepartmentId = new SelectList(_departmentManager.GetAll(), "Id", "Name", employeeViewModel.DepartmentId);
                    ViewBag.DesignationId = new SelectList(_designationManager.GetAll(), "Id", "Title", employeeViewModel.DesignationId);
                    ViewBag.OrganizationId = new SelectList(_organizationManager.GetAll(), "Id", "Name", employeeViewModel.OrganizationId);
                    ViewBag.EmployeeTypeId = new SelectList(_employeeTypeManager.GetAll(), "Id", "Type", employeeViewModel.EmployeeTypeId);
                    employeeViewModel.DivisionList = (List<Division>)_divisionManager.GetAllDivisions();
                    ViewBag.DistrictDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select District" } }, "Value", "Text");
                    ViewBag.UpazilaDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select Upazila" } }, "Value", "Text");
                    return View(employeeViewModel);
                }
                _employeeManager.Update(employee);
                TempData["msg"] = "Information has been updated successfully";
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(_departmentManager.GetAll(), "Id", "Name", employeeViewModel.DepartmentId);
            ViewBag.DesignationId = new SelectList(_designationManager.GetAll(), "Id", "Title", employeeViewModel.DesignationId);
            ViewBag.OrganizationId = new SelectList(_organizationManager.GetAll(), "Id", "Name", employeeViewModel.OrganizationId);
            ViewBag.EmployeeTypeId = new SelectList(_employeeTypeManager.GetAll(), "Id", "Type", employeeViewModel.EmployeeTypeId);
            employeeViewModel.DivisionList = (List<Division>)_divisionManager.GetAllDivisions();
            ViewBag.DistrictDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select District" } }, "Value", "Text");
            ViewBag.UpazilaDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select Upazila" } }, "Value", "Text");
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

        public JsonResult GetDistrictsByDivisionId(int? divisionId)
        {
            if (divisionId == null)
            {
                return null;
            }

            var districts = _districtManager.GetDistrictsById((int)divisionId);
            return Json(districts, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUpazilaByDistrictId(int? districtId)
        {
            if (districtId == null)
            {
                return null;
            }

            var upazilas = _upazilaManager.GetUpazilasById((int)districtId);
            return Json(upazilas, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _employeeManager.Dispose();
                _departmentManager.Dispose();
                _designationManager.Dispose();
                _organizationManager.Dispose();
                _employeeTypeManager.Dispose();
                _divisionManager.Dispose();
                _districtManager.Dispose();
                _upazilaManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using RMS.App.ViewModels;
using RMS.BLL.Contracts;
using RMS.Models.DatabaseContext;
using RMS.Models.EntityModels;
using RMS.Models.EntityModels.Identity;
using RMS.Models.Identity.IdentityConfig;

namespace RMS.App.Controllers
{
    [Authorize(Roles = "Controller,Administrator")]
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
        //Account controller 

        private AppUserManager _userManager;
        private AppUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().Get<AppUserManager>();

            }
        }

        // GET: Employees
        public ActionResult Index(string searchText)
        {
            try
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
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Employees", "Index"));
            }
            
        }
        //Get Only Employee
        public ActionResult GetAllEmployee()
        {
            ICollection<Employee> employee = _employeeManager.GetAllEmployees();
            IEnumerable<EmployeeViewModel> employeeViewModels = Mapper.Map<IEnumerable<EmployeeViewModel>>(employee);
            return View(employeeViewModels);
        }

        //Get Only Driver
        public ActionResult GetAllDriver()
        {
            ICollection<Employee> employee = _employeeManager.GetAllDriver();
            IEnumerable<DriverViewModel> driverViewModels = Mapper.Map<IEnumerable<DriverViewModel>>(employee);
            return View(driverViewModels);
        }
        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            try
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
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Employees", "Details"));
            }
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.DepartmentId = new SelectList(_departmentManager.GetAll(), "Id", "Name");
                ViewBag.DesignationId = new SelectList(_designationManager.GetAll(), "Id", "Title");
                ViewBag.OrganizationId = new SelectList(_organizationManager.GetAll(), "Id", "Name");
                ViewBag.EmployeeTypeId = new SelectList(_employeeTypeManager.GetAll(), "Id", "Type",1);
                EmployeeViewModel employeeViewModel = new EmployeeViewModel();
                employeeViewModel.DivisionList = (List<Division>)_divisionManager.GetAllDivisions();
                ViewBag.DistrictDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select District" } }, "Value", "Text");
                ViewBag.UpazilaDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select Upazila" } }, "Value", "Text");
                return View(employeeViewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Employees", "Create"));
            }
        }
        

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include ="Id,FullName,Email,ContactNo,NID,OrganizationId,DepartmentId,DesignationId,EmployeeTypeId,Addresses,Password,ConfirmPassword,IsChecked")] EmployeeViewModel employeeViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if(employeeViewModel.EmployeeTypeId==1 && employeeViewModel.IsChecked==false)
                    {
                        Employee employee = Mapper.Map<Employee>(employeeViewModel);

                        var email = employee.Email.Trim();
                        var contactNo = employee.ContactNo.Trim();
                        var nid = employee.NID.Trim();

                        if (_employeeManager.GetAll().Count(o => o.Email == email) > 0)
                        {
                            ViewBag.Message1 = "Employee email already exist.";
                        }

                        if (_employeeManager.GetAll().Count(o => o.ContactNo == contactNo) > 0)
                        {
                            ViewBag.Message2 = "Employee contact no already exist.";
                        }

                        if (_employeeManager.GetAll().Count(o => o.NID == nid) > 0)
                        {
                            ViewBag.Message3 = "Employee NID already exist.";
                        }
                        if (employee.DrivingLicence != null)
                        {
                            var drivingLicence = employee.DrivingLicence.Trim();
                            if (_employeeManager.GetAll().Count(o => o.DrivingLicence == drivingLicence) > 0)
                            {
                                ViewBag.Message4 = "Employee driving licence no already exist.";
                            }
                        }

                        if (ViewBag.Message1 == null && ViewBag.Message2 == null && ViewBag.Message3 == null &&
                            ViewBag.Message4 == null)
                        {

                            var result = UserManager.AddUserForEmployee(employee);
                            if (result != 0)
                            {
                                employee.AppUserId = result;
                                _employeeManager.Add(employee);
                                TempData["msg"] = "Information has been saved successfully";
                                return RedirectToAction("GetAllEmployee");
                            }

                        }
                    }
                    if (employeeViewModel.EmployeeTypeId == 1 && employeeViewModel.IsChecked == true)
                    {
                        Employee employee = Mapper.Map<Employee>(employeeViewModel);

                        var email = employee.Email.Trim();
                        var contactNo = employee.ContactNo.Trim();
                        var nid = employee.NID.Trim();

                        if (_employeeManager.GetAll().Count(o => o.Email == email) > 0)
                        {
                            ViewBag.Message1 = "Employee email already exist.";
                        }

                        if (_employeeManager.GetAll().Count(o => o.ContactNo == contactNo) > 0)
                        {
                            ViewBag.Message2 = "Employee contact no already exist.";
                        }

                        if (_employeeManager.GetAll().Count(o => o.NID == nid) > 0)
                        {
                            ViewBag.Message3 = "Employee NID already exist.";
                        }
                        if (employee.DrivingLicence != null)
                        {
                            var drivingLicence = employee.DrivingLicence.Trim();
                            if (_employeeManager.GetAll().Count(o => o.DrivingLicence == drivingLicence) > 0)
                            {
                                ViewBag.Message4 = "Employee driving licence no already exist.";
                            }
                        }

                        if (ViewBag.Message1 == null && ViewBag.Message2 == null && ViewBag.Message3 == null &&
                            ViewBag.Message4 == null)
                        {

                            var result = UserManager.AddControllerForEmployee(employee);
                            if (result != 0)
                            {
                                employee.AppUserId = result;
                                _employeeManager.Add(employee);
                                TempData["msg"] = "Information has been saved successfully";
                                return RedirectToAction("GetAllEmployee");
                            }

                        }
                    }
                    }
                  
                 TempData["msg"] = "Please Check Your Information! You have missed to give some information.";
                ViewBag.DepartmentId = new SelectList(_departmentManager.GetAll(), "Id", "Name", employeeViewModel.DepartmentId);
                ViewBag.DesignationId = new SelectList(_designationManager.GetAll(), "Id", "Title", employeeViewModel.DesignationId);
                ViewBag.OrganizationId = new SelectList(_organizationManager.GetAll(), "Id", "Name", employeeViewModel.OrganizationId);
                ViewBag.EmployeeTypeId = new SelectList(_employeeTypeManager.GetAll(), "Id", "Type", employeeViewModel.EmployeeTypeId);
                employeeViewModel.DivisionList = (List<Division>)_divisionManager.GetAllDivisions();
                ViewBag.DistrictDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select District" } }, "Value", "Text");
                ViewBag.UpazilaDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select Upazila" } }, "Value", "Text");
                return View(employeeViewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Employees", "Create"));
            }
         }


        //Get Employee Create (Driver)
        public ActionResult CreateDriver()
        {
            try
            {
                ViewBag.DepartmentId = new SelectList(_departmentManager.GetAll(), "Id", "Name");
                ViewBag.DesignationId = new SelectList(_designationManager.GetAll(), "Id", "Title");
                ViewBag.OrganizationId = new SelectList(_organizationManager.GetAll(), "Id", "Name");
                ViewBag.EmployeeTypeId = new SelectList(_employeeTypeManager.GetAll(), "Id", "Type",2);
                DriverViewModel driverViewModel = new DriverViewModel();
                driverViewModel.DivisionList = (List<Division>)_divisionManager.GetAllDivisions();
                ViewBag.DistrictDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select District" } }, "Value", "Text");
                ViewBag.UpazilaDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select Upazila" } }, "Value", "Text");
                return View(driverViewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Employees", "CreateDriver"));
            }
        }


        // POST: Driver/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDriver([Bind(Include = "Id,FullName,ContactNo,NID,DrivingLicence,BloodGroup,OrganizationId,DepartmentId,DesignationId,EmployeeTypeId,Addresses")] DriverViewModel driverViewModel)
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    if (driverViewModel.EmployeeTypeId == 2)
                    {
                        Employee employee = Mapper.Map<Employee>(driverViewModel);
                        if (employee.Email!=null) 
                        {
                            var email = employee.Email.Trim();
                            if (_employeeManager.GetAll().Count(o => o.Email == email) > 0)
                            {
                                ViewBag.Message1 = "Employee email already exist.";
                            }
                        }
                        
                        var contactNo = employee.ContactNo.Trim();
                        var nid = employee.NID.Trim();
                        var drivingLicence = employee.DrivingLicence.Trim();
                        if (_employeeManager.GetAll().Count(o => o.ContactNo == contactNo) > 0)
                        {
                            ViewBag.Message2 = "Employee contact no already exist.";
                        }

                        if (_employeeManager.GetAll().Count(o => o.NID == nid) > 0)
                        {
                            ViewBag.Message3 = "Employee NID already exist.";
                        }                         
                        if (_employeeManager.GetAll().Count(o => o.DrivingLicence == drivingLicence) > 0)
                        {
                            ViewBag.Message4 = "Employee driving licence no already exist.";
                        }

                        if (ViewBag.Message1 == null && ViewBag.Message2 == null && ViewBag.Message3 == null &&
                            ViewBag.Message4 == null)
                        {
                            _employeeManager.Add(employee);
                            TempData["msg"] = "Information has been saved successfully";
                            return RedirectToAction("GetAllDriver");
                        }

                    }
                }

                TempData["msg"] = "Please Check Your Information! You have missed to give some information.";
                ViewBag.DepartmentId = new SelectList(_departmentManager.GetAll(), "Id", "Name", driverViewModel.DepartmentId);
                ViewBag.DesignationId = new SelectList(_designationManager.GetAll(), "Id", "Title", driverViewModel.DesignationId);
                ViewBag.OrganizationId = new SelectList(_organizationManager.GetAll(), "Id", "Name", driverViewModel.OrganizationId);
                ViewBag.EmployeeTypeId = new SelectList(_employeeTypeManager.GetAll(), "Id", "Type", driverViewModel.EmployeeTypeId);
                driverViewModel.DivisionList = (List<Division>)_divisionManager.GetAllDivisions();
                ViewBag.DistrictDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select District" } }, "Value", "Text");
                ViewBag.UpazilaDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select Upazila" } }, "Value", "Text");
                return View(driverViewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Employees", "CreateDriver"));
            }
        }


        

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            try
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
                EmployeeEditViewModel employeeEditViewModel = Mapper.Map<EmployeeEditViewModel>(employee);
                employeeEditViewModel.DivisionList = (List<Division>)_divisionManager.GetAllDivisions();
                ViewBag.DistrictDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select District" } }, "Value", "Text");
                ViewBag.UpazilaDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select Upazila" } }, "Value", "Text");
                return View(employeeEditViewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Employees", "Edit"));
            }
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FullName,Email,ContactNo,NID,BloodGroup,OrganizationId,DepartmentId,DesignationId,DrivingLicence,EmployeeTypeId,Addresses")] EmployeeEditViewModel employeeEditViewModel)
       {
            try
            {
                if (ModelState.IsValid)
                {
                    Employee employee = Mapper.Map<Employee>(employeeEditViewModel);

                    var email = employee.Email.Trim();
                    var contactNo = employee.ContactNo.Trim();
                    var nid = employee.NID.Trim();

                    if (_employeeManager.GetAll().Count(o => o.Email == email && o.Id != employee.Id) > 0)
                    {
                        ViewBag.Message1 = "Employee email already exist.";
                    }
                    if (_employeeManager.GetAll().Count(o => o.ContactNo == contactNo && o.Id != employee.Id) > 0)
                    {
                        ViewBag.Message2 = "Employee contact no already exist.";
                    }
                    if (_employeeManager.GetAll().Count(o => o.NID == nid && o.Id != employee.Id) > 0)
                    {
                        ViewBag.Message3 = "Employee NID already exist.";
                    }
                    if (employee.DrivingLicence != null)
                    {
                        var drivingLicence = employee.DrivingLicence.Trim();
                        if (_employeeManager.GetAll().Count(o => o.DrivingLicence == drivingLicence && o.Id != employee.Id) > 0)
                        {
                            ViewBag.Message4 = "Employee driving licence no already exist.";
                        }
                    }

                    if (ViewBag.Message1 == null && ViewBag.Message2 == null && ViewBag.Message3 == null && ViewBag.Message4 == null)
                    {
                        _employeeManager.Update(employee);
                        TempData["msg"] = "Information has been updated successfully";
                        return RedirectToAction("GetAllEmployee");
                    }
                }

                TempData["msg"] = "Please Check Your Information! You have missed to give some information.";
                ViewBag.DepartmentId = new SelectList(_departmentManager.GetAll(), "Id", "Name", employeeEditViewModel.DepartmentId);
                ViewBag.DesignationId = new SelectList(_designationManager.GetAll(), "Id", "Title", employeeEditViewModel.DesignationId);
                ViewBag.OrganizationId = new SelectList(_organizationManager.GetAll(), "Id", "Name", employeeEditViewModel.OrganizationId);
                ViewBag.EmployeeTypeId = new SelectList(_employeeTypeManager.GetAll(), "Id", "Type", employeeEditViewModel.EmployeeTypeId);
                employeeEditViewModel.DivisionList = (List<Division>)_divisionManager.GetAllDivisions();
                ViewBag.DistrictDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select District" } }, "Value", "Text");
                ViewBag.UpazilaDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select Upazila" } }, "Value", "Text");
                return View(employeeEditViewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Employees", "Edit"));
            }
        }

        // GET: Driver/Edit/5
        public ActionResult EditDriver(int? id)
        {
            try
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
                DriverViewModel driverViewModel = Mapper.Map<DriverViewModel>(employee);
                driverViewModel.DivisionList = (List<Division>)_divisionManager.GetAllDivisions();
                ViewBag.DistrictDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select District" } }, "Value", "Text");
                ViewBag.UpazilaDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select Upazila" } }, "Value", "Text");
                return View(driverViewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Employees", "EditDriver"));
            }
        }

        // POST: Driver/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDriver([Bind(Include = "Id,FullName,Email,ContactNo,NID,BloodGroup,OrganizationId,DepartmentId,DesignationId,DrivingLicence,EmployeeTypeId,Addresses")] DriverViewModel driverViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Employee employee = Mapper.Map<Employee>(driverViewModel);

                    if (employee.Email != null)
                    {
                        var email = employee.Email.Trim();
                        if (_employeeManager.GetAll().Count(o => o.Email == email && o.Id != employee.Id) > 0)
                        {
                            ViewBag.Message1 = "Employee email already exist.";
                        }
                    }

                    var contactNo = employee.ContactNo.Trim();
                    var nid = employee.NID.Trim();
                    var drivingLicence = employee.DrivingLicence.Trim();

                    if (_employeeManager.GetAll().Count(o => o.ContactNo == contactNo && o.Id != employee.Id) > 0)
                    {
                        ViewBag.Message2 = "Employee contact no already exist.";
                    }
                    if (_employeeManager.GetAll().Count(o => o.NID == nid && o.Id != employee.Id) > 0)
                    {
                        ViewBag.Message3 = "Employee NID already exist.";
                    }   
                    if (_employeeManager.GetAll().Count(o => o.DrivingLicence == drivingLicence && o.Id != employee.Id) > 0)

                        {
                            ViewBag.Message4 = "Employee driving licence no already exist.";
                        }


                    if (ViewBag.Message1 == null && ViewBag.Message2 == null && ViewBag.Message3 == null && ViewBag.Message4 == null)
                    {
                        _employeeManager.Update(employee);
                        TempData["msg"] = "Information has been updated successfully";
                        return RedirectToAction("GetAllDriver");
                    }
                }

                TempData["msg"] = "Please Check Your Information! You have missed to give some information.";
                ViewBag.DepartmentId = new SelectList(_departmentManager.GetAll(), "Id", "Name", driverViewModel.DepartmentId);
                ViewBag.DesignationId = new SelectList(_designationManager.GetAll(), "Id", "Title", driverViewModel.DesignationId);
                ViewBag.OrganizationId = new SelectList(_organizationManager.GetAll(), "Id", "Name", driverViewModel.OrganizationId);
                ViewBag.EmployeeTypeId = new SelectList(_employeeTypeManager.GetAll(), "Id", "Type", driverViewModel.EmployeeTypeId);
                driverViewModel.DivisionList = (List<Division>)_divisionManager.GetAllDivisions();
                ViewBag.DistrictDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select District" } }, "Value", "Text");
                ViewBag.UpazilaDropDown = new SelectList(new[] { new SelectListItem() { Value = "", Text = "Select Upazila" } }, "Value", "Text");
                return View(driverViewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Employees", "EditDriver"));
            }
        }


        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            try
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
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Employees", "Delete"));
            }
            
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Employee employee = _employeeManager.FindById((int)id);
                _employeeManager.Remove(employee);
                TempData["msg"] = "Information has been deleted successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Employees", "Delete"));
            }
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

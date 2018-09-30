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
    [Authorize(Roles = "Controller,Administrator")]
    public class EmployeeTypesController : Controller
    {
        private IEmployeeTypeManager _employeeTypeManager;

        public EmployeeTypesController(IEmployeeTypeManager employeeTypeManager)
        {
            this._employeeTypeManager = employeeTypeManager;
        }
        // GET: EmployeeTypes
        public ActionResult Index()
        {
            try
            {
                    ICollection<EmployeeType> employeeTypes = _employeeTypeManager.GetAll();
                    IEnumerable<EmployeeTypeViewModel> employeeTypeViewModels = Mapper.Map<IEnumerable<EmployeeTypeViewModel>>(employeeTypes);
                    return View(employeeTypeViewModels);

            }
            catch (Exception ex)
            {
                return View("Error",new HandleErrorInfo(ex,"EmployeeTypes","Index"));
            }
        }

        // GET: EmployeeTypes/Details/5
        public ActionResult Details(int? id)
        {
            try
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
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "EmployeeTypes", "Details"));
            }
        }

        // GET: EmployeeTypes/Create
        public ActionResult Create()
        {
            try
            {

                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "EmployeeTypes", "Create"));
            }
        }

        // POST: EmployeeTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Type")] EmployeeTypeViewModel employeeTypeViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmployeeType employeeType = Mapper.Map<EmployeeType>(employeeTypeViewModel);

                    if (_employeeTypeManager.GetAll().Count(o => o.Type == employeeType.Type) > 0)
                    {
                        ViewBag.Message = "Employee type name already exist.";
                    }
                    if (ViewBag.Message == null)
                    {
                        _employeeTypeManager.Add(employeeType);
                        TempData["msg"] = "Information has been saved successfully";
                        return RedirectToAction("Index");
                    }
                }

                return View(employeeTypeViewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "EmployeeTypes", "Create"));
            }
            
        }

        // GET: EmployeeTypes/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    try
        //    {
        //        if (id == null)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }
        //        EmployeeType employeeType = _employeeTypeManager.FindById((int)id);
        //        if (employeeType == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        EmployeeTypeViewModel employeeTypeViewModel = Mapper.Map<EmployeeTypeViewModel>(employeeType);
        //        return View(employeeTypeViewModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        return View("Error", new HandleErrorInfo(ex, "EmployeeTypes", "Edit"));
        //    }
            
        //}

        // POST: EmployeeTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Type")] EmployeeTypeViewModel employeeTypeViewModel)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            EmployeeType employeeType = Mapper.Map<EmployeeType>(employeeTypeViewModel);

        //            if (_employeeTypeManager.GetAll().Count(o => o.Type == employeeType.Type && o.Id != employeeType.Id) > 0)
        //            {
        //                ViewBag.Message = "Employee type name already exist.";
        //            }
        //            if (ViewBag.Message == null)
        //            {
        //                _employeeTypeManager.Update(employeeType);
        //                TempData["msg"] = "Information has been updated successfully";
        //                return RedirectToAction("Index");
        //            }
        //        }
        //        return View(employeeTypeViewModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        return View("Error", new HandleErrorInfo(ex, "EmployeeTypes", "Edit"));
        //    }
            
        //}

        // GET: EmployeeTypes/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    try
        //    {
        //        if (id == null)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }
        //        EmployeeType employeeType = _employeeTypeManager.FindById((int)id);
        //        if (employeeType == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        EmployeeTypeViewModel employeeTypeViewModel = Mapper.Map<EmployeeTypeViewModel>(employeeType);
        //        return View(employeeTypeViewModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        return View("Error", new HandleErrorInfo(ex, "EmployeeTypes", "Delete"));
        //    }
            
        //}

        // POST: EmployeeTypes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    try
        //    {
        //        EmployeeType employeeType = _employeeTypeManager.FindById(id);
        //        _employeeTypeManager.Remove(employeeType);
        //        TempData["msg"] = "Information has been deleted successfully";
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception ex)
        //    {
        //        return View("Error", new HandleErrorInfo(ex, "EmployeeTypes", "Delete"));
        //    }
            
        //}

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

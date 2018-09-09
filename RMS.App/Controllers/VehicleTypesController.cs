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
    public class VehicleTypesController : Controller
    {
        private IVehicleTypeManager _vehicleTypeManager;

        public VehicleTypesController(IVehicleTypeManager manager)
        {
            this._vehicleTypeManager = manager;
        }

        // GET: VehicleTypes
        public ActionResult Index(string searchText)
        {
            try
            {

                if (searchText != null)
                {
                    ICollection<VehicleType> vehicleType = _vehicleTypeManager.SearchByText(searchText);
                    IEnumerable<VehicleTypeViewModel> vehicleTypeViewModels =
                        Mapper.Map<IEnumerable<VehicleTypeViewModel>>(vehicleType);
                    return View(vehicleTypeViewModels);
                }
                else
                {
                    ICollection<VehicleType> vehicleType = _vehicleTypeManager.GetAll();
                    IEnumerable<VehicleTypeViewModel> vehicleTypeViewModels =
                        Mapper.Map<IEnumerable<VehicleTypeViewModel>>(vehicleType);
                    return View(vehicleTypeViewModels);
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "VehicleTypes", "Index"));
            }

        }

        // GET: VehicleTypes/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                VehicleType vehicleType = _vehicleTypeManager.FindById((int)id);
                if (vehicleType == null)
                {
                    return HttpNotFound();
                }
                VehicleTypeViewModel vehicleTypeViewModel = Mapper.Map<VehicleTypeViewModel>(vehicleType);
                return View(vehicleTypeViewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "VehicleTypes", "Details"));
            }
        }

        // GET: VehicleTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] VehicleTypeViewModel vehicleTypeViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    VehicleType vehicleType = Mapper.Map<VehicleType>(vehicleTypeViewModel);

                    var name = vehicleType.Name.Trim();
                    if (_vehicleTypeManager.GetAll().Count(o => o.Name == name) > 0)
                    {
                        ViewBag.Message = "Vehicle type name already exist.";
                    }
                    if (ViewBag.Message == null)
                    {
                        _vehicleTypeManager.Add(vehicleType);
                        TempData["msg"] = "Information has been saved successfully";
                        return RedirectToAction("Index");
                    }
                }

                return View(vehicleTypeViewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "VehicleTypes", "Create"));
            }
        }

        // GET: VehicleTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                VehicleType vehicleType = _vehicleTypeManager.FindById((int)id);
                if (vehicleType == null)
                {
                    return HttpNotFound();
                }
                VehicleTypeViewModel vehicleTypeViewModel = Mapper.Map<VehicleTypeViewModel>(vehicleType);
                return View(vehicleTypeViewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "VehicleTypes", "Edit"));
            }
        }

        // POST: VehicleTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] VehicleTypeViewModel vehicleTypeViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    VehicleType vehicleType = Mapper.Map<VehicleType>(vehicleTypeViewModel);

                    var name = vehicleType.Name.Trim();
                    if (_vehicleTypeManager.GetAll().Count(o => o.Name == name && o.Id != vehicleType.Id) > 0)
                    {
                        ViewBag.Message = "Vehicle type name already exist.";
                    }
                    if (ViewBag.Message == null)
                    {
                        _vehicleTypeManager.Update(vehicleType);
                        TempData["msg"] = "Information has been updated successfully";
                        return RedirectToAction("Index");
                    }
                }
                return View(vehicleTypeViewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "VehicleTypes", "Edit"));
            }
        }

        // GET: VehicleTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                VehicleType vehicleType = _vehicleTypeManager.FindById((int)id);
                if (vehicleType == null)
                {
                    return HttpNotFound();
                }
                VehicleTypeViewModel vehicleTypeViewModel = Mapper.Map<VehicleTypeViewModel>(vehicleType);
                return View(vehicleTypeViewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "VehicleTypes", "Delete"));
            }
        }

        // POST: VehicleTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                VehicleType vehicleType = _vehicleTypeManager.FindById((int)id);
                _vehicleTypeManager.Remove(vehicleType);
                TempData["msg"] = "Information has been deleted successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "VehicleTypes", "Delete"));
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _vehicleTypeManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

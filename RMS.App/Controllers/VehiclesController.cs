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
    [Authorize(Roles= "Controller,Administrator")]
    public class VehiclesController : Controller
    {
        private IVehicleManager _vehicleManager;
        private IVehicleTypeManager _vehicleTypeManager;
        private IRequisitionStatusManager _requisitionStatusManager;
        private IAssignRequisitionManager _assignRequisitionManager;

        public VehiclesController(IVehicleManager manager,IVehicleTypeManager vehicleTypeManager,IRequisitionStatusManager requisitionStatusManager,IAssignRequisitionManager assignRequisitionManager)
        {
            this._vehicleManager = manager;
            this._vehicleTypeManager = vehicleTypeManager;
            this._requisitionStatusManager = requisitionStatusManager;
            this._assignRequisitionManager = assignRequisitionManager;
        }


        // GET: Vehicles
        public ActionResult Index(string searchText)
        {

            try
            {
                if (searchText != null)
                {
                    ICollection<Vehicle> vehicles = _vehicleManager.SearchByText(searchText);
                    IEnumerable<VehicleViewModel> vehicleViewModels = Mapper.Map<IEnumerable<VehicleViewModel>>(vehicles);
                    return View(vehicleViewModels);
                }
                else
                {
                    ICollection<Vehicle> vehicles = _vehicleManager.GetAll();
                    IEnumerable<VehicleViewModel> vehicleViewModels = Mapper.Map<IEnumerable<VehicleViewModel>>(vehicles);
                    return View(vehicleViewModels);
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Vehicles", "Index"));
            }

        }

        // GET: Vehicles/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Vehicle vehicle = _vehicleManager.FindById((int)id);
                if (vehicle == null)
                {
                    return HttpNotFound();
                }
                VehicleViewModel vehicleViewModel = Mapper.Map<VehicleViewModel>(vehicle);
                return View(vehicleViewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Vehicles", "Details"));
            }
        }

        // GET: Vehicles/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.VehicleTypeId = new SelectList(_vehicleTypeManager.GetAll(), "Id", "Name");
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Vehicles", "Create"));
            }
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BrandName,ModelName,RegNo,ChassisNo,SeatCapacity,VehicleTypeId")] VehicleViewModel vehicleViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Vehicle vehicle = Mapper.Map<Vehicle>(vehicleViewModel);

                    var chassisNo = vehicle.ChassisNo.Trim();
                    var regNo = vehicle.RegNo.Trim();
                    if (_vehicleManager.GetAll().Count(o => o.ChassisNo == chassisNo) > 0)
                    {
                        ViewBag.Message1 = "Vehicle chassis no already exist.";
                    }
                    if (_vehicleManager.GetAll().Count(o => o.RegNo == regNo) > 0)
                    {
                        ViewBag.Message2 = "Vehicle registration no already exist.";
                    }
                    if (ViewBag.Message1 == null && ViewBag.Message2 == null)
                    {
                        _vehicleManager.Add(vehicle);
                        TempData["msg"] = "Information has been saved successfully";
                        return RedirectToAction("Index");
                    }
                }

                ViewBag.VehicleTypeId = new SelectList(_vehicleTypeManager.GetAll(), "Id", "Name", vehicleViewModel.VehicleTypeId);
                return View(vehicleViewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Vehicles", "Create"));
            }
        }

        // GET: Vehicles/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Vehicle vehicle = _vehicleManager.FindById((int)id);
                if (vehicle == null)
                {
                    return HttpNotFound();
                }
                ViewBag.VehicleTypeId = new SelectList(_vehicleTypeManager.GetAll(), "Id", "Name", vehicle.VehicleTypeId);
                VehicleViewModel vehicleViewModel = Mapper.Map<VehicleViewModel>(vehicle);
                return View(vehicleViewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Vehicles", "Edit"));
            }
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BrandName,ModelName,RegNo,ChassisNo,SeatCapacity,VehicleTypeId")] VehicleViewModel vehicleViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Vehicle vehicle = Mapper.Map<Vehicle>(vehicleViewModel);

                    var chassisNo = vehicle.ChassisNo.Trim();
                    var regNo = vehicle.RegNo.Trim();
                    if (_vehicleManager.GetAll().Count(o => o.ChassisNo == chassisNo && o.Id != vehicle.Id) > 0)
                    {
                        ViewBag.Message1 = "Vehicle chassis no already exist.";
                    }

                    if (_vehicleManager.GetAll().Count(o => o.RegNo == regNo && o.Id != vehicle.Id) > 0)
                    {
                        ViewBag.Message2 = "Vehicle registration no already exist.";
                    }
                    if (ViewBag.Message1 == null && ViewBag.Message2 == null)
                    {
                        _vehicleManager.Update(vehicle);
                        TempData["msg"] = "Information has been updated successfully";
                        return RedirectToAction("Index");
                    }
                }
                ViewBag.VehicleTypeId = new SelectList(_vehicleTypeManager.GetAll(), "Id", "Name", vehicleViewModel.VehicleTypeId);
                return View(vehicleViewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Vehicles", "Edit"));
            }
        }

        // GET: Vehicles/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Vehicle vehicle = _vehicleManager.FindById((int)id);
                if (vehicle == null)
                {
                    return HttpNotFound();
                }
                VehicleViewModel vehicleViewModel = Mapper.Map<VehicleViewModel>(vehicle);
                return View(vehicleViewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Vehicles", "Delete"));
            }
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Vehicle vehicle = _vehicleManager.FindById((int)id);
                _vehicleManager.Remove(vehicle);
                TempData["msg"] = "Information has been delete successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Vehicles", "Delete"));
            }
        }

        public JsonResult GetByVehicleType(int? vehicleTypeId)
        {
            try
            {

                if (vehicleTypeId == null)
                {
                    return null;
                }

                var vehicles = _vehicleManager.GetAll().Where(c => c.VehicleTypeId == vehicleTypeId).ToList();

                return Json(vehicles, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public JsonResult GetVehicleStatusByVehicleId(int? vehicleId)
        {
            if (vehicleId == null)
            {
                return null;
            }
            var status = _assignRequisitionManager.GetAll().Where(c => c.VehicleId == vehicleId);
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _vehicleManager.Dispose();
                _vehicleTypeManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

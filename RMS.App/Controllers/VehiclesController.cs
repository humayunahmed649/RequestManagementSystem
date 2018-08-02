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
    public class VehiclesController : Controller
    {
        private IVehicleManager _vehicleManager;
        private IVehicleTypeManager _vehicleTypeManager;

        public VehiclesController(IVehicleManager manager,IVehicleTypeManager vehicleTypeManager)
        {
            this._vehicleManager = manager;
            this._vehicleTypeManager = vehicleTypeManager;
        }


        // GET: Vehicles
        public ActionResult Index()
        {
            var vehicles = _vehicleManager.GetAll();
            return View(vehicles.ToList());
        }

        // GET: Vehicles/Details/5
        public ActionResult Details(int? id)
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
            return View(vehicle);
        }

        // GET: Vehicles/Create
        public ActionResult Create()
        {
            ViewBag.VehicleTypeId = new SelectList(_vehicleTypeManager.GetAll(), "Id", "Name");
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BrandName,ModelName,RegNo,ChassisNo,VehicleTypeId")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                _vehicleManager.Add(vehicle);
                return RedirectToAction("Index");
            }

            ViewBag.VehicleTypeId = new SelectList(_vehicleTypeManager.GetAll(), "Id", "Name", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public ActionResult Edit(int? id)
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
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BrandName,ModelName,RegNo,ChassisNo,VehicleTypeId")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                _vehicleManager.Update(vehicle);
                return RedirectToAction("Index");
            }
            ViewBag.VehicleTypeId = new SelectList(_vehicleTypeManager.GetAll(), "Id", "Name", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public ActionResult Delete(int? id)
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
            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vehicle vehicle = _vehicleManager.FindById((int)id);
            _vehicleManager.Remove(vehicle);
            return RedirectToAction("Index");
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

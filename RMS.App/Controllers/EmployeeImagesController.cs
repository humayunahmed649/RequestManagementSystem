using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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
    [Authorize(Roles = "Controller,Administrator,User")]
    public class EmployeeImagesController : Controller
    {
        private IEmployeeImageManager _employeeImageManager;

        public EmployeeImagesController(IEmployeeImageManager employeeImageManager)
        {
            this._employeeImageManager = employeeImageManager;
        }

        // GET: EmployeeImages
        public ActionResult Index()
        {
            ICollection<EmployeeImage> employeeImages = _employeeImageManager.GetAll();
            IEnumerable<EmployeeImageViewModel> employeeImageViewModels =
                Mapper.Map<IEnumerable<EmployeeImageViewModel>>(employeeImages);
            return View(employeeImageViewModels);
        }

        // GET: EmployeeImages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeImage employeeImage = _employeeImageManager.FindById((int) id);
            if (employeeImage == null)
            {
                return HttpNotFound();
            }
            EmployeeImageViewModel employeeImageViewModel = Mapper.Map<EmployeeImageViewModel>(employeeImage);
            return View(employeeImageViewModel);
        }

        // GET: EmployeeImages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ImageName,ImageByte,ImagePath")] EmployeeImageViewModel employeeImageViewModel)
        {
            if (ModelState.IsValid)
            {
                EmployeeImage employeeImage = Mapper.Map<EmployeeImage>(employeeImageViewModel);
                _employeeImageManager.Add(employeeImage);
                return RedirectToAction("Index");
            }

            return View(employeeImageViewModel);
        }

        // GET: EmployeeImages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeImage employeeImage = _employeeImageManager.FindById((int) id);
            if (employeeImage == null)
            {
                return HttpNotFound();
            }
            EmployeeImageViewModel employeeImageViewModel = Mapper.Map<EmployeeImageViewModel>(employeeImage);
            return View(employeeImageViewModel);
        }

        // POST: EmployeeImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ImageName,ImageByte,ImagePath")] EmployeeImageViewModel employeeImageViewModel)
        {
            if (ModelState.IsValid)
            {
                EmployeeImage employeeImage = Mapper.Map<EmployeeImage>(employeeImageViewModel);
                _employeeImageManager.Update(employeeImage);
                return RedirectToAction("Index");
            }
            return View(employeeImageViewModel);
        }

        // GET: EmployeeImages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeImage employeeImage = _employeeImageManager.FindById((int) id);
            if (employeeImage == null)
            {
                return HttpNotFound();
            }
            EmployeeImageViewModel employeeImageViewModel = Mapper.Map<EmployeeImageViewModel>(employeeImage);
            return View(employeeImageViewModel);
        }

        // POST: EmployeeImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeImage employeeImage = _employeeImageManager.FindById(id);
            _employeeImageManager.Remove(employeeImage);
            return RedirectToAction("Index");
        }

        //Delete Image
        public ActionResult DeleteImage(int id)
        {
            EmployeeImage employeeImage = _employeeImageManager.FindById(id);
            _employeeImageManager.Remove(employeeImage);
            return null;
        }

        public JsonResult UploadImage(EmployeeImageViewModel employeeImageViewModel)
        {
            var file = employeeImageViewModel.ImageFile;
            byte[] imageBytes = null;
            int imgId = 0;
            if (file!=null)
            {
                //file.SaveAs(Server.MapPath("/EmployeeProfileImages/" + file.FileName));
                BinaryReader reader=new BinaryReader(file.InputStream);
                imageBytes = reader.ReadBytes(file.ContentLength);

                EmployeeImage employeeImage=new EmployeeImage();
                employeeImage.ImageByte =imageBytes;
                employeeImage.ImageName = file.FileName;
                employeeImage.ImagePath = null; /*"/EmployeeProfileImages/" + file.FileName;*/

                _employeeImageManager.Add(employeeImage);
                imgId = employeeImage.Id;
            }
            return Json(imgId, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RetriveImage(int imgId )
        {
            try
            {
                var empImg = _employeeImageManager.FindById(imgId);
                return File(empImg.ImageByte, "image/jpg");
            }
            catch (Exception ex)
            {

                ExceptionMessage(ex);
                return View("Error", new HandleErrorInfo(ex, "Employees", "Create"));
            }
            
        }

        private void ExceptionMessage(Exception ex)
        {
            ViewBag.ErrorMsg = ex.Message;

            if (ex.InnerException != null)
            {
                ViewBag.ErrorMsg = ex.InnerException.Message;
            }
            if (ex.InnerException?.InnerException != null)
            {
                ViewBag.ErrorMsg = ex.InnerException.InnerException.Message;
            }
            if (ex.InnerException?.InnerException?.InnerException != null)
            {
                ViewBag.ErrorMsg = ex.InnerException.InnerException.InnerException.Message;
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _employeeImageManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using RMS.App.ViewModels;
using RMS.BLL.Contracts;
using RMS.Models.DatabaseContext;
using RMS.Models.EntityModels;

namespace RMS.App.Controllers
{
    public class OrganizationsController : Controller
    {
        private IOrganizationManager _organizationManager;

        public OrganizationsController(IOrganizationManager organizationManager)
        {
            this._organizationManager = organizationManager;
        }

        // GET: Organizations
        public ActionResult Index(string searchText)
        {
            if (searchText != null)
            {
                ICollection<Organization> organization = _organizationManager.SearchByText(searchText);
                IEnumerable<OrganizationViewModel> organizationViewModels = Mapper.Map<IEnumerable<OrganizationViewModel>>(organization);
                return View(organizationViewModels);
            }
            else
            {
                ICollection<Organization> organization = _organizationManager.GetAll();
                IEnumerable<OrganizationViewModel> organizationViewModels = Mapper.Map<IEnumerable<OrganizationViewModel>>(organization);
                return View(organizationViewModels);
            }

            
        }

        // GET: Organizations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = _organizationManager.FindById((int)id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            OrganizationViewModel organizationViewModel = Mapper.Map<OrganizationViewModel>(organization);
            return View(organizationViewModel);
        }

        // GET: Organizations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Organizations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Code,RegNo")] OrganizationViewModel organizationViewModel)
        {
            if (ModelState.IsValid)
            {
                Organization organization = Mapper.Map<Organization>(organizationViewModel);

                var name = organization.Name.Trim();
                var code = organization.Code.Trim();
                var regNo = organization.RegNo.Trim();

                if (_organizationManager.GetAll().Count(o=>o.Name==name)>0)
                {
                    ViewBag.Message1 = "Organization name already exist.";
                }
                if (_organizationManager.GetAll().Count(o => o.Code == code) > 0)
                {
                    ViewBag.Message2 = "Organization code already exist.";
                }
                if (_organizationManager.GetAll().Count(o => o.RegNo == regNo) > 0)
                {
                    ViewBag.Message3 = "Organization registration no already exist.";
                }
                if(ViewBag.Message1==null && ViewBag.Message2==null && ViewBag.Message3 ==null )
                {
                    _organizationManager.Add(organization);

                    TempData["msg"] = "Information has been save successfully";
                    return RedirectToAction("Index");
                }
            }
            
            return View(organizationViewModel);
        }

        // GET: Organizations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = _organizationManager.FindById((int)id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            OrganizationViewModel organizationViewModel = Mapper.Map<OrganizationViewModel>(organization);
            return View(organizationViewModel);
        }

        // POST: Organizations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Code,RegNo")] OrganizationViewModel organizationViewModel)
        {
            if (ModelState.IsValid)
            {
                Organization organization = Mapper.Map<Organization>(organizationViewModel);

                var name = organization.Name.Trim();
                var code = organization.Code.Trim();
                var regNo = organization.RegNo.Trim();

                if (_organizationManager.GetAll().Count(o => o.Name == name && o.Id!=organization.Id) > 0)
                {
                    ViewBag.Message1 = "Organization name already exist.";
                }
                if (_organizationManager.GetAll().Count(o => o.Code == code && o.Id != organization.Id) > 0)
                {
                    ViewBag.Message2 = "Organization code already exist.";
                }
                if (_organizationManager.GetAll().Count(o => o.RegNo == regNo && o.Id != organization.Id) > 0)
                {
                    ViewBag.Message3 = "Organization registration no already exist.";
                }
                if (ViewBag.Message1 == null && ViewBag.Message2 == null && ViewBag.Message3 == null)
                {
                    _organizationManager.Update(organization);
                    TempData["msg"] = "Information has been updated successfully";
                    return RedirectToAction("Index");
                }
            }
            return View(organizationViewModel);
        }

        // GET: Organizations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = _organizationManager.FindById((int)id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            OrganizationViewModel organizationViewModel = Mapper.Map<OrganizationViewModel>(organization);
            return View(organizationViewModel);
        }

        // POST: Organizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Organization organization = _organizationManager.FindById((int)id);
            _organizationManager.Remove(organization);
            TempData["msg"] = "Information has been deleted successfully";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _organizationManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

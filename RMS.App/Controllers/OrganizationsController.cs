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
using System.Linq.Dynamic;

namespace RMS.App.Controllers
{
    [Authorize(Roles = "Controller,Administrator")]
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
            try
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
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Organizations", "Index"));
            }


        }

       
        [HttpPost]
        public ActionResult GetList()
        {
            try
            {

                int start = Convert.ToInt32(Request["start"]);
                int length = Convert.ToInt32(Request["length"]);
                string searchValue = Request["search[value]"];
                string shortingColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
                string sortDirection = Request["order[0][dir]"];

                List<Organization> organizations = (List<Organization>)_organizationManager.GetAll();
                int totalRows = organizations.Count();
                if (!string.IsNullOrEmpty(searchValue))
                {
                    organizations = organizations.Where(x => x.Name.ToLower().Contains(searchValue.ToLower())).ToList<Organization>();
                }
                //Shorting.....
                int dataAfterFiltering = organizations.Count();
                organizations = organizations.OrderBy(shortingColumnName + " " + sortDirection).ToList<Organization>();
                //Paging.....
                organizations = organizations.Skip(start).Take(length).ToList<Organization>();

                return Json(new { data = organizations, draw = Request["draw"], recordsTotal = totalRows, recordsFiltered = dataAfterFiltering }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Organizations", "GetList"));
            }
        }

        // GET: Organizations/Details/5
        public ActionResult Details(int? id)
        {
            try
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
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Organizations", "Details"));
            }
        }

        // GET: Organizations/Create
        public ActionResult Create()
        {
            try
            {

                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Organizations", "Create"));
            }
        }

        // POST: Organizations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] OrganizationViewModel organizationViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Organization organization = Mapper.Map<Organization>(organizationViewModel);

                    var name = organization.Name.Trim();

                    if (_organizationManager.GetAll().Count(o => o.Name == name) > 0)
                    {
                        ViewBag.Message1 = "Organization name already exist.";
                    }
                    if (ViewBag.Message1 == null)
                    {
                        _organizationManager.Add(organization);

                        TempData["msg"] = "Information has been save successfully";
                        return RedirectToAction("Index");
                    }
                }

                return View(organizationViewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Organizations", "Create"));
            }
        }

        // GET: Organizations/Edit/5
        public ActionResult Edit(int? id)
        {
            try
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
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Organizations", "Edit"));
            }
        }

        // POST: Organizations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] OrganizationViewModel organizationViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Organization organization = Mapper.Map<Organization>(organizationViewModel);

                    var name = organization.Name.Trim();

                    if (_organizationManager.GetAll().Count(o => o.Name == name && o.Id != organization.Id) > 0)
                    {
                        ViewBag.Message1 = "Organization name already exist.";
                    }
                    if (ViewBag.Message1 == null)
                    {
                        _organizationManager.Update(organization);
                        TempData["msg"] = "Information has been updated successfully";
                        return RedirectToAction("Index");
                    }
                }
                return View(organizationViewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Organizations", "Edit"));
            }
        }

        // GET: Organizations/Delete/5
        public ActionResult Delete(int? id)
        {
            try
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
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Organizations", "Delete"));
            }
        }

        // POST: Organizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Organization organization = _organizationManager.FindById((int)id);
                _organizationManager.Remove(organization);
                TempData["msg"] = "Information has been deleted successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Organizations", "Delete"));
            }

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

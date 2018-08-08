using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using RMS.App.ViewModels;
using RMS.Models.EntityModels;

namespace RMS.App
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Organization, OrganizationViewModel>();
                cfg.CreateMap<OrganizationViewModel, Organization>();

                cfg.CreateMap<Department, DepartmentViewModel>();
                cfg.CreateMap<DepartmentViewModel, Department>();

                cfg.CreateMap<Designation, DesignationViewModel>();
                cfg.CreateMap<DesignationViewModel, Designation>();

                cfg.CreateMap<Employee, EmployeeViewModel>();
                cfg.CreateMap<EmployeeViewModel, Employee>();

                cfg.CreateMap<VehicleType, VehicleTypeViewModel>();
                cfg.CreateMap<VehicleTypeViewModel, VehicleType>();

                cfg.CreateMap<Vehicle, VehicleViewModel>();
                cfg.CreateMap<VehicleViewModel, Vehicle>();

                cfg.CreateMap<Requisition, RequisitionViewModel>();
                cfg.CreateMap<RequisitionViewModel, Requisition>();
            });
            
        }
    }
}

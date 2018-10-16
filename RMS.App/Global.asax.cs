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
                cfg.CreateMap<Employee, EmployeeEditViewModel>();
                cfg.CreateMap<EmployeeEditViewModel, Employee>();

                cfg.CreateMap<Employee, DriverViewModel>();
                cfg.CreateMap<DriverViewModel, Employee>();

                cfg.CreateMap<Address, AddressViewModel>();
                cfg.CreateMap<AddressViewModel, Address>();

                cfg.CreateMap<VehicleType, VehicleTypeViewModel>();
                cfg.CreateMap<VehicleTypeViewModel, VehicleType>();

                cfg.CreateMap<Vehicle, VehicleViewModel>();
                cfg.CreateMap<VehicleViewModel, Vehicle>();

                cfg.CreateMap<Requisition, RequisitionViewModel>();
                cfg.CreateMap<RequisitionViewModel, Requisition>();

                cfg.CreateMap<AssignRequisition, AssignRequisitionViewModel>();
                cfg.CreateMap<AssignRequisitionViewModel, AssignRequisition>();

                cfg.CreateMap<RequisitionStatus, RequisitionStatusViewModel>();
                cfg.CreateMap<RequisitionStatusViewModel, RequisitionStatus>();

                cfg.CreateMap<Feedback, FeedbackViewModel>();
                cfg.CreateMap<FeedbackViewModel,Feedback>();

                cfg.CreateMap<CancelRequisition, CancelRequisitionViewModel>();
                cfg.CreateMap<CancelRequisitionViewModel, CancelRequisition>();

                cfg.CreateMap<RequisitionHistory, RequisitionHistoryViewModel>();
                cfg.CreateMap<RequisitionHistoryViewModel, RequisitionHistory>();

                cfg.CreateMap<Reply, ReplyViewModel>();
                cfg.CreateMap<ReplyViewModel, Reply>();
            });
            
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["Language"];
            if (cookie != null && cookie.Value != null)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture=new System.Globalization.CultureInfo(cookie.Value);
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(cookie.Value);
            }
            else
            {

                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            }
        }
    }
}

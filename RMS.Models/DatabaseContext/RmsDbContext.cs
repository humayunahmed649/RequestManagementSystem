using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.App.ViewModels.Report;
using RMS.Models.EntityModels;

namespace RMS.Models.DatabaseContext
{
    public class RmsDbContext:DbContext
    {
        public DbSet<Organization> Organizations { get; set; } 
        public DbSet<Department> Departments { get; set; }

        public System.Data.Entity.DbSet<RMS.Models.EntityModels.Designation> Designations { get; set; }

        public System.Data.Entity.DbSet<RMS.Models.EntityModels.Employee> Employees { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public System.Data.Entity.DbSet<RMS.Models.EntityModels.VehicleType> VehicleTypes { get; set; }

        public System.Data.Entity.DbSet<RMS.Models.EntityModels.Vehicle> Vehicles { get; set; }
        public DbSet<Requisition> Requisitions { get; set; }
        public DbSet<AssignRequisition> AssignRequisitions { get; set; } 
        public DbSet<Division> Divisions { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Upazila> Upazilas { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<RequisitionStatus> RequisitionStatuses { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        public IQueryable<RequisitionSummaryReportVM> GetRequisitionSummaryReport()
        {
            var report=Database.SqlQuery<RequisitionSummaryReportVM>("Select * From RequisitionSummary");
            return report.AsQueryable();
        } 
        
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using RMS.App.ViewModels.Report;
using RMS.Models.EntityModels;
using RMS.Models.EntityModels.Identity;

namespace RMS.Models.DatabaseContext
{
    public class RmsDbContext:IdentityDbContext<AppUser,AppRole,int,AppUserLogin,AppUserRole,AppUserClaim>
    {
        public RmsDbContext() : base("RmsDbContext")
        {
            
        }

        public static RmsDbContext Create()
        {
            return new RmsDbContext();
        }

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
        public DbSet<Notification> Notifications { get; set; }
        public System.Data.Entity.DbSet<RMS.Models.EntityModels.MailService> MailServices { get; set; }
        public System.Data.Entity.DbSet<RMS.Models.EntityModels.EmployeeImage> EmployeeImages { get; set; }
        public DbSet<ContactModel> Contacts { get; set; }
        public DbSet<CancelRequisition> CancelRequisitions { get; set; }
        public DbSet<RequisitionHistory> RequisitionHistories { get; set; } 


        public ICollection<AssignRequisitionReportVM> GetAssignRequisitionSummaryReport()
        {
            var report=Database.SqlQuery<AssignRequisitionReportVM>("Select * From AssignRequisitionReport");
            return report.ToList();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AppUser>().ToTable("Users");
            modelBuilder.Entity<AppRole>().ToTable("Roles");
            modelBuilder.Entity<AppUserRole>().ToTable("UserRole");
            modelBuilder.Entity<AppUserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<AppUserClaim>().ToTable("UserClaim");
        }
        
    }
}

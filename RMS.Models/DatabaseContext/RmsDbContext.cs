using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Models.EntityModels;

namespace RMS.Models.DatabaseContext
{
    public class RmsDbContext:DbContext
    {
        public DbSet<Organization> Organizations { get; set; } 
        public DbSet<Department> Departments { get; set; }

        public System.Data.Entity.DbSet<RMS.Models.EntityModels.Designation> Designations { get; set; }

        public System.Data.Entity.DbSet<RMS.Models.EntityModels.Employee> Employees { get; set; }
    }
}

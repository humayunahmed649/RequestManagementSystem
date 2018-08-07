using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Models.DatabaseContext;
using RMS.Models.EntityModels;
using RMS.Repositories.Base;
using RMS.Repositories.Contracts;

namespace RMS.Repositories
{
    public class EmployeeRepository:Repository<Employee>,IEmployeeRepository
    {
        
        public EmployeeRepository(DbContext db) : base(db)
        {
        }

        public override ICollection<Employee> GetAll()
        {
            return db.Set<Employee>().Include(c => c.Department).Include(c=>c.Organization).Include(c=>c.Designation).ToList();
        }

        public ICollection<Employee> SearchByName(string searchTextEmpName)
        {
            return
                db.Set<Employee>()
                    .Include(c => c.Organization)
                    .Include(c => c.Department)
                    .Include(c => c.Designation)
                    .Where(c => c.FullName.StartsWith(searchTextEmpName))
                    .ToList();
        }
    }
}

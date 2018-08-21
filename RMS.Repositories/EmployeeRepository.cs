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
            return db.Set<Employee>().Include(c => c.Department).Include(c=>c.Organization).Include(c=>c.Designation).Include(c=>c.EmployeeType).Include(c=>c.Addresses).ToList();
        }

        public ICollection<Employee> SearchByText(string searchText)
        {
            return
                db.Set<Employee>()
                    .Include(c => c.Organization).Include(c => c.Department).Include(c => c.Designation).Include(c=>c.EmployeeType).Include(c=>c.Addresses)
                    .Where(c => c.FullName.StartsWith(searchText)||c.ContactNo.StartsWith(searchText)||c.NID.StartsWith(searchText)
                    ||c.BloodGroup.StartsWith(searchText)||c.DrivingLicence.StartsWith(searchText)||c.Organization.Name.StartsWith(searchText)
                    ||c.Department.Name.StartsWith(searchText)||c.Designation.Title.StartsWith(searchText)||c.Email.StartsWith(searchText)
                    ||c.EmployeeType.Type.StartsWith(searchText)).ToList();
        }
        public override Employee FindById(int id)
        {
            return db.Set<Employee>().Where(c => c.Id == id).Include(c => c.EmployeeType).Include(c=>c.Organization)
                .Include(c=>c.Department).Include(c=>c.Designation).Include(c=>c.Addresses).SingleOrDefault();
        }
        public ICollection<Employee> GetAllDriver()
        {
            return db.Set<Employee>().Include(c=>c.EmployeeType).Where(c => c.EmployeeType.Type == "Driver").Include(c=>c.Addresses).ToList();
        }
    }
}

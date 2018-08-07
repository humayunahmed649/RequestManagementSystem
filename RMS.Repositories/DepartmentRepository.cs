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
    public class DepartmentRepository:Repository<Department>,IDepartmentRepository
    {
        //public RmsDbContext db
        //{
        //    get
        //    {
        //        return db as RmsDbContext;
        //    }
        //}
        public DepartmentRepository(DbContext db) : base(db)
        {
        }

        public List<Department> SearchByName(string searchText)
        {
            return db.Set<Department>().Include(c=>c.Organization).Where(c => c.Name.StartsWith(searchText)).ToList();
        }

        public override ICollection<Department> GetAll()
        {
            return db.Set<Department>().Include(c => c.Organization).ToList();
        }
    }
}

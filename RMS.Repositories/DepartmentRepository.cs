using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
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

        public ICollection<Department> SearchByText(string searchText)
        {
            return db.Set<Department>().Include(c=>c.Organization).Where(c => c.Name.StartsWith(searchText)||c.Code.StartsWith(searchText)||c.Organization.Name.StartsWith(searchText)).ToList();
        }

        public override ICollection<Department> GetAll()
        {
            return db.Set<Department>().Include(c => c.Organization).AsNoTracking().ToList();
        }
        public override Department FindById(int id)
        {
            return db.Set<Department>().Where(c=>c.Id==id).Include(c=>c.Organization).AsNoTracking().SingleOrDefault();
        }

    }
}
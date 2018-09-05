using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Models.EntityModels;
using RMS.Repositories.Base;
using RMS.Repositories.Contracts;

namespace RMS.Repositories
{
    public class RequisitionRepository:Repository<Requisition>,IRequisitionRepository
    {
        public RequisitionRepository(DbContext db) : base(db)
        {
        }

        public override ICollection<Requisition> GetAll()
        {
            return db.Set<Requisition>().Include(c => c.Employee).AsNoTracking().ToList();
        }

        public ICollection<Requisition> GetAllWithEmployee()
        {
            return db.Set<Requisition>().Include(c => c.Employee).Include(c=>c.Employee.Designation).AsNoTracking().ToList();
        }

        public override Requisition FindById(int id)
        {
            return db.Set<Requisition>().Where(c => c.Id == id).Include(c => c.Employee).AsNoTracking().SingleOrDefault();
        }
        
    }
}

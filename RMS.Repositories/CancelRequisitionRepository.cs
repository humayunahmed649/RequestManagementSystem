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
    public class CancelRequisitionRepository:Repository<CancelRequisition>,ICancelRequisitionRepository
    {
        private RmsDbContext _db;
        public CancelRequisitionRepository(RmsDbContext db) : base(db)
        {
            _db = db;
        }

        public override ICollection<CancelRequisition> GetAll()
        {
            return db.Set<CancelRequisition>().Include(c => c.RequisitionStatus.Requisition.Employee.Designation).ToList();
        }

        public override CancelRequisition FindById(int id)
        {
            return db.Set<CancelRequisition>().Where(c=>c.Id==id).Include(c => c.RequisitionStatus.Requisition.Employee.Designation).FirstOrDefault();
        }
    }
}

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
    public class FeedbackRepository:Repository<Feedback>,IFeedbackRepository
    {
        public FeedbackRepository(DbContext db) : base(db)
        {
        }

     

        public override ICollection<Feedback> GetAll()
        {
            return db.Set<Feedback>().Include(c => c.Employee).Include(d=>d.Requisition.Employee.Designation).ToList();
        }

        public override Feedback FindById(int id)
        {
            return
                db.Set<Feedback>()
                    .Include(c => c.Requisition.Employee.Designation)
                    .Where(c => c.Id == id).AsNoTracking()
                    .FirstOrDefault();
        }

        public ICollection<Feedback> GetAllByRequisitionId(int requisitionId)
        {
            return db.Set<Feedback>().Include(c=>c.Employee.Designation).Where(c => c.RequisitionId == requisitionId).ToList();
        }
    }
}

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
            return db.Set<Feedback>().Include(c => c.Employee.Designation).Include(d=>d.Requisition.Employee.Designation).ToList();
        }
    }
}

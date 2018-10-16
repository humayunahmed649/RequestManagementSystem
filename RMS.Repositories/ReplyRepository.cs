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
    public class ReplyRepository:Repository<Reply>,IReplyRepository
    {
        private RmsDbContext _db;
        public ReplyRepository(RmsDbContext db) : base(db)
        {
            _db = db;
        }


        public Reply GetAllByFeedbackId(int feedbackId)
        {
            return db.Set<Reply>().Include(c => c.Feedback.Requisition.Employee.Designation).Include(c=>c.Employee.Designation).Where(c => c.FeedbackId == feedbackId).FirstOrDefault();
        }
    }
}

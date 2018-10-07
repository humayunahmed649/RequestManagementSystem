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
    public class RequisitionHistoryRepository:Repository<RequisitionHistory>,IRequisitionHistoryRepository
    {
        private RmsDbContext _db;
        public RequisitionHistoryRepository(RmsDbContext db) : base(db)
        {
            _db = db;
        }

        public  RequisitionHistory FindByRequisitionId(int id)
        {
            return db.Set<RequisitionHistory>().FirstOrDefault(c => c.RequisitionId == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.App.ViewModels.Report;
using RMS.Models.DatabaseContext;
using RMS.Models.EntityModels;
using RMS.Repositories.Base;
using RMS.Repositories.Contracts;

namespace RMS.Repositories
{
    public class RequisitionRepository:Repository<Requisition>,IRequisitionRepository
    {
        private RmsDbContext _db;
        public RequisitionRepository(RmsDbContext db) : base(db)
        {
            _db = db;
        }

        public override ICollection<Requisition> GetAll()
        {
            return db.Set<Requisition>().Include(c => c.Employee).OrderByDescending(c=>c.Id).AsNoTracking().ToList();
        }

        public ICollection<Requisition> GetAllWithEmployee()
        {
            return db.Set<Requisition>().Include(c=>c.Employee.Designation).OrderByDescending(c=>c.Id).AsNoTracking().ToList();
        }

        public override Requisition FindById(int id)
        {
            return db.Set<Requisition>().Where(c => c.Id == id).Include(c => c.Employee.Designation).AsNoTracking().SingleOrDefault();
        }

        public RequisitionSummaryReportVM GetRequisitionSummaryReport(int id)
        {
            var report = _db.GetRequisitionSummaryReport();
            return report.FirstOrDefault();
        }

        public ICollection<Requisition> GetAllRequisitionByEmployeeId(int id)
        {
            return
                db.Set<Requisition>()
                    .Where(c => c.EmployeeId == id)
                    .Include(d => d.Employee.Designation).OrderByDescending(c=>c.Id)
                    .AsNoTracking()
                    .ToList();
        }
    }
}

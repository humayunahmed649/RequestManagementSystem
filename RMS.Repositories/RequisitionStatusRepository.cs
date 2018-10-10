using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using RMS.Models.DatabaseContext;
using RMS.Models.EntityModels;
using RMS.Repositories.Base;
using RMS.Repositories.Contracts;

namespace RMS.Repositories
{
    public class RequisitionStatusRepository:Repository<RequisitionStatus>,IRequisitionStatusRepository
    {
        private RmsDbContext _db;
        public RequisitionStatusRepository(RmsDbContext db) : base(db)
        {
            _db = db;
        }
        public override ICollection<RequisitionStatus> GetAll()
        {
            return db.Set<RequisitionStatus>().Include(c => c.Requisition).Include(c => c.Requisition.Employee).OrderByDescending(c => c.Id).AsNoTracking().ToList();
        }
        public ICollection<RequisitionStatus> GetAllWithRequisitionDetails()
        {
            return db.Set<RequisitionStatus>().Include(c => c.Requisition).Where(c=>c.StatusType=="Assigned").OrderByDescending(c=>c.Id).AsNoTracking().ToList();
        }

        public ICollection<RequisitionStatus> GetAllStatusNew()
        {
            return db.Set<RequisitionStatus>().Include(c => c.Requisition.Employee.Designation).Where(c => c.StatusType == "New").OrderByDescending(c=>c.Id).AsNoTracking().ToList();
        }
        public ICollection<RequisitionStatus> GetAllStatusExecute()
        {
            return db.Set<RequisitionStatus>().Include(c => c.Requisition).Where(c => c.StatusType == "OnExecute").OrderByDescending(c=>c.Id).AsNoTracking().ToList();
        }

        public ICollection<RequisitionStatus> SearchByRequisitionId(string searchText)
        {
            return
                db.Set<RequisitionStatus>().Where(c=>c.StatusType=="New")
                    .Include(c => c.Requisition)
                    .Where(c => c.Requisition.RequisitionNumber.StartsWith(searchText))
                    .ToList();
        }

        public ICollection<RequisitionStatus> GetAllById(int id)
        {
            return
                db.Set<RequisitionStatus>()
                    .Where(c => c.Requisition.Employee.Id == id)
                    .Include(c => c.Requisition.Employee).OrderByDescending(c=>c.Id)
                    .ToList();
        }

        public  RequisitionStatus FindByRequisitionId(int id)
        {
            return
                db.Set<RequisitionStatus>()
                    .Where(c => c.RequisitionId == id)
                    .Include(c => c.Requisition.Employee)
                    .FirstOrDefault();
        }

        public override RequisitionStatus FindById(int id)
        {
            return
                db.Set<RequisitionStatus>()
                .Where(c => c.Id == id)
                .Include(c => c.Requisition.Employee.Designation)
                .FirstOrDefault();
        }

        public ICollection<RequisitionStatus> GetAllRequisitions()
        {
            return
                db.Set<RequisitionStatus>()
                    .Include(c => c.Requisition.Employee.Designation)
                    .Where(c => c.StatusType == "New" || c.StatusType == "Assigned").ToList();
        }

        public ICollection<RequisitionStatus> GetAllCheckOutCheckIn()
        {
            return db.Set<RequisitionStatus>().Include(c => c.Requisition.Employee).Where(c => c.StatusType == "Assigned" || c.StatusType=="OnExecute").OrderByDescending(c => c.Id).AsNoTracking().ToList();
        }
    }
}

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
    public class AssignRequisitionRepository:Repository<AssignRequisition>,IAssignRequisitionRepository
    {
        private RmsDbContext _db;
        public AssignRequisitionRepository(RmsDbContext db) : base(db)
        {
            _db = db;
        }

        public override ICollection<AssignRequisition> GetAll()
        {
            return db.Set<AssignRequisition>().Include(c => c.Employee).Include(c => c.Requisition).Include(c => c.Vehicle).AsNoTracking().ToList();
        }

        public ICollection<AssignRequisition> GetAllWithInformation()
        {
            return db.Set<AssignRequisition>()
                .Include(c => c.Employee).Include(c=>c.Employee.Designation)
                .Include(c => c.Vehicle)
                .Include(c => c.Vehicle.VehicleType)
                .Include(c => c.Requisition).Include(c=>c.Requisition.Employee).Include(c=>c.Requisition.Employee.Designation).AsNoTracking()
                .ToList();
        }

        public override AssignRequisition FindById(int id)
        {
            return
                db.Set<AssignRequisition>()
                    .Where(c=>c.RequisitionId==id)
                    .Include(c => c.Vehicle.VehicleType)
                    .Include(c=>c.Requisition.Employee.Designation)
                    .Include(c => c.RequisitionStatus)
                    .Include(c=>c.Employee)
                    .AsNoTracking()
                    .FirstOrDefault();
        }

        public AssignRequisition SearchByText(string searchByText)
        {
            return db.Set<AssignRequisition>().Include(c => c.Requisition).FirstOrDefault();
        }

        public string GetVehicleStatus(int vehicleId)
        {
            var vehicleStatus = (from requisitionStatus in db.Set<RequisitionStatus>()

                join assignRequisition in db.Set<AssignRequisition>()

                    on requisitionStatus.RequisitionId equals assignRequisition.RequisitionId
                orderby requisitionStatus.Requisition.EndDateTime descending
                where assignRequisition.VehicleId == vehicleId

                select new
                {
                    RequisitionNumber = assignRequisition.RequisitionNumber,
                    VehicleId = assignRequisition.VehicleId,
                    DriverId = assignRequisition.Employee.FullName,
                    Status = requisitionStatus.StatusType
                }
                ).FirstOrDefault();
            if (vehicleStatus != null)
            {
                if (vehicleStatus.Status == "Completed")
                {

                    return "This Vehicle Is  Available";
                }
                else
                {
                    string status = vehicleStatus.RequisitionNumber + "," + vehicleStatus.Status + "," + vehicleStatus.DriverId;
                    return status;
                }
            }
            return  "This Vehicle Is  Available";
        }

        public ICollection<AssignRequisitionReportVM> GetRequisitionSummaryReport()
        {
            var report = _db.GetAssignRequisitionSummaryReport();
            return report;
        }
    }
    
}

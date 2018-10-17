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
            return db.Set<AssignRequisition>().Include(c => c.Employee).Include(c => c.Requisition).Include(c => c.Vehicle).Include(c=>c.Vehicle.VehicleType).AsNoTracking().ToList();
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

        //Get Vehicle Status
        public AssignRequisition GetVehicleStatus(int vehicleId)
        {
            try
            {
                var statusInformation=new AssignRequisition();
                var vehicleStatus = (from requisitionStatus in db.Set<RequisitionStatus>()
                join assignRequisition in db.Set<AssignRequisition>()     
                on requisitionStatus.RequisitionId equals assignRequisition.RequisitionId
                orderby requisitionStatus.Requisition.EndDateTime descending 
                where assignRequisition.VehicleId == vehicleId
                select new
                {
                    requisitionId=assignRequisition.RequisitionId,
                    vehicleId=assignRequisition.VehicleId,
                    vehicle=assignRequisition.Vehicle.RegNo,
                    driverId=assignRequisition.Employee.Id,
                    status=requisitionStatus.StatusType
                }).FirstOrDefault();

                if (vehicleStatus != null && vehicleStatus.status != "Completed")
                {
                    
                      statusInformation =  db.Set<AssignRequisition>()
                                            .Where(c => c.RequisitionId == vehicleStatus.requisitionId)
                                            .Include(c => c.Requisition.Employee.Designation)
                                            .Include(c => c.Vehicle)
                                            .Include(c => c.Employee)
                                            .Include(c => c.RequisitionStatus)
                                            .FirstOrDefault();
                    return statusInformation;
                }
                return statusInformation;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            
        }
        
        //Get Driver Status
        public AssignRequisition GetDriverStatus(int driverId)
        {
            try
            {
                var statusInformation = new AssignRequisition();
                var driverStatus = (from requisitionStatus in db.Set<RequisitionStatus>()
                    join assignRequisition in db.Set<AssignRequisition>()
                        on requisitionStatus.RequisitionId equals assignRequisition.RequisitionId
                     orderby requisitionStatus.Requisition.EndDateTime descending
                    where assignRequisition.EmployeeId == driverId
                    select new
                    {
                        requisitionId = assignRequisition.RequisitionId,
                        vehicleId = assignRequisition.VehicleId,
                        vehicle = assignRequisition.Vehicle.RegNo,
                        driverId = assignRequisition.Employee.Id,
                        status = requisitionStatus.StatusType
                    }).FirstOrDefault();

                if (driverStatus != null && driverStatus.status != "Completed")
                {

                    statusInformation = db.Set<AssignRequisition>()
                        .Where(c => c.RequisitionId == driverStatus.requisitionId)
                        .Include(c => c.Requisition.Employee.Designation)
                        .Include(c => c.Vehicle)
                        .Include(c => c.Employee)
                        .Include(c => c.RequisitionStatus)
                        .FirstOrDefault();
                    return statusInformation;
                }
                return statusInformation;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
        

        public ICollection<AssignRequisitionReportVM> GetRequisitionSummaryReport()
        {
            var report = _db.GetAssignRequisitionSummaryReport();
            return report;
        }
    }
    
}

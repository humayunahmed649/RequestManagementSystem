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
    public class AssignRequisitionRepository:Repository<AssignRequisition>,IAssignRequisitionRepository
    {
        public AssignRequisitionRepository(DbContext db) : base(db)
        {
        }

        public override ICollection<AssignRequisition> GetAll()
        {
            return
                db.Set<AssignRequisition>().Include(c => c.Employee).Include(c => c.Requisition).Include(c => c.Vehicle).ToList();
        }

        public ICollection<AssignRequisition> GetAllWithInformation()
        {
            return db.Set<AssignRequisition>()
                .Include(c => c.Employee).Include(c=>c.Employee.Designation)
                .Include(c => c.Vehicle)
                .Include(c => c.Vehicle.VehicleType)
                .Include(c => c.Requisition).Include(c=>c.Requisition.Employee).Include(c=>c.Requisition.Employee.Designation)
                .ToList();
        }

        public AssignRequisition SearchByName(string searchByText)
        {
            return db.Set<AssignRequisition>().Include(c => c.Requisition).FirstOrDefault();
        }

        
    }
    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models.EntityModels
{
    public class AssignRequisition
    {
        public int Id { get; set; }
        public string RequisitionNumber { get; set; }
        public int RequisitionId { get; set; }
        public Requisition Requisition { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}

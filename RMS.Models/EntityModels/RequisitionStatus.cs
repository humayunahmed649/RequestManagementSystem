using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models.EntityModels
{
    public class RequisitionStatus
    {
        public int Id { get; set; }
        public string StatusType { get; set; }
        public string RequisitionNumber { get; set; }
        public int RequisitionId { get; set; }
        public Requisition Requisition { get; set; }
        //public int AssignRequisitionId { get; set; }
        //public AssignRequisition AssignRequisition { get; set; }

    }
}

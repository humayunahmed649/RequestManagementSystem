using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models.EntityModels
{
    public class RequisitionHistory
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime? SubmitDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public DateTime? DeletedDateTime { get; set; }
        public int RequisitionId { get; set; }
        public Requisition Requisition { get; set; }
    }
}

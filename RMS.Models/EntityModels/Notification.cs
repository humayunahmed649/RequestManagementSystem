using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models.EntityModels
{
    public class Notification
    {
        public int Id { get; set; }

        [ForeignKey("Employee")]
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [ForeignKey("Requisition")]
        public int? RequisitionId { get; set; }
        public Requisition Requisition { get; set; }

        public string SenderText { get; set; }
        public string SenderViewStatus { get; set; }
        public DateTime SenderNotifyDateTime { get; set; }
        

        public string ControllerText { get; set; }
        public string ControllerViewStatus { get; set; }
        public DateTime ControllerNotifyDateTime { get; set; }


    }
}

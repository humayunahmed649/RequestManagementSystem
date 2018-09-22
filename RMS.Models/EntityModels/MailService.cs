using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models.EntityModels
{
    public class MailService
    {
        public int Id { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime MailSendingDateTime { get; set; }

        [ForeignKey("Requisition")]
        public int? RequisitionId { get; set; }
        public Requisition Requisition { get; set; }

    }
}

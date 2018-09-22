using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using RMS.Models.EntityModels;

namespace RMS.App.ViewModels
{
    public class MailServiceViewModel
    {
        public int Id { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }

        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        public DateTime MailSendingDateTime { get; set; }

        [Display(Name = "Requisition Id")]
        public int? RequisitionId { get; set; }
        public Requisition Requisition { get; set; }
    }
}
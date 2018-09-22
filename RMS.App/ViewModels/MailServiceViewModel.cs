using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using RMS.Models.EntityModels;

namespace RMS.App.ViewModels
{
    public class MailServiceViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please input valid email address!")]
        [EmailAddress]
        public string To { get; set; }

        public string From { get; set; }

        public string Subject { get; set; }

        [Display(Name = "Message")]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        [Display(Name = "Sending Date and Time")]
        public DateTime MailSendingDateTime { get; set; }

        [Display(Name = "Requisition Id")]
        public int? RequisitionId { get; set; }
        public Requisition Requisition { get; set; }
    }
}
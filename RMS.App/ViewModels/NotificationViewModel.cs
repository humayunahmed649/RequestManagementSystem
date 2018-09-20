using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using RMS.Models.EntityModels;

namespace RMS.App.ViewModels
{
    public class NotificationViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int? RequisitionId { get; set; }
        public Requisition Requisition { get; set; }

        public DateTime NotifyDateTime { get; set; }

        public string SenderViewStatus { get; set; }

        public string ControllerViewStatus { get; set; }

    }
}
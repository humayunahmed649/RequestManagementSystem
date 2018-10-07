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

        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }

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
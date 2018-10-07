using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RMS.Models.EntityModels;

namespace RMS.App.ViewModels
{
    public class RequisitionHistoryViewModel
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
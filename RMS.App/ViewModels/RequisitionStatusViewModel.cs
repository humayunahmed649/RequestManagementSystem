using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RMS.Models.EntityModels;

namespace RMS.App.ViewModels
{
    public class RequisitionStatusViewModel
    {
        public int Id { get; set; }
        public string StatusType { get; set; }
        public string RequisitionNumber { get; set; }
        public int RequisitionId { get; set; }
        public RequisitionViewModel Requisition { get; set; }
        public IEnumerable<RequisitionStatusViewModel> RequisitionStatusView { get; set; } 
    }
}
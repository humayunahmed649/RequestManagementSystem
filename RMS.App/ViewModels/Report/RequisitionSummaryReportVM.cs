using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMS.App.ViewModels.Report
{
    public class RequisitionSummaryReportVm
    {
        public int Id { get; set; }
        public string FromPlace { get; set; }
        public string DestinationPlace { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Description { get; set; }
        public string RequisitionNumber { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
    }
}
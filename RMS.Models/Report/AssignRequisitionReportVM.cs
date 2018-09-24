using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMS.App.ViewModels.Report
{
    public class AssignRequisitionReportVM
    {
        public int Id { get; set; }
        public string RequisitionNumber { get; set; }
        public string FromPlace { get; set; }
        public string DestinationPlace { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string RequisitionType { get; set; }
        public string BrandName { get; set; }
        public string VehicleRegNo { get; set; }
        public string VehicleTypeName { get; set; }
        public string DriverName { get; set; }
        public string DriverContactNo { get; set; }
    }
}
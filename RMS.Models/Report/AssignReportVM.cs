using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models.Report
{
    public class AssignReportVm
    {
        public int Id { get; set; }
        public string RequisitionNumber { get; set; }
        public string Requestor { get; set; }
        public string Designation { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string FromPlace { get; set; }
        public string DestinationPlace { get; set; }
        public string Description { get; set; }
        public string RegNo { get; set; }
        public string DriverName { get; set; }
        public string DriverContactNo { get; set; }
    }
}

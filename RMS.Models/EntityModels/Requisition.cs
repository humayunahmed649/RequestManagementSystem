using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models.EntityModels
{
    public class Requisition
    {
        public int Id { get; set; }
        public string FromPlace { get; set; }
        public string DestinationPlace { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Description { get; set; }
        public string RequestFor { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

    }
}

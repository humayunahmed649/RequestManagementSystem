using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using RMS.Models.EntityModels;

namespace RMS.Models.EntityViewModel
{
    [NotMapped]
    public class VehicleStatusViewModel
    {
        public int Id { get; set; }
        public int RequisitionId { get; set; }
        public Requisition Requisition { get; set; }
        public string RequisitionNumber { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public string Status { get; set; }
          
    }
}
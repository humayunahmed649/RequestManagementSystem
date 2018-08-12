using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RMS.Models.EntityModels;

namespace RMS.App.ViewModels
{
    public class AssignRequisitionViewModel
    {
        public int Id { get; set; }
        public int RequisitionId { get; set; }
        public Requisition Requisition { get; set; }

        [Required(ErrorMessage = "Please Enter valid Vehicle Information")]
        [Display(Name = "Vehicle Registration No")]
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        [Required(ErrorMessage = "Please Enter valid Driver Information")]
        [Display(Name = "Name of Driver")]
        public int DriverId { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public string Status { get; set; }
        public List<AssignRequisitionViewModel> assignRequisitionViewModels { get; set; } 
        public IEnumerable<RequisitionViewModel> RequisitionViewModel { get; set; }
    }
}
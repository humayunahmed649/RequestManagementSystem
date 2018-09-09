using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using RMS.Models.EntityModels;

namespace RMS.App.ViewModels
{
    public class AssignRequisitionViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Requisition Number")]
        public string RequisitionNumber { get; set; }

        [Display(Name = "Requisition Id")]
        public int RequisitionId { get; set; }
        public Requisition Requisition { get; set; }

        [Required(ErrorMessage = "Please Enter valid Vehicle Information")]
        [Display(Name = "Vehicle Reg No")]
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        [Required(ErrorMessage = "Please Enter valid Driver Information")]
        [Display(Name = "Driver")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Display(Name = "Requisition Status")]
        public int RequisitionStatusId { get; set; }
        public RequisitionStatus RequisitionStatus { get; set; }

        public List<VehicleType> VehicleTypes { get; set; } 

        //private IEnumerable<AssignRequisitionViewModel> AssignRequisitionViewModels { get; set; } 

    }
}
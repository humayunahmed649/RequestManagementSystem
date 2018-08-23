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
        public string RequisitionNumber { get; set; }
        public int RequisitionId { get; set; }
        public Requisition Requisition { get; set; }

        [Required(ErrorMessage = "Please Enter valid Vehicle Information")]
        [Display(Name = "Vehicle Registration No")]
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        [Required(ErrorMessage = "Please Enter valid Driver Information")]
        [Display(Name = "Driver Name")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int RequisitionStatusId { get; set; }
        public RequisitionStatus RequisitionStatus { get; set; }


        //private IEnumerable<AssignRequisitionViewModel> AssignRequisitionViewModels { get; set; } 

    }
}
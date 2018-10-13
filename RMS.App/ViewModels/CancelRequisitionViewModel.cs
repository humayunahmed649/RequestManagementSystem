using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RMS.Models.EntityModels;

namespace RMS.App.ViewModels
{
    public class CancelRequisitionViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Cancel Reason Must be Needed to Cancle the Requisition")]
        [Display(Name = "Explanation")]
        public string Reason { get; set; }
        [Required]
        public int RequisitionStatusId { get; set; }
        public RequisitionStatus RequisitionStatus { get; set; }
        public string RequisitionNumber { get; set; }
        [Required]
        public int RequisitionId { get; set; }
        public Requisition Requisition { get; set; }



    }
}
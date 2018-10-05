using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using RMS.Models.EntityModels;

namespace RMS.App.ViewModels
{
    public class RequisitionStatusViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Status Type")]
        public string StatusType { get; set; }

        [Display(Name = "Requisition Number")]
        public string RequisitionNumber { get; set; }

        [Display(Name = "Requisition Id")]
        public int RequisitionId { get; set; }
        public RequisitionViewModel Requisition { get; set; }

        public IEnumerable<RequisitionStatusViewModel> RequisitionStatusView { get; set; }

        [NotMapped]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please write a note for cancellation!")]
        public string Reason { get; set; }
         
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using RMS.Models.EntityModels;

namespace RMS.App.ViewModels
{
    public class FeedbackViewModel
    {
        [Key]
        public int  Id { get; set; }
        [Required]
        [StringLength(250)]
        public string CommentText { get; set; }
        public int RequisitionId { get; set; }

        public Requisition Requisition { get; set; }
        public int FeedbackId { get; set; }
    }
}
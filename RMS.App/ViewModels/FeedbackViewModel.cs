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
        public int  Id { get; set; }

        [Required (ErrorMessage = "Please give a feedback message!")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Write your comment")]
        public string CommentText { get; set; }

        [Display(Name = "Requiest Details")]
        public int RequisitionId { get; set; }
        public Requisition Requisition { get; set; }

        [Display(Name = "Feedback")]
        public int FeedbackId { get; set; }

    }
}
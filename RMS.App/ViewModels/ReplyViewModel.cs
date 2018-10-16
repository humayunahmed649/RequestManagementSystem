using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using RMS.Models.EntityModels;

namespace RMS.App.ViewModels
{
    public class ReplyViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please fill reply text box")]
        [StringLength(250)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Put your Reply")]
        public string ReplyText { get; set; }
        [NotMapped]
        public int FeedbackId { get; set; }
        [NotMapped]
        public Feedback Feedback { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
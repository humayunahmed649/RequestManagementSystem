using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models.EntityModels
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string CommentText { get; set; }
        [Required]
        public int RequisitionId { get; set; }
        public Requisition Requisition { get; set; }

        public int? FeedBackId { get; set; }

        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}

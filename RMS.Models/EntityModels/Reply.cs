using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models.EntityModels
{
    public class Reply
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string ReplyText { get; set; }
        [Required]
        public int FeedbackId { get; set; }
        public virtual Feedback Feedback { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}

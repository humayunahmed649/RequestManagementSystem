using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models.EntityModels
{
    public class ContactModel
    {

        public int Id { get; set; }
        [Required]
        [Display(Name = "Email Or FullName")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNo { get; set; }
        [Required]
        [Display(Name = "Message")]
        public string Message { get; set; }
    }
}

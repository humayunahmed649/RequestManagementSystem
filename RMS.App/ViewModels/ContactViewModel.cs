using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RMS.App.ViewModels
{
    public class ContactViewModel
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
        //public IEnumerable<ContactViewModel> ContactViewModels { get; set; } 
    }
}
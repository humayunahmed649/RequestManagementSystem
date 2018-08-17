using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RMS.App.ViewModels
{
    public class EmployeeTypeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide a employee type!")]
        [Display(Name = "Employee Type")]
        public string Type { get; set; }

    }  
}
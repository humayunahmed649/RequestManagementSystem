using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using RMS.Models.EntityModels;

namespace RMS.App.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide a name!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please provide a code!")]
        public string Code { get; set; }

        [Display(Name = "Organization")]
        [Required(ErrorMessage = "Please select a department!")]
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }

    }
}

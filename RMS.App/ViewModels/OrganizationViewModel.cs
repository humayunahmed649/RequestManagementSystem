using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.App.ViewModels
{
    public class OrganizationViewModel  
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide a name!")]
        [Display(Name = "Organization")]
        public string Name { get; set; }
        

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.App.ViewModels
{
    public class DesignationViewModel   
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide a Title!")]
        public string Title { get; set; }
    }
}

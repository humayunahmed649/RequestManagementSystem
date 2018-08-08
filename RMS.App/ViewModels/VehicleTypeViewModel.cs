using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.App.ViewModels
{
    public class VehicleTypeViewModel   
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide a type name!")]
        public string Name { get; set; }

    }
}

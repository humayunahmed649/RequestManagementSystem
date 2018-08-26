using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Models.EntityModels;

namespace RMS.App.ViewModels
{
    public class VehicleViewModel 
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide a brand name!")]
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }

        [Required(ErrorMessage = "Please provide a model name!")]
        [Display(Name = "Model Name")]
        public string ModelName { get; set; }

        [Required(ErrorMessage = "Please provide a registration no!")]
        [Display(Name = "Reg No")]
        public string RegNo { get; set; }

        [Required(ErrorMessage = "Please provide a chassis no!")]
        [Display(Name = "Chassis No")]
        public string ChassisNo { get; set; }

        [Required(ErrorMessage = "Please provide seat capacity!")]
        [Display(Name = "Seat Capacity")]
        public int SeatCapacity { get; set; }

        [Required(ErrorMessage = "Please select a vehicle type!")]
        [Display(Name = "Vehicle Type")]
        public int VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.App.ViewModels.ValidationModels;
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

        
        [Display(Name = "Seat Capacity")]
        [GreaterThanZero(ErrorMessage = "Value must be greater then 0!")]
        [Required(ErrorMessage = "Please provide seat capacity!")]
        public int SeatCapacity { get; set; }

        [Required(ErrorMessage = "Please select a vehicle type!")]
        [Display(Name = "Vehicle Type")]
        public int VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; }

    }
}

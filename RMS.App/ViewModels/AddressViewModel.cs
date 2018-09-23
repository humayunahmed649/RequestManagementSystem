using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using RMS.Models.EntityModels;

namespace RMS.App.ViewModels
{
    public class AddressViewModel
    {
        
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Please provide address Type!")]
        [Display(Name = "Address Type")]
        public string AddressType { get; set; }

        [Display(Name = "Address Line 1 ")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        [Required(ErrorMessage = "Please provide Village/Moholla name!")]
        public string AddressLine2 { get; set; }

        [Display(Name = "Post Office")]
        [Required(ErrorMessage = "Please provide your post office!")]
        public string PostOffice { get; set; }

        [Display(Name = "Post Code ")]
        [Required(ErrorMessage = "Please provide your post code!")]
        public string PostCode { get; set; }

        [Display(Name = "Division ")]
        [Required(ErrorMessage = "Please select a division!")]
        public int DivisionId { get; set; }
        public Division Division { get; set; }

        [Display(Name = "District ")]
        [Required(ErrorMessage = "Please select a district!")]
        public int DistrictId { get; set; }
        public District District { get; set; }

        [Display(Name = "UPZ/PS ")]
        [Required(ErrorMessage = "Please select a upazila!")]
        public int UpazilaId { get; set; }
        public Upazila Upazila { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
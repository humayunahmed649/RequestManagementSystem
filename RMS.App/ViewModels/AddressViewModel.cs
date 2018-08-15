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
        [Key]
        public int Id { get; set; }
        
        public string AddressType { get; set; }

        [StringLength(250)]
        [Display(Name = "Address ")]
        [Required(ErrorMessage = "Please Enter Your Address Please")]
        public string AddressLine1 { get; set; }

        [StringLength(250)]
        [Display(Name = "Address")]
        [Required(ErrorMessage = "Please Enter Your Address Please")]
        public string AddressLine2 { get; set; }

        [StringLength(250)]
        [Display(Name = "Post Office")]
        [Required(ErrorMessage = "Please Enter Your PostOffice Please")]
        public string PostOffice { get; set; }

        [StringLength(6)]
        [Display(Name = "Post Code ")]
        [Required(ErrorMessage = "Please Enter Your Post Code Please")]
        public string PostCode { get; set; }

        [ForeignKey("Division")]

        [Display(Name = "Address ")]
        [Required(ErrorMessage = "Please Enter Your Address Please")]
        public int DivisionId { get; set; }
        public Division Division { get; set; }

        [Display(Name = "Address ")]
        [Required(ErrorMessage = "Please Enter Your Address Please")]
        [ForeignKey("District")]
        public int DistrictId { get; set; }
        public District District { get; set; }

        [Display(Name = "Address ")]
        [Required(ErrorMessage = "Please Enter Your Address Please")]
        [ForeignKey("Upazila")]
        public int UpazilaId { get; set; }
        public Upazila Upazila { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
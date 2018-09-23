using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using RMS.Models.Contracts;
using RMS.Models.EntityModels.Identity;

namespace RMS.Models.EntityModels
{
    public class Employee:IDelatable
    {
        [Key]
        public int Id { get; set; }

        [Required,StringLength(250)]
        [Display(Name = "Name")]
        public string FullName { get; set; }
        //[Required,StringLength(50)]
        public string Email { get; set; }

        [Required, StringLength(15)]
        public string ContactNo { get; set; }

        [Required, StringLength(19)]
        public string NID { get; set; }

        public string BloodGroup { get; set; }

        [ForeignKey("Organization")]
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        [ForeignKey("Designation")]
        public int DesignationId { get; set; }
        public Designation Designation { get; set; }

        [StringLength(25)]
        public string DrivingLicence { get; set; }

        [ForeignKey("EmployeeType")]
        public int? EmployeeTypeId { get; set; }
        public EmployeeType EmployeeType { get; set; }

        public List<Address> Addresses { get; set; }

        [ForeignKey("AppUser")]
        public int? AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        [NotMapped]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [NotMapped]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public bool IsDelete()
        {
            return false;
        }
    }
}

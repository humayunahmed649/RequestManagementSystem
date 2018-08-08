using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using RMS.Models.EntityModels;

namespace RMS.App.ViewModels
{
    public class EmployeeViewModel 
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide name!")]
        [Display(Name = "Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please provide email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please provide contact no!")]
        [Display(Name = "Contact No")]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "Please provide NID no!")]
        public string NID { get; set; }

        [Display(Name = "Blood Group")]
        public string BloodGroup { get; set; }

        [Display(Name = "Organization")]
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }

        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        [Display(Name = "Designation")]
        public int DesignationId { get; set; }
        public Designation Designation { get; set; }
        [Required]
        [Display(Name = "Permanent Address")]
        public string PermanentAddress { get; set; }
        [Required]
        [Display(Name = "Permanent Address")]
        public string PresentAddress { get; set; }
        public string EmployeeTypes { get; set; }
        
    }
 

    
}

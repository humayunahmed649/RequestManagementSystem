using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using RMS.Models.Contracts;

namespace RMS.Models.EntityModels
{
    public class Employee:IDelatable
    {
        [Key]
        public int Id { get; set; }

        [Required,StringLength(250)]
        public string FullName { get; set; }

        [Required,StringLength(50)]
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

        [Required,StringLength(500)]
        public string PermanentAddress { get; set; }
       
        [Required,StringLength(500)]
        public string PresentAddress { get; set; }

        [Required,StringLength(50)]
        public string EmployeeTypes { get; set; }

        public bool IsDelete()
        {
            return false;
        }
    }
}

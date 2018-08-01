using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models.EntityModels
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string NID { get; set; }
        public string BloodGroup { get; set; }
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int DesignationId { get; set; }
        public Designation Designation { get; set; }
        public string HouseNo { get; set; }
        public string RoadNo { get; set; }
        public string FloorNo { get; set; }
        public string PostOffice { get; set; }
        public string District { get; set; }
        public string Division { get; set; }

    }
}

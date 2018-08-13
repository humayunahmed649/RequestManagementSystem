using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models.EntityModels
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [Required,StringLength(250)]
        public string AddressType { get; set; }

        [StringLength(250)]
        public string AddressLine1 { get; set; }

        [Required,StringLength(250)]
        public string AddressLine2 { get; set; }

        [Required,StringLength(250)]
        public string PostOffice { get; set; }

        [StringLength(6)]
        public string PostCode { get; set; }

        [ForeignKey("Division")]
        public int DivisionId { get; set; }
        public Division Division { get; set; }

        [ForeignKey("District")]
        public int DistrictId { get; set; }
        public District District { get; set; }

        [ForeignKey("Upazila")]
        public int UpazilaId { get; set; }
        public Upazila Upazila { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}

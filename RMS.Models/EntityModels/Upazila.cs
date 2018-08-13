using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models.EntityModels
{ 
    public class Upazila
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("District")]
        public int DistrictId { get; set; }
        public District District { get; set; }

        [Required,StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(50)]
        public string BnName { get; set; }
    }
}
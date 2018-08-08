using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models.EntityModels
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        [Required,StringLength(150)]
        public string BrandName { get; set; }

        [Required,StringLength(150)]
        public string ModelName { get; set; }

        [Required,StringLength(50)]
        public string RegNo { get; set; }

        [Required,StringLength(50)]
        public string ChassisNo { get; set; }

        [ForeignKey("VehicleType")]
        public int VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; }

    }
}

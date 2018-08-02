using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models.EntityModels
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string RegNo { get; set; }
        public string ChassisNo { get; set; }
        public int VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; }

    }
}

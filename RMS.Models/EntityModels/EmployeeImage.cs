using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models.EntityModels
{
    public class EmployeeImage
    {
        [Key]
        public int Id { get; set; }

        [StringLength(1000)]
        public string ImageName { get; set; }
        public byte[] ImageByte { get; set; }

        [StringLength(1000)]
        public string ImagePath { get; set; }


    }
}

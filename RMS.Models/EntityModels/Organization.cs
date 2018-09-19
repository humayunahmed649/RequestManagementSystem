using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models.EntityModels
{
    public class Organization
    {
        [Key]
        public int Id { get; set; }

        [Required,StringLength(250)]
        public string Name { get; set; }
        

    }
}

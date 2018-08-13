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
    public class District
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Division")]
        public int DivisionId { get; set; }
        public Division Division { get; set; }

        [Required,StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(50)]
        public string BnName { get; set; }

        [Required]
        public float Lat { get; set; }

        [Required]
        public float Lon { get; set; }

        [Required,StringLength(150)]
        public string Website { get; set; }

    }
}
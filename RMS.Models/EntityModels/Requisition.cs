using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models.EntityModels
{
    public class Requisition
    {
        [Key]
        public int Id { get; set; }

        [Required,StringLength(250)]
        public string FromPlace { get; set; }

        [Required,StringLength(250)]
        public string DestinationPlace { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }

        [Required]
        public DateTime EndDateTime { get; set; }

        [Required,StringLength(500)]
        public string Description { get; set; }

        [Required,StringLength(250)]
        public string RequestFor { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

    }
}

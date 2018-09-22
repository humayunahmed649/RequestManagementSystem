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
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required,StringLength(250)]
        public string Name { get; set; }

        //[Required, StringLength(50)]
        public string Code { get; set; }

        [ForeignKey("Organization")]
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }

    }
}

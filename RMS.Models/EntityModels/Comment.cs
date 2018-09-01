using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models.EntityModels
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int RequisitionId { get; set; }

        public Requisition Requisition { get; set; }
    }
}

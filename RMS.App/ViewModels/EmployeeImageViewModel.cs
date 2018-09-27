using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RMS.App.ViewModels
{
    public class EmployeeImageViewModel
    {
        public int Id { get; set; }

        public string ImageName { get; set; }

        public byte[] ImageByte { get; set; }

        public string ImagePath { get; set; }

        [NotMapped]
        public HttpPostedFileWrapper ImageFile { get; set; }
    }
}
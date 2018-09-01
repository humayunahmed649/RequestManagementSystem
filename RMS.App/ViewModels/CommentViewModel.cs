using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using RMS.Models.EntityModels;

namespace RMS.App.ViewModels
{
    public class CommentViewModel
    {
        public int  Id { get; set; }
        public string Text { get; set; }
        public int RequisitionId { get; set; }

        public Requisition Requisition { get; set; }
    }
}
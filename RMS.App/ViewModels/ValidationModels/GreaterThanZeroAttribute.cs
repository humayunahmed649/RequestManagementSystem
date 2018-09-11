using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RMS.App.ViewModels.ValidationModels
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class GreaterThanZeroAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value != null && value is int && (int)value > 0;
        }
    }
}
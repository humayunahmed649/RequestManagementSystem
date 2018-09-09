using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RMS.App.ViewModels;

namespace RMS.App.ViewModels.ValidationModels
{
    public class RequisitionDateTimeValidationCheckForOwn:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var requisitionDateForOwn = (RequisitionViewModel)validationContext.ObjectInstance;
            var presentDateTime = DateTime.Today;

            if (presentDateTime > requisitionDateForOwn.EndDateTime.Date)
            {
                return new ValidationResult("Return date must be greater then present date time!");
            }
            if (requisitionDateForOwn.StartDateTime.Date > requisitionDateForOwn.EndDateTime.Date)
            {
                return new ValidationResult("Return date must be greater then journey date time!");
            }
            return ValidationResult.Success;
        }
    }
}
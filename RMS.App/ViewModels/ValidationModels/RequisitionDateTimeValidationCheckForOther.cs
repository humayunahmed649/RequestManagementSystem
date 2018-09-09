using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RMS.App.ViewModels.ValidationModels
{
    public class RequisitionDateTimeValidationCheckForOther : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var requisitionDateForOther = (RequisitonForAnotherViewModel) validationContext.ObjectInstance;
            var presentDateTime = DateTime.Today;

            if (presentDateTime > requisitionDateForOther.EndDateTime.Date)
            {
                return new ValidationResult("Return date must be greater then present date time!");
            }
            if (requisitionDateForOther.StartDateTime.Date > requisitionDateForOther.EndDateTime.Date)
            {
                return new ValidationResult("Return date must be greater then journey date time!");
            }

            return ValidationResult.Success;
        }
    }
}
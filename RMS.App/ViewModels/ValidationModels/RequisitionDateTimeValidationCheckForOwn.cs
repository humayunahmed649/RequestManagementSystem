using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.App.ViewModels;

namespace RMS.App.ViewModels.ValidationModels
{
    public class RequisitionDateTimeValidationCheckForOwn:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var requisitionDateForOwn = (RequisitionViewModel)validationContext.ObjectInstance;

            if (requisitionDateForOwn.StartDateTime.Date > requisitionDateForOwn.EndDateTime.Date)
            {
                return new ValidationResult("Return date must be greater then journey date time!");
            }
            return ValidationResult.Success;
        }

    }
}
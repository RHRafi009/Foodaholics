using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Foodaholics.ValidationRules
{
    public class PasswordValidaton : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string pass = value.ToString();
            if (pass.Length < 8)
                return new ValidationResult("Password must have at least 8 characters");
            
            return ValidationResult.Success;

        }
    }
}
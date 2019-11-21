using Foodaholics.Models;
using Foodaholics.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Foodaholics.ValidationRules
{
    public class EmailSignInValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            UserRepository uRepo = new UserRepository();
            string email = value.ToString();

            foreach (User u in uRepo.GetAll())
            {
                if (u.Email == email)
                    return ValidationResult.Success;
            }
            return new ValidationResult("Email does not exist");

        }
    }
}
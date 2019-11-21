using Foodaholics.ValidationRules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Foodaholics.Models
{
    public class CreateUser
    {
        [Required(ErrorMessage = "Name can not be empty")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email can not be empty"), EmailSIgnUpValidation(ErrorMessage ="")]
        public String Email { get; set; }
        [Required(ErrorMessage = "Password can not be empty"), PasswordValidaton(ErrorMessage ="")]
        public string Password { get; set; }
        [Required(ErrorMessage = "This filed can not be empty"), DisplayName("Confirm password"), PasswordValidaton(ErrorMessage ="")]
        public string ConPassword { get; set; }
        public CreateUser() 
        { 
        
        }
    }
}
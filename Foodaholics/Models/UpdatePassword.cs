using Foodaholics.ValidationRules;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Foodaholics.Models
{
    public class UpdatePassword
    {
        public int id { get; set; }

        [Required(ErrorMessage = "This field can be empty")]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "This field can be empty"), PasswordValidaton(ErrorMessage ="")]
        public string Password { get; set; }
        [Required(ErrorMessage = "This field can be empty"), PasswordValidaton(ErrorMessage = "")]
        public string ConPassword { get; set; }
    }
}
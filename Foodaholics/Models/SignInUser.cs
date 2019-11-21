using Foodaholics.ValidationRules;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Foodaholics.Models
{
    public class SignInUser
    {
        [Required(ErrorMessage ="Email can not be empty"), EmailSignInValidation(ErrorMessage ="")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Password can not be empty")]
        public string Password { get; set; }
    }
}
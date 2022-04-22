using DemoWeb.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWeb.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [CustomValidations(allowDomain: "gmail.com",ErrorMessage ="@gmail.com")]
        [Remote(action:"isEmailInUse", controller:"Account")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name ="Confirm Password")]
        [Compare("Password",ErrorMessage ="Password and confirm password must be match")]
        public string ConfirmPassword { get; set; }
        [Display(Name ="City")]
        public string City { get; set; }
    }
}

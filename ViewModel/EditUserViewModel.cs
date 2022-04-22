using DemoWeb.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWeb.ViewModel
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            Claims = new List<string>();
            Roles = new List<string>();
        }
        public string UserID { get; set; }
        [Remote(action: "VerifyEmail", controller: "Admin", AdditionalFields = nameof(OldEmail))]
        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }
        [Remote(action: "VerifyEmail", controller: "Admin", AdditionalFields = nameof(UserEmail))]
        public string OldEmail { get; set; }
        [Required]
        public string UserName { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        public List<string> Claims { get; set; }
        public List<string> Roles { get; set; }
    }
}

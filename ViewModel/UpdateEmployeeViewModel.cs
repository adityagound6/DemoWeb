using DemoWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWeb.ViewModel
{
    public class UpdateEmployeeViewModel : CreateEmployeeViewModel
    {
        public int EmoloyeeId { get; set; }
        public string ExistingPhotoPath { get; set; }

        [Remote(action: "VerifyEmail", controller: "Home", AdditionalFields = nameof(UpdateEmail))]
        public string GetEmail { get; set; }

        [EmailAddress]
        [Remote(action: "VerifyEmail", controller: "Home", AdditionalFields = nameof(GetEmail))]
        //[Remote(action: "isEmailInUse", controller:"Home")]
        public string UpdateEmail { get; set; }
    }
}

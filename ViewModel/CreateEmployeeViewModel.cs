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
    public class CreateEmployeeViewModel 
    {
        [Required(ErrorMessage ="Email field is reqired")]
        [Remote(action: "isEmailInUse", controller: "Home")]
        //[Remote(action: "VerifyName", controller: "Users", AdditionalFields = nameof())]
        [EmailAddress()]
        public string email { get; set; }


        [Required(ErrorMessage = "Name is required")]
        [MaxLength(10)]
        public string EmployeeName { get; set; }


        [Required(ErrorMessage = "Please select option")]
        public Dept Department { get; set; }



        [Required(ErrorMessage = "Salary is required")]
        public int Salary { get; set; }
        public IFormFile Photo { get; set; }
    }
}

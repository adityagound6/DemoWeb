using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWeb.Models
{
    public class Employee
    {
        [Required(ErrorMessage ="Email is reqired")]
        public string Email { get; set; }


        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(10)]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage ="Please select option")]
        public Dept Department { get; set; }
        
        [Required(ErrorMessage ="Salary is required")]
        
        public int Salary { get; set; }
        public string PhotoPath { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}

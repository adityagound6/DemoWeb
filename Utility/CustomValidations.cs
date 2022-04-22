using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace DemoWeb.Utility
{
    public class CustomValidations : ValidationAttribute
    {
        private readonly string allowDomain;
        public CustomValidations(string allowDomain)
        {
            this.allowDomain = allowDomain;
        }
        public override bool IsValid(object value)
        {
            string[] str = value.ToString().Split("@");
            return str[1].ToUpper() == allowDomain.ToUpper();
            //return base.IsValid(value);
        }
    }
}

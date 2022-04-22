using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWeb.Models
{
    interface IDepartment
    {
        public Department GetDepartmentById(int id);
    }
}

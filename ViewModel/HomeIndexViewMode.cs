using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoWeb.Models;

namespace DemoWeb.ViewModel
{
    public class HomeIndexViewMode
    {
        public IEnumerator<Employee> AllEmployee { get; set; }
        public string PageTitle { get; set; }
    }
}

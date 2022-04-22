using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWeb.ViewModel
{
    public class RoleListViewModel
    {
        public IEnumerator<IdentityRole> identityRole { get; set; }
    }
}

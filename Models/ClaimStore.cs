using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DemoWeb.Models
{
    public static class ClaimStore
    {
        public static List<Claim> AllClaim = new List<Claim>()
        {
            new Claim("Create Role","Create Role"),
            new Claim("Edit Role","Edit Role"),
            new Claim("Delete Role","Delete Role")
        };
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWeb.ViewModel
{
    public class UserClaimViewModel
    {
        public UserClaimViewModel()
        {
            Claim = new List<UserClaim>();
        }
        public List<UserClaim> Claim  { get; set; }
        public string UserId { get; set; }
    }
    public class UserClaim
    {
        public string ClaimType { get; set; }
        public bool IsSelected { get; set; }
    }
}

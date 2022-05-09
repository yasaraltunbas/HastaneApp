using System;
using Microsoft.AspNetCore.Identity;

namespace HastaneAPP.WebUI.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
    }
}
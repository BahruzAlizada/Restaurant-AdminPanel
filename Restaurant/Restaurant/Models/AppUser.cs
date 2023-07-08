using Microsoft.AspNetCore.Identity;
using System;

namespace Restaurant.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeactive { get; set; }
    }
}

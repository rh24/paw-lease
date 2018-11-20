using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace EcomProject_JimmyRebecca.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime AccountCreation { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public bool LovesCats { get; set; }

        // Navigation props:
        public ICollection<Cart> Carts { get; set; }
    }
}

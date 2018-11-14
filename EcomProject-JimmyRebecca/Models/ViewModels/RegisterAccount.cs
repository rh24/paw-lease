using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcomProject_JimmyRebecca.Models.ViewModels
{
    public class RegisterAccount
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime AccountCreation { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string PaswordConfirmation { get; set; }
    }
}

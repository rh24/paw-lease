using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EcomProject_JimmyRebecca.Models.ViewModels
{
    public class LoginVM
    {
        // Added View Model
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Enter Your Username or Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

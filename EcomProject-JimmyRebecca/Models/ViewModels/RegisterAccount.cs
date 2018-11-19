using System;
using System.ComponentModel.DataAnnotations;

namespace EcomProject_JimmyRebecca.Models.ViewModels
{
    public class RegisterAccount
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }


        public DateTime AccountCreation { get; set; } = DateTime.Now;

        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Do you love cats?")]
        public bool LovesCats { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The passwords don't match!")]
        [Display(Name = "Confirm Password")]
        public string PaswordConfirmation { get; set; }
    }
}

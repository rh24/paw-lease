using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcomProject_JimmyRebecca.Models.ViewModels
{
    public class Order
    {
        public string CreditCardNumber { get; set; }
        public int CartID { get; set; }

        // Shadow Property
        public Cart Cart { get; set; }

        public ICollection<LineItem> OrderItems { get; set; }

        // User information to capture
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Billing Address")]
        public string BillingAddress { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}

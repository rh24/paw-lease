using System.Collections.Generic;

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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}

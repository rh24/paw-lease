using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcomProject_JimmyRebecca.Models
{
    public class Product
    {
        public int ID { get; set; }
        [Required]
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal SuggestedDonation { get; set; }
        public bool IsCat { get; set; }

        // Navigation prop:
        public ICollection<LineItem> LineItems { get; set; }
    }
}

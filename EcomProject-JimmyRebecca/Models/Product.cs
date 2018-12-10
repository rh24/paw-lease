using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcomProject_JimmyRebecca.Models
{
    public class Product
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Product")]
        public string ProductName { get; set; }
        public string Description { get; set; }
        [Display(Name = "Suggested Donation")]
        public decimal SuggestedDonation { get; set; }
        [Display(Name = "Image URL")]
        public string image_url { get; set; }
        public bool IsCat { get; set; }

        // Navigation prop:
        public ICollection<LineItem> LineItems { get; set; }
    }
}

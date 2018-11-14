using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EcomProject_JimmyRebecca.Models
{
    public class Product
    {
        public int ID { get; set; }
        [Required]
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal SuggestedDonation { get; set; }
    }
}

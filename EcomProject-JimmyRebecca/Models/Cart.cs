﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcomProject_JimmyRebecca.Models
{
    public class Cart
    {
        // Primary key
        public int ID { get; set; }
        public bool OrderFulfilled { get; set; }

        // Navigation props:
        public ICollection<LineItem> LineItems { get; set; }

        // A cart belongs to a user
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}

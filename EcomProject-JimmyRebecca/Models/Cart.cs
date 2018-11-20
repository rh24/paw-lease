using System.Collections.Generic;

namespace EcomProject_JimmyRebecca.Models
{
    public class Cart
    {
        public int ID { get; set; }
        public int ApplicationUserID { get; set; }

        // Navigation props:
        public ICollection<LineItem> LineItems { get; set; }
        public ApplicationUser User { get; set; }
    }
}

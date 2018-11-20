namespace EcomProject_JimmyRebecca.Models
{
    /// <summary>
    /// Entity join model (Product + Cart) with payload (quantity).
    /// </summary>
    public class LineItem
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int CartID { get; set; }
        public int Quantity { get; set; }

        // Nav props:
        public Cart Cart { get; set; }
        public Product Product { get; set; }
    }
}

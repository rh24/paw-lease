using System.ComponentModel.DataAnnotations;

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
        public Quantity Quantity { get; set; }

        // Nav props:
        public Cart Cart { get; set; }
        public Product Product { get; set; }
    }

    public enum Quantity
    {
        [Display(Name = "0")]
        zero,
        [Display(Name = "1")]
        one,
        [Display(Name = "2")]
        two,
        [Display(Name = "3")]
        three,
        [Display(Name = "4")]
        four,
        [Display(Name = "5")]
        five,
        [Display(Name = "6")]
        six,
        [Display(Name = "7")]
        seven,
        [Display(Name = "8")]
        eight,
        [Display(Name = "9")]
        nine,
        [Display(Name = "10")]
        ten,
        [Display(Name = "11")]
        eleven,
        [Display(Name = "12")]
        twelve
    }
}

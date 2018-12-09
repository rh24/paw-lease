using EcomProject_JimmyRebecca.Models;
using Microsoft.EntityFrameworkCore;

namespace EcomProject_JimmyRebecca.Data
{
    public class ProductDBContext : DbContext
    {
        public ProductDBContext(DbContextOptions<ProductDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            mb.Entity<Product>().HasData(
                new Product
                {
                    ID = 1,
                    ProductName = "Tabby",
                    Description = "It's garfield, who doesn't want garfield???",
                    SuggestedDonation = 900,
                    IsCat = true,
                    image_url = "/assets/cats/tabby.jpg"
                },
                new Product
                {
                    ID = 2,
                    ProductName = "Laser Pointer",
                    Description = "Endless fun! For your cat too, I guess...",
                    SuggestedDonation = 30,
                    IsCat = false,
                    image_url = "/assets/cats/laser_cat.jpg"
                },
                new Product
                {
                    ID = 3,
                    ProductName = "Meowjiuana",
                    Description = "When you just don't wanna deal with them anymore",
                    SuggestedDonation = 35,
                    IsCat = false,
                    image_url = "/assets/cats/meowijuana.jpg"
                },
                new Product
                {
                    ID = 4,
                    ProductName = "Scottish Fold",
                    Description = "Scottish folds are normal cats that fold their ears.",
                    SuggestedDonation = 1100,
                    IsCat = true,
                    image_url = "/assets/cats/scottish_fold.png"
                },
                new Product
                {
                    ID = 5,
                    ProductName = "Cat-shirt",
                    Description = "They might not like it, but it'll keep them warm",
                    SuggestedDonation = 40,
                    IsCat = false,
                    image_url = "/assets/cats/sweater.jpg"
                },
                new Product
                {
                    ID = 6,
                    ProductName = "Simba Outfit",
                    Description = "This is how lion king started.",
                    SuggestedDonation = 75,
                    IsCat = false,
                    image_url = "/assets/cats/lion_king.jpg"
                },
                new Product
                {
                    ID = 7,
                    ProductName = "Fat Cat",
                    Description = "It's like a normal cat, but fat.",
                    SuggestedDonation = 900,
                    IsCat = true,
                    image_url = "/assets/cats/fat_cat.jpg"
                },
                new Product
                {
                    ID = 8,
                    ProductName = "Cat-stle",
                    Description = "Buy a cat a home, they'll leave you forever.",
                    SuggestedDonation = 120,
                    IsCat = false,
                    image_url = "/assets/cats/cat-stle.gif"
                },
                new Product
                {
                    ID = 9,
                    ProductName = "Cat Food",
                    Description = "When you want mindless zombies...",
                    SuggestedDonation = 50,
                    IsCat = false,
                    image_url = "/assets/cats/cat-food.png"
                },
                new Product
                {
                    ID = 10,
                    ProductName = "Russian Blue",
                    Description = "If you enjoy vodka, this is your friend.",
                    SuggestedDonation = 900,
                    IsCat = true,
                    image_url = "/assets/cats/russian-blue.jpg"
                }
            );
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
        public DbSet<EcomProject_JimmyRebecca.Models.ViewModels.UserProfile> UserProfile { get; set; }
    }
}

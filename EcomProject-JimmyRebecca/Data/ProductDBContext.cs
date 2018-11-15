using EcomProject_JimmyRebecca.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcomProject_JimmyRebecca.Data
{
    public class ProductDBContext : DbContext
    {
        public ProductDBContext(DbContextOptions<ProductDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Product>().HasData(
                new Product
                {
                    ID = 1,
                    ProductName = "Tabby",
                    Description = "It's garfield, who doesn't want garfield???",
                    SuggestedDonation = 900
                },
                new Product
                {
                    ID = 2,
                    ProductName = "Laser Pointer",
                    Description = "Endless fun! For your cat too, I guess...",
                    SuggestedDonation = 30
                },
                new Product
                {
                    ID = 3,
                    ProductName = "Catnip",
                    Description = "When you just don't wanna deal with them anymore",
                    SuggestedDonation = 35
                },
                new Product
                {
                    ID = 4,
                    ProductName = "Scottish Fold",
                    Description = "Scottish folds are normal cats that fold their ears.",
                    SuggestedDonation = 1100
                },
                new Product
                {
                    ID = 5,
                    ProductName = "Cat-shirt",
                    Description = "They might not like it, but it'll keep them warm",
                    SuggestedDonation = 40
                },
                new Product
                {
                    ID = 6,
                    ProductName = "Cat Post",
                    Description = "This is where lion king started",
                    SuggestedDonation = 75
                },
                new Product
                {
                    ID = 7,
                    ProductName = "Bengal",
                    Description = "It's like a bengal, but less dangerous.",
                    SuggestedDonation = 900
                },
                new Product
                {
                    ID = 8,
                    ProductName = "Cat-stle",
                    Description = "Buy a cat a home, they'll leave you forever.",
                    SuggestedDonation = 120
                },
                new Product
                {
                    ID = 9,
                    ProductName = "Cat Food",
                    Description = "When you want mindless zombies...",
                    SuggestedDonation = 50
                },
                new Product
                {
                    ID = 10,
                    ProductName = "Russian Blue",
                    Description = "If you enjoy vodka, this is your friend.",
                    SuggestedDonation = 900
                }
            );
        }

        DbSet<Product> Products { get; set; }
    }
}

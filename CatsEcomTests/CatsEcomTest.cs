using System;
using Xunit;
using EcomProject_JimmyRebecca.Models;

namespace CatsEcomTests
{
    public class CatsEcomTest
    {
        [Theory]
        [InlineData("Tabby")]
        [InlineData("Bengal")]
        [InlineData("Black Cat")]
        public void ProductGetTest(string name)
        {
            Product prod = new Product()
            {
                ID = 1,
                ProductName = name,
                Description = "cat",
                SuggestedDonation = 900,
                IsCat = true
            };

            Assert.True(prod.ProductName == name);
        }

        [Theory]
        [InlineData("Tabby")]
        [InlineData("Bengal")]
        [InlineData("Black Cat")]
        public void ProductSetTest(string name)
        {
            Product prod = new Product()
            {
                ID = 1,
                ProductName = "kitty",
                Description = "cat",
                SuggestedDonation = 900,
                IsCat = true
            };

            prod.ProductName = name;

            Assert.True(prod.ProductName == name);
        }
    }
}

using EcomProject_JimmyRebecca.Models;
using System;
using Xunit;

namespace Sprint1_Tests
{
    /// <summary>
    /// Test getters and setters for all models
    /// </summary>
    public class Sprint1_Tests
    {
        /// <summary>
        /// Test getters on ApplicationUser model
        /// </summary>
        [Fact]
        public void GetApplicationUser()
        {
            ApplicationUser au = new ApplicationUser
            {
                FirstName = "Jane",
                LastName = "Doe",
                Address = "1 Cat St",
                Birthday = new DateTime(1992, 09, 24),
                LovesCats = true
            };

            Assert.Equal("Jane", au.FirstName);
            Assert.Equal("Doe", au.LastName);
            Assert.Equal("1 Cat St", au.Address);
            Assert.Equal("9/24/1992 12:00:00 AM", au.Birthday.ToString());
            Assert.True(au.LovesCats);
        }

        /// <summary>
        /// Test setters on ApplicationUser model
        /// </summary>
        [Fact]
        public void SetApplicationUser()
        {
            ApplicationUser au = new ApplicationUser
            {
                FirstName = "Jane",
                LastName = "Doe",
                Address = "1 Cat St",
                Birthday = new DateTime(1992, 09, 24),
                LovesCats = true
            };

            au.FirstName = "John";
            au.LastName = "Joe";
            au.Address = "2 Dog St";
            au.Birthday = new DateTime(1990, 10, 01);
            au.LovesCats = false;

            Assert.Equal("John", au.FirstName);
            Assert.Equal("Joe", au.LastName);
            Assert.Equal("2 Dog St", au.Address);
            Assert.Equal("10/1/1990 12:00:00 AM", au.Birthday.ToString());
            Assert.False(au.LovesCats);
        }

        /// <summary>
        /// Tests the products get
        /// </summary>
        /// <param name="name">the name of the product</param>
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

        /// <summary>
        /// Tests the product set
        /// </summary>
        /// <param name="name">the name of the cat</param>
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

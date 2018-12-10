using EcomProject_JimmyRebecca.Data;
using EcomProject_JimmyRebecca.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace Sprint3_CRUD_Tests
{
    public class CartsTest
    {
        /// <summary>
        /// Tests getting Cart object property. OrderFulFilled defaults to false.
        /// </summary>
        [Fact]
        public void GetCartProps()
        {
            Cart cart = new Cart { ID = 2 };

            Assert.False(cart.OrderFulfilled);
        }

        /// <summary>
        /// Test setting Cart object property.
        /// </summary>
        [Fact]
        public void SetCartProps()
        {
            Cart cart = new Cart { ID = 1, OrderFulfilled = false };
            cart.OrderFulfilled = true;

            Assert.True(cart.OrderFulfilled);
        }

        /// <summary>
        /// Tests creating a cart
        /// </summary>
        [Fact]
        public void CreateCart()
        {
            DbContextOptions<ProductDBContext> options = new DbContextOptionsBuilder<ProductDBContext>().UseInMemoryDatabase("Cart").Options;

            using (ProductDBContext context = new ProductDBContext(options))
            {
                Cart cart = new Cart();
                context.Add(cart);
                cart.ID = 2;
                context.SaveChanges();

                var foundCart = context.Carts.FirstOrDefault(c => c.ID == 2);

                Assert.NotNull(foundCart);
            }
        }

        /// <summary>
        /// Tests reading from the carts table
        /// </summary>
        [Fact]
        public void ReadFromCarts()
        {
            DbContextOptions<ProductDBContext> options = new DbContextOptionsBuilder<ProductDBContext>().UseInMemoryDatabase("Cart").Options;

            using (ProductDBContext context = new ProductDBContext(options))
            {
                Cart cart = new Cart();
                context.Add(cart);
                cart.ID = 5;
                context.SaveChanges();

                var foundCart = context.Carts.FirstOrDefault(c => c.ID == 5);
                var cartID = foundCart.ID;

                Assert.Equal(5, cartID);
            }
        }

        /// <summary>
        /// Tests updating a cart in the db
        /// </summary>
        [Fact]
        public void UpdateCarts()
        {
            DbContextOptions<ProductDBContext> options = new DbContextOptionsBuilder<ProductDBContext>().UseInMemoryDatabase("Cart").Options;

            using (ProductDBContext context = new ProductDBContext(options))
            {
                Cart cart = new Cart();
                context.Add(cart);
                cart.ID = 8;
                context.SaveChanges();

                var foundCart = context.Carts.FirstOrDefault(c => c.ID == 8);
                bool oldStatus = foundCart.OrderFulfilled;

                // Change status to true
                foundCart.OrderFulfilled = true;
                context.SaveChanges();

                Assert.NotEqual(oldStatus, foundCart.OrderFulfilled);
            }
        }

        /// <summary>
        /// Tests updating a cart in the db
        /// </summary>
        [Fact]
        public void DeleteCart()
        {
            DbContextOptions<ProductDBContext> options = new DbContextOptionsBuilder<ProductDBContext>().UseInMemoryDatabase("Cart").Options;

            using (ProductDBContext context = new ProductDBContext(options))
            {
                Cart cart = new Cart();
                context.Add(cart);
                cart.ID = 10;
                context.SaveChanges();

                context.Carts.Remove(cart);
                context.SaveChanges();

                var foundCart = context.Carts.Find(cart.ID);

                Assert.Null(foundCart);
            }
        }
    }
}

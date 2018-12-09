using EcomProject_JimmyRebecca.Data;
using EcomProject_JimmyRebecca.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Sprint3_CRUD_Tests
{
    public class UnitTest1
    {
        /// <summary>
        /// Create
        /// </summary>
        [Fact]
        public void Test1()
        {
            DbContextOptions<ProductDBContext> options = new DbContextOptionsBuilder<ProductDBContext>().UseInMemoryDatabase("Cart").Options;

            using (ProductDBContext context = new ProductDBContext(options))
            {
                Cart cart = new Cart();
                context.Add(cart);
                cart.ID = 2;
                context.SaveChanges();

                var foundCart = context.Carts.FirstOrDefaultAsync(c => c.ID == 2);

                Assert.NotNull(foundCart);
            }
        }
    }
}

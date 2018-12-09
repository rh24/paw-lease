using EcomProject_JimmyRebecca.Data;
using EcomProject_JimmyRebecca.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Sprint3_CRUD_Tests
{
    public class LineItemsTest
    {
        /// <summary>
        /// Tests the getter for the line items
        /// </summary>
        [Fact]
        public void GetLineItemTest()
        {
            LineItem li = new LineItem()
            {
                ID = 1,
                ProductID = 1,
                Quantity = Quantity.two
            };

            Assert.Equal(1, li.ID);
        }

        /// <summary>
        /// Tests the setter for line items
        /// </summary>
        [Fact]
        public void SetLineItemTest()
        {
            LineItem li = new LineItem()
            {
                ID = 1,
                ProductID = 1,
                Quantity = Quantity.two
            };

            li.ProductID = 3;

            Assert.Equal(3, li.ProductID);
        }

        /// <summary>
        /// Tests adding LI to the db
        /// </summary>
        [Fact]
        public async void AddLineItemToDBTest()
        {
            DbContextOptions<ProductDBContext> options = new DbContextOptionsBuilder<ProductDBContext>().UseInMemoryDatabase("AddLI").Options;
            using (ProductDBContext context = new ProductDBContext(options))
            {
                LineItem li = new LineItem()
                {
                    ID = 1,
                    ProductID = 1,
                    Quantity = Quantity.two
                };

                context.LineItems.Add(li);
                await context.SaveChangesAsync();

                LineItem result = await context.LineItems.FirstOrDefaultAsync(item => item.ID == 1);

                Assert.Equal(1, result.ProductID);
            }
        }

        /// <summary>
        /// Tests updating LI in the DB
        /// </summary>
        [Fact]
        public async void UpdateLineItemDBTest()
        {
            DbContextOptions<ProductDBContext> options = new DbContextOptionsBuilder<ProductDBContext>().UseInMemoryDatabase("UpdateLI").Options;
            using (ProductDBContext context = new ProductDBContext(options))
            {
                LineItem li = new LineItem()
                {
                    ID = 1,
                    ProductID = 1,
                    Quantity = Quantity.two
                };

                context.LineItems.Add(li);
                await context.SaveChangesAsync();

                LineItem found = await context.LineItems.FirstOrDefaultAsync(item => item.ID == 1);

                found.ProductID = 2;
                context.LineItems.Update(found);
                await context.SaveChangesAsync();

                LineItem result = await context.LineItems.FirstOrDefaultAsync(item => item.ID == 1);

                Assert.Equal(2, result.ProductID);
            }
        }
    }
}

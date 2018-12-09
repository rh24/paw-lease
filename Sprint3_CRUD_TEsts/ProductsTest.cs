using EcomProject_JimmyRebecca.Data;
using EcomProject_JimmyRebecca.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Sprint3_CRUD_Tests
{
    public class ProductsTest
    {
        /// <summary>
        /// Tests the getter for the line items
        /// </summary>
        [Fact]
        public void GetProductTest()
        {
            Product cat = new Product()
            {
                ID = 1,
                ProductName = "Cat",
                Description = "A kitty cat"
            };

            Assert.Equal(1, cat.ID);
        }

        /// <summary>
        /// Tests the setter for line items
        /// </summary>
        [Fact]
        public void SetProductTest()
        {
            Product cat = new Product()
            {
                ID = 1,
                ProductName = "Cat",
                Description = "A kitty cat"
            };

            cat.ProductName = "Dog";

            Assert.Equal("Dog", cat.ProductName);
        }

        /// <summary>
        /// Tests creating Product to the db
        /// </summary>
        [Fact]
        public async void CreateProductDBTest()
        {
            DbContextOptions<ProductDBContext> options = new DbContextOptionsBuilder<ProductDBContext>().UseInMemoryDatabase("CreateProduct").Options;
            using (ProductDBContext context = new ProductDBContext(options))
            {
                Product cat = new Product()
                {
                    ID = 1,
                    ProductName = "Cat",
                    Description = "A kitty cat"
                };

                context.Products.Add(cat);
                await context.SaveChangesAsync();

                Product result = await context.Products.FirstOrDefaultAsync(item => item.ID == 1);

                Assert.Equal("Cat", result.ProductName);
            }
        }

        /// <summary>
        /// Tests updating LI in the DB
        /// </summary>
        [Fact]
        public async void UpdateProductDBTest()
        {
            DbContextOptions<ProductDBContext> options = new DbContextOptionsBuilder<ProductDBContext>().UseInMemoryDatabase("UpdateProduct").Options;
            using (ProductDBContext context = new ProductDBContext(options))
            {
                Product cat = new Product()
                {
                    ID = 1,
                    ProductName = "Cat",
                    Description = "A kitty cat"
                };

                context.Products.Add(cat);
                await context.SaveChangesAsync();

                Product found = await context.Products.FirstOrDefaultAsync(item => item.ID == 1);

                found.ProductName = "Dog";
                context.Products.Update(found);
                await context.SaveChangesAsync();

                Product result = await context.Products.FirstOrDefaultAsync(item => item.ID == 1);

                Assert.Equal("Dog", result.ProductName);
            }
        }

        /// <summary>
        /// Tests deleting LI in the DB
        /// </summary>
        [Fact]
        public async void DeleteProductDBTest()
        {
            DbContextOptions<ProductDBContext> options = new DbContextOptionsBuilder<ProductDBContext>().UseInMemoryDatabase("DeleteProduct").Options;
            using (ProductDBContext context = new ProductDBContext(options))
            {
                Product cat = new Product()
                {
                    ID = 1,
                    ProductName = "Cat",
                    Description = "A kitty cat"
                };

                context.Products.Add(cat);
                await context.SaveChangesAsync();

                Product found = await context.Products.FirstOrDefaultAsync(item => item.ID == 1);
                context.Products.Remove(found);
                await context.SaveChangesAsync();

                Product result = await context.Products.FirstOrDefaultAsync(item => item.ID == 1);

                Assert.True(result == null);
            }
        }
    }
}

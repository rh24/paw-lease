using EcomProject_JimmyRebecca.Data;
using EcomProject_JimmyRebecca.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcomProject_JimmyRebecca.Models.Services
{
    public class ProductService : IProduct
    {
        private readonly ProductDBContext _context;

        public ProductService(ProductDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a product to the db
        /// </summary>
        /// <param name="product">Product object</param>
        /// <returns></returns>
        public async Task CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Removes a product from the db
        /// </summary>
        /// <param name="product">Product object</param>
        /// <returns></returns>
        public async Task DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Grabs a product by its primary key
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>Product object</returns>
        public async Task<Product> GetProduct(int? id)
        {
            return await _context.Products
                 .FirstOrDefaultAsync(m => m.ID == id);
        }

        /// <summary>
        /// Gets a list of all products in the db
        /// </summary>
        /// <returns>IEnumerable collection of Products</returns>
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        /// <summary>
        /// Updates a product in the db
        /// </summary>
        /// <param name="product">Product object</param>
        /// <returns></returns>
        public async Task UpdateProduct(Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}

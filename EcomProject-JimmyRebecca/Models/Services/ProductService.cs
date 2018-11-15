using EcomProject_JimmyRebecca.Data;
using EcomProject_JimmyRebecca.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> GetProduct(int? id)
        {
           return await _context.Products
                .FirstOrDefaultAsync(m => m.ID == id);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task UpdateProduct(Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}

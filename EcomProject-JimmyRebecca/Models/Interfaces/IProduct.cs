using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcomProject_JimmyRebecca.Models.Interfaces
{
    public interface IProduct
    {
        // interface for products
        Task CreateProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(Product product);
        Task<Product> GetProduct(int? id);
        Task<IEnumerable<Product>> GetProducts();
    }
}

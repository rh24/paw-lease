using EcomProject_JimmyRebecca.Data;
using EcomProject_JimmyRebecca.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcomProject_JimmyRebecca.Models.Services
{
    public class LineItemService : ILineItem
    {
        private readonly ProductDBContext _context;

        public LineItemService(ProductDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a LineItem to the database
        /// </summary>
        /// <param name="lineitem">LineItem object</param>
        /// <returns></returns>
        public async Task CreateLineItem(LineItem lineitem)
        {
            _context.LineItems.Add(lineitem);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Removes a LineItem from the database
        /// </summary>
        /// <param name="lineitem">LineItem object</param>
        /// <returns></returns>
        public async Task DeleteLineItem(LineItem lineitem)
        {
            _context.LineItems.Remove(lineitem);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets a LineItem object by its primary key
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns></returns>
        public async Task<LineItem> GetLineItem(int? id)
        {
            return await _context.LineItems.Include(li => li.Product).FirstOrDefaultAsync(li => li.ID == id);
        }

        /// <summary>
        /// Gets a line item by its associated Cart ID and Product ID foriegn keys.
        /// </summary>
        /// <param name="cartId">Cart ID</param>
        /// <param name="productId">Product ID</param>
        /// <returns>LineItem object</returns>
        public async Task<LineItem> GetLineItemByProduct(int cartId, int productId)
        {
            return await _context.LineItems.Include(li => li.Product).FirstOrDefaultAsync(li => li.CartID == cartId && li.ProductID == productId);
        }

        /// <summary>
        /// Gets all line items
        /// </summary>
        /// <returns>IEnumerable collection of LineItems</returns>
        public async Task<IEnumerable<LineItem>> GetLineItems()
        {
            return await _context.LineItems.Include(li => li.Product).ToListAsync();
        }

        /// <summary>
        /// Gets all the LineItems in a specific cart
        /// </summary>
        /// <param name="id">Cart ID</param>
        /// <returns>IEnumerable collection of LineItems</returns>
        public async Task<IEnumerable<LineItem>> GetLineItems(int id)
        {
            return await _context.LineItems.Include(li => li.Product).Where(li => li.CartID == id).ToListAsync();
        }

        /// <summary>
        /// Updates a LineItem in the database
        /// </summary>
        /// <param name="lineitem">LineItem object</param>
        /// <returns></returns>
        public async Task UpdateLineItem(LineItem lineitem)
        {
            _context.LineItems.Update(lineitem);
            await _context.SaveChangesAsync();
        }
    }
}

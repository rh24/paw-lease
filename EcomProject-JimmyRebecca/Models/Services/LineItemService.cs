using EcomProject_JimmyRebecca.Data;
using EcomProject_JimmyRebecca.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace EcomProject_JimmyRebecca.Models.Services
{
    public class LineItemService : ILineItem
    {
        private readonly ProductDBContext _context;

        public LineItemService(ProductDBContext context)
        {
            _context = context;
        }

        public async Task CreateLineItem(LineItem lineitem)
        {
            _context.LineItems.Add(lineitem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLineItem(LineItem lineitem)
        {
            _context.LineItems.Remove(lineitem);
            await _context.SaveChangesAsync();
        }

        public async Task<LineItem> GetLineItem(int? id)
        {
            return await _context.LineItems.Include(li => li.Product).FirstOrDefaultAsync(li => li.ID == id);
        }

        public async Task<LineItem> GetLineItemByProduct(int cartId, int productId)
        {
            return await _context.LineItems.Include(li => li.Product).FirstOrDefaultAsync(li => li.CartID == cartId && li.ProductID == productId);
        }

        public async Task<IEnumerable<LineItem>> GetLineItems()
        {
            return await _context.LineItems.Include(li => li.Product).ToListAsync();
        }

        public async Task<IEnumerable<LineItem>> GetLineItems(int id)
        {
            return await _context.LineItems.Include(li => li.Product).Where(li => li.CartID == id).ToListAsync();
        }

        public async Task UpdateLineItem(LineItem lineitem)
        {
            _context.LineItems.Update(lineitem);
            await _context.SaveChangesAsync();
        }
    }
}

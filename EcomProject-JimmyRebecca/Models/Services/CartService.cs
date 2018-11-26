using EcomProject_JimmyRebecca.Data;
using EcomProject_JimmyRebecca.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcomProject_JimmyRebecca.Models.Services
{
    public class CartService : ICart
    {
        private readonly ProductDBContext _context;

        public CartService(ProductDBContext context)
        {
            _context = context;
        }

        public async Task CreateCart(Cart cart)
        {
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCart(Cart cart)
        {
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
        }

        public async Task<Cart> GetCart(int? id)
        {
            return await _context.Carts.FirstOrDefaultAsync(c => c.ID == id);
        }

        public async Task<Cart> GetCartByUserId(string userId)
        {
            return await _context.Carts.FirstOrDefaultAsync(c => c.User.Id == userId);
        }

        public async Task<IEnumerable<Cart>> GetCarts()
        {
            return await _context.Carts.ToListAsync();
        }

        public async Task UpdateCart(Cart cart)
        {
            _context.Update(cart);
            await _context.SaveChangesAsync();
        }
    }
}

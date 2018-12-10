using EcomProject_JimmyRebecca.Data;
using EcomProject_JimmyRebecca.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// Adds a cart to the db context
        /// </summary>
        /// <param name="cart">cart object to add</param>
        /// <returns></returns>
        public async Task CreateCart(Cart cart)
        {
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Removes a cart from the db context
        /// </summary>
        /// <param name="cart">cart object to remove</param>
        /// <returns></returns>
        public async Task DeleteCart(Cart cart)
        {
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets a cart by a specific ID
        /// </summary>
        /// <param name="id">ID of cart to grab</param>
        /// <returns>Cart object</returns>
        public async Task<Cart> GetCart(int? id)
        {
            return await _context.Carts.Include(c => c.LineItems).FirstOrDefaultAsync(c => c.ID == id);
        }

        /// <summary>
        /// Grabs a cart by a user's ID
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>Cart object</returns>
        public async Task<Cart> GetCartByUserId(string userId)
        {
            return await _context.Carts.Include(c => c.LineItems).FirstOrDefaultAsync(c => c.User.Id == userId && c.OrderFulfilled == false);
        }

        /// <summary>
        /// Get all carts
        /// </summary>
        /// <returns>IEnumerable collection of Carts</returns>
        public async Task<IEnumerable<Cart>> GetCarts()
        {
            return await _context.Carts.Include(c => c.LineItems).ToListAsync();
        }

        /// <summary>
        /// Updates a cart object in the database
        /// </summary>
        /// <param name="cart">Cart to update</param>
        /// <returns></returns>
        public async Task UpdateCart(Cart cart)
        {
            _context.Update(cart);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Grabs all the carts that have been checked out.
        /// </summary>
        /// <returns>IEnumerable collection of Carts</returns>
        public async Task<IEnumerable<Cart>> GetPastOrdersCarts()
        {
            // Includes the line items in the cart the includes the products within the line items
            return await _context.Carts
                .Include(c => c.LineItems)
                .ThenInclude(li => li.Product)
                .OrderByDescending(c => c.ID)
                .Take(10)
                .Where(c => c.LineItems.Count > 0 && c.OrderFulfilled == true)
                .ToListAsync();
        }

        /// <summary>
        /// Method overload that gets the checked out carts with a specific User's ID.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns></returns>
        public async Task<IEnumerable<Cart>> GetPastOrdersCarts(string userId)
        {
            // Includes the line items in the cart the includes the products within the line items
            return await _context.Carts
                .Include(c => c.LineItems)
                .ThenInclude(li => li.Product)
                .OrderByDescending(c => c.ID)
                .Take(10)
                .Where(c => c.LineItems.Count > 0 && c.OrderFulfilled == true && c.User.Id == userId)
                .ToListAsync();
        }
    }
}

using EcomProject_JimmyRebecca.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EcomProject_JimmyRebecca.Components
{
    public class ViewCart : ViewComponent
    {
        private readonly ProductDBContext _context;

        /// <summary>
        /// Constructor injection
        /// </summary>
        /// <param name="context">ProductDBContext</param>
        public ViewCart(ProductDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to query the db context for cart that belongs to current signed in user where cart order is unfulfilled.
        /// </summary>
        /// <param name="userID">Current user ID</param>
        /// <returns>View that calls this method</returns>
        public async Task<IViewComponentResult> InvokeAsync(string userID)
        {
            var cart = await _context.Carts
                .Include(c => c.LineItems)
                .Include(c => c.User)
                .Where(c => c.User.ID == userID && !c.OrderFulfilled).ToListAsync();

            return View(cart);
        }
    }
}

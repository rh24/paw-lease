using EcomProject_JimmyRebecca.Data;
using EcomProject_JimmyRebecca.Models;
using EcomProject_JimmyRebecca.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EcomProject_JimmyRebecca.Components
{
    public class ViewCart : ViewComponent
    {
        private readonly ProductDBContext _context;
        private readonly ApplicationDbContext _userContext;
        private readonly ILineItem _liContext;


        /// <summary>
        /// Constructor injection
        /// </summary>
        /// <param name="context">ProductDBContext</param>
        public ViewCart(ProductDBContext context, ApplicationDbContext userContext, ILineItem liContext)
        {
            _context = context;
            _userContext = userContext;
            _liContext = liContext;
        }

        /// <summary>
        /// Method to query the db context for cart that belongs to current signed in user where cart order is unfulfilled.
        /// </summary>
        /// <param name="userID">Current user ID</param>
        /// <returns>View that calls this method</returns>
        public async Task<IViewComponentResult> InvokeAsync(string userId)
        {
            ApplicationUser user = await _userContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var foundCart = await _context.Carts.FirstOrDefaultAsync(c => c.OrderFulfilled == false && c.User == user);
            if (user != null && foundCart != null)
            {
                Cart cart = await _context.Carts
                    .Include(c => c.LineItems)
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(c => c.User.Id == user.Id && !c.OrderFulfilled);

                var lineItems = await _liContext.GetLineItems(cart.ID);
                decimal cartTotal = lineItems.Sum(li => li.Product.SuggestedDonation * (int)li.Quantity);
                ViewBag.CartTotal = cartTotal;
                return View(lineItems);
            }

            return View();
        }
    }
}

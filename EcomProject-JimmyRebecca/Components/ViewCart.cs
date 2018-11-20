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
        /// Method to query the db context for LineItem objects where the cart ID and Product ID is equal to the values passed in. The arguments will be provided by the view that invokes this method.
        /// </summary>
        /// <param name="cartID">Cart ID</param>
        /// <param name="productID">Product ID</param>
        /// <returns>View that calls this method</returns>
        public async Task<IViewComponentResult> InvokeAsync(int cartID, int productID)
        {
            var lineItems = await _context.LineItems.Where(li => li.Product.ID == productID && li.Cart.ID == cartID).ToListAsync();

            return View(lineItems);
        }
    }
}

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

        public ViewCart(ProductDBContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int cartID, int productID)
        {
            var lineItems = await _context.LineItems.Where(li => li.Product.ID == productID && li.Cart.ID == cartID).ToListAsync();

            return View(lineItems);
        }
    }
}

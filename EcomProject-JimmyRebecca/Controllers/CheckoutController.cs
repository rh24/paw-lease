using EcomProject_JimmyRebecca.Models;
using EcomProject_JimmyRebecca.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EcomProject_JimmyRebecca.Controllers
{
    public class CheckoutController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly ICart _context;

        public CheckoutController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ICart context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        /// <summary>
        /// This method brings in all of the items in the user's cart that they have purchased and passes it along to the Receipt.cshtml view.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Receipt(int cartId)
        {
            var cart = await _context.GetCart(cartId);
            return View(cart);
        }
    }
}

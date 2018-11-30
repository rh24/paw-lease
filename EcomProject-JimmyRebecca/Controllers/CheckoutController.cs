using EcomProject_JimmyRebecca.Models;
using EcomProject_JimmyRebecca.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcomProject_JimmyRebecca.Controllers
{
    public class CheckoutController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly ILineItem _context;
        private readonly IEmailSender _email;

        public CheckoutController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILineItem context, IEmailSender email)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _email = email;
        }

        /// <summary>
        /// This method brings in all of the items in the user's cart that they have purchased and passes it along to the Receipt.cshtml view.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Receipt(int cartId)
        {
            var userId = _userManager.GetUserId(User);
            var lineItems = await _context.GetLineItems(cartId);
            decimal cartTotal = lineItems.Sum(li => li.Product.SuggestedDonation * (int)li.Quantity);
            ViewBag.CartTotal = cartTotal;
            return View(lineItems);
        }

        [HttpGet]
        public async Task<IActionResult> OrderProcessed(int cartId)
        {
            var user = await _userManager.GetUserAsync(User);
            var lineItems = await _context.GetLineItems(cartId);
            decimal cartTotal = lineItems.Sum(li => li.Product.SuggestedDonation * (int)li.Quantity);

            await _email.SendEmailAsync(user.Email, "Order Confirmation", CreateEmailString(lineItems, cartTotal));
            
            return View();
        }

        private string CreateEmailString(IEnumerable<LineItem> lineItems, decimal total)
        {
            string email = $"<h2>Receipt for Order {lineItems.First().CartID}</h2>";
            foreach(LineItem li in lineItems)
            {
                email += $"<div><h5>{li.Product.ProductName}</h5><p> Price: {li.Product.SuggestedDonation}</p><p>Quantity: {(int)li.Quantity}</p></div>";
            }
            email += $"<h4>Total: {total}</h4>";
            return email;
        }
    }
}

﻿using EcomProject_JimmyRebecca.Models;
using EcomProject_JimmyRebecca.Models.Interfaces;
using EcomProject_JimmyRebecca.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly ICart _cart;

        public CheckoutController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILineItem context, IEmailSender email, ICart cart)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _email = email;
            _cart = cart;
        }

        /// <summary>
        /// This method brings in all of the items in the user's cart that they have purchased and passes it along to the Receipt.cshtml view.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Receipt(Order order)
        {
            var userId = _userManager.GetUserId(User);
            var lineItems = await _context.GetLineItems(order.CartID);
            decimal cartTotal = lineItems.Sum(li => li.Product.SuggestedDonation * (int)li.Quantity);
            ViewBag.CartTotal = cartTotal;
            order.OrderItems = lineItems.ToList();
            return View(order);
        }

        /// <summary>
        /// Processes the order
        /// </summary>
        /// <param name="cartId">Gets the cart id to process the order</param>
        /// <returns>Returns the processed order view</returns>
        [HttpGet]
        public async Task<IActionResult> OrderProcessed(int cartId)
        {
            var user = await _userManager.GetUserAsync(User);
            var lineItems = await _context.GetLineItems(cartId);

            Cart newCart = new Cart
            {
                UserID = user.Id
            };

            // creates a new cart for the user
            await _cart.CreateCart(newCart);

            // fulfills the old cart
            var cart = await _cart.GetCart(cartId);
            cart.OrderFulfilled = true;
            await _cart.UpdateCart(cart);

            // gets the total for the cart
            decimal cartTotal = lineItems.Sum(li => li.Product.SuggestedDonation * (int)li.Quantity);

            // sends an email to confirm the order
            await _email.SendEmailAsync(user.Email, "Order Confirmation", CreateEmailString(lineItems, cartTotal));

            return View();
        }

        /// <summary>
        /// Checks out and sends to a page to capture user information
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            // creates a select list that contains fake credit card information
            var fakeCreditCardNumbers = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem { Selected = true, Text = "Visa", Value = "xxxx-xxxx-xxxx-1460" },
                    new SelectListItem { Selected = false, Text = "MasterCard", Value = "xxxx-xxxx-xxxx-3247" }
                }, "Value", "Text");

            ViewBag.FakeCreditCardNumbers = fakeCreditCardNumbers;

            var cart = await _cart.GetCartByUserId(_userManager.GetUserId(User));

            ViewBag.Cart = cart;

            return View();
        }

        /// <summary>
        /// Gets the receipt for the order
        /// </summary>
        /// <param name="order">Takes in an order model</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostReceipt(Order order)
        {
            var userId = _userManager.GetUserId(User);
            var lineItems = await _context.GetLineItems(order.CartID);
            decimal cartTotal = lineItems.Sum(li => li.Product.SuggestedDonation * (int)li.Quantity);
            ViewBag.CartTotal = cartTotal;
            return RedirectToAction("Receipt", order);
        }

        /// <summary>
        /// creates the html for the email
        /// </summary>
        /// <param name="lineItems">the items in the cart</param>
        /// <param name="total">the total for the receipt</param>
        /// <returns>returns an string of html for the email</returns>
        private string CreateEmailString(IEnumerable<LineItem> lineItems, decimal total)
        {
            string email = $"<h2>Receipt for Order {lineItems.First().CartID}</h2>";

            // adds onto the string for each item
            foreach (LineItem li in lineItems)
            {
                email += $"<div><h5>{li.Product.ProductName}</h5><p> Price: {li.Product.SuggestedDonation}</p><p>Quantity: {(int)li.Quantity}</p></div>";
            }
            email += $"<h4>Total: {total}</h4>";
            return email;
        }
    }
}

﻿using EcomProject_JimmyRebecca.Data;
using EcomProject_JimmyRebecca.Models;
using EcomProject_JimmyRebecca.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EcomProject_JimmyRebecca.Controllers
{
    public class LineItemsController : Controller
    {
        private readonly ILineItem _context;
        private readonly ApplicationDbContext _user;
        private readonly ProductDBContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public LineItemsController(ILineItem context, ApplicationDbContext user, ProductDBContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _user = user;
            _dbContext = dbContext;
            _userManager = userManager;
        }

        // GET: LineItems
        public async Task<IActionResult> Index()
        {
            var lineItems = await _context.GetLineItems();
            return View(lineItems);
        }

        // POST: LineItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int productId, string userId, int quantity)
        {
            ApplicationUser user = await _user.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var foundCart = await _dbContext.Carts.FirstOrDefaultAsync(c => c.OrderFulfilled == false && c.User == user);

            var lineItem = await _context.GetLineItemByProduct(foundCart.ID, productId);

            // updates the item quantity if the item is already in the cart
            if (lineItem != null)
            {
                lineItem.Quantity = (Quantity)quantity;
                await Edit(lineItem.ID, lineItem);
            }
            else
            {

                lineItem = new LineItem()
                {
                    ProductID = productId,
                    CartID = foundCart.ID,
                    Quantity = (Quantity)quantity
                };

                await _context.CreateLineItem(lineItem);
            }
            return RedirectToAction("Index", "Products");
        }

        // POST: LineItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ProductID,CartID,Quantity")] LineItem lineItem)
        {
            if (id != lineItem.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.UpdateLineItem(lineItem);
                    await CheckIfQuantityIsZero(lineItem);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await LineItemExists(lineItem.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Active", "Carts", new { userID = _userManager.GetUserId(User)});
            }
            return View(lineItem);
        }

        private async Task<bool> LineItemExists(int id)
        {
            return await _context.GetLineItem(id) != null;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var lineItem = await _context.GetLineItem(id);
            await _context.DeleteLineItem(lineItem);
            return RedirectToAction("Active", "Carts", new { userID = _userManager.GetUserId(User) });
        }

        private async Task CheckIfQuantityIsZero(LineItem lineItem)
        {
            if (lineItem.Quantity == 0)
            {
                await _context.DeleteLineItem(lineItem);
            }
        }
    }
}

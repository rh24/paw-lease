using EcomProject_JimmyRebecca.Data;
using EcomProject_JimmyRebecca.Models;
using EcomProject_JimmyRebecca.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EcomProject_JimmyRebecca.Controllers
{
    [Authorize(Policy = "CatLover")]
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

        /// <summary>
        /// Returns all line items
        /// </summary>
        /// <returns>Line times view</returns>
        public async Task<IActionResult> Index()
        {
            var lineItems = await _context.GetLineItems();
            return View(lineItems);
        }

        /// <summary>
        /// Create a line item
        /// </summary>
        /// <param name="productId">the product id</param>
        /// <param name="userId">the user id</param>
        /// <param name="quantity">the quantity</param>
        /// <returns></returns>
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

        /// <summary>
        /// Updates the lineitem
        /// </summary>
        /// <param name="id">the id of the lineitem to update</param>
        /// <param name="lineItem">the lineitem</param>
        /// <returns>the view of the updated lineitem</returns>
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
                return RedirectToAction("Active", "Carts", new { userID = _userManager.GetUserId(User) });
            }
            return View(lineItem);
        }

        /// <summary>
        /// checkts to see if lineitem exists
        /// </summary>
        /// <param name="id">the id of the line item</param>
        /// <returns>If it exists or not</returns>
        private async Task<bool> LineItemExists(int id)
        {
            return await _context.GetLineItem(id) != null;
        }

        /// <summary>
        /// Deletes a specific line item
        /// </summary>
        /// <param name="id">the id of the specific line item to delete</param>
        /// <returns>Returns to the carts view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var lineItem = await _context.GetLineItem(id);
            await _context.DeleteLineItem(lineItem);
            return RedirectToAction("Active", "Carts", new { userID = _userManager.GetUserId(User) });
        }

        /// <summary>
        /// Helper function to see if the items is 0
        /// </summary>
        /// <param name="lineItem">the lineitem</param>
        /// <returns>Nothing</returns>
        private async Task CheckIfQuantityIsZero(LineItem lineItem)
        {
            if (lineItem.Quantity == 0)
            {
                await _context.DeleteLineItem(lineItem);
            }
        }
    }
}

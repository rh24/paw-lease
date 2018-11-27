using EcomProject_JimmyRebecca.Data;
using EcomProject_JimmyRebecca.Models;
using EcomProject_JimmyRebecca.Models.Interfaces;
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

        public LineItemsController(ILineItem context, ApplicationDbContext user, ProductDBContext dbContext)
        {
            _context = context;
            _user = user;
            _dbContext = dbContext;
        }

        // GET: LineItems
        public async Task<IActionResult> Index()
        {
            var lineItems = await _context.GetLineItems();
            return View(lineItems);
        }

        // GET: LineItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineItem = await _context.GetLineItem(id);
            if (lineItem == null)
            {
                return NotFound();
            }

            return View(lineItem);
        }

        // GET: LineItems/Create
        public IActionResult Create()
        {
            //ViewData["CartID"] = new SelectList(_context.Carts, "ID", "ID");
            //ViewData["ProductID"] = new SelectList(_context.Products, "ID", "ProductName");
            return View();
        }

        // POST: LineItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int productId, string userId)
        {
            ApplicationUser user = await _user.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var foundCart = await _dbContext.Carts.FirstOrDefaultAsync(c => c.OrderFulfilled == false && c.User == user);

            LineItem lineItem = new LineItem()
            {
                ProductID = productId,
                CartID = foundCart.ID,
                Quantity = Quantity.one
            };

            await _context.CreateLineItem(lineItem);
            return RedirectToAction(nameof(Index));
            //ViewData["CartID"] = new SelectList(_context., "ID", "ID", lineItem.CartID);
            //ViewData["ProductID"] = new SelectList(_context.Products, "ID", "ProductName", lineItem.ProductID);

            //return View(lineItem);
        }

        // GET: LineItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineItem = await _context.GetLineItem(id);
            if (lineItem == null)
            {
                return NotFound();
            }
            //ViewData["CartID"] = new SelectList(_context.Carts, "ID", "ID", lineItem.CartID);
            //ViewData["ProductID"] = new SelectList(_context.Products, "ID", "ProductName", lineItem.ProductID);
            return View(lineItem);
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
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LineItemExists(lineItem.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CartID"] = new SelectList(_context.Carts, "ID", "ID", lineItem.CartID);
            //ViewData["ProductID"] = new SelectList(_context.Products, "ID", "ProductName", lineItem.ProductID);
            return View(lineItem);
        }

        private bool LineItemExists(int iD)
        {
            throw new NotImplementedException();
        }

        // GET: LineItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineItem = await _context.GetLineItem(id);

            if (lineItem == null)
            {
                return NotFound();
            }

            return View(lineItem);
        }

        // POST: LineItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lineItem = await _context.GetLineItem(id);
            await _context.DeleteLineItem(lineItem);
            return RedirectToAction(nameof(Index));
        }

        private async Task<IActionResult> CheckIfQuantityIsZero(LineItem lineItem)
        {
            if (lineItem.Quantity == 0)
            {
                await _context.DeleteLineItem(lineItem);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

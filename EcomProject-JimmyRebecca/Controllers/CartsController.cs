using EcomProject_JimmyRebecca.Models;
using EcomProject_JimmyRebecca.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EcomProject_JimmyRebecca.Controllers
{
    public class CartsController : Controller
    {
        private readonly ICart _context;
        private readonly ILineItem _liContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public CartsController(ICart context, ILineItem liContext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _liContext = liContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Gets the carts
        /// </summary>
        /// <returns>returns all carts</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.GetCarts());
        }

        /// <summary>
        /// Gets the details of the specific cart
        /// </summary>
        /// <param name="id">the id of the cart to get</param>
        /// <returns>returns the details of that specific cart</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.GetCart(id);
            //if (cart == null)
            //var cart = await _context.GetCartByUserId(id.ToString());

            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        /// <summary>
        /// This method views the user's current, unfulfilled cart by grabbing its user ID from the route.
        /// </summary>
        /// <param name="userId">ApplicationUser's string ID</param>
        /// <returns></returns>
        public async Task<IActionResult> Active()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return NotFound();
            }

            var cart = await _context.GetCartByUserId(userId);

            if (cart == null)
            {
                return NotFound();
            }

            var li = await _liContext.GetLineItems(cart.ID);
            return View("Details", li);
        }

        /// <summary>
        /// Returns the create view for carts
        /// </summary>
        /// <returns>The create view for carts</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Creates the cart
        /// </summary>
        /// <param name="cart">the cart to create</param>
        /// <returns>Returns a view of all carts</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ApplicationUserID")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                await _context.CreateCart(cart);
                return RedirectToAction("Index", "Products");
            }

            return View(cart);
        }

        /// <summary>
        /// Edits a specific cart
        /// </summary>
        /// <param name="id">The id of the cart to edit</param>
        /// <returns>Returns the view of the cart</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.GetCart(id);

            if (cart == null)
            {
                return NotFound();
            }
            return View(cart);
        }

        /// <summary>
        /// Edits the actual cart
        /// </summary>
        /// <param name="id">id of cart to edit</param>
        /// <param name="cart">The cart to update</param>
        /// <returns>View of updated cart</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ApplicationUserID")] Cart cart)
        {
            if (id != cart.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.UpdateCart(cart);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_context.GetCart(id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Products");
            }
            return View(cart);
        }

        private bool CartExists(int id)
        {
            return _context.GetCart(id) != null;
        }

        // GET: Carts/Delete/5
        /// <summary>
        /// Deletes the cart with the specific id
        /// </summary>
        /// <param name="id">the id of the cart to delete</param>
        /// <returns>returns a view</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.GetCart(id);

            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        /// <summary>
        /// Does the actual delete
        /// </summary>
        /// <param name="id">id of cart to delete</param>
        /// <returns>redirects to products</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cart = await _context.GetCart(id);
            await _context.DeleteCart(cart);

            return RedirectToAction("Index", "Products");
        }
    }
}

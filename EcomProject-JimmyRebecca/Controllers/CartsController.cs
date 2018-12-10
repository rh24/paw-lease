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

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            return View(await _context.GetCarts());
        }

        // GET: Carts/Details/5
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

        // GET: Carts/Active/{userId}
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

        // GET: Carts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Carts/Create
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

        // GET: Carts/Edit/5
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

        // POST: Carts/Edit/5
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
                    if (!CartExists(cart.ID))
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

        // GET: Carts/Delete/5
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

        // POST: Carts/Delete/5
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

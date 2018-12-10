using EcomProject_JimmyRebecca.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EcomProject_JimmyRebecca.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProduct _context;

        public ProductsController(IProduct context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all of the products
        /// </summary>
        /// <returns>a view of all of the products</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.GetProducts());
        }

        /// <summary>
        /// Gets a specific product with id
        /// </summary>
        /// <param name="id">the id of the product</param>
        /// <returns>returns a view of that specific product</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}

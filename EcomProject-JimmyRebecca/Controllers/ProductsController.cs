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

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.GetProducts());
            // Make view model containing products and cartid, line items
        }

        // GET: Products/Details/5
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

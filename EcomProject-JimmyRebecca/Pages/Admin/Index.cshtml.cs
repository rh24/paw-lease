using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EcomProject_JimmyRebecca.Models;
using EcomProject_JimmyRebecca.Data;
using Microsoft.AspNetCore.Authorization;

namespace EcomProject_JimmyRebecca.Pages.Admin
{
    // only allows admins to access
    [Authorize(Roles = UserRoles.Admin)]
    public class IndexModel : PageModel
    {
        private readonly ProductDBContext _context;

        /// <summary>
        /// Constructs past orders with DB context
        /// </summary>
        /// <param name="context">DB Context</param>
        public IndexModel(ProductDBContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; }

        /// <summary>
        /// Returns a list of all the products
        /// </summary>
        /// <returns>the page of all products</returns>
        public async Task OnGetAsync()
        {
            Product = await _context.Products.ToListAsync();
        }
    }
}

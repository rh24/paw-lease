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

        public IndexModel(ProductDBContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; }

        public async Task OnGetAsync()
        {
            Product = await _context.Products.ToListAsync();
        }
    }
}

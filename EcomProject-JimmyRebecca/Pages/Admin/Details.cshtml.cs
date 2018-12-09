using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EcomProject_JimmyRebecca.Data;
using EcomProject_JimmyRebecca.Models;
using Microsoft.AspNetCore.Authorization;

namespace EcomProject_JimmyRebecca.Pages.Admin
{
    // only allows admins to access
    [Authorize(Roles = UserRoles.Admin)]
    public class DetailsModel : PageModel
    {
        private readonly ProductDBContext _context;

        public DetailsModel(ProductDBContext context)
        {
            _context = context;
        }

        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Products.FirstOrDefaultAsync(m => m.ID == id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

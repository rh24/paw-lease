using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EcomProject_JimmyRebecca.Models;
using EcomProject_JimmyRebecca.Data;
using Microsoft.AspNetCore.Authorization;

namespace EcomProject_JimmyRebecca.Pages.Admin
{
    // only allows admins to access
    [Authorize(Roles = UserRoles.Admin)]
    public class CreateModel : PageModel
    {
        private readonly ProductDBContext _context;

        public CreateModel(ProductDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Constructs past orders with DB context
        /// </summary>
        /// <param name="context">DB Context</param>
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }

        /// <summary>
        /// Adds the product on post
        /// </summary>
        /// <returns>Returns a page</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
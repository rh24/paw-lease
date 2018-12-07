using EcomProject_JimmyRebecca.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace EcomProject_JimmyRebecca.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly EcomProject_JimmyRebecca.Data.ProductDBContext _context;

        public DetailsModel(EcomProject_JimmyRebecca.Data.ProductDBContext context)
        {
            _context = context;
        }

        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The passwords don't match!")]
        [Display(Name = "Confirm Password")]
        public string PaswordConfirmation { get; set; }

        public UserProfile UserProfile { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserProfile = await _context.UserProfile.FirstOrDefaultAsync(m => m.ID == id);

            if (UserProfile == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

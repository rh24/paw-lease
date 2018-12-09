using EcomProject_JimmyRebecca.Data;
using EcomProject_JimmyRebecca.Models;
using EcomProject_JimmyRebecca.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;

namespace EcomProject_JimmyRebecca.Pages.Profile
{
    public class DetailsModel : PageModel
    {
        private readonly EcomProject_JimmyRebecca.Data.ProductDBContext _context;
        private readonly EcomProject_JimmyRebecca.Data.ApplicationDbContext _userContext;
        private UserManager<ApplicationUser> _userManager;

        public DetailsModel(ProductDBContext context, ApplicationDbContext userContext, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userContext = userContext;
            _userManager = userManager;
        }

        public UserProfile UserProfile { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var foundUser = await _userManager.GetUserAsync(User);

            if (foundUser == null) return NotFound();

            // Initialize new UserProfile view model based on claims of foundUser
            UserProfile = new UserProfile
            {
                Email = foundUser.Email,
                FirstName = foundUser.FirstName,
                LastName = foundUser.LastName,
                LovesCats = foundUser.LovesCats,
                Address = foundUser.Address,
                Birthday = foundUser.Birthday
            };

            var userCarts = _context.Carts.Where(c => c.User.Id == foundUser.Id);

            // Get last 5 carts
            if (userCarts.Count() >= 5)
            {
                UserProfile.LastFiveOrders = userCarts.Take(5).ToList();
            }
            else
            {
                UserProfile.LastFiveOrders = userCarts.ToList();
            }

            return Page();
        }
    }
}

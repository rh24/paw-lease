using EcomProject_JimmyRebecca.Data;
using EcomProject_JimmyRebecca.Models;
using EcomProject_JimmyRebecca.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
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

        public async Task OnGetAsync()
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var foundUser = await _userManager.GetUserAsync(User);

            // Initialize new UserProfile view model based on claims of foundUser
            UserProfile = new UserProfile
            {
                Email = foundUser.Email
            };

            //if (UserProfile == null)
            //{
            //    return NotFound();
            //}
            //return Page();
        }
    }
}

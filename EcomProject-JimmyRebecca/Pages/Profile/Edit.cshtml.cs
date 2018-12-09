using EcomProject_JimmyRebecca.Data;
using EcomProject_JimmyRebecca.Models;
using EcomProject_JimmyRebecca.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EcomProject_JimmyRebecca.Pages.Profile
{
    public class EditModel : PageModel
    {
        private readonly ProductDBContext _context;
        private readonly ApplicationDbContext _userContext;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public EditModel(ProductDBContext context, ApplicationDbContext userContext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userContext = userContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public UserProfile UserProfile { get; set; }

        /// <summary>
        /// Gets the user's information
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync()
        {
            string userId = _userManager.GetUserId(User);
            ApplicationUser foundUser = await _userContext.Users.FirstOrDefaultAsync(m => m.Id == userId);

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


            return Page();
        }

        /// <summary>
        /// Updates the user's information
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            CheckUserRolesExist();

            // Get signed in user
            var user = await _userManager.GetUserAsync(User);

            // Remove all associated claims from current user's identity
            await _userManager.RemoveClaimsAsync(user, User.Claims);

            // Add newly updated claims and sign user back in
            if (ModelState.IsValid)
            {
                // update user's info
                user.UserName = UserProfile.Email;
                user.FirstName = UserProfile.FirstName;
                user.LastName = UserProfile.LastName;
                user.Email = UserProfile.Email;
                user.Address = UserProfile.Address;
                user.Birthday = UserProfile.Birthday;
                user.LovesCats = UserProfile.LovesCats;

                // Reset password of user
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                await _userManager.ResetPasswordAsync(user, resetToken, UserProfile.Password);
                var updatedResult = await _userManager.UpdateAsync(user);

                if (updatedResult.Succeeded)
                {// Custom Claim type for full name
                    Claim fullNameClaim = new Claim("FullName", $"{user.FirstName} {user.LastName}");

                    // Custom claim type for loves cats
                    Claim lovesCatsClaim = new Claim("LovesCats", user.LovesCats.ToString().ToLower());

                    // claim type for birthday
                    Claim birthdayClaim = new Claim(
                        ClaimTypes.DateOfBirth,
                        new DateTime(user.Birthday.Year, user.Birthday.Month, user.Birthday.Day).ToString("u"), ClaimValueTypes.DateTime);

                    // claim type for email
                    Claim emailClaim = new Claim(ClaimTypes.Email, user.Email, ClaimValueTypes.Email);

                    // claim for  type address
                    Claim addressClaim = new Claim(ClaimTypes.StreetAddress, user.Address);

                    List<Claim> myclaims = new List<Claim>()
                            {
                                fullNameClaim,
                                birthdayClaim,
                                emailClaim,
                                addressClaim,
                                lovesCatsClaim
                            };

                    // adds the claims
                    await _userManager.AddClaimsAsync(user, myclaims);

                    // make admins if emails are these
                    if (UserProfile.Email.ToLower() == "amanda@codefellows.com" || UserProfile.Email.ToLower() == "jimmyn123@gmail.com" || UserProfile.Email.ToLower() == "rebeccayhong@gmail.com")
                    {
                        await _userManager.AddToRoleAsync(user, UserRoles.Admin);
                    }
                    else
                    {
                        await _userManager.RemoveFromRoleAsync(user, UserRoles.Admin);
                    }
                    await _userManager.AddToRoleAsync(user, UserRoles.Member);

                    await _signInManager.RefreshSignInAsync(user);
                }

                try
                {
                    await _userContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserProfileExists())
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return RedirectToPage("./Index");
        }

        /// <summary>
        /// Checks if the User exists in the _userContext
        /// </summary>
        /// <returns></returns>
        private bool UserProfileExists()
        {
            string userId = _userManager.GetUserId(User);
            return _userContext.Users.Any(e => e.Id == userId);
        }

        /// <summary>
        /// Checks if the roles exist
        /// </summary>
        public void CheckUserRolesExist()
        {
            if (!_userContext.Roles.Any())
            {
                List<IdentityRole> Roles = new List<IdentityRole>
                {
                    new IdentityRole{Name = UserRoles.Admin, NormalizedName=UserRoles.Admin.ToString(), ConcurrencyStamp = Guid.NewGuid().ToString()},
                    new IdentityRole{Name = UserRoles.Member, NormalizedName=UserRoles.Member.ToString(), ConcurrencyStamp = Guid.NewGuid().ToString()},
                };
                foreach (var role in Roles)
                {
                    _userContext.Roles.Add(role);
                    _userContext.SaveChanges();
                }
            }
        }
    }
}

﻿using EcomProject_JimmyRebecca.Data;
using EcomProject_JimmyRebecca.Models;
using EcomProject_JimmyRebecca.Models.Interfaces;
using EcomProject_JimmyRebecca.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EcomProject_JimmyRebecca.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly ICart _context;
        private readonly ApplicationDbContext _user;
        private readonly IEmailSender _email;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ICart context, ApplicationDbContext user, IEmailSender email)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _user = user;
            _email = email;
        }

        /// <summary>
        /// Gets the register view if no user is currently signed in. Pulls up the user registered account info if user is signed in. This is to add future ability to PUT registration information in case user wants to change address, email, birthday, etc.
        /// </summary>
        /// <returns>Register view</returns>
        [HttpGet]
        public IActionResult Register()
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                var existingUser = HttpContext.User.Claims;
                var fullNameClaim = existingUser.First(name => name.Type == "FullName").Value;
                string[] fullName = fullNameClaim.Split(' ');

                RegisterAccount eu = new RegisterAccount()
                {
                    FirstName = fullName[0],
                    LastName = fullName[1],
                    Email = existingUser.First(name => name.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value,
                    Address = existingUser.First(name => name.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/streetaddress").Value,
                    Birthday = Convert.ToDateTime(existingUser.First(name => name.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth").Value)
                };

                return View(eu);
            }

            return View();
        }

        /// <summary>
        /// Register a user action.
        /// </summary>
        /// <param name="ra">Takes in a user model to register</param>
        /// <returns>Returns a view</returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterAccount ra)
        {
            if (ModelState.IsValid)
            {
                CheckUserRolesExist();

                ApplicationUser newUser = new ApplicationUser()
                {
                    UserName = ra.Email,
                    FirstName = ra.FirstName,
                    LastName = ra.LastName,
                    Email = ra.Email,
                    AccountCreation = ra.AccountCreation,
                    Address = ra.Address,
                    Birthday = ra.Birthday,
                    LovesCats = ra.LovesCats
                };

                var result = await _userManager.CreateAsync(newUser, ra.Password);

                if (result.Succeeded)
                {// Custom Claim type for full name
                    Claim fullNameClaim = new Claim("FullName", $"{newUser.FirstName} {newUser.LastName}");

                    // Custom claim type for loves cats
                    Claim lovesCatsClaim = new Claim("LovesCats", newUser.LovesCats.ToString().ToLower());

                    // claim type for birthday
                    Claim birthdayClaim = new Claim(
                        ClaimTypes.DateOfBirth,
                        new DateTime(newUser.Birthday.Year, newUser.Birthday.Month, newUser.Birthday.Day).ToString("u"), ClaimValueTypes.DateTime);

                    // claim type for email
                    Claim emailClaim = new Claim(ClaimTypes.Email, newUser.Email, ClaimValueTypes.Email);

                    // claim for  type address
                    Claim addressClaim = new Claim(ClaimTypes.StreetAddress, newUser.Address);

                    List<Claim> myclaims = new List<Claim>()
                    {
                        fullNameClaim,
                        birthdayClaim,
                        emailClaim,
                        addressClaim,
                        lovesCatsClaim
                    };

                    //adds the claims
                    await _userManager.AddClaimsAsync(newUser, myclaims);

                    // create a cart for the user
                    var cart = new Cart
                    {
                        User = newUser
                    };


                    // make admins if emails are these
                    if (ra.Email.ToLower() == "amanda@codefellows.com" || ra.Email.ToLower() == "jimmyn123@gmail.com" || ra.Email.ToLower() == "rebeccayhong@gmail.com" || ra.Email.ToLower() == "jeffxmartinez@gmail.com")
                    {
                        await _userManager.AddToRoleAsync(newUser, UserRoles.Admin);
                    }

                    // send user an email confirmation that registration was successful
                    await _email.SendEmailAsync(newUser.Email, "Thank you for logging in", "<p> Hello!!! <strong>I am Happy!! </strong> </p>");

                    await _userManager.AddToRoleAsync(newUser, UserRoles.Member);

                    await _context.CreateCart(cart);

                    await _signInManager.SignInAsync(newUser, isPersistent: false);
                }
            }

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// This method updates a user's account information if a user is already signed in
        /// </summary>
        /// <param name="ra">Account info to update current user</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> EditProfile(RegisterAccount ra)
        {
            if (_signInManager.IsSignedIn(User))
            {
                // Get signed in user
                var user = await _userManager.GetUserAsync(HttpContext.User);

                // Remove all associated claims from current user's identity
                await _userManager.RemoveClaimsAsync(user, HttpContext.User.Claims);

                // Add newly updated claims and sign user back in
                if (ModelState.IsValid)
                {
                    // update user's info
                    user.UserName = ra.Email;
                    user.FirstName = ra.FirstName;
                    user.LastName = ra.LastName;
                    user.Email = ra.Email;
                    user.Address = ra.Address;
                    user.Birthday = ra.Birthday;
                    user.LovesCats = ra.LovesCats;

                    // Reset password of user
                    string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                    await _userManager.ResetPasswordAsync(user, resetToken, ra.Password);
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
                        if (ra.Email.ToLower() == "amanda@codefellows.com" || ra.Email.ToLower() == "jimmyn123@gmail.com" || ra.Email.ToLower() == "rebeccayhong@gmail.com")
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
                }
            }

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Login Action
        /// </summary>
        /// <returns>returns the view to log in</returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Logs the user in with username and password
        /// </summary>
        /// <param name="lvm">The login model that has email and pass</param>
        /// <returns>Redirected view</returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM lvm)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(lvm.Email, lvm.Password, false, false);

                if (result.Succeeded)
                {

                    var admins = await _userManager.GetUsersInRoleAsync(UserRoles.Admin);

                    var match = admins.Where(admin => admin.Email.ToLower() == lvm.Email.ToLower());

                    if (match.Count() != 0)
                    {
                        return RedirectToAction("Index", "Admin");
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Wrong username/password");
                }

            }

            return View(lvm);
        }

        /// <summary>
        /// Logs user out after form submit.
        /// </summary>
        /// <returns>Redirect to Home page</returns>
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task ForceLogout()
        {
            await _signInManager.SignOutAsync();
        }

        /// <summary>
        /// Checks if the roles exist
        /// </summary>
        public void CheckUserRolesExist()
        {
            if (!_user.Roles.Any())
            {
                List<IdentityRole> Roles = new List<IdentityRole>
                {
                    new IdentityRole{Name = UserRoles.Admin, NormalizedName=UserRoles.Admin.ToString(), ConcurrencyStamp = Guid.NewGuid().ToString()},
                    new IdentityRole{Name = UserRoles.Member, NormalizedName=UserRoles.Member.ToString(), ConcurrencyStamp = Guid.NewGuid().ToString()},
                };
                foreach (var role in Roles)
                {
                    _user.Roles.Add(role);
                    _user.SaveChanges();
                }
            }
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using EcomProject_JimmyRebecca.Models;
using EcomProject_JimmyRebecca.Models.ViewModels;
using System.Security.Claims;

namespace EcomProject_JimmyRebecca.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Gets the register view
        /// </summary>
        /// <returns>Register view</returns>
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Register a user action
        /// </summary>
        /// <param name="ra">Takes in a user model to register</param>
        /// <returns>Returns a view</returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterAccount ra)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser newUser = new ApplicationUser()
                {
                    UserName = ra.Email,
                    FirstName = ra.FirstName,
                    LastName = ra.LastName,
                    Email = ra.Email,
                    AccountCreation = ra.AccountCreation,
                    Address = ra.Address,
                    Birthday = ra.Birthday
                };

                var result = await _userManager.CreateAsync(newUser, ra.Password);

                if (result.Succeeded)
                {// Custom Claim type for full name
                    Claim fullNameClaim = new Claim("FullName", $"{newUser.FirstName} {newUser.LastName}");

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
                        addressClaim
                    };

                    // adds the claims
                    await _userManager.AddClaimsAsync(newUser, myclaims);

                    await _signInManager.SignInAsync(newUser, isPersistent: false);
                }
            }

            return View();
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
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Wrong username/password");
                }

            }

            return View(lvm);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using EcomProject_JimmyRebecca.Models;
using EcomProject_JimmyRebecca.Models.ViewModels;

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
                {
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
    }
}
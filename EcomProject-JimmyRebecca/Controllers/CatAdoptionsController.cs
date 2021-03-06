﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcomProject_JimmyRebecca.Controllers
{
    [Authorize(Policy = "CatLover")]
    public class CatAdoptionsController : Controller
    {
        /// <summary>
        /// This page will show all the cats up for adoption.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// This will be the action that gets the checkout page for adopting a cat.
        /// </summary>
        /// <returns></returns>
        public IActionResult Checkout()
        {
            return View();
        }
    }
}

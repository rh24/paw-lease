using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EcomProject_JimmyRebecca.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Returns the home page
        /// </summary>
        /// <returns>Home view</returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}
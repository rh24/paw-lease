using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcomProject_JimmyRebecca.Controllers
{
    [AllowAnonymous]
    public class BasketController : Controller
    {
        /// <summary>
        /// This will be the action that gets the checkout page for items in the basket a cat.
        /// </summary>
        /// <returns></returns>
        public IActionResult Checkout()
        {
            return View();
        }
    }
}

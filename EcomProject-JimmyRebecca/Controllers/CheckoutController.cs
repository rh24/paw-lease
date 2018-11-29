using Microsoft.AspNetCore.Mvc;

namespace EcomProject_JimmyRebecca.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Receipt()
        {
            return View();
        }
    }
}

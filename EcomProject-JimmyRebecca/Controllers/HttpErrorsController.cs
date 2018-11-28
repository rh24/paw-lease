using Microsoft.AspNetCore.Mvc;

namespace EcomProject_JimmyRebecca.Controllers
{
    public class HttpErrorsController : Controller
    {
        public IActionResult NotFound()
        {
            return View();
        }
    }
}

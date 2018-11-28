using Microsoft.AspNetCore.Mvc;

namespace EcomProject_JimmyRebecca.Controllers
{
    [Route("error")]
    public class HttpErrorsController : Controller
    {
        [Route("404")]
        public new IActionResult NotFound()
        {
            return View();
        }
    }
}

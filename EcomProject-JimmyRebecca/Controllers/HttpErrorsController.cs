using Microsoft.AspNetCore.Mvc;

namespace EcomProject_JimmyRebecca.Controllers
{
    [Route("error")]
    public class HttpErrorsController : Controller
    {
        /// <summary>
        /// Returns the view for errors
        /// </summary>
        /// <returns>404 view</returns>
        [Route("404")]
        public new IActionResult NotFound()
        {
            return View();
        }
    }
}

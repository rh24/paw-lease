using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcomProject_JimmyRebecca.Controllers
{
    [Authorize(Policy = "CatLover")]
    public class CatAdoptionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

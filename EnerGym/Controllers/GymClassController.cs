using Microsoft.AspNetCore.Mvc;

namespace EnerGym.Controllers
{
    public class GymClassController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}

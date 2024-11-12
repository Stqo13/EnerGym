using Microsoft.AspNetCore.Mvc;

namespace EnerGym.Controllers
{
    public class WorkoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace EnerGym.Controllers
{
    public class GymClassController (
        ILogger<GymClassController> logger
        )
        : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occured while trying to fetch all gym classes. {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }
    }
}

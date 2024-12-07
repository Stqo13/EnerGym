using Microsoft.AspNetCore.Mvc;

namespace EnerGym.Controllers
{
    public class PersonalHallController : Controller
    {
        public async Task<IActionResult> UserProgress()
        {
            return View();
        }

        public async Task<IActionResult> UserMembershipPlans()
        {
            return View();
        }

        public async Task<IActionResult> UserEnrolledClasses()
        {
            return View();
        }
    }
}

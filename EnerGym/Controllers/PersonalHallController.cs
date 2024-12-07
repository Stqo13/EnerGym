using Microsoft.AspNetCore.Mvc;
using EnerGym.ViewModels.PersonalHallViewModels;
using EnerGym.Data.Models;
using EnerGym.Services.Data.Interfaces;
using System.Security.Claims;

namespace EnerGym.Controllers
{
    public class PersonalHallController (
        IPersonalHallService personalHallService,
        ILogger<PersonalHallController> logger
        ) : Controller
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

        private string GetCurrentClientId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }
    }
}

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
        public async Task<IActionResult> UserMembershipPlans()
        {
            try
            {
                var plans = await personalHallService.GetUserMembershipPlansAsync(GetCurrentClientId());
                return View(plans);
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occured while fetching the user's membership plans. {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> UserEnrolledClasses()
        {
            try
            {
                var classes = await personalHallService.GetUserEnrolledClassesAsync(GetCurrentClientId());
                return View(classes);
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occured while fetching the user's enrolled gym classes. {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        private string GetCurrentClientId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }
    }
}

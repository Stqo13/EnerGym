using EnerGym.Services.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnerGym.Controllers
{
    public class MembershipPlanController (
        IMembershipPlanService membershipPlanService,
        ILogger<MembershipPlanController> logger)
        : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var membershipPlans = await membershipPlanService.GetAllMembershipPlansAsync();

                return View(membershipPlans);
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occured while fethcing membership plans. {ex.Message}");
                return RedirectToAction("");
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}

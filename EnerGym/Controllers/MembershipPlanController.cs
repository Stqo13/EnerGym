using EnerGym.Services.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EnerGym.Controllers
{
    [Authorize]
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

        [HttpGet]
        public async Task<IActionResult>ObtainDetails(int id)
        {
            try
            {
                var model = await membershipPlanService.GetMembershipPlanObtainDetailsAsync(id);

                return View(model);
            }
            catch (NullReferenceException nex)
            {
                logger.LogError($"An error occured while getting membership plan details. {nex.Message}");
                return RedirectToAction("Error", "Home", new { code = 404 });
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occured while getting membership plan details. {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddToPersonalHall(int id)
        {
            try
            {
                string userId = GetCurrentClientId();

                await membershipPlanService.AddToPersonalHall(id, userId);

                return RedirectToAction(nameof(Index));
            }
            catch (NullReferenceException nex)
            {
                logger.LogError($"An error occured while adding the membership plan to personal hall. {nex.Message}");
                return RedirectToAction("Error", "Home", new { code = 404 });
            }
            catch (InvalidOperationException iex)
            {
                logger.LogError($"An error occured while adding the membership plan to personal hall. {iex.Message}");
                return RedirectToAction("Error", "Home", new { code = 500 });
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occured while adding the membership plan to personal hall. {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        private string GetCurrentClientId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }
    }
}

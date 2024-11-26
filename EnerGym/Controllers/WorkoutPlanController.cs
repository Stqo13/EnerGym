using EnerGym.Services.Data.Interfaces;
using EnerGym.ViewModels.WorkoutPlanViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EnerGym.Controllers
{
    public class WorkoutPlanController(
        IWorkoutPlanSevice workoutPlanService,
        ILogger<WorkoutPlanController> logger)
        : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var plans = await workoutPlanService.GetAllAsync();

                return View(plans);
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occured while getting all workout plans. {ex.Message}");
                return RedirectToAction("Error", "Home");   
            }
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var workoutPlan = new WorkoutPlanAddViewModel();

            return View(workoutPlan);
        }

        [HttpPost]
        public async Task<IActionResult> Add(WorkoutPlanAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await workoutPlanService.AddPlanAsync(model);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occured while adding workout plan. {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var plan = await workoutPlanService.GetPlanDetailsAsync(id);
                return View(plan);
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occured while trying to show workout plan details. {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var plan = await workoutPlanService.GetEditPlanByIdAsync(id);
                return View(plan);
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occured while trying to get the workout plan. {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPlanViewModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var plan = await workoutPlanService.EditPlanAsync(model, id);
                return RedirectToAction(nameof(Details), new { id = plan.Id });
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occured while trying to edit workout plan. {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var plan = await workoutPlanService.GetDeletePlanByIdAsync(id, GetCurrentClientName());
                return View(plan);
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occured while showing delete workout plan {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeletePlanViewModel model)
        {
            try
            {
                await workoutPlanService.DeletePlanAsync(model);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occured while trying to delete workout plan {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        private string GetCurrentClientName()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }
    }
}

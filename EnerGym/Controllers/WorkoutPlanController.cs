using EnerGym.Services.Data.Interfaces;
using EnerGym.ViewModels.WorkoutPlanViewModels;
using EnerGym.ViewModels.WorkoutRoutineViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static EnerGym.Common.ApplicationConstraints.ApplicationUserConstraints;

namespace EnerGym.Controllers
{
    [Authorize]
    public class WorkoutPlanController(
        IWorkoutPlanSevice workoutPlanService,
        ILogger<WorkoutPlanController> logger)
        : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            try
            {
                int pageSize = 3;
                var plans = await workoutPlanService.GetAllAsync(pageNumber, pageSize);
                var totalPages = await workoutPlanService.GetTotalPagesAsync(pageSize);

                var viewModel = new WorkoutPlansIndexViewModel
                {
                    WorkoutPlans = plans,
                    CurrentPage = pageNumber,
                    TotalPages = totalPages
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occurred while getting all workout plans. {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public IActionResult Add()
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
            catch (NullReferenceException nex)
            {
                logger.LogError($"An error occured while trying to show workout plan details. {nex.Message}");
                return RedirectToAction("Error", "Home", new { code = 404 });
            }
            catch (ArgumentException aex)
            {
                logger.LogError($"An error occured while trying to show workout plan details. {aex.Message}");
                return RedirectToAction("Error", "Home", new { code = 500});
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
            catch (ArgumentException aex)
            {
                logger.LogError($"An error occured while trying to get the workout plan. {aex.Message}");
                return RedirectToAction("Error", "Home", new { code = 500 });
            }
            catch (NullReferenceException nex)
            {
                logger.LogError($"An error occured while trying to get the workout plan. {nex.Message}");
                return RedirectToAction("Error", "Home", new { code = 404 });
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
            catch (ArgumentException aex)
            {
                logger.LogError($"An error occured while trying to edit workout plan. {aex.Message}");
                return RedirectToAction("Error", "Home", new { code = 500});
            }
            catch (NullReferenceException nex)
            {
                logger.LogError($"An error occured while trying to edit workout plan. {nex.Message}");
                return RedirectToAction("Error", "Home", new { code = 404});
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
                var plan = await workoutPlanService.GetDeletePlanByIdAsync(id, GetCurrentClientId());
                return View(plan);
            }
            catch (NullReferenceException nex)
            {
                logger.LogError($"An error occured while showing delete workout plan {nex.Message}");
                return RedirectToAction("Error", "Home", new { code = 404 });
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

        [HttpGet]
        public async Task<IActionResult> AddRoutines(int workoutPlanId, string searchQuery, int? sets, int? reps)
        {
            try
            {
                var routines = await workoutPlanService.GetAllRoutinesAsync(searchQuery, sets, reps);

                var model = new AddRoutinesViewModel
                {
                    WorkoutPlanId = workoutPlanId,
                    AvailableRoutines = routines,
                    SearchQuery = "",
                    Sets = null,
                    Reps = null
                };

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occurred while fetching routines. {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddRoutines(AddRoutinesViewModel model)
        {
            if(model.SelectedRoutineIds == null || !model.SelectedRoutineIds.Any())
            {
                ModelState.AddModelError("", "Please select at least one routine.");
                return View(model);
            }

            try
            {
                await workoutPlanService.AddRoutinesToPlanAsync(model.WorkoutPlanId, model.SelectedRoutineIds);
                return RedirectToAction("Details", new { id = model.WorkoutPlanId });
            }
            catch (NullReferenceException nex)
            {
                logger.LogError($"An error occurred while adding routines to the plan. {nex.Message}");
                return RedirectToAction("Error", "Home", new { code = 404 });
            }
            catch (ArgumentException aex)
            {
                logger.LogError($"An error occurred while adding routines to the plan. {aex.Message}");
                return RedirectToAction("Error", "Home", new { code = 500 });
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occurred while adding routines to the plan. {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ShowWorkoutRoutines(int id)
        {
            try
            {
                var plan = await workoutPlanService.GetPlanDetailsAsync(id);

                var routines = await workoutPlanService.GetRoutinesByPlanIdAsync(id);

                var model = new WorkoutPlanWithRoutinesViewModel()
                {
                    Plan = plan,
                    WorkoutRoutines = routines
                };

                return View(model);
            }
            catch (NullReferenceException nex)
            {
                logger.LogError($"An error occured while fetching the plan's routines. {nex.Message}");
                return RedirectToAction("Error", "Home", new { code = 404 });
            }
            catch (ArgumentException aex)
            {
                logger.LogError($"An error occured while fetching the plan's routines. {aex.Message}");
                return RedirectToAction("Error", "Home", new { code = 500 });
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occured while fetching the plan's routines. {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        private string GetCurrentClientId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }
    }
}


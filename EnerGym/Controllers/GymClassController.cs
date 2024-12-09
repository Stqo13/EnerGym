using EnerGym.Services.Data.Interfaces;
using EnerGym.ViewModels.GymClassViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EnerGym.Controllers
{
    public class GymClassController (
        IGymClassService gymClassService,
        ILogger<GymClassController> logger)
        : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            try
            {
                int pageSize = 3;
                var clases = await gymClassService.GetAllGymClassesAsync(pageNumber, pageSize);
                var totalPages = await gymClassService.GetTotalPagesAsync(pageSize);

                var viewModel = new GymClassesIndexViewModel()
                {
                    GymClasses = clases,
                    TotalPages = totalPages,
                    CurrentPage = pageNumber
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occured while trying to fetch all gym classes. {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public IActionResult Add()
        {
            var workoutPlan = new GymClassAddViewModel();

            return View(workoutPlan);
        }

        [HttpPost]
        public async Task<IActionResult> Add(GymClassAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await gymClassService.AddGymClassAsync(model);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occured while fetching form for adding gym class. {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var gymClass = await gymClassService.GetGymClassDetailsAsync(id);

                return View(gymClass);
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occured while fetching gym class detals. {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var model = await gymClassService.GetEditGymClassByIdAsync(id);
                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occured while fetching the edit for gym classes. {ex.Message}");
                return RedirectToAction("Error", "Home");   
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GymClassEditViewModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var gymClass = await gymClassService.EditGymClassAsync(model, id);
                return RedirectToAction(nameof(Details), new { id = gymClass.Id });
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occured while editing the gym class. {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Deactivate(int id)
        {
            try
            {
                var model = await gymClassService.GetDeleteGymClassByIdAsync(id, GetCurrentClientId());
                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occured while fetching delete form for gym class. {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Deactivate(GymClassDeleteViewModel model)
        {
            try
            {
                await gymClassService.DeleteGymClassAsync(model);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occured while deleting the gym class. {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Enroll(int id)
        {
            try
            {
                var model = await gymClassService.GetDetailsGymClassByIdAsync(id);
                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occured while fetching confirm enrollment. {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EnrollConfirm(int id)
        {
            try
            {
                var userId = GetCurrentClientId();

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                await gymClassService.EnrollUserAsync(id, userId);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occured while adding the enrolling the user. {ex.Message}");
                return RedirectToAction("Error", "Home");   
                
            }
        }

        private string GetCurrentClientId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }
    }
}

using EnerGym.Data.Models;
using EnerGym.Services.Data.Interfaces;
using EnerGym.ViewModels.WorkoutRoutineViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using static EnerGym.Common.ApplicationConstraints.ApplicationUserConstraints;

namespace EnerGym.Controllers
{
    [Authorize]
    public class WorkoutRoutineController(
        IWorkoutRoutineService workoutRoutineService,
        ILogger<WorkoutRoutineController> logger)
        : Controller
    {
        

        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            try
            {
                int pageSize = 3;
                var routines = await workoutRoutineService.GetAllWorkoutRoutinesAsync(pageNumber, pageSize);
                var totalPages = await workoutRoutineService.GetTotalPagesAsync(pageSize);

                var model = new WorkoutRoutinesIndexViewModel()
                {
                    WorkoutRoutines = routines,
                    CurrentPage = pageNumber,
                    TotalPages = totalPages
                };

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occured while getting all workout routines. {ex.Message}");
                return RedirectToAction("Error", "Index");
            }
        }

        [HttpGet]
        public IActionResult Add()
        {
            var route = new WorkoutRoutineAddViewModel();

            return View(route);
        }

        [HttpPost]
        public async Task<IActionResult> Add(WorkoutRoutineAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }  
            
            try
            {
                await workoutRoutineService.AddWorkoutRoutineAsync(model);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occured while trying to add workout routine. {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var routine = await workoutRoutineService.GetDeleteWorkoutRoutineByIdAsync(id);

                return View(routine);
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occured while getting delete UI. {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(WorkoutRoutineDeleteViewModel model)
        {
            try
            {
                await workoutRoutineService.DeleteWorkoutRoutineAsync(model);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occured while deleting workout routine. {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var routine = await workoutRoutineService.GetRoutineDetailsAsync(id);

                return View(routine);
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occured while trying to show workout routine details. {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var routine = await workoutRoutineService.GetEditWorkoutPlanByIdAsync(id);
                return View(routine);
            }
            catch (Exception ex)
            {
                logger.LogError($"An error ocurred while fetching edit UI. {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(WorkoutRoutineEditViewModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var routine = await workoutRoutineService.EditWorkoutRoutine(model, id);
                return RedirectToAction(nameof(Index), new { id = routine.Id });
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occured while edditing workout routine. {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }
    }
}

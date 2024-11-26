using EnerGym.Data.Models;
using EnerGym.Services.Data.Interfaces;
using EnerGym.ViewModels.WorkoutRoutineViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EnerGym.Controllers
{
    public class WorkoutRoutineController(
        IWorkoutRoutineService workoutRoutineService,
        ILogger logger)
        : Controller
    {
        

        public async Task<IActionResult> Index()
        {
            try
            {
                var routines = await workoutRoutineService.GetAllWorkoutRoutinesAsync();
                return View(routines);
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

            }
            catch (Exception ex)
            {

                throw;
            }

            return RedirectToAction(nameof(Add));
        }

        //public async Task<IActionResult> AddToPlan()
    }
}

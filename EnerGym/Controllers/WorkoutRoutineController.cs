using EnerGym.Data.Models;
using EnerGym.Data.Repository.Interfaces;
using EnerGym.ViewModels.WorkoutRoutineViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EnerGym.Controllers
{
    public class WorkoutRoutineController : Controller
    {
        private readonly IRepository<WorkoutRoutine, int> workoutRoutineRepository;
        private readonly IRepository<WorkoutPlan, int> workoutPlanRepository;

        public WorkoutRoutineController(IRepository<WorkoutPlan, int> workoutPlanRepository,
            IRepository<WorkoutRoutine, int> workoutRoutineRepository)
        {
            this.workoutRoutineRepository = workoutRoutineRepository;
            this.workoutPlanRepository = workoutPlanRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
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

            WorkoutRoutine routine = new WorkoutRoutine()
            {
                ExerciseName = model.ExerciseName,
                Description = model.ExerciseDescription,
                Weight = model.Weight,
                Reps = model.Reps,
                Sets = model.Sets,
            };

            await workoutRoutineRepository.AddAsync(routine);

            return RedirectToAction(nameof(Add));
        }

        //public async Task<IActionResult> AddToPlan()
    }
}

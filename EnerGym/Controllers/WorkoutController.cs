using EnerGym.Data.Models;
using EnerGym.Data.Repository.Interfaces;
using EnerGym.ViewModels.WorkoutViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnerGym.Controllers
{
    public class WorkoutController : Controller
    {
        private readonly IRepository<WorkoutRoutine, int> workoutRoutineRepository;
        private readonly IRepository<WorkoutPlan, int> workoutPlanRepository;

        public WorkoutController(IRepository<WorkoutRoutine, int> workoutRoutineRepository,
            IRepository<WorkoutPlan, int> workoutPlanRepository)
        {
            this.workoutRoutineRepository = workoutRoutineRepository;
            this.workoutPlanRepository = workoutPlanRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {


            var plans = await workoutPlanRepository
                .GetAllAttached()
                .Where(p => p.IsDeleted == false)
                .Select(p => new WorkoutPlanInfoViewModel() 
                { 
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl
                })
                .AsNoTracking()
                .ToListAsync();

            return View(plans);
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

            WorkoutPlan plan = new WorkoutPlan()
            {
                Name = model.PlanName,
                Description = model.PlanDescription,
                Routines = model.Routines
            };

            await workoutPlanRepository.AddAsync(plan);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var entity = await workoutPlanRepository.GetByIdAsync(id);
            
            if (entity == null)
            {
                return NotFound();
            }

            var plan = new WorkoutPlanDetailsViewModel()
            {
                Name = entity.Name,
                ImageUrl = entity.ImageUrl,
                Description = entity.Description,
                Routines = entity.Routines
            };

            return View(plan);
        }

        [HttpGet]
        public async Task<IActionResult> AddRoutine()
        {
            var route = new WorkoutRoutineAddViewModel();

            return View(route);
        }

        private async Task<ICollection<WorkoutRoutine>> GetRoutines()
        {
            return await workoutRoutineRepository.GetAllAsync();
        }
    }
}

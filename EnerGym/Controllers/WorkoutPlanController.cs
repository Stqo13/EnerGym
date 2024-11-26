using EnerGym.Data.Models;
using EnerGym.Data.Repository.Interfaces;
using EnerGym.ViewModels.WorkoutPlanViewModels;
using EnerGym.ViewModels.WorkoutRoutineViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;

namespace EnerGym.Controllers
{
    public class WorkoutPlanController : Controller
    {
        private readonly ILogger<WorkoutPlanController> logger;
        private readonly IRepository<WorkoutRoutine, int> workoutRoutineRepository;
        private readonly IRepository<WorkoutPlan, int> workoutPlanRepository;

        public WorkoutPlanController(
            IRepository<WorkoutRoutine, int> workoutRoutineRepository,
            IRepository<WorkoutPlan, int> workoutPlanRepository,
            ILogger<WorkoutPlanController> logger)
        {
            this.workoutRoutineRepository = workoutRoutineRepository;
            this.workoutPlanRepository = workoutPlanRepository;
            this.logger = logger;
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
                Id = entity.Id,
                Name = entity.Name,
                ImageUrl = entity.ImageUrl,
                Description = entity.Description,
                Routines = entity.Routines
            };

            return View(plan);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var entity = await workoutPlanRepository.GetByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            var plan = new EditPlanViewModel()
            {
                Id = entity.Id,
                PlanName = entity.Name,
                ImageUrl = entity.ImageUrl,
                PlanDescription = entity.Description,
                Routines = entity.Routines
            };

            return View(plan);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPlanViewModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            WorkoutPlan plan = await workoutPlanRepository.GetByIdAsync(id);

            if (plan == null || plan.IsDeleted)
            {
                throw new ArgumentException("Invalid id!");
            }

            plan.Name = model.PlanName;
            plan.ImageUrl = model.ImageUrl;
            plan.Description = model.PlanDescription;
            plan.Routines = model.Routines;

            await workoutPlanRepository.UpdateAsync(plan);  

            return RedirectToAction(nameof(Details), new { id = plan.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var plan = await workoutPlanRepository
                .GetAllAttached()
                .Where(p => p.IsDeleted == false)
                .Where(p => p.Id == id)
                .Select(p => new DeletePlanViewModel()
                {
                    Id = p.Id,
                    PlanName = p.Name,
                    PublishedBy = GetCurrentClientId()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return View(plan);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeletePlanViewModel model)
        {
            WorkoutPlan? plan = await workoutPlanRepository
                .GetAllAttached()
                .Where(p => p.Id == model.Id)
                .Where(p => p.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (plan != null)
            {
                plan.IsDeleted = true;

                await workoutPlanRepository.UpdateAsync(plan);
            }

            return RedirectToAction(nameof(Index));
        }

        private string GetCurrentClientId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }

        private async Task<ICollection<WorkoutRoutine>> GetRoutines()
        {
            return await workoutRoutineRepository.GetAllAsync();
        }
    }
}

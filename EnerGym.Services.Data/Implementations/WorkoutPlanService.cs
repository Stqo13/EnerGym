#nullable disable

using EnerGym.Data.Models;
using EnerGym.Data.Repository.Interfaces;
using EnerGym.Services.Data.Interfaces;
using EnerGym.ViewModels.WorkoutPlanViewModels;
using EnerGym.ViewModels.WorkoutRoutineViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EnerGym.Services.Data.Implementations
{
    public class WorkoutPlanService (
        IRepository<WorkoutRoutine, int> workoutRoutineRepository,
        IRepository<WorkoutPlan, int> workoutPlanRepository,
        UserManager<ApplicationUser> userManager) 
        : IWorkoutPlanSevice
    {
        public async Task AddPlanAsync(WorkoutPlanAddViewModel model)
        {
            WorkoutPlan plan = new WorkoutPlan()
            {
                Name = model.PlanName,
                Description = model.PlanDescription,
                ImageUrl = model.ImageUrl,
                WorkoutRoutines = model.Routines
            };

            await workoutPlanRepository.AddAsync(plan);
        }

        public async Task<WorkoutPlan> EditPlanAsync(EditPlanViewModel model, int id)
        {
            WorkoutPlan plan = await workoutPlanRepository.GetByIdAsync(id);

            if (plan == null)
            {
                throw new ArgumentException("Invalid id!");
            }
            else if (plan.IsDeleted)
            {
                throw new ArgumentException("Plan already deleted!");
            }

            plan.Name = model.PlanName;
            plan.ImageUrl = model.ImageUrl;
            plan.Description = model.PlanDescription;

            await workoutPlanRepository.UpdateAsync(plan);

            return plan;
        }

        public async Task<IEnumerable<WorkoutPlanInfoViewModel>> GetAllAsync(int pageNumber = 1, int pageSize = 3)
        {
            var plans = await workoutPlanRepository
                .GetAllAttached()
                .Where(p => p.IsDeleted == false)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new WorkoutPlanInfoViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl
                })
                .AsNoTracking()
                .ToListAsync();

            return plans;
        }

        public async Task<int> GetTotalPagesAsync(int pageSize = 3)
        {
            var totalPlans = await workoutPlanRepository
                .GetAllAttached()
                .Where(p => p.IsDeleted == false)
                .CountAsync();

            return (int)Math.Ceiling(totalPlans / (double)pageSize);
        }

        public async Task<EditPlanViewModel> GetEditPlanByIdAsync(int id)
        {
            var entity = await workoutPlanRepository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new NullReferenceException("Entity not found!");
            }
            else if (entity.IsDeleted)
            {
                throw new ArgumentException("Entity is already deleted!");
            }

            var plan = new EditPlanViewModel()
            {
                Id = entity.Id,
                PlanName = entity.Name,
                ImageUrl = entity.ImageUrl,
                PlanDescription = entity.Description
            };

            return plan;
        }

        public async Task<WorkoutPlanDetailsViewModel> GetPlanDetailsAsync(int id)
        {
            var entity = await workoutPlanRepository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new NullReferenceException("Entity not found!");
            }
            else if (entity.IsDeleted)
            {
                throw new ArgumentException("Entity is already deleted!");
            }

            var plan = new WorkoutPlanDetailsViewModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                ImageUrl = entity.ImageUrl,
                Description = entity.Description
            };

            return plan;
        }

        public async Task<DeletePlanViewModel> GetDeletePlanByIdAsync(int id, string publishedBy)
        {
            var user = await userManager.FindByIdAsync(publishedBy);

            if (user == null)
            {
                throw new NullReferenceException("User Id was invalid!");
            }

            string username = $"{user.FirstName} {user.LastName}";

			var plan = await workoutPlanRepository
                .GetAllAttached()
                .Where(p => p.Id == id && p.IsDeleted == false)
                .Select(p => new DeletePlanViewModel()
                {
                    Id = p.Id,
                    PlanName = p.Name,
                    PublishedBy = username ?? string.Empty
                })
                .FirstOrDefaultAsync();


			if (plan == null)
            {
                throw new NullReferenceException("Entity not found!");
            }

            return plan;
        }

        public async Task DeletePlanAsync(DeletePlanViewModel model)
        {
            WorkoutPlan plan = await workoutPlanRepository
                .GetAllAttached()
                .Where(p => p.Id == model.Id && p.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (plan != null)
            {
                plan.IsDeleted = true;

                await workoutPlanRepository.UpdateAsync(plan);
            }
        }

        public async Task<IEnumerable<WorkoutRoutineSelectViewModel>> GetAllRoutinesAsync(string searchQuery, int? sets, int? reps)
        {
            var query = workoutRoutineRepository.GetAllAttached();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(r => EF.Functions.Like(r.ExerciseName, "%" + searchQuery + "%"));
            }

            if (sets.HasValue)
            {
                query = query.Where(r => r.Sets == sets);
            }

            if (reps.HasValue)
            {
                query = query.Where(r => r.Reps == reps);
            }

            var routinesAsQuery = query
                .Select(r => new WorkoutRoutineSelectViewModel()
                {
                    Id = r.Id,
                    ExerciseName = r.ExerciseName,
                    SearchQuery = searchQuery,
                    Sets = sets,
                    Reps = reps
                });

            var routines = await routinesAsQuery.ToListAsync();
                
            return routines;
        }

        public async Task AddRoutinesToPlanAsync(int workoutPlanId, List<int> routineIds)
        {
            var workoutPlan = await workoutPlanRepository.GetByIdAsync(workoutPlanId);

            if (workoutPlan == null)
            {
                throw new NullReferenceException("Workout plan not found!");
            }

            var routines = await workoutRoutineRepository
                .GetAllAttached()
                .Where(r => routineIds.Contains(r.Id))
                .ToListAsync();

            if (routines == null || routines.Count == 0)
            {
                throw new ArgumentException("No routines found with the provided IDs.");
            }

            foreach (var routine in routines)
            {
                if (!workoutPlan.WorkoutRoutines.Contains(routine))
                {
                    workoutPlan.WorkoutRoutines.Add(routine);
                }
            }

            await workoutPlanRepository.UpdateAsync(workoutPlan);
        }

        public async Task<IEnumerable<WorkoutRoutineInfoViewModel>> GetRoutinesByPlanIdAsync(int id)
        {
            var routines = await workoutPlanRepository
                .GetAllAttached()
                .Where(p => p.Id == id && p.IsDeleted == false)
                .SelectMany(p => p.WorkoutRoutines)
                .Select(r => new WorkoutRoutineInfoViewModel()
                {
                    Id = r.Id,
                    ExerciseName = r.ExerciseName,
                    ExerciseDescription = r.Description,
                    Sets = r.Sets,
                    Reps = r.Reps
                })
                .AsNoTracking()
                .ToListAsync();

            return routines;
        }
        
    }
}
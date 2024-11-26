using EnerGym.Data.Models;
using EnerGym.Data.Repository.Interfaces;
using EnerGym.Services.Data.Interfaces;
using EnerGym.ViewModels.WorkoutPlanViewModels;
using Microsoft.EntityFrameworkCore;

namespace EnerGym.Services.Data.Implementations
{
    public class WorkoutPlanService (
        IRepository<WorkoutRoutine, int> workoutRoutineRepository,
        IRepository<WorkoutPlan, int> workoutPlanRepository) 
        : IWorkoutPlanSevice
    {
        public async Task AddPlanAsync(WorkoutPlanAddViewModel model)
        {
            WorkoutPlan plan = new WorkoutPlan()
            {
                Name = model.PlanName,
                Description = model.PlanDescription,
                Routines = model.Routines
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

        public async Task<IEnumerable<WorkoutPlanInfoViewModel>> GetAllAsync()
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

            return plans;
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
            var plan = await workoutPlanRepository
                .GetAllAttached()
                .Where(p => p.Id == id && p.IsDeleted == false)
                .Select(p => new DeletePlanViewModel()
                {
                    Id = p.Id,
                    PlanName = p.Name,
                    PublishedBy = publishedBy
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
            WorkoutPlan? plan = await workoutPlanRepository
                .GetAllAttached()
                .Where(p => p.Id == model.Id && p.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (plan != null)
            {
                plan.IsDeleted = true;

                await workoutPlanRepository.UpdateAsync(plan);
            }
        }
    }
}
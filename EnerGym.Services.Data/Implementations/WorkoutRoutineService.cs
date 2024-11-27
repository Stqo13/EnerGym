using EnerGym.Data.Models;
using EnerGym.Data.Repository.Interfaces;
using EnerGym.Services.Data.Interfaces;
using EnerGym.ViewModels.WorkoutRoutineViewModels;
using Microsoft.EntityFrameworkCore;

namespace EnerGym.Services.Data.Implementations
{
    public class WorkoutRoutineService (
        IRepository<WorkoutRoutine, int> workoutRoutineRepository,
        IRepository<WorkoutPlan, int> workoutPlanRepository)
        : IWorkoutRoutineService
    {
        public async Task AddWorkoutRoutineAsync(WorkoutRoutineAddViewModel model)
        {
            WorkoutRoutine routine = new WorkoutRoutine()
            {
                ExerciseName = model.ExerciseName,
                Description = model.ExerciseDescription,
                Weight = model.Weight,
                Sets = model.Sets,
                Reps = model.Reps
            };

            await workoutRoutineRepository.AddAsync(routine);
        }

        public async Task<IEnumerable<WorkoutRoutineInfoViewModel>> GetAllWorkoutRoutinesAsync()
        {
            var routines = await workoutRoutineRepository
                .GetAllAttached()
                .Where(r => r.IsDeleted == false)
                .Select(r => new WorkoutRoutineInfoViewModel()
                {
                    Id = r.Id,
                    ExerciseName = r.ExerciseName,
                    ExerciseDescription = r.Description,
                    Weight = r.Weight,
                    Reps = r.Reps,
                    Sets = r.Sets,
                })
                .AsNoTracking()
                .ToListAsync();

            return routines;
        }

        public async Task<WorkoutRoutineDeleteViewModel> GetDeleteWorkoutRoutineByIdAsync(int id)
        {
            var routine = await workoutRoutineRepository 
                .GetAllAttached()
                .Where(r => r.Id == id && r.IsDeleted == false)
                .Select(r => new WorkoutRoutineDeleteViewModel()
                {
                    Id = r.Id,
                    ExerciseName = r.ExerciseName,
                    ExerciseDescription = r.Description,
                    WorkoutPlanId = r.WorkoutPlanId,
                })
                .FirstOrDefaultAsync();

            if (routine == null)
            {
                throw new NullReferenceException($"Entity not found!");
            }

            return routine;

        }

        public async Task DeleteWorkoutRoutineAsync(WorkoutRoutineDeleteViewModel model)
        {
            WorkoutRoutine? routine = await workoutRoutineRepository
                .GetAllAttached()
                .Where(r => r.Id == model.Id && r.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (routine != null)
            {
                routine.IsDeleted = true;

                await workoutRoutineRepository.UpdateAsync(routine);
            }
        }
    }
}

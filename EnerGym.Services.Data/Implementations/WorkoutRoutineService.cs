using EnerGym.Data.Models;
using EnerGym.Data.Repository.Interfaces;
using EnerGym.Services.Data.Interfaces;
using EnerGym.ViewModels.WorkoutRoutineViewModels;
using Microsoft.EntityFrameworkCore;

namespace EnerGym.Services.Data.Implementations
{
    public class WorkoutRoutineService (
        IRepository<WorkoutRoutine, int> workoutRoutineRepository)
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

        public async Task<IEnumerable<WorkoutRoutineInfoViewModel>> GetAllWorkoutRoutinesAsync(int pageNumber = 1, int pageSize = 3)
        {
            var routines = await workoutRoutineRepository
                .GetAllAttached()
                .Where(r => r.IsDeleted == false)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
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

        public async Task<int> GetTotalPagesAsync(int pageSize = 3)
        {
            var totalRoutines = await workoutRoutineRepository
                .GetAllAttached()
                .Where(p => p.IsDeleted == false)
                .CountAsync();

            return (int)Math.Ceiling(totalRoutines / (double)pageSize);
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

        public async Task<WorkoutRoutineDetailsViewModel> GetRoutineDetailsAsync(int id)
        {
            var entity = await workoutRoutineRepository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new NullReferenceException("Entity not found!");
            }
            else if (entity.IsDeleted)
            {
                throw new ArgumentException("Entity is already deleted!");
            }

            var routine = new WorkoutRoutineDetailsViewModel()
            {
                Id = entity.Id,
                ExerciseName = entity.ExerciseName,
                ExerciseDescription = entity.Description,
                Weight = entity.Weight,
                Reps= entity.Reps,
                Sets = entity.Sets
            };

            return routine;
        }

        public async Task<WorkoutRoutineEditViewModel> GetEditWorkoutPlanByIdAsync(int id)
        {
            var entity = await workoutRoutineRepository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new NullReferenceException("Entity not found!");
            }
            else if (entity.IsDeleted)
            {
                throw new ArgumentException("Entity is already deleted!");
            }

            var routine = new WorkoutRoutineEditViewModel()
            {
                Id = entity.Id,
                ExerciseName = entity.ExerciseName,
                ExerciseDescription = entity.Description,
                Weight = entity.Weight,
                Reps = entity.Reps,
                Sets = entity.Sets
            };

            return routine;
        }

        public async Task<WorkoutRoutine> EditWorkoutRoutine(WorkoutRoutineEditViewModel model, int id)
        {
            WorkoutRoutine routine = await workoutRoutineRepository.GetByIdAsync(id);

            if (routine == null)
            {
                throw new ArgumentException("Invalid id!");
            }
            else if (routine.IsDeleted)
            {
                throw new ArgumentException("Routine already deleted!");
            }

            routine.ExerciseName = model.ExerciseName;
            routine.Description = model.ExerciseDescription;
            routine.Weight = model.Weight;
            routine.Reps = model.Reps;
            routine.Sets = model.Sets;

            await workoutRoutineRepository.UpdateAsync(routine);

            return routine;
        }
    }
}

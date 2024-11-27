using EnerGym.ViewModels.WorkoutRoutineViewModels;

namespace EnerGym.Services.Data.Interfaces
{
    public interface IWorkoutRoutineService
    {
        Task AddWorkoutRoutineAsync(WorkoutRoutineAddViewModel model);
        Task<IEnumerable<WorkoutRoutineInfoViewModel>> GetAllWorkoutRoutinesAsync();
        Task DeleteWorkoutRoutineAsync(WorkoutRoutineDeleteViewModel model);
        Task<WorkoutRoutineDeleteViewModel> GetDeleteWorkoutRoutineByIdAsync(int id);
        
    }
}

using EnerGym.Data.Models;
using EnerGym.ViewModels.WorkoutRoutineViewModels;

namespace EnerGym.Services.Data.Interfaces
{
    public interface IWorkoutRoutineService
    {
        Task AddWorkoutRoutineAsync(WorkoutRoutineAddViewModel model);
        Task<IEnumerable<WorkoutRoutineInfoViewModel>> GetAllWorkoutRoutinesAsync(int pageNumber = 1, int pageSize = 3);
        Task DeleteWorkoutRoutineAsync(WorkoutRoutineDeleteViewModel model);
        Task<WorkoutRoutineDeleteViewModel> GetDeleteWorkoutRoutineByIdAsync(int id);
        Task<WorkoutRoutineDetailsViewModel> GetRoutineDetailsAsync(int id);
        Task<int> GetTotalPagesAsync(int pageSize);
        Task<WorkoutRoutine> EditWorkoutRoutine(WorkoutRoutineEditViewModel model, int id);
        Task<WorkoutRoutineEditViewModel> GetEditWorkoutPlanByIdAsync(int id);
    }
}

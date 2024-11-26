using EnerGym.Data.Models;
using EnerGym.ViewModels.WorkoutPlanViewModels;
using EnerGym.ViewModels.WorkoutRoutineViewModels;

namespace EnerGym.Services.Data.Interfaces
{
    public interface IWorkoutPlanSevice
    {
        Task<IEnumerable<WorkoutPlanInfoViewModel>> GetAllAsync(int pageNumber, int pageSize);

        Task<int> GetTotalPagesAsync(int pageSize);

        Task AddPlanAsync(WorkoutPlanAddViewModel model);

        Task<EditPlanViewModel> GetEditPlanByIdAsync(int id);

        Task DeletePlanAsync(DeletePlanViewModel model);

        Task<WorkoutPlanDetailsViewModel> GetPlanDetailsAsync(int id);

        Task<WorkoutPlan> EditPlanAsync(EditPlanViewModel model, int id);

        Task<DeletePlanViewModel> GetDeletePlanByIdAsync(int id, string publishedBy);

        Task<IEnumerable<WorkoutRoutineSelectViewModel>> GetAllRoutinesAsync();

        Task AddRoutinesToPlanAsync(int planId, List<int> selectedRoutinesIds);

        Task<IEnumerable<WorkoutRoutineInfoViewModel>> GetRoutinesByPlanIdAsync(int id);
    }
}

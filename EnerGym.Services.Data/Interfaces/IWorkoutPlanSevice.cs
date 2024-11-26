using EnerGym.Data.Models;
using EnerGym.ViewModels.WorkoutPlanViewModels;

namespace EnerGym.Services.Data.Interfaces
{
    public interface IWorkoutPlanSevice
    {
        Task<IEnumerable<WorkoutPlanInfoViewModel>> GetAllAsync();

        Task AddPlanAsync(WorkoutPlanAddViewModel model);

        Task<EditPlanViewModel> GetEditPlanByIdAsync(int id);

        Task DeletePlanAsync(DeletePlanViewModel model);

        Task<WorkoutPlanDetailsViewModel> GetPlanDetailsAsync(int id);

        Task<WorkoutPlan> EditPlanAsync(EditPlanViewModel model, int id);

        Task<DeletePlanViewModel> GetDeletePlanByIdAsync(int id, string publishedBy);
    }
}

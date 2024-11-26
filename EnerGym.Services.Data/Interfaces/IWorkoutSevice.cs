using EnerGym.Data.Models;

namespace EnerGym.Services.Data.Interfaces
{
    public interface IWorkoutPlanSevice
    {
        Task GetAllAsync();

        Task AddPlanAsync();

        Task RemovePlanAsync();

        Task GetPlanDetailsAsync();

        Task EditPlanAsync(WorkoutPlan plan, int id);
    }
}

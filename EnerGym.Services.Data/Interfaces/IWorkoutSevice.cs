using EnerGym.Data.Models;

namespace EnerGym.Services.Data.Interfaces
{
    public interface IWorkoutSevice
    {
        Task CreateAsync();
        Task EditAsync();  
        Task DeleteAsync();

        Task<IEnumerable<WorkoutPlan>> GetAllAsync();
    }
}

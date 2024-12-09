using EnerGym.Data.Models;
using EnerGym.ViewModels.GymClassViewModels;

namespace EnerGym.Services.Data.Interfaces
{
    public interface IGymClassService
    {
        Task<IEnumerable<GymClassInfoViewModel>> GetAllGymClassesAsync(int pageNumber = 1, int pageSize = 3);
        Task<int> GetTotalPagesAsync(int pageSize = 3);
        Task AddGymClassAsync(GymClassAddViewModel model);
        Task<GymClassDetailsViewModel> GetGymClassDetailsAsync(int id);
        Task<GymClassEditViewModel> GetEditGymClassByIdAsync(int id);
        Task<GymClass> EditGymClassAsync(GymClassEditViewModel model, int id);
        Task<GymClassDeleteViewModel> GetDeleteGymClassByIdAsync(int id, string publishedBy);
        Task DeleteGymClassAsync(GymClassDeleteViewModel model);
        Task<GymClassDetailsViewModel> GetDetailsGymClassByIdAsync(int id);
        Task EnrollUserAsync(int id, string userId);
        Task<ScheduleAddViewModel> GetScheduletToClassAsync(int id);
        Task AddScheduleToClassAsync(ScheduleAddViewModel model);
        Task<IEnumerable<ScheduleInfoViewModel>> GetGymClassSchedulesAsync(int id);
    }
}

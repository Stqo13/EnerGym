using EnerGym.Data.Models;
using EnerGym.ViewModels.PersonalHallViewModels;

namespace EnerGym.Services.Data.Interfaces
{
    public interface IPersonalHallService
    {
        Task<IEnumerable<MembershipPlanInfoViewModel>> GetUserMembershipPlansAsync(string userId);
        Task<IEnumerable<GymClassInfoViewModel>> GetUserEnrolledClassesAsync(string userId);
    }
}

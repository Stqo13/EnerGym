using EnerGym.Data.Models;
using EnerGym.Data.Repository.Interfaces;
using EnerGym.Services.Data.Interfaces;
using EnerGym.ViewModels.PersonalHallViewModels;
using Microsoft.EntityFrameworkCore;

namespace EnerGym.Services.Data.Implementations
{
    public class PersonalHallService(
        IRepository<MembershipPlan, int> membershipPlanRepository,
        IRepository<AttendantClass, object> gymClassRepository)
        : IPersonalHallService
    {
        public async Task<IEnumerable<MembershipPlanInfoViewModel>> GetUserMembershipPlansAsync(string userId)
        {
            var plans = await membershipPlanRepository
                .GetAllAttached()
                .Where(p => p.AttendantId == userId)
                .Select(p => new MembershipPlanInfoViewModel()
                {
                    Id = p.Id,
                    PlanType = p.PlanType.ToString(),
                    Description = p.Description,
                    Price = p.Price,
                    DurationInMonth = p.Duration
                })
                .ToListAsync();

            return plans;
        }

        public async Task<IEnumerable<GymClassInfoViewModel>> GetUserEnrolledClassesAsync(string userId)
        {
            var classes = await gymClassRepository
                .GetAllAttached()
                .Where(c => c.AttendantId == userId)
                .Select(c => new GymClassInfoViewModel()
                {
                    Id = c.GymClassId,
                    ClassName = c.GymClass.ClassName,
                    Capacity = c.GymClass.Capacity,
                    IsActive = c.GymClass.IsActive,
                    InstructorName = c.GymClass.InstructorName
                })
                .ToListAsync();

            return classes;
        }
    }
}

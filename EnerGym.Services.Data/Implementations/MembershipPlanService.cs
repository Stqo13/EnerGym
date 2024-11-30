using EnerGym.Data.Models;
using EnerGym.Data.Repository.Interfaces;
using EnerGym.Services.Data.Interfaces;
using EnerGym.ViewModels.MembershipPlanViewModels;
using Microsoft.EntityFrameworkCore;

namespace EnerGym.Services.Data.Implementations
{
    public class MembershipPlanService(
        IRepository<MembershipPlan, int> membershipPlanRepository)
        : IMembershipPlanService
    {
        public async Task<IEnumerable<MembershipPlanInfoViewModel>> GetAllMembershipPlansAsync()
        {
            var membershipPlans = await membershipPlanRepository
                .GetAllAttached()
                .Select(m => new MembershipPlanInfoViewModel
                {
                    Id = m.Id,
                    PlanType = m.PlanType.ToString(),
                    Description = m.Description,
                    Price = m.Price,
                    DurationInMonth = m.Duration
                })
                .ToListAsync();

            return membershipPlans;
        }

        public Task<MembershipPlanDetailsViewModel> GetMembershipPlanDetails()
        {
            throw new NotImplementedException();
        }
    }
}

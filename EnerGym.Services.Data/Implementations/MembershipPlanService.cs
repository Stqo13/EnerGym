using EnerGym.Data.Repository.Interfaces;
using EnerGym.Services.Data.Interfaces;
using EnerGym.ViewModels.MembershipPlanViewModels;
using Microsoft.EntityFrameworkCore;

namespace EnerGym.Services.Data.Implementations
{
    public class MembershipPlanService(
        IRepository<MembershipPlanService, int> membershipPlanRepository)
        : IMembershipPlanService
    {
        public async Task<IEnumerable<MembershipPlanInfoViewModel>> GetAllMembershipPlansAsync()
        {
            var membershipPlans = await membershipPlanRepository
                .GetAllAttached()
                .Select(m => new MembershipPlanInfoViewModel()
                {

                })
                .ToListAsync();

            return membershipPlans;
        }
    }
}

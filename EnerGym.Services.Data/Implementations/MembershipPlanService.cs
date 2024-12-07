using EnerGym.Data;
using EnerGym.Data.Models;
using EnerGym.Data.Repository.Interfaces;
using EnerGym.Services.Data.Interfaces;
using EnerGym.ViewModels.MembershipPlanViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EnerGym.Services.Data.Implementations
{
    public class MembershipPlanService(
        IRepository<MembershipPlan, int> membershipPlanRepository,
        UserManager<ApplicationUser> userManager)
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

        public async Task<MembershipPlanDetailsViewModel> GetMembershipPlanObtainDetailsAsync(int id)
        {
            var entity = await membershipPlanRepository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new NullReferenceException("Entity was null");
            }

            MembershipPlanDetailsViewModel membershipPlan = new MembershipPlanDetailsViewModel()
            {
                Id = entity.Id,
                PlanType = entity.PlanType.ToString(),
                Description = entity.Description,
                Price = entity.Price,
                DurationInMonth = entity.Duration
            };

            return membershipPlan;
        }

        public async Task AddToPersonalHall(int planId, string userId)
        {
            ApplicationUser? user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new NullReferenceException("User was not found!");
            }

            MembershipPlan? plan = await membershipPlanRepository.GetByIdAsync(planId);

            if (plan == null)
            {
                throw new NullReferenceException("Entity was not found!");
            }

            bool hasPlan = user.MembershipPlans.Any(p => p.Id == planId);

            if (hasPlan)
            {
                throw new InvalidOperationException("User already has active plan!");
            }

            user.MembershipPlans.Add(plan);
        }
    }
}

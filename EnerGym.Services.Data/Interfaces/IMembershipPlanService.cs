﻿using EnerGym.ViewModels.MembershipPlanViewModels;

namespace EnerGym.Services.Data.Interfaces
{
    public interface IMembershipPlanService
    {
        Task<IEnumerable<MembershipPlanInfoViewModel>> GetAllMembershipPlansAsync();

        Task<MembershipPlanDetailsViewModel> GetMembershipPlanObtainDetailsAsync(int id);

        Task AddToPersonalHall(int planId, string userId);
    }
}

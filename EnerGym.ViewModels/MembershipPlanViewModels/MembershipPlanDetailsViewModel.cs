﻿namespace EnerGym.ViewModels.MembershipPlanViewModels
{
    public class MembershipPlanDetailsViewModel
    {
        public int Id { get; set; }

        public required string PlanType { get; set; }

        public string? Description { get; set; }

        public required decimal Price { get; set; }

        public required int DurationInMonth { get; set; }
    }
}

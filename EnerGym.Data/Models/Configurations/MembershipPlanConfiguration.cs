using EnerGym.Data.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnerGym.Data.Models.Configurations
{
    public class MembershipPlanConfiguration : IEntityTypeConfiguration<MembershipPlan>
    {
        public void Configure(EntityTypeBuilder<MembershipPlan> builder)
        {
            builder
                .HasData(this.CreateMembershipPlans());
        }

        private IEnumerable<MembershipPlan> CreateMembershipPlans()
        {
            IEnumerable<MembershipPlan> membershipPlans = new List<MembershipPlan>()
            {
                new MembershipPlan
                {
                    Id = 1,
                    PlanType = PlanType.Standart,
                    Price = 29.99m,
                    Description = "Basic monthly plan with access to all gym facilities.",
                    Duration = 1
                },
                new MembershipPlan
                {
                    Id = 2,
                    PlanType = PlanType.Premium,
                    Price = 79.99m,
                    Description = "Quarterly plan with a discount on personal training sessions.",
                    Duration = 3
                },
                new MembershipPlan
                {
                    Id = 3,
                    PlanType = PlanType.VIP,
                    Price = 299.99m,
                    Description = "Yearly plan with unlimited access to all classes and facilities.",
                    Duration = 12
                },
                new MembershipPlan
                {
                    Id = 4,
                    PlanType = PlanType.Student,
                    Price = 39.99m,
                    Description = "Monthly plan with access to premium equipment and classes.",
                    Duration = 1
                },
                new MembershipPlan
                {
                    Id = 5,
                    PlanType = PlanType.Senior,
                    Price = 109.99m,
                    Description = "Quarterly plan for couples with a discounted price.",
                    Duration = 3
                }
            };

            return membershipPlans;
        }
    }
}

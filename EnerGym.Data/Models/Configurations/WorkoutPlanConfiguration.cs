using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnerGym.Data.Models.Configurations
{
    public class WorkoutPlanConfiguration : IEntityTypeConfiguration<WorkoutPlan>
    {
        public void Configure(EntityTypeBuilder<WorkoutPlan> builder)
        {
            builder
                .HasData(this.CreateWorkoutPlans());
        }

        private IEnumerable<WorkoutPlan> CreateWorkoutPlans()
        {
            IEnumerable<WorkoutPlan> workoutPlans = new List<WorkoutPlan>()
            {
                new WorkoutPlan
                {
                    Id = 1,
                    Name = "Beginner Strength Training",
                    ImageUrl = "https://experiencelife.lifetime.life/wp-content/uploads/2022/07/etin22556950-card-getting-bigger-and-stronger-1136x640-1.jpg",
                    Description = "A strength-focused plan for beginners covering major muscle groups with basic exercises."
                },
                new WorkoutPlan
                {
                    Id = 2,
                    Name = "Cardio Blast",
                    ImageUrl = "https://www.verywellfit.com/thmb/Y-pUPTgW0nQOwfBz7ahVRaTMHBg=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/everything-you-need-to-know-about-cardio-1229553-8e08847ffcfb4845b29c08fa27e76d32.jpg",
                    Description = "An intense cardio plan aimed at improving stamina and cardiovascular health."
                },
                new WorkoutPlan
                {
                    Id = 3,
                    Name = "Flexibility and Mobility",
                    ImageUrl = "https://media.glamour.com/photos/64b8316162c0e3f198870e20/4:3/w_1440,h_1080,c_limit/0718-flexibility.png",
                    Description = "A plan designed to improve flexibility and range of motion with a series of daily stretches."
                },
                new WorkoutPlan
                {
                    Id = 4,
                    Name = "Weight Loss Program",
                    ImageUrl = "https://myauthentikspoon.com/wp-content/uploads/Strenght-Training-benefits-photo2_C-1024x1024.webp",
                    Description = "A comprehensive plan combining cardio and strength training to aid in weight loss."
                },
                new WorkoutPlan
                {
                    Id = 5,
                    Name = "Advanced Muscle Building",
                    ImageUrl = "https://i0.wp.com/www.muscleandfitness.com/wp-content/uploads/2019/01/Muscular-Fitness-Model-Looking-In-The-Mirror-Next-To-Dumbbell-Rack.jpg?quality=86&strip=all",
                    Description = "An advanced workout plan focusing on muscle hypertrophy with compound and isolation exercises."
                }
            };

            return workoutPlans;
        }
    }
}

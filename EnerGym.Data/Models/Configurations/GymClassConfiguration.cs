using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnerGym.Data.Models.Configurations
{
    public class GymClassConfiguration : IEntityTypeConfiguration<GymClass>
    {
        public void Configure(EntityTypeBuilder<GymClass> builder)
        {
            builder
                .HasData(this.CreateGymClasses());
        }

        private IEnumerable<GymClass> CreateGymClasses()
        {
            IEnumerable<GymClass> gymClasses = new List<GymClass>()
            {
                new GymClass
                {
                    Id = 1,
                    ClassName = "Yoga Basics",
                    Capacity = 20,
                    Description = "A beginner-friendly yoga class focusing on fundamental poses.",
                    IsActive = true,
                    InstructorName = "Pesho"
                },
                new GymClass
                {
                    Id = 2,
                    ClassName = "HIIT Training",
                    Capacity = 15,
                    Description = "High-Intensity Interval Training for a quick, powerful workout.",
                    IsActive = true,
                    InstructorName = "Teodor"
                },
                new GymClass
                {
                    Id = 3,
                    ClassName = "Strength and Conditioning",
                    Capacity = 25,
                    Description = "Strength-building exercises to improve overall muscle tone.",
                    IsActive = true,
                    InstructorName = "Aleksandar"
                }
            };

            return gymClasses;
        }
    }
}

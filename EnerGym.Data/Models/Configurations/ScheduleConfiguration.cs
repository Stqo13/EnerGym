using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnerGym.Data.Models.Configurations
{
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder
                .HasData(this.CreateSchedules());
        }

        private IEnumerable<Schedule> CreateSchedules()
        {
            IEnumerable<Schedule> schedules = new List<Schedule>()
            {
                new Schedule
                {
                    Id = 1,
                    Monday = true,
                    Wednesday = true,
                    Friday = true,
                    Week = new DateTime(2023, 10, 2),
                    TimeSchedule = new TimeOnly(9, 0),
                    GymClassId = 1
                },
                new Schedule
                {
                    Id = 2,
                    Tuesday = true,
                    Thursday = true,
                    Saturday = true,
                    Week = new DateTime(2023, 10, 9),
                    TimeSchedule = new TimeOnly(11, 0),
                    GymClassId = 1
                },
                new Schedule
                {
                    Id = 3,
                    Monday = true,
                    Thursday = true,
                    Week = new DateTime(2023, 10, 16),
                    TimeSchedule = new TimeOnly(7, 0),
                    GymClassId = 2
                },
                new Schedule
                {
                    Id = 4,
                    Wednesday = true,
                    Friday = true,
                    Sunday = true,
                    Week = new DateTime(2023, 10, 23),
                    TimeSchedule = new TimeOnly(16, 0),
                    GymClassId = 2
                },
                new Schedule
                {
                    Id = 5,
                    Tuesday = true,
                    Saturday = true,
                    Week = new DateTime(2023, 10, 30),
                    TimeSchedule = new TimeOnly(18, 30),
                    GymClassId = 3

                },
                new Schedule
                {
                    Id = 6,
                    Wednesday = true,
                    Friday = true,
                    Week = new DateTime(2024, 7, 30),
                    TimeSchedule = new TimeOnly(16, 30),
                    GymClassId = 3
                }
            };

            return schedules;
        }
    }
}

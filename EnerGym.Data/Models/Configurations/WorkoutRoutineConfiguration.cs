using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnerGym.Data.Models.Configurations
{
    public class WorkoutRoutineConfiguration : IEntityTypeConfiguration<WorkoutRoutine>
    {
        public void Configure(EntityTypeBuilder<WorkoutRoutine> builder)
        {
            builder
                .HasData(this.CreateWorkoutRoutines());
        }

        private IEnumerable<WorkoutRoutine> CreateWorkoutRoutines()
        {
            IEnumerable<WorkoutRoutine> workoutRoutines = new List<WorkoutRoutine>()
            {
                new WorkoutRoutine
                {
                    Id = 1,
                    ExerciseName = "Bench Press",
                    Description = "A compound upper-body exercise that targets the chest, shoulders, and triceps.",
                    Weight = 60.0,
                    Reps = 12,
                    Sets = 3,
                    WorkoutPlanId = 1
                },
                new WorkoutRoutine
                {
                    Id = 2,
                    ExerciseName = "Squats",
                    Description = "A full-body exercise that primarily targets the quadriceps, hamstrings, and glutes.",
                    Reps = 10,
                    Sets = 3,
                    WorkoutPlanId = 1
                },
                new WorkoutRoutine
                {
                    Id = 3,
                    ExerciseName = "Deadlifts",
                    Description = "A strength exercise that works the lower back, glutes, and hamstrings.",
                    Weight = 100.0,
                    Reps = 8,
                    Sets = 2,
                    WorkoutPlanId = 2
                },
                new WorkoutRoutine
                {
                    Id = 4,
                    ExerciseName = "Jump Rope",
                    Description = "A cardio exercise that improves cardiovascular endurance and agility.",
                    Reps = 200,
                    Sets = 4,
                    WorkoutPlanId = 2
                },
                new WorkoutRoutine
                {
                    Id = 5,
                    ExerciseName = "Lat Pulldown",
                    Description = "Strength exercise focused on the back muscles",
                    Weight = 70.0,
                    Reps = 12,
                    Sets = 3,
                    WorkoutPlanId = 3
                },
                new WorkoutRoutine
                {
                    Id = 6,
                    ExerciseName = "Leg Press",
                    Description = "Lower body exercise targeting quads and glutes",
                    Weight = 150.0,
                    Reps = 12,
                    Sets = 3,
                    WorkoutPlanId = 3
                },
                new WorkoutRoutine
                {
                    Id = 7,
                    ExerciseName = "Push-Ups",
                    Description = "An upper-body exercise targeting chest, shoulders, and triceps, performed with bodyweight.",
                    Reps = 20,
                    Sets = 5,
                    WorkoutPlanId = 4
                },
                new WorkoutRoutine
                {
                    Id = 8,
                    ExerciseName = "Dumbbell Curls",
                    Description = "Upper body exercise focusing on the biceps",
                    Weight = 15.0,
                    Reps = 12,
                    Sets = 3,
                    WorkoutPlanId = 4
                },
                new WorkoutRoutine
                {
                    Id = 9,
                    ExerciseName = "Tricep Dips",
                    Description = "Bodyweight exercise targeting the triceps",
                    Reps = 15,
                    Sets = 3,
                    WorkoutPlanId = 5
                }
            };

            return workoutRoutines;
        }
    }
}

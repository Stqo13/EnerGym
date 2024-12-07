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
                },
                new WorkoutRoutine
                {
                    Id = 10,
                    ExerciseName = "Barbell Squats",
                    Description = "A compound lower body exercise that targets the quads, glutes, and hamstrings.",
                    Weight = 80.0,
                    Reps = 10,
                    Sets = 4,
                    WorkoutPlanId = 1
                },
                new WorkoutRoutine
                {
                    Id = 11,
                    ExerciseName = "Incline Dumbbell Press",
                    Description = "Upper-body exercise focusing on the upper chest and shoulders.",
                    Weight = 30.0,
                    Reps = 12,
                    Sets = 3,
                    WorkoutPlanId = 1
                },
                new WorkoutRoutine
                {
                    Id = 12,
                    ExerciseName = "Romanian Deadlift",
                    Description = "A variation of the deadlift focusing on hamstrings, glutes, and lower back.",
                    Weight = 90.0,
                    Reps = 8,
                    Sets = 3,
                    WorkoutPlanId = 2
                },
                new WorkoutRoutine
                {
                    Id = 13,
                    ExerciseName = "Mountain Climbers",
                    Description = "A cardio and core exercise that also works shoulders and legs.",
                    Reps = 30,
                    Sets = 4,
                    WorkoutPlanId = 2
                },
                new WorkoutRoutine
                {
                    Id = 14,
                    ExerciseName = "Seated Cable Row",
                    Description = "A back exercise that targets the lats, traps, and biceps.",
                    Weight = 60.0,
                    Reps = 12,
                    Sets = 3,
                    WorkoutPlanId = 3
                },
                new WorkoutRoutine
                {
                    Id = 15,
                    ExerciseName = "Lunges",
                    Description = "Lower body exercise that targets quads, glutes, and hamstrings.",
                    Weight = 40.0,
                    Reps = 12,
                    Sets = 3,
                    WorkoutPlanId = 3
                },
                new WorkoutRoutine
                {
                    Id = 16,
                    ExerciseName = "Pull-Ups",
                    Description = "Bodyweight exercise that works the back and biceps.",
                    Reps = 10,
                    Sets = 4,
                    WorkoutPlanId = 4
                },
                new WorkoutRoutine
                {
                    Id = 17,
                    ExerciseName = "Dumbbell Shoulder Press",
                    Description = "Upper body exercise focusing on the deltoids and triceps.",
                    Weight = 20.0,
                    Reps = 12,
                    Sets = 3,
                    WorkoutPlanId = 4
                },
                new WorkoutRoutine
                {
                    Id = 18,
                    ExerciseName = "Close-Grip Bench Press",
                    Description = "A variation of the bench press that targets the triceps more.",
                    Weight = 50.0,
                    Reps = 10,
                    Sets = 3,
                    WorkoutPlanId = 5
                },
                new WorkoutRoutine
                {
                    Id = 19,
                    ExerciseName = "Cable Tricep Pushdown",
                    Description = "Isolation exercise for the triceps.",
                    Weight = 30.0,
                    Reps = 15,
                    Sets = 3,
                    WorkoutPlanId = 5
                },
                new WorkoutRoutine
                {
                    Id = 20,
                    ExerciseName = "Leg Curls",
                    Description = "Exercise that isolates the hamstrings.",
                    Weight = 40.0,
                    Reps = 12,
                    Sets = 4,
                    WorkoutPlanId = 1
                },
                new WorkoutRoutine
                {
                    Id = 21,
                    ExerciseName = "Chest Fly",
                    Description = "A machine or dumbbell exercise that targets the chest muscles.",
                    Weight = 20.0,
                    Reps = 12,
                    Sets = 3,
                    WorkoutPlanId = 1
                },
                new WorkoutRoutine
                {
                    Id = 22,
                    ExerciseName = "Leg Extensions",
                    Description = "Exercise targeting the quadriceps.",
                    Weight = 60.0,
                    Reps = 12,
                    Sets = 3,
                    WorkoutPlanId = 2
                },
                new WorkoutRoutine
                {
                    Id = 23,
                    ExerciseName = "Burpees",
                    Description = "A full-body exercise combining squats, push-ups, and jumps, improving strength and conditioning.",
                    Reps = 20,
                    Sets = 3,
                    WorkoutPlanId = 2
                },
                new WorkoutRoutine
                {
                    Id = 24,
                    ExerciseName = "Barbell Rows",
                    Description = "A back exercise targeting the lats, traps, and rhomboids.",
                    Weight = 70.0,
                    Reps = 12,
                    Sets = 3,
                    WorkoutPlanId = 3
                },
                new WorkoutRoutine
                {
                    Id = 25,
                    ExerciseName = "Dumbbell Lateral Raises",
                    Description = "An isolation exercise targeting the deltoids (shoulders).",
                    Weight = 10.0,
                    Reps = 12,
                    Sets = 3,
                    WorkoutPlanId = 3
                },
                new WorkoutRoutine
                {
                    Id = 26,
                    ExerciseName = "Planks",
                    Description = "Core stability exercise that strengthens the abdominals and lower back.",
                    Reps = 60,
                    Sets = 3,
                    WorkoutPlanId = 4
                },
                new WorkoutRoutine
                {
                    Id = 27,
                    ExerciseName = "Barbell Bicep Curls",
                    Description = "Strength exercise that isolates the biceps.",
                    Weight = 25.0,
                    Reps = 12,
                    Sets = 3,
                    WorkoutPlanId = 4
                },
                new WorkoutRoutine
                {
                    Id = 28,
                    ExerciseName = "Chest Press Machine",
                    Description = "A machine-based exercise targeting the chest muscles.",
                    Weight = 50.0,
                    Reps = 12,
                    Sets = 3,
                    WorkoutPlanId = 5
                },
                new WorkoutRoutine
                {
                    Id = 29,
                    ExerciseName = "Hammer Curls",
                    Description = "Bicep exercise targeting the brachialis and forearms.",
                    Weight = 15.0,
                    Reps = 12,
                    Sets = 3,
                    WorkoutPlanId = 5
                },
                new WorkoutRoutine
                {
                    Id = 30,
                    ExerciseName = "Hip Thrusts",
                    Description = "Lower body exercise targeting the glutes.",
                    Weight = 80.0,
                    Reps = 12,
                    Sets = 3,
                    WorkoutPlanId = 1
                },
                new WorkoutRoutine
                {
                    Id = 31,
                    ExerciseName = "Cable Chest Fly",
                    Description = "Chest exercise targeting the pectoral muscles with a cable machine.",
                    Weight = 25.0,
                    Reps = 12,
                    Sets = 3,
                    WorkoutPlanId = 1
                },
                new WorkoutRoutine
                {
                    Id = 32,
                    ExerciseName = "Dumbbell Lunges",
                    Description = "Lower body exercise that targets quads, glutes, and hamstrings with dumbbells.",
                    Weight = 30.0,
                    Reps = 12,
                    Sets = 3,
                    WorkoutPlanId = 2
                },
                new WorkoutRoutine
                {
                    Id = 33,
                    ExerciseName = "Battle Ropes",
                    Description = "A cardio and strength exercise that engages the full body, especially the arms and core.",
                    Reps = 30,
                    Sets = 3,
                    WorkoutPlanId = 2
                },
                new WorkoutRoutine
                {
                    Id = 34,
                    ExerciseName = "Barbell Overhead Press",
                    Description = "Upper body compound movement that targets the shoulders, triceps, and upper chest.",
                    Weight = 50.0,
                    Reps = 10,
                    Sets = 3,
                    WorkoutPlanId = 3
                },
                new WorkoutRoutine
                {
                    Id = 35,
                    ExerciseName = "Smith Machine Squats",
                    Description = "Variation of squats using a Smith machine for added stability.",
                    Weight = 100.0,
                    Reps = 10,
                    Sets = 3,
                    WorkoutPlanId = 3
                },
                new WorkoutRoutine
                {
                    Id = 36,
                    ExerciseName = "Chest Dips",
                    Description = "Bodyweight exercise that targets the chest, shoulders, and triceps.",
                    Reps = 12,
                    Sets = 3,
                    WorkoutPlanId = 4
                },
                new WorkoutRoutine
                {
                    Id = 37,
                    ExerciseName = "Incline Bicep Curls",
                    Description = "Bicep isolation exercise performed on an incline bench.",
                    Weight = 12.0,
                    Reps = 12,
                    Sets = 3,
                    WorkoutPlanId = 4
                },
                new WorkoutRoutine
                {
                    Id = 38,
                    ExerciseName = "Face Pulls",
                    Description = "Upper body exercise targeting the rear delts and traps.",
                    Weight = 20.0,
                    Reps = 12,
                    Sets = 3,
                    WorkoutPlanId = 5
                },
                new WorkoutRoutine
                {
                    Id = 39,
                    ExerciseName = "Close-Grip Push-Ups",
                    Description = "A variation of push-ups that emphasize the triceps.",
                    Reps = 15,
                    Sets = 3,
                    WorkoutPlanId = 5
                },
                new WorkoutRoutine
                {
                    Id = 40,
                    ExerciseName = "Leg Press Calf Raises",
                    Description = "Calf exercise performed on the leg press machine.",
                    Weight = 80.0,
                    Reps = 15,
                    Sets = 4,
                    WorkoutPlanId = 1
                }
            };

            return workoutRoutines;
        }
    }
}

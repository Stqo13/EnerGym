using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EnerGym.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedWorkoutRoutines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "WorkoutRoutines",
                columns: new[] { "Id", "Description", "ExerciseName", "IsDeleted", "Reps", "Sets", "Weight", "WorkoutPlanId" },
                values: new object[,]
                {
                    { 10, "A compound lower body exercise that targets the quads, glutes, and hamstrings.", "Barbell Squats", false, 10, 4, 80.0, 1 },
                    { 11, "Upper-body exercise focusing on the upper chest and shoulders.", "Incline Dumbbell Press", false, 12, 3, 30.0, 1 },
                    { 12, "A variation of the deadlift focusing on hamstrings, glutes, and lower back.", "Romanian Deadlift", false, 8, 3, 90.0, 2 },
                    { 13, "A cardio and core exercise that also works shoulders and legs.", "Mountain Climbers", false, 30, 4, null, 2 },
                    { 14, "A back exercise that targets the lats, traps, and biceps.", "Seated Cable Row", false, 12, 3, 60.0, 3 },
                    { 15, "Lower body exercise that targets quads, glutes, and hamstrings.", "Lunges", false, 12, 3, 40.0, 3 },
                    { 16, "Bodyweight exercise that works the back and biceps.", "Pull-Ups", false, 10, 4, null, 4 },
                    { 17, "Upper body exercise focusing on the deltoids and triceps.", "Dumbbell Shoulder Press", false, 12, 3, 20.0, 4 },
                    { 18, "A variation of the bench press that targets the triceps more.", "Close-Grip Bench Press", false, 10, 3, 50.0, 5 },
                    { 19, "Isolation exercise for the triceps.", "Cable Tricep Pushdown", false, 15, 3, 30.0, 5 },
                    { 20, "Exercise that isolates the hamstrings.", "Leg Curls", false, 12, 4, 40.0, 1 },
                    { 21, "A machine or dumbbell exercise that targets the chest muscles.", "Chest Fly", false, 12, 3, 20.0, 1 },
                    { 22, "Exercise targeting the quadriceps.", "Leg Extensions", false, 12, 3, 60.0, 2 },
                    { 23, "A full-body exercise combining squats, push-ups, and jumps, improving strength and conditioning.", "Burpees", false, 20, 3, null, 2 },
                    { 24, "A back exercise targeting the lats, traps, and rhomboids.", "Barbell Rows", false, 12, 3, 70.0, 3 },
                    { 25, "An isolation exercise targeting the deltoids (shoulders).", "Dumbbell Lateral Raises", false, 12, 3, 10.0, 3 },
                    { 26, "Core stability exercise that strengthens the abdominals and lower back.", "Planks", false, 60, 3, null, 4 },
                    { 27, "Strength exercise that isolates the biceps.", "Barbell Bicep Curls", false, 12, 3, 25.0, 4 },
                    { 28, "A machine-based exercise targeting the chest muscles.", "Chest Press Machine", false, 12, 3, 50.0, 5 },
                    { 29, "Bicep exercise targeting the brachialis and forearms.", "Hammer Curls", false, 12, 3, 15.0, 5 },
                    { 30, "Lower body exercise targeting the glutes.", "Hip Thrusts", false, 12, 3, 80.0, 1 },
                    { 31, "Chest exercise targeting the pectoral muscles with a cable machine.", "Cable Chest Fly", false, 12, 3, 25.0, 1 },
                    { 32, "Lower body exercise that targets quads, glutes, and hamstrings with dumbbells.", "Dumbbell Lunges", false, 12, 3, 30.0, 2 },
                    { 33, "A cardio and strength exercise that engages the full body, especially the arms and core.", "Battle Ropes", false, 30, 3, null, 2 },
                    { 34, "Upper body compound movement that targets the shoulders, triceps, and upper chest.", "Barbell Overhead Press", false, 10, 3, 50.0, 3 },
                    { 35, "Variation of squats using a Smith machine for added stability.", "Smith Machine Squats", false, 10, 3, 100.0, 3 },
                    { 36, "Bodyweight exercise that targets the chest, shoulders, and triceps.", "Chest Dips", false, 12, 3, null, 4 },
                    { 37, "Bicep isolation exercise performed on an incline bench.", "Incline Bicep Curls", false, 12, 3, 12.0, 4 },
                    { 38, "Upper body exercise targeting the rear delts and traps.", "Face Pulls", false, 12, 3, 20.0, 5 },
                    { 39, "A variation of push-ups that emphasize the triceps.", "Close-Grip Push-Ups", false, 15, 3, null, 5 },
                    { 40, "Calf exercise performed on the leg press machine.", "Leg Press Calf Raises", false, 15, 4, 80.0, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WorkoutRoutines",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "WorkoutRoutines",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "WorkoutRoutines",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "WorkoutRoutines",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "WorkoutRoutines",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "WorkoutRoutines",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "WorkoutRoutines",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "WorkoutRoutines",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "WorkoutRoutines",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "WorkoutRoutines",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "WorkoutRoutines",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "WorkoutRoutines",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "WorkoutRoutines",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "WorkoutRoutines",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "WorkoutRoutines",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "WorkoutRoutines",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "WorkoutRoutines",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "WorkoutRoutines",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "WorkoutRoutines",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "WorkoutRoutines",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "WorkoutRoutines",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "WorkoutRoutines",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "WorkoutRoutines",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "WorkoutRoutines",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "WorkoutRoutines",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "WorkoutRoutines",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "WorkoutRoutines",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "WorkoutRoutines",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "WorkoutRoutines",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "WorkoutRoutines",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "WorkoutRoutines",
                keyColumn: "Id",
                keyValue: 40);
        }
    }
}

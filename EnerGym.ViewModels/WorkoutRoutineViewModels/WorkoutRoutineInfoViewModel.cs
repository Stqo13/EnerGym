namespace EnerGym.ViewModels.WorkoutRoutineViewModels
{
    public class WorkoutRoutineInfoViewModel
    {
        public int Id { get; set; }
        public required string ExerciseName { get; set; } = null!;
        public string? ExerciseDescription { get; set; }
        public double? Weight { get; set; }
        public string? ImageUrl { get; set; }
        public required int Reps { get; set; }
        public required int Sets { get; set; }
    }
}

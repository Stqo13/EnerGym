namespace EnerGym.ViewModels.WorkoutRoutineViewModels
{
    public class WorkoutRountineInfoViewModel
    {
        public int Id { get; set; }
        public required string ExerciseName { get; set; } = null!;
        public string? ExerciseDescription { get; set; }
        public int? Weight { get; set; }
        public required int Reps { get; set; }
        public required int Sets { get; set; }
    }
}

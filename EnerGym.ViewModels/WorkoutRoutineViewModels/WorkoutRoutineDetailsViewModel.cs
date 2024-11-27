namespace EnerGym.ViewModels.WorkoutRoutineViewModels
{
    public class WorkoutRoutineDetailsViewModel
    {
        public int Id { get; set; }
        public required string ExerciseName { get; set; } = null!;
        public string? ExerciseDescription { get; set; }
        public int Reps { get; set; }
        public int Sets { get; set; }
        public double? Weight { get; set; }


    }
}

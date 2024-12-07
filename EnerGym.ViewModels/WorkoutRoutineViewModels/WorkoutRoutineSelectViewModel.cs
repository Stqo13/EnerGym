using Microsoft.AspNetCore.Routing;

namespace EnerGym.ViewModels.WorkoutRoutineViewModels
{
    public class WorkoutRoutineSelectViewModel
    {
        public int Id { get; set; }

        public required string ExerciseName { get; set; } = null!;

        public string? SearchQuery { get; set; }

        public int? Sets { get; set; }

        public int? Reps { get; set; }
    }
}

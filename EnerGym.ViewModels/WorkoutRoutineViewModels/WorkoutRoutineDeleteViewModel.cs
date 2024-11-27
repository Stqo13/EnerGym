namespace EnerGym.ViewModels.WorkoutRoutineViewModels
{
	public class WorkoutRoutineDeleteViewModel
	{
		public required int Id { get; set; }

		public required string ExerciseName { get; set; } = null!;

		public string? ExerciseDescription { get; set;}

		public required int WorkoutPlanId { get; set; }
	}
}

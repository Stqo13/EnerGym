using System.ComponentModel.DataAnnotations;
using static EnerGym.Common.ApplicationConstraints.WorkoutRoutineConstraints;

namespace EnerGym.ViewModels.WorkoutRoutineViewModels
{
    public class WorkoutRoutineEditViewModel
    {
        [Required]
        public int Id { get; set; }

        [StringLength(ExerciseNameMaxLength, MinimumLength = ExerciseNameMinLength, ErrorMessage = "Exercise must be between 20 and 100 characters.")]
        [Required]
        public string ExerciseName { get; set; } = null!;

        [StringLength(DescriptionMaxLength, ErrorMessage = "Description mustn't exceed 300 characters.")]
        public string? ExerciseDescription { get; set; }

        public double? Weight { get; set; }

        [Required]
        public int Reps { get; set; }
        [Required]
        public int Sets { get; set; }
    }
}

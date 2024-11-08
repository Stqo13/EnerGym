using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static EnerGym.Common.ApplicationConstraints.WorkoutRoutineConstraints;

namespace EnerGym.Data.Models
{
    public class WorkoutRoutine
    {
        [Comment("Workout Routine Identifier")]
        [Key]
        public int Id { get; set; }

        [Comment("Workout Routine Exercise Name")]
        [Required]
        [MaxLength(ExerciseNameMaxLenght)]
        public string ExerciseName { get; set; } = null!;

        [Comment("Exercise Description")]
        [Required]
        [MaxLength(DescriptionMaxLenght)]
        public string Description { get; set; } = null!;

        [Comment("Exercise Equipment Weight")]
        [Required]
        public double Weight { get; set; }

        [Comment("Exercise Reps")]
        [Required]
        public int Reps { get; set; }

        [Comment("Workout Plan Foreign Key")]
        public int WorkoutPlanId { get; set; }

        [Comment("Workout Plan Navigation Property")]
        [ForeignKey(nameof(WorkoutPlanId))]
        public virtual WorkoutPlan WorkoutPlan { get; set; } = null!;
    }
}

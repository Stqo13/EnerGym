using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static EnerGym.Common.ApplicationConstraints.WorkoutPlanConstraints;

namespace EnerGym.Data.Models
{
    public class WorkoutPlan
    {
        [Comment("Workout Plant Identifier")]
        [Key]
        public int Id { get; set; }

        [Comment("Workout Plan Name")]
        [Required]
        [MaxLength(PlanNameMaxLenght)]
        public string Name { get; set; } = null!;

        [Comment("Workout Plan Description")]
        [Required]
        [MaxLength(DescriptionMaxLenght)]
        public string Description { get; set; } = null!;

        public virtual ICollection<WorkoutRoutine> Routines { get; set; }
            = new List<WorkoutRoutine>();
    }
}

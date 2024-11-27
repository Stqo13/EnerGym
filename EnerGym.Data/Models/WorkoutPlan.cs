using Azure.Identity;
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
        [MaxLength(PlanNameMaxLength)]
        public string Name { get; set; } = null!;

        [Comment("Workout plan image url")]
        public string? ImageUrl { get; set; }

        [Comment("Workout Plan Description")]
        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Comment("Soft delete")]
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<WorkoutRoutine> WorkoutRoutines { get; set; }
            = new List<WorkoutRoutine>();
    }
}

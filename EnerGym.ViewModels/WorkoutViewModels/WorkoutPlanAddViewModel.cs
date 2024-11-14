using EnerGym.Data.Models;
using System.ComponentModel.DataAnnotations;
using static EnerGym.Common.ApplicationConstraints.WorkoutPlanConstraints;

namespace EnerGym.ViewModels.WorkoutViewModels
{
    public class WorkoutPlanAddViewModel
    {
        [StringLength(PlanNameMaxLength, MinimumLength = PlanNameMinLength, ErrorMessage = "Plan name must be between 20 and 70 characters.")]
        [Required]
        public string PlanName { get; set; } = null!;

        public string? ImageUrl { get; set; }

        [StringLength(DescriptionMaxLength, ErrorMessage = "Description mustn't exceed 300 characters.")]
        public string? PlanDescription { get; set; }

        public List<WorkoutRoutine> Routines { get; set; }
            = new List<WorkoutRoutine>();
    }
}

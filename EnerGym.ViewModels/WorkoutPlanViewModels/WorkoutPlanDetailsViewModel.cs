using EnerGym.Data.Models;

namespace EnerGym.ViewModels.WorkoutPlanViewModels
{
    public class WorkoutPlanDetailsViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }

        public ICollection<WorkoutRoutine> Routines
            = new List<WorkoutRoutine>();
    }
}

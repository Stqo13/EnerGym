using EnerGym.ViewModels.WorkoutRoutineViewModels;

namespace EnerGym.ViewModels.WorkoutPlanViewModels
{
    public class WorkoutPlanWithRoutinesViewModel
    {
        public int Id { get; set; } 

        public ICollection<WorkoutRoutineInfoViewModel> WorkoutRoutines { get; set; }
            = new List<WorkoutRoutineInfoViewModel>();  
    }
}

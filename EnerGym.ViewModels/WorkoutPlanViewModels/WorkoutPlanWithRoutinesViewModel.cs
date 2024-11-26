using EnerGym.ViewModels.WorkoutRoutineViewModels;

namespace EnerGym.ViewModels.WorkoutPlanViewModels
{
    public class WorkoutPlanWithRoutinesViewModel
    {
        public WorkoutPlanDetailsViewModel? Plan { get; set; } 

        public IEnumerable<WorkoutRoutineInfoViewModel> WorkoutRoutines { get; set; }
            = new List<WorkoutRoutineInfoViewModel>();  
    }
}

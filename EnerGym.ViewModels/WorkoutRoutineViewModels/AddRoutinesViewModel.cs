namespace EnerGym.ViewModels.WorkoutRoutineViewModels
{
    public class AddRoutinesViewModel
    {
        public int WorkoutPlanId { get; set; }
        public IEnumerable<WorkoutRoutineInfoViewModel> AvailableRoutines { get; set; }
             = new List<WorkoutRoutineInfoViewModel>();
        public List<int> SelectedRoutineIds { get; set; } 
             = new List<int>();
    }
}

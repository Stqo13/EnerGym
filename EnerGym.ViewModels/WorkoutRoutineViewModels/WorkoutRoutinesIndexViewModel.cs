namespace EnerGym.ViewModels.WorkoutRoutineViewModels
{
    public class WorkoutRoutinesIndexViewModel
    {
        public IEnumerable<WorkoutRoutineInfoViewModel> WorkoutRoutines { get; set; }
            = new List<WorkoutRoutineInfoViewModel>();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}

namespace EnerGym.ViewModels.WorkoutPlanViewModels
{
    public class WorkoutPlansIndexViewModel
    {
        public IEnumerable<WorkoutPlanInfoViewModel> WorkoutPlans { get; set; }
            = new List<WorkoutPlanInfoViewModel>();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}

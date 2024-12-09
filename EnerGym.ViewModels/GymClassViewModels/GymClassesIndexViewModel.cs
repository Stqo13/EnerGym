using EnerGym.ViewModels.WorkoutPlanViewModels;

namespace EnerGym.ViewModels.GymClassViewModels
{
    public class GymClassesIndexViewModel
    {
        public IEnumerable<GymClassInfoViewModel> GymClasses { get; set; }
            = new List<GymClassInfoViewModel>();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}

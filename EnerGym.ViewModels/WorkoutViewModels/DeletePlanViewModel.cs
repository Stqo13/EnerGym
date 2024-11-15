namespace EnerGym.ViewModels.WorkoutViewModels
{
    public class DeletePlanViewModel
    {
        public int Id { get; set; }

        public required string PlanName { get; set; }

        public required string PublishedBy { get; set; }
    }
}

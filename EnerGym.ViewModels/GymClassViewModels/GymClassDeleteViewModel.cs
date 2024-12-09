namespace EnerGym.ViewModels.GymClassViewModels
{
    public class GymClassDeleteViewModel
    {
        public int Id { get; set; }

        public required string ClassName { get; set; } = null!;

        public required string PublishedBy { get; set; } = null!;
    }
}

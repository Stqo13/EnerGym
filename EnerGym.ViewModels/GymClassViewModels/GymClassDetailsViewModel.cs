namespace EnerGym.ViewModels.GymClassViewModels
{
    public class GymClassDetailsViewModel
    {
        public required int Id { get; set; }
        public required string ClassName { get; set; } = null!;

        public string? Description { get; set; }
        public int Capacity { get; set; }

        public bool IsActive { get; set; }

        public required string InstructorName { get; set; } = null!;
    }
}

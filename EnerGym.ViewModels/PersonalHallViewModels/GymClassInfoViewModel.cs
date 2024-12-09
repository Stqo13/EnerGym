namespace EnerGym.ViewModels.PersonalHallViewModels
{
    public class GymClassInfoViewModel
    {
        public int Id { get; set; }
        public required string ClassName { get; set; } = null!;

        public int Capacity { get; set; }

        public bool IsActive { get; set; }

        public required string InstructorName { get; set; } = null!;
    }
}

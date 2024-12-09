namespace EnerGym.ViewModels.GymClassViewModels
{
    public class ScheduleInfoViewModel
    {
        public int ScheduleId { get; set; }
        public string? TimeSchedule { get; set; }
        public DateTime StartDate { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace EnerGym.ViewModels.GymClassViewModels
{
    public class ScheduleAddViewModel
    {
        public int GymClassId { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        public DateTime Week { get; set; }

        [Required(ErrorMessage = "Please specify a time.")]
        public TimeOnly TimeSchedule { get; set; }
    }
}

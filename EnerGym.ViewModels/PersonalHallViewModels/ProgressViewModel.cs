using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static EnerGym.Common.ApplicationConstraints.ProgressConstraints;

namespace EnerGym.ViewModels.PersonalHallViewModels
{
    public class ProgressViewModel
    {
        [Comment("Attendant's current weight")]
        [Required]
        public double Weight { get; set; }

        [Comment("Attendant's Date Of Progress Record")]
        [Required]
        public DateTime Date { get; set; }

        [Comment("Attedant's Total Reps For The Date's Plan")]
        [Required]
        public int TotalReps { get; set; }

        [Comment("Attedant's Total Sets For The Date's Plan")]
        [Required]
        public int TotalSets { get; set; }

        [Comment("List Of Future Goals")]
        [StringLength(NotesMaxLength)]
        public string? Notes { get; set; }
    }
}

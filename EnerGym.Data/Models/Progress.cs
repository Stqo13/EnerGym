using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static EnerGym.Common.ApplicationConstraints.ProgressConstraints;

namespace EnerGym.Data.Models
{
    public class Progress
    {
        [Comment("Attendant's Progress Identifier")]
        [Key]
        public int Id { get; set; }

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
        [MaxLength(NotesMaxLenght)]
        public string? Notes { get; set; }

        [Comment("Attendant's Progress Foreign Key")]
        [Required]
        public string AttendantId { get; set; } = null!;

        [ForeignKey(nameof(AttendantId))]
        public virtual ApplicationUser Attendant { get; set; } = null!;
    }
}

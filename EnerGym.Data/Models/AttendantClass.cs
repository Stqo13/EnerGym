using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnerGym.Data.Models
{
    [PrimaryKey(nameof(GymClassId), nameof(AttendantId))]
    public class AttendantClass
    {
        [Comment("Gym Class Foreign Key")]
        [Required]
        public int GymClassId { get; set; }

        [Comment("Gym Class Navigation Property")]
        [ForeignKey(nameof(GymClassId))]
        public virtual GymClass GymClass { get; set; } = null!;

        [Comment("Attendant Foreign Key")]
        [Required]
        public string AttendantId { get; set; } = null!;

        [Comment("Attendant Navigation Property")]
        [ForeignKey(nameof(AttendantId))]
        public ApplicationUser Attendant { get; set; } = null!;
    }
}

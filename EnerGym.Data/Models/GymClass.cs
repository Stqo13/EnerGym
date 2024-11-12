using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static EnerGym.Common.ApplicationConstraints.GymClassConstraints;

namespace EnerGym.Data.Models
{
    public class GymClass
    {
        [Comment("Gym Class Identifier")]
        [Key]
        public int Id { get; set; }

        [Comment("Gym Class Name")]
        [Required]
        [MaxLength(ClassNameMaxLength)]
        public string ClassName { get; set; } = null!;

        [Comment("Gym Class Attendants Capacity")]
        [Required]
        public int Capacity { get; set; }

        [Comment("Gym Class Description")]
        [MaxLength(DescriptionMaxLength)]
        public string? Description { get; set; }

        [Comment("Soft Delete Flag")]
        public bool IsActive { get; set; } = true;

        [Comment("Instructor Foreign Key")]
        [Required]
        public string InstructorId { get; set; } = null!;

        [Comment("Instructor Navigation Property")]
        [ForeignKey(nameof(InstructorId))]
        public virtual ApplicationUser Instructor { get; set; } = null!;

        /// <summary>
        /// Pagination with a list of class schedules 
        /// (Instructors can create schedule tables for the future)
        /// </summary>
        public virtual ICollection<Schedule> Schedule { get; set; }
            = new List<Schedule>();

        public virtual ICollection<AttendantClass> AttendantClasses { get; set; }
            = new List<AttendantClass>();
    }
}

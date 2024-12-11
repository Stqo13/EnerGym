using EnerGym.Data.Models;
using System.ComponentModel.DataAnnotations;
using static EnerGym.Common.ApplicationConstraints.GymClassConstraints;

namespace EnerGym.ViewModels.GymClassViewModels
{
    public class GymClassEditViewModel
    {
        public int Id { get; set; } 

        [StringLength(ClassNameMaxLength, MinimumLength = ClassNameMinLength, ErrorMessage = "Class name must be between 5 and 150 characters.")]
        [Required]
        public string ClassName { get; set; } = null!;

        [MaxLength(DescriptionMaxLength)]
        public string? Descripton { get; set; }

        [Required]
        public int Capacity { get; set; }

        [StringLength(InstructorNameMaxLength, MinimumLength = InstructorNameMinLength, ErrorMessage = "Instructor name must be between 5 and 70 characters.")]
        [Required]
        public string InstructorName { get; set; } = null!;

        public List<Schedule> Schedules { get; set; }
            = new List<Schedule>();
    }
}

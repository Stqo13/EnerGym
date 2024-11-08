using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static EnerGym.Common.ApplicationConstraints.ApplicationUserConstraints;

namespace EnerGym.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Comment("User's First Name")]
        [Required]
        [MaxLength(FirstNameMaxLenght)]
        public string FirstName { get; set; } = null!;

        [Comment("User's Last Name")]
        [Required]
        [MaxLength(LastNameMaxLenght)]
        public string LastName { get; set; } = null!;
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnerGym.Data.Models
{
    public class Schedule
    {
        [Comment("Schedule Identifier")]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The possible days available for the conducting of the gym class
        /// (checkbox input fields)
        /// </summary>
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }

        [Comment("The Week Of The Schedule")]
        [Required]
        public DateTime Week { get; set; }

        [Comment("Gym Class Foreign Key")]
        public int GymClassId { get; set; }

        [Comment("The Gym Class time of conducting")]
        [Required]
        public TimeOnly TimeSchedule { get; set; }

        [Comment("Gym Class Navigation Property")]
        [ForeignKey(nameof(GymClassId))]
        public virtual GymClass GymClass { get; set; } = null!;
    }
}

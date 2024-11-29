using EnerGym.Data.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static EnerGym.Common.ApplicationConstraints.MembershipPlanConstraints;

namespace EnerGym.Data.Models
{
    public class MembershipPlan
    {
        [Comment("Membership Plan Identifier")]
        [Key]
        public int Id { get; set; }

        [Comment("Membership Plan Type Options")]
        [Required]
        public PlanType PlanType { get; set; }

        /// <summary>
        /// The price is depending on the time period of the plan
        /// </summary>
        [Comment("Membership Plan Price")]
        [Required]
        public decimal Price { get; set; }

        [MaxLength(DescriptionMaxLenght)]
        public string? Description { get; set; }

        /// <summary>
        /// The plan will be tracked in mounths
        /// </summary>
        [Comment("Membership Plan Duration")]
        [Required]
        public int Duration { get; set; }

        [Comment("Attendat Identifier")]
        [Required]
        public string? AttendantId { get; set; }

        [Comment("Foreign Key To Identity User")]
        [ForeignKey(nameof(AttendantId))]
        public virtual ApplicationUser? Attendant { get; set; }
    }
}

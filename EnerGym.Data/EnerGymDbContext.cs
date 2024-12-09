using EnerGym.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EnerGym.Data
{
    public class EnerGymDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public EnerGymDbContext()
        {
        }

        public EnerGymDbContext(DbContextOptions<EnerGymDbContext> options) 
            : base(options)
        {
        }

        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;
        public virtual DbSet<AttendantClass> AttendantsClasses { get; set; } = null!;
        public virtual DbSet<GymClass> GymClasses { get; set; } = null!;
        public virtual DbSet<MembershipPlan> MembershipPlans { get; set; } = null!;
        public virtual DbSet<Schedule> Schedules { get; set; } = null!;
        public virtual DbSet<WorkoutPlan> WorkoutPlans { get; set; } = null!;
        public virtual DbSet<WorkoutRoutine> WorkoutRoutines { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

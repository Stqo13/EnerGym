using EnerGym.Data.Models;
using EnerGym.Data.Repository;
using EnerGym.Data.Repository.Interfaces;
using EnerGym.Services.Data.Implementations;
using EnerGym.Services.Data.Interfaces;

namespace EnerGym.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterRepositories(
                this IServiceCollection services)
        {
            services.AddScoped<IRepository<MembershipPlan, int>, Repository<MembershipPlan, int>>();
            services.AddScoped<IRepository<GymClass, int>, Repository<GymClass, int>>();
            services.AddScoped<IRepository<AttendantClass, object>, Repository<AttendantClass, object>>();
            services.AddScoped<IRepository<Progress, int>, Repository<Progress, int>>();
            services.AddScoped<IRepository<Schedule, int>, Repository<Schedule, int>>();
            services.AddScoped<IRepository<WorkoutPlan, int>, Repository<WorkoutPlan, int>>();
            services.AddScoped<IRepository<WorkoutRoutine, int>, Repository<WorkoutRoutine, int>>();

            return services;
        }

        public static IServiceCollection RegisterUserDefinedServices(
            this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWorkoutPlanSevice, WorkoutPlanService>();
            services.AddScoped<IWorkoutRoutineService, WorkoutRoutineService>();

            return services;
        }
    }
}

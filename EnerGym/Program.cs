using EnerGym.Data;
using EnerGym.Data.Models;
using EnerGym.Data.Models.Configurations;
using EnerGym.Data.Repository;
using EnerGym.Data.Repository.Interfaces;
using EnerGym.Services.Data.Implementations;
using EnerGym.Services.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EnerGym
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //string adminEmail = builder.Configuration.GetValue<string>("Administrator:Email")!;

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<EnerGymDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<EnerGymDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(cfg =>
            {
                cfg.LoginPath = "/Identity/Account/Login";
            });

            //Need to add for all the tables that need the repo
            builder.Services.AddScoped<IRepository<MembershipPlan, int>, Repository<MembershipPlan, int>>();
            builder.Services.AddScoped<IRepository<GymClass, int>, Repository<GymClass, int>>();
            builder.Services.AddScoped<IRepository<AttendantClass, object>, Repository<AttendantClass, object>>();
            builder.Services.AddScoped<IRepository<Progress, int>, Repository<Progress, int>>();
            builder.Services.AddScoped<IRepository<Schedule, int>, Repository<Schedule, int>>();
            builder.Services.AddScoped<IRepository<WorkoutPlan, int>, Repository<WorkoutPlan, int>>();
            builder.Services.AddScoped<IRepository<WorkoutRoutine, int>, Repository<WorkoutRoutine, int>>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IWorkoutPlanSevice, WorkoutPlanService>();
            builder.Services.AddScoped<IWorkoutRoutineService, WorkoutRoutineService>();

            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<EnerGymDbContext>();

                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while creating the database.");
                }
            }

            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var context = scope.ServiceProvider.GetRequiredService<EnerGymDbContext>();

                if (app.Environment.IsDevelopment())
                {
                    await DbSeeder.SeedDatabase(userManager, roleManager, context);
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}

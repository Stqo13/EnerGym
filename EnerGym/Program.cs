using EnerGym.Data;
using EnerGym.Data.Models;
using EnerGym.Data.Models.Configurations;
using EnerGym.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static EnerGym.Common.ApplicationConstraints.ApplicationUserConstraints;

namespace EnerGym
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region UserInfo

            string adminEmail = builder.Configuration.GetValue<string>("Administrator:Email")!;
            string adminFirstName = builder.Configuration.GetValue<string>("Administrator:FirstName")!;
            string adminLastName = builder.Configuration.GetValue<string>("Administrator:LastName")!;
            string adminUsername = builder.Configuration.GetValue<string>("Administrator:UserName")!;
            string adminPassword = builder.Configuration.GetValue<string>("Administrator:Password")!;

            string[] instructorEmails = builder.Configuration.GetSection("Instrucors:Emails").Get<string[]>()!;
            string[] instructorFirstNames = builder.Configuration.GetSection("Instrucors:FirstNames").Get<string[]>()!;
            string[] instructorLastNames = builder.Configuration.GetSection("Instrucors:LastNames").Get<string[]>()!;
            string[] instructorUsernames = builder.Configuration.GetSection("Instrucors:Usernames").Get<string[]>()!;
            string[] instructorPasswords = builder.Configuration.GetSection("Instrucors:Passwords").Get<string[]>()!;

            string[] gymMemberEmails = builder.Configuration.GetSection("GymMembers:Emails").Get<string[]>()!;
            string[] gymMemberFirstNames = builder.Configuration.GetSection("GymMembers:FirstNames").Get<string[]>()!;
            string[] gymMemberLastNames = builder.Configuration.GetSection("GymMembers:LastNames").Get<string[]>()!;
            string[] gymMemberUsernames = builder.Configuration.GetSection("GymMembers:Usernames").Get<string[]>()!;
            string[] gymMemberPasswords = builder.Configuration.GetSection("GymMembers:Passwords").Get<string[]>()!;

            string[] userEmails = builder.Configuration.GetSection("Users:Emails").Get<string[]>()!;
            string[] userFirstNames = builder.Configuration.GetSection("Users:FirstNames").Get<string[]>()!;
            string[] userLastNames = builder.Configuration.GetSection("Users:LastNames").Get<string[]>()!;
            string[] userUsernames = builder.Configuration.GetSection("Users:Usernames").Get<string[]>()!;
            string[] userPasswords = builder.Configuration.GetSection("Users:Passwords").Get<string[]>()!;

            #endregion

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

            builder.Services
                .RegisterRepositories()
                .RegisterUserDefinedServices(); 

            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            #region SeedUsers

            await app.SeedSignleUserAsync(
                email: adminEmail,
                username: adminUsername,
                firstName: adminFirstName,
                lastName: adminLastName,
                password: adminPassword,
                roleName: AdminRole);

            await app.SeedMultipleUsersAsync(
                emails: instructorEmails,
                usernames: instructorUsernames,
                firstNames: instructorFirstNames,
                lastNames: instructorLastNames,
                passwords: instructorPasswords,
                roleName: InstructorRole
                );

            await app.SeedMultipleUsersAsync(
                emails: gymMemberEmails,
                usernames: gymMemberUsernames,
                firstNames: gymMemberFirstNames,
                lastNames: gymMemberLastNames,
                passwords: gymMemberPasswords,
                roleName: GymMemberRole
                );

            await app.SeedMultipleUsersAsync(
                emails: userEmails,
                usernames: userUsernames,
                firstNames: userFirstNames,
                lastNames: userLastNames,
                passwords: userPasswords,
                roleName: UserRole
                );
            
            #endregion

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=UserPanel}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}

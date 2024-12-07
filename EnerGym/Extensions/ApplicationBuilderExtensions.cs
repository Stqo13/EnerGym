using EnerGym.Data;
using EnerGym.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EnerGym.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task<IApplicationBuilder> SeedSignleUserAsync
            (this IApplicationBuilder app, string email, string username, string firstName, string lastName, string password, string roleName)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            IServiceProvider serviceProvider = scope.ServiceProvider;

            RoleManager<IdentityRole>? roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            IUserStore<ApplicationUser>? userStore = serviceProvider.GetService<IUserStore<ApplicationUser>>();
            UserManager<ApplicationUser>? userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            IdentityRole role = await EnsureRoleExists(roleManager, userStore, userManager, roleName);

            await EnsureUserExist(roleManager!, userStore!, userManager!, roleName, email,
                 username, firstName, lastName, password);

            return app;
        }

        public static async Task<IApplicationBuilder> SeedMultipleUsersAsync
            (this IApplicationBuilder app,
            string[] emails,
            string[] usernames,
            string[] firstNames,
            string[] lastNames,
            string[] passwords,
            string roleName)
        {
            if (!(emails.Length == usernames.Length 
                && usernames.Length == firstNames.Length 
                && firstNames.Length == lastNames.Length 
                && lastNames.Length == passwords.Length))
            {
                throw new ArgumentException("The {0} information arrays must have the same length.", roleName);
            }

            using IServiceScope scope = app.ApplicationServices.CreateScope();
            IServiceProvider serviceProvider = scope.ServiceProvider;

            RoleManager<IdentityRole>? roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            IUserStore<ApplicationUser>? userStore = serviceProvider.GetService<IUserStore<ApplicationUser>>();
            UserManager<ApplicationUser>? userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            IdentityRole role = await EnsureRoleExists(roleManager, userStore, userManager, roleName);

            for (int i = 0; i <= emails.Length - 1; i++)
            {
                await EnsureUserExist(roleManager!, userStore!, userManager!, roleName, emails[i],
                    usernames[i], firstNames[i], lastNames[i], passwords[i]);
            }

            return app;
        }

        private static async Task<IdentityRole> EnsureRoleExists
            (RoleManager<IdentityRole>? roleManager,
            IUserStore<ApplicationUser>? userStore, 
            UserManager<ApplicationUser>? userManager, 
            string roleName)
        {
            if (roleManager is null)
            {
                throw new ArgumentNullException(nameof(roleManager),
                    $"Service for {typeof(RoleManager<IdentityRole>)} cannot be obtained!");
            }

            if (userStore is null)
            {
                throw new ArgumentNullException(nameof(userStore),
                    $"Service for {typeof(IUserStore<ApplicationUser>)} cannot be obtained!");
            }

            if (userManager is null)
            {
                throw new ArgumentNullException(nameof(userManager),
                    $"Service for {typeof(UserManager<ApplicationUser>)} cannot be obtained!");
            }

            bool roleExists = await roleManager.RoleExistsAsync(roleName);

            IdentityRole? role = null;
            if (!roleExists)
            {
                role = new IdentityRole(roleName)
                {
                    Id = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                };

                IdentityResult result = await roleManager.CreateAsync(role);
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException($"Error occurred while creating the {roleName} role!");
                }
            }
            else
            {
                role = await roleManager.FindByNameAsync(roleName);
            }

            return role!;
        }

        private async static Task EnsureUserExist(
        RoleManager<IdentityRole> roleManager,
        IUserStore<ApplicationUser> userStore,
        UserManager<ApplicationUser> userManager,
        string roleName, string email, string username, string firstName, string lastName, string password)
        {
            ApplicationUser? user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                user ??= await CreateUserAsync(email, username, firstName, lastName,
                    password, userStore, userManager);
            }

            if (!await userManager.IsInRoleAsync(user, roleName))
            {
                IdentityResult result = await userManager.AddToRoleAsync(user, roleName);
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException($"Error occurred while adding the user {username} to the {roleName} role!");
                }
            }
        }

        private static async Task<ApplicationUser> CreateUserAsync(
           string email, string username, string firstName, string lastName, string password,
           IUserStore<ApplicationUser> userStore,
           UserManager<ApplicationUser> userManager)
        {
            var applicationUser = new ApplicationUser
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
            };

            await userStore.SetUserNameAsync(applicationUser, username, CancellationToken.None);

            IdentityResult result = await userManager.CreateAsync(applicationUser, password);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Error occurred while registering {username}!");
            }

            return applicationUser;
        }
    }
}

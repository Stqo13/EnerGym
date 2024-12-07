using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using EnerGym.Data.Models;

namespace EnerGym.Areas.Identity.Pages.Account
{
    public class ProfileModel(
        UserManager<ApplicationUser> userManager,
        ILogger<ProfileModel> logger)
        : PageModel
    {
        [BindProperty]
        public ApplicationUser CurrentUser { get; set; } = null!;

        public List<string> Roles { get; set; } = [];

        public async Task<IActionResult> OnGetAsync()
        {
            string userId = userManager
                .GetUserId(User)!;

            ApplicationUser? user = await userManager
                .FindByIdAsync(userId);

            if(user is null)
            {
                logger.LogError("No user found");
                ViewData["ErrorMessage"] = "User not found";

                return NotFound();
            }

            CurrentUser = user;

            Roles.AddRange(await userManager
                .GetRolesAsync(user));

            return Page();
        }
    }
}

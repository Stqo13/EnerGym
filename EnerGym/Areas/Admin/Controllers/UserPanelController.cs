using EnerGym.Services.Data.Interfaces;
using EnerGym.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static EnerGym.Common.ApplicationConstraints.ApplicationUserConstraints;

namespace EnerGym.Areas.Admin.Controllers
{
    [Area(AdminRole)]
    [Authorize(Roles = AdminRole)]
    public class UserPanelController 
        (IUserService userService)
        : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AllUsersViewModel> allUsers = await userService.GetAllUsersAsync();

            return View(allUsers);
        }
    }
}

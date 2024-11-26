using EnerGym.ViewModels.UserViewModels;

namespace EnerGym.Services.Data.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<AllUsersViewModel>> GetAllUsersAsync();

        Task<bool> UserExistsByIdAsync(string userId);

        Task<bool> AssignUserToRoleAsync(string userId, string roleName);

        Task<bool> RemoveUserFromRoleAsync(string userId, string roleName);

        Task<bool> DeleteUserAsync(string userId);
    }
}

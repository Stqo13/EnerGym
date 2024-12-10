using Moq;
using EnerGym.Data.Models;
using EnerGym.Services.Data.Implementations;
using Microsoft.AspNetCore.Identity;
using EnerGym.Services.Data.Interfaces;
using MockQueryable.Moq;
using EnerGym.Data.Repository.Interfaces;
using EnerGym.Data.Models.Enums;

namespace EnerGym.Tests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<UserManager<ApplicationUser>> _mockUserManager;
        private Mock<RoleManager<IdentityRole>> _mockRoleManager;
        private UserService _userService;

        [SetUp]
        public void SetUp()
        {
            // Mock IUserStore<ApplicationUser> since it's a dependency for UserManager
            var mockUserStore = new Mock<IUserStore<ApplicationUser>>();

            // Mock the UserManager
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(
                mockUserStore.Object,
                null!, null!, null!, null!, null!, null!, null!, null!);

            // Mock the RoleManager
            _mockRoleManager = new Mock<RoleManager<IdentityRole>>(
                new Mock<IRoleStore<IdentityRole>>().Object,
                null!, null!, null!, null!);

            // Create the UserService instance
            _userService = new UserService(_mockUserManager.Object, _mockRoleManager.Object);
        }

        #region AssignUserToRoleAsync Tests

        [Test]
        public async Task AssignUserToRoleAsync_ShouldReturnTrue_WhenRoleAssignmentIsSuccessful()
        {
            // Arrange
            var userId = "1";
            var roleName = "Admin";
            var user = new ApplicationUser { Id = userId };

            _mockUserManager.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync(user);
            _mockRoleManager.Setup(r => r.RoleExistsAsync(roleName)).ReturnsAsync(true);
            _mockUserManager.Setup(m => m.IsInRoleAsync(user, roleName)).ReturnsAsync(false);
            _mockUserManager.Setup(m => m.AddToRoleAsync(user, roleName)).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _userService.AssignUserToRoleAsync(userId, roleName);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task AssignUserToRoleAsync_ShouldReturnFalse_WhenUserNotFound()
        {
            // Arrange
            var userId = "1";
            var roleName = "Admin";

            _mockUserManager.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync((ApplicationUser)null!);

            // Act
            var result = await _userService.AssignUserToRoleAsync(userId, roleName);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task AssignUserToRoleAsync_ShouldReturnFalse_WhenRoleNotFound()
        {
            // Arrange
            var userId = "1";
            var roleName = "Admin";
            var user = new ApplicationUser { Id = userId };

            _mockUserManager.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync(user);
            _mockRoleManager.Setup(r => r.RoleExistsAsync(roleName)).ReturnsAsync(false);

            // Act
            var result = await _userService.AssignUserToRoleAsync(userId, roleName);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task AssignUserToRoleAsync_ShouldReturnFalse_WhenAddToRoleFails()
        {
            var userId = "1";
            var roleName = "Admin";
            _mockUserManager.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync(null as ApplicationUser);

            var result = await _userService.AssignUserToRoleAsync(userId, roleName);

            Assert.That(result, Is.False);
        }

        #endregion

        #region DeleteUserAsync Tests

        [Test]
        public async Task DeleteUserAsync_ShouldReturnTrue_WhenUserIsDeleted()
        {
            // Arrange
            var userId = "1";
            var user = new ApplicationUser { Id = userId };

            _mockUserManager.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync(user);
            _mockUserManager.Setup(m => m.DeleteAsync(user)).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _userService.DeleteUserAsync(userId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteUserAsync_ShouldReturnFalse_WhenUserNotFound()
        {
            // Arrange
            var userId = "1";

            _mockUserManager.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync((ApplicationUser)null!);

            // Act
            var result = await _userService.DeleteUserAsync(userId);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task DeleteUserAsync_ShouldReturnFalse_WhenDeleteFails()
        {
            // Arrange
            var userId = "1";
            var user = new ApplicationUser { Id = userId };

            _mockUserManager.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync(user);
            _mockUserManager.Setup(m => m.DeleteAsync(user)).ReturnsAsync(IdentityResult.Failed());

            // Act
            var result = await _userService.DeleteUserAsync(userId);

            // Assert
            Assert.IsFalse(result);
        }

        #endregion

        #region GetAllUsersAsync Tests

        [Test]
        public async Task GetAllUsersAsync_ShouldReturnUsers()
        {
            // Arrange
            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "1", Email = "user1@example.com" },
                new ApplicationUser { Id = "2", Email = "user2@example.com" }
            };

            var queryable = users.AsQueryable().BuildMockDbSet();

            _mockUserManager.Setup(m => m.Users)
                .Returns(queryable.Object);

            _mockUserManager.Setup(m => m.GetRolesAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(new List<string> { "Admin" });

            // Act
            var result = await _userService.GetAllUsersAsync();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().Email, Is.EqualTo("user1@example.com"));
        }


        #endregion

        #region RemoveUserFromRoleAsync Tests

        [Test]
        public async Task RemoveUserFromRoleAsync_ShouldReturnTrue_WhenRoleRemovedSuccessfully()
        {
            // Arrange
            var userId = "1";
            var roleName = "Admin";
            var user = new ApplicationUser { Id = userId };

            _mockUserManager.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync(user);
            _mockRoleManager.Setup(r => r.RoleExistsAsync(roleName)).ReturnsAsync(true);
            _mockUserManager.Setup(m => m.IsInRoleAsync(user, roleName)).ReturnsAsync(true);
            _mockUserManager.Setup(m => m.RemoveFromRoleAsync(user, roleName)).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _userService.RemoveUserFromRoleAsync(userId, roleName);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task RemoveUserFromRoleAsync_ShouldReturnFalse_WhenUserNotFound()
        {
            // Arrange
            var userId = "1";
            var roleName = "Admin";

            _mockUserManager.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync((ApplicationUser)null!);

            // Act
            var result = await _userService.RemoveUserFromRoleAsync(userId, roleName);

            // Assert
            Assert.IsFalse(result);
        }

        #endregion

        #region UserExistsByIdAsync Tests

        [Test]
        public async Task UserExistsByIdAsync_ShouldReturnTrue_WhenUserExists()
        {
            // Arrange
            var userId = "1";
            var user = new ApplicationUser { Id = userId };

            _mockUserManager.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync(user);

            // Act
            var result = await _userService.UserExistsByIdAsync(userId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task UserExistsByIdAsync_ShouldReturnFalse_WhenUserNotFound()
        {
            // Arrange
            var userId = "1";

            _mockUserManager.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync((ApplicationUser)null!);

            // Act
            var result = await _userService.UserExistsByIdAsync(userId);

            // Assert
            Assert.IsFalse(result);
        }

        #endregion

        #region RemoveUserRoleAsync Tests

        [Test]
        public async Task RemoveUserRoleAsync_ShouldReturnTrue_WhenRoleRemovedSuccessfully()
        {
            // Arrange
            var userId = "1";
            var roleName = "Admin";
            var user = new ApplicationUser { Id = userId };

            _mockUserManager.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync(user);
            _mockRoleManager.Setup(r => r.RoleExistsAsync(roleName)).ReturnsAsync(true);
            _mockUserManager.Setup(m => m.IsInRoleAsync(user, roleName)).ReturnsAsync(true);
            _mockUserManager.Setup(m => m.RemoveFromRoleAsync(user, roleName)).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _userService.RemoveUserRoleAsync(userId, roleName);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task RemoveUserRoleAsync_ShouldReturnFalse_WhenUserNotFound()
        {
            // Arrange
            var userId = "1";
            var roleName = "Admin";

            _mockUserManager.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync((ApplicationUser)null!);

            // Act
            var result = await _userService.RemoveUserRoleAsync(userId, roleName);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task RemoveUserRoleAsync_ShouldReturnFalse_WhenRoleNotExists()
        {
            // Arrange
            var userId = "1";
            var roleName = "Admin";
            var user = new ApplicationUser { Id = userId };

            _mockUserManager.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync(user);
            _mockRoleManager.Setup(r => r.RoleExistsAsync(roleName)).ReturnsAsync(false);

            // Act
            var result = await _userService.RemoveUserRoleAsync(userId, roleName);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task RemoveUserRoleAsync_ShouldReturnFalse_WhenRemoveFromRoleFails()
        {
            // Arrange
            var userId = "1";
            var roleName = "Admin";
            var user = new ApplicationUser { Id = userId };

            _mockUserManager.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync(user);
            _mockRoleManager.Setup(r => r.RoleExistsAsync(roleName)).ReturnsAsync(true);
            _mockUserManager.Setup(m => m.IsInRoleAsync(user, roleName)).ReturnsAsync(true);
            _mockUserManager.Setup(m => m.RemoveFromRoleAsync(user, roleName)).ReturnsAsync(IdentityResult.Failed());

            // Act
            var result = await _userService.RemoveUserRoleAsync(userId, roleName);

            // Assert
            Assert.IsFalse(result);
        }

        #endregion
    }
}


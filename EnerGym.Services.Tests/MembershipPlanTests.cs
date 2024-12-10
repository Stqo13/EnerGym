using EnerGym.Data.Models;
using EnerGym.Data.Models.Enums;
using EnerGym.Data.Repository.Interfaces;
using EnerGym.Services.Data.Implementations;
using Microsoft.AspNetCore.Identity;
using Moq;
using MockQueryable.Moq;

namespace EnerGym.Tests.Services
{
    [TestFixture]
    public class MembershipPlanServiceTests
    {
        private MembershipPlanService _service;
        private Mock<IRepository<MembershipPlan, int>> _mockRepository;
        private Mock<UserManager<ApplicationUser>> _mockUserManager;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IRepository<MembershipPlan, int>>();

            var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(
                mockUserStore.Object, null!, null!, null!, null!, null!, null!, null!, null!);

            _service = new MembershipPlanService(_mockRepository.Object, _mockUserManager.Object);
        }

        [Test]
        public async Task GetAllMembershipPlansAsync_ReturnsAllPlans()
        {
            // Arrange: Create sample data
            var membershipPlans = new[]
            {
                new MembershipPlan { Id = 1, PlanType = PlanType.Standart, Description = "Basic Plan", Price = 20, Duration = 1 },
                new MembershipPlan { Id = 2, PlanType = PlanType.Premium, Description = "Premium Plan", Price = 50, Duration = 3 },
                new MembershipPlan { Id = 3, PlanType = PlanType.VIP, Description = "VIP Plan", Price = 100, Duration = 6 }
            };

            var queryable = membershipPlans.AsQueryable().BuildMockDbSet();

            _mockRepository.Setup(repo => repo.GetAllAttached())
                .Returns(queryable.Object);

            // Act: Fetch all membership plans
            var result = await _service.GetAllMembershipPlansAsync();

            var itemsArray = result.ToArray();

            // Assert: Validate results
            Assert.Multiple(() =>
            {
                Assert.That(itemsArray, Has.Length.EqualTo(3));
                Assert.That(itemsArray[0].Description, Is.EqualTo("Basic Plan"));
                Assert.That(itemsArray[1].Description, Is.EqualTo("Premium Plan"));
                Assert.That(itemsArray[2].Description, Is.EqualTo("VIP Plan"));
            });
        }

        [Test]
        public void GetMembershipPlanObtainDetailsAsync_WhenPlanNotFound_ShouldThrowException()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((MembershipPlan)null!);

            // Act & Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await _service.GetMembershipPlanObtainDetailsAsync(1));
        }

        [Test]
        public void AddToPersonalHall_WhenUserAlreadyHasPlan_ShouldThrowException()
        {
            // Arrange
            var plan = new MembershipPlan { Id = 1, PlanType = PlanType.Standart, Description = "Basic Plan", Price = 20, Duration = 1 };
            var user = new ApplicationUser
            {
                Id = "user1",
                MembershipPlans = new List<MembershipPlan> { plan }
            };

            _mockRepository.Setup(r => r.GetByIdAsync(It.Is<int>(id => id == 1))).ReturnsAsync(plan);
            _mockUserManager.Setup(u => u.FindByIdAsync(It.Is<string>(id => id == "user1"))).ReturnsAsync(user);

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () => await _service.AddToPersonalHall(1, "user1"));
        }

        [Test]
        public async Task GetMembershipPlanObtainDetailsAsync_ShouldReturnCorrectViewModel_WhenPlanExists()
        {
            // Arrange: Set up the sample data
            var planId = 1;
            var plan = new MembershipPlan
            {
                Id = planId,
                PlanType = PlanType.Premium,
                Description = "Premium Plan",
                Price = 50,
                Duration = 3
            };

            _mockRepository.Setup(r => r.GetByIdAsync(planId))
                .ReturnsAsync(plan);

            // Act: Call the service method
            var result = await _service.GetMembershipPlanObtainDetailsAsync(planId);

            // Assert: Verify the result
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(planId));
                Assert.That(result.PlanType, Is.EqualTo("Premium"));
                Assert.That(result.Description, Is.EqualTo("Premium Plan"));
                Assert.That(result.Price, Is.EqualTo(50));
                Assert.That(result.DurationInMonth, Is.EqualTo(3));
            });
        }

        [Test]
        public void GetMembershipPlanObtainDetailsAsync_ShouldThrowException_WhenPlanDoesNotExist()
        {
            // Arrange: Set up the case where the plan does not exist
            var planId = 1;
            _mockRepository.Setup(r => r.GetByIdAsync(planId))
                .ReturnsAsync((MembershipPlan)null!);  // Plan does not exist

            // Act & Assert: Ensure the method throws the appropriate exception
            Assert.ThrowsAsync<NullReferenceException>(async () => await _service.GetMembershipPlanObtainDetailsAsync(planId));
        }
    }
    //[Test]
    //public async Task AddToPersonalHall_ShouldAddPlanToUser()
    //{
    //    // Arrange
    //    var plan = new MembershipPlan { Id = 1, PlanType = PlanType.Standart, Description = "Basic Plan", Price = 20, Duration = 1 };
    //    var user = new ApplicationUser
    //    {
    //        Id = "user1",
    //        MembershipPlans = new List<MembershipPlan>()
    //    };

    //    _mockRepository.Setup(r => r.GetByIdAsync(It.Is<int>(id => id == 1))).ReturnsAsync(plan);

    //    _mockUserManager.Setup(u => u.FindByIdAsync(It.Is<string>(id => id == "user1"))).ReturnsAsync(user);

    //    _mockUserManager.Setup(u => u.AddToRoleAsync(It.Is<ApplicationUser>(usr => usr.Id == "user1"), It.Is<string>(role => role == "GymMemberRole")))
    //                    .ReturnsAsync(IdentityResult.Success)
    //                    .Verifiable();

    //    await _service.AddToPersonalHall(1, "user1");

    //    Assert.That(user.MembershipPlans.Count, Is.EqualTo(1));
    //    Assert.That(user.MembershipPlans.First().Id, Is.EqualTo(plan.Id));

    //    _mockUserManager.Verify(
    //        u => u.AddToRoleAsync(It.Is<ApplicationUser>(usr => usr.Id == "user1"),
    //                              It.Is<string>(role => role == "GymMemberRole")),
    //        Times.Once);
    //}
}


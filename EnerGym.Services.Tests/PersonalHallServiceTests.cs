using Moq;
using EnerGym.Services.Data.Implementations;
using EnerGym.Data.Models;
using EnerGym.Data.Repository.Interfaces;
using EnerGym.Data.Models.Enums;
using MockQueryable.Moq;

namespace EnerGym.Tests.Services
{
    [TestFixture]
    public class PersonalHallServiceTests
    {
        private Mock<IRepository<MembershipPlan, int>> _mockMembershipPlanRepository;
        private Mock<IRepository<AttendantClass, object>> _mockGymClassRepository;
        private PersonalHallService _service;

        [SetUp]
        public void SetUp()
        {
            _mockMembershipPlanRepository = new Mock<IRepository<MembershipPlan, int>>();
            _mockGymClassRepository = new Mock<IRepository<AttendantClass, object>>();
            _service = new PersonalHallService(_mockMembershipPlanRepository.Object, _mockGymClassRepository.Object);
        }

        [Test]
        public async Task GetUserMembershipPlansAsync_ShouldReturnPlans_WhenUserHasMembershipPlans()
        {
            // Arrange
            var userId = "user1";
            var membershipPlans = new List<MembershipPlan>
        {
            new MembershipPlan { Id = 1, PlanType = PlanType.Standart, Description = "Basic Plan", Price = 20, Duration = 12, AttendantId = userId },
            new MembershipPlan { Id = 2, PlanType = PlanType.Premium, Description = "Premium Plan", Price = 40, Duration = 12, AttendantId = userId }
        }.AsQueryable();

            var queryablePlans = membershipPlans.BuildMockDbSet();

            _mockMembershipPlanRepository.Setup(r => r.GetAllAttached()).Returns(queryablePlans.Object);

            // Act
            var result = await _service.GetUserMembershipPlansAsync(userId);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));  // Should return 2 plans
            Assert.That(result.First().PlanType, Is.EqualTo("Standart"));  // First plan should be Standart
            Assert.That(result.Last().PlanType, Is.EqualTo("Premium"));  // Last plan should be Premium
        }

        [Test]
        public async Task GetUserEnrolledClassesAsync_ShouldReturnClasses_WhenUserIsEnrolledInClasses()
        {
            // Arrange
            var userId = "user1";
            var attendantClasses = new List<AttendantClass>
        {
            new AttendantClass { GymClassId = 1, AttendantId = userId, GymClass = new GymClass { ClassName = "Yoga", Capacity = 10, IsActive = true, InstructorName = "Jane Doe" } },
            new AttendantClass { GymClassId = 2, AttendantId = userId, GymClass = new GymClass { ClassName = "Pilates", Capacity = 12, IsActive = true, InstructorName = "John Smith" } }
        }.AsQueryable();

            var queryableClasses = attendantClasses.BuildMockDbSet();

            _mockGymClassRepository.Setup(r => r.GetAllAttached()).Returns(queryableClasses.Object);

            // Act
            var result = await _service.GetUserEnrolledClassesAsync(userId);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));  // Should return 2 classes
            Assert.That(result.First().ClassName, Is.EqualTo("Yoga"));  // First class should be Yoga
            Assert.That(result.Last().ClassName, Is.EqualTo("Pilates"));  // Last class should be Pilates
        }
    }
}
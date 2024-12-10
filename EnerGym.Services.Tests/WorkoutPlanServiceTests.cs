using EnerGym.Data.Models;
using EnerGym.Data.Repository.Interfaces;
using EnerGym.Services.Data.Implementations;
using EnerGym.ViewModels.WorkoutPlanViewModels;
using Microsoft.AspNetCore.Identity;
using MockQueryable;
using MockQueryable.Moq;
using Moq;

namespace EnerGym.Tests.Services
{
    [TestFixture]
    public class WorkoutPlanServiceTests
    {
        private Mock<IRepository<WorkoutRoutine, int>> _mockWorkoutRoutineRepository;
        private Mock<IRepository<WorkoutPlan, int>> _mockWorkoutPlanRepository;
        private Mock<UserManager<ApplicationUser>> _mockUserManager;
        private WorkoutPlanService _service;

        [SetUp]
        public void SetUp()
        {
            var mockWorkoutRoutineStore = new Mock<IRepository<WorkoutRoutine, int>>();
            var mockWorkoutPlanStore = new Mock<IRepository<WorkoutPlan, int>>();
            var mockUserManagerStore = new Mock<IUserStore<ApplicationUser>>();

            _mockWorkoutRoutineRepository = new Mock<IRepository<WorkoutRoutine, int>>();
            _mockWorkoutPlanRepository = new Mock<IRepository<WorkoutPlan, int>>();
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(mockUserManagerStore.Object, null!, null!, null!, null!, null!, null!, null!, null!);

            _service = new WorkoutPlanService(
                _mockWorkoutRoutineRepository.Object,
                _mockWorkoutPlanRepository.Object,
                _mockUserManager.Object
            );
        }

        [Test]
        public async Task AddPlanAsync_ShouldAddNewPlan_WhenValidModel()
        {
            // Arrange
            var model = new WorkoutPlanAddViewModel
            {
                PlanName = "New Plan",
                PlanDescription = "Plan Description",
                ImageUrl = "http://image.url",
                Routines = new List<WorkoutRoutine>()
            };

            _mockWorkoutPlanRepository.Setup(repo => repo.AddAsync(It.IsAny<WorkoutPlan>())).Returns(Task.CompletedTask);

            // Act
            await _service.AddPlanAsync(model);

            // Assert
            _mockWorkoutPlanRepository.Verify(r => r.AddAsync(It.Is<WorkoutPlan>(p => p.Name == model.PlanName && p.Description == model.PlanDescription)), Times.Once);
        }

        [Test]
        public async Task EditPlanAsync_ShouldEditPlan_WhenValidId()
        {
            // Arrange
            var model = new EditPlanViewModel
            {
                PlanName = "Updated Plan",
                PlanDescription = "Updated Description",
                ImageUrl = "http://updatedimage.url"
            };

            var existingPlan = new WorkoutPlan
            {
                Id = 1,
                Name = "Old Plan",
                Description = "Old Description",
                ImageUrl = "http://oldimage.url",
                IsDeleted = false
            };

            _mockWorkoutPlanRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(existingPlan);
            _mockWorkoutPlanRepository.Setup(repo => repo.UpdateAsync(It.IsAny<WorkoutPlan>())).ReturnsAsync(true);

            // Act
            var result = await _service.EditPlanAsync(model, 1);

            // Assert
            Assert.That(result.Name, Is.EqualTo("Updated Plan"));
            Assert.That(result.Description, Is.EqualTo("Updated Description"));
            _mockWorkoutPlanRepository.Verify(repo => repo.UpdateAsync(It.IsAny<WorkoutPlan>()), Times.Once);
        }

        [Test]
        public void EditPlanAsync_ShouldThrowArgumentException_WhenPlanNotFound()
        {
            // Arrange
            var model = new EditPlanViewModel();
            _mockWorkoutPlanRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((WorkoutPlan)null!);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _service.EditPlanAsync(model, 1));
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnPaginatedPlans_WhenValidPagination()
        {
            // Arrange
            var plans = new List<WorkoutPlan>
    {
        new WorkoutPlan { Id = 1, Name = "Plan 1", IsDeleted = false },
        new WorkoutPlan { Id = 2, Name = "Plan 2", IsDeleted = false }
    };

            _mockWorkoutPlanRepository.Setup(repo => repo.GetAllAttached()).Returns(plans.AsQueryable().BuildMock());

            // Act
            var result = await _service.GetAllAsync(1, 2);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task GetTotalPagesAsync_ShouldReturnCorrectTotalPages()
        {
            // Arrange
            var plans = new List<WorkoutPlan>
            {
                new WorkoutPlan { Id = 1, Name = "Plan 1", IsDeleted = false },
                new WorkoutPlan { Id = 2, Name = "Plan 2", IsDeleted = false },
                new WorkoutPlan { Id = 3, Name = "Plan 3", IsDeleted = false },
                new WorkoutPlan { Id = 4, Name = "Plan 4", IsDeleted = false },
                new WorkoutPlan { Id = 5, Name = "Plan 5", IsDeleted = false }
            };

            var queryable = plans.AsQueryable().BuildMockDbSet();

            _mockWorkoutPlanRepository.Setup(repo => repo.GetAllAttached())
                .Returns(queryable.Object);

            // Act
            var totalPages = await _service.GetTotalPagesAsync(2);

            // Assert
            Assert.That(totalPages, Is.EqualTo(3));
        }

        [Test]
        public async Task GetPlanDetailsAsync_ShouldReturnPlanDetails_WhenValidId()
        {
            // Arrange
            var plan = new WorkoutPlan { Id = 1, Name = "Plan 1", Description = "Plan Description", ImageUrl = "http://image.url" };
            _mockWorkoutPlanRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(plan);

            // Act
            var result = await _service.GetPlanDetailsAsync(1);

            // Assert
            Assert.That(result.Name, Is.EqualTo("Plan 1"));
        }

        [Test]
        public async Task GetDeletePlanByIdAsync_ShouldReturnPlanForDelete_WhenValidUserAndPlan()
        {
            // Arrange
            var user = new ApplicationUser { Id = "user1", FirstName = "John", LastName = "Doe" };
            var plan = new WorkoutPlan { Id = 1, Name = "Plan 1", IsDeleted = false };
            _mockUserManager.Setup(um => um.FindByIdAsync("user1")).ReturnsAsync(user);
            _mockWorkoutPlanRepository.Setup(rp => rp.GetAllAttached()).Returns(new List<WorkoutPlan> { plan }.AsQueryable().BuildMock());

            // Act
            var result = await _service.GetDeletePlanByIdAsync(1, "user1");

            // Assert
            Assert.That(result.PlanName, Is.EqualTo("Plan 1"));
        }

        [Test]
        public async Task DeletePlanAsync_ShouldDeletePlan_WhenPlanExists()
        {
            // Arrange
            var plan = new WorkoutPlan { Id = 1, Name = "Plan 1", IsDeleted = false };
            _mockWorkoutPlanRepository.Setup(repo => repo.GetAllAttached()).Returns(new List<WorkoutPlan> { plan }.AsQueryable().BuildMock());
            _mockWorkoutPlanRepository.Setup(repo => repo.UpdateAsync(It.IsAny<WorkoutPlan>())).ReturnsAsync(true);

            var model = new DeletePlanViewModel { Id = 1, PlanName = "Plan 1", PublishedBy = "John Doe" };

            // Act
            await _service.DeletePlanAsync(model);

            // Assert
            Assert.IsTrue(plan.IsDeleted);
        }

        [Test]
        public async Task GetAllRoutinesAsync_ShouldReturnFilteredRoutines_WhenSetsMatch()
        {
            // Arrange
            var routines = new List<WorkoutRoutine>
            {
                new WorkoutRoutine { Id = 1, ExerciseName = "Push Up", Sets = 3, Reps = 15 },
                new WorkoutRoutine { Id = 2, ExerciseName = "Squat", Sets = 4, Reps = 20 }
            };

            var queryable = routines.AsQueryable().BuildMockDbSet();

            _mockWorkoutRoutineRepository.Setup(r => r.GetAllAttached())
                .Returns(queryable.Object);

            // Act
            var result = await _service.GetAllRoutinesAsync(null, 3, null);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task GetAllRoutinesAsync_ShouldReturnFilteredRoutines_WhenRepsMatch()
        {
            // Arrange
            var routines = new List<WorkoutRoutine>
            {
                new WorkoutRoutine { Id = 1, ExerciseName = "Push Up", Sets = 3, Reps = 15 },
                new WorkoutRoutine { Id = 2, ExerciseName = "Squat", Sets = 4, Reps = 20 }
            };

            var queryable = routines.AsQueryable().BuildMockDbSet();

            _mockWorkoutRoutineRepository.Setup(r => r.GetAllAttached())
                .Returns(queryable.Object);

            // Act
            var result = await _service.GetAllRoutinesAsync(null, null, 15);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(1)); // Should match routine with 15 reps ("Push Up")
        }

        [Test]
        public async Task AddRoutinesToPlanAsync_ShouldAddRoutines_WhenValidIds()
        {
            // Arrange
            var plan = new WorkoutPlan { Id = 1, Name = "Plan 1", IsDeleted = false };
            var routine = new WorkoutRoutine { Id = 1, ExerciseName = "Push Up", Sets = 3, Reps = 15 };
            _mockWorkoutPlanRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(plan);
            _mockWorkoutRoutineRepository.Setup(repo => repo.GetAllAttached()).Returns(new List<WorkoutRoutine> { routine }.AsQueryable().BuildMock());

            // Act
            await _service.AddRoutinesToPlanAsync(1, new List<int> { 1 });

            // Assert
            Assert.That(plan.WorkoutRoutines.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task GetRoutinesByPlanIdAsync_ShouldReturnRoutines_WhenValidPlanId()
        {
            // Arrange
            var plan = new WorkoutPlan
            {
                Id = 1,
                WorkoutRoutines = new List<WorkoutRoutine>
        {
            new WorkoutRoutine { Id = 1, ExerciseName = "Push Up", Sets = 3, Reps = 15 }
        }
            };

            _mockWorkoutPlanRepository.Setup(repo => repo.GetAllAttached()).Returns(new List<WorkoutPlan> { plan }.AsQueryable().BuildMock());

            // Act
            var result = await _service.GetRoutinesByPlanIdAsync(1);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(1));
        }
    }
}
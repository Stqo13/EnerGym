using Moq;
using EnerGym.Data.Models;
using EnerGym.Data.Repository.Interfaces;
using EnerGym.Services.Data.Implementations;
using EnerGym.ViewModels.WorkoutRoutineViewModels;
using MockQueryable.Moq;

namespace EnerGym.Tests.Services
{
    [TestFixture]
    public class WorkoutRoutineServiceTests
    {
        private Mock<IRepository<WorkoutRoutine, int>> _mockWorkoutRoutineRepository;
        private WorkoutRoutineService _service;

        [SetUp]
        public void SetUp()
        {
            _mockWorkoutRoutineRepository = new Mock<IRepository<WorkoutRoutine, int>>();
            _service = new WorkoutRoutineService(_mockWorkoutRoutineRepository.Object);
        }

        #region AddWorkoutRoutineAsync

        [Test]
        public async Task AddWorkoutRoutineAsync_ShouldAddNewRoutine()
        {
            // Arrange
            var model = new WorkoutRoutineAddViewModel
            {
                ExerciseName = "Push Up",
                ExerciseDescription = "Push-ups for chest",
                Weight = 0,
                Sets = 3,
                Reps = 15
            };

            _mockWorkoutRoutineRepository.Setup(repo => repo.AddAsync(It.IsAny<WorkoutRoutine>()))
                .Returns(Task.CompletedTask);

            // Act
            await _service.AddWorkoutRoutineAsync(model);

            // Assert
            _mockWorkoutRoutineRepository.Verify(repo => repo.AddAsync(It.Is<WorkoutRoutine>(r => r.ExerciseName == "Push Up")), Times.Once);
        }

        #endregion

        #region GetAllWorkoutRoutinesAsync

        [Test]
        public async Task GetAllWorkoutRoutinesAsync_ShouldReturnPagedRoutines()
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
            var result = await _service.GetAllWorkoutRoutinesAsync(1, 1); // Page 1 with 1 item per page

            // Assert
            Assert.That(result.Count(), Is.EqualTo(1));
        }

        #endregion

        #region GetTotalPagesAsync

        [Test]
        public async Task GetTotalPagesAsync_ShouldReturnCorrectTotalPages()
        {
            // Arrange
            var routines = new List<WorkoutRoutine>
            {
                new WorkoutRoutine { Id = 1, ExerciseName = "Push Up" },
                new WorkoutRoutine { Id = 2, ExerciseName = "Squat" },
                new WorkoutRoutine { Id = 3, ExerciseName = "Lunge" },
                new WorkoutRoutine { Id = 4, ExerciseName = "Plank" },
                new WorkoutRoutine { Id = 5, ExerciseName = "Crunch" }
            };

            var queryable = routines.AsQueryable().BuildMockDbSet();

            _mockWorkoutRoutineRepository.Setup(r => r.GetAllAttached())
                .Returns(queryable.Object);

            // Act
            var totalPages = await _service.GetTotalPagesAsync(2); // 2 items per page

            // Assert
            Assert.That(totalPages, Is.EqualTo(3)); // 5 items / 2 per page = 3 pages
        }

        #endregion

        #region GetDeleteWorkoutRoutineByIdAsync

        [Test]
        public void GetDeleteWorkoutRoutineByIdAsync_ShouldThrowException_WhenRoutineNotFound()
        {
            // Arrange
            var invalidId = 999;

            var queryable = Enumerable.Empty<WorkoutRoutine>().AsQueryable().BuildMockDbSet();

            _mockWorkoutRoutineRepository.Setup(repo => repo.GetAllAttached())
                .Returns(queryable.Object);

            // Act & Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await _service.GetDeleteWorkoutRoutineByIdAsync(invalidId));
        }


        [Test]
        public async Task GetDeleteWorkoutRoutineByIdAsync_ShouldReturnRoutine_WhenRoutineExists()
        {
            // Arrange
            var validId = 1;
            var routine = new WorkoutRoutine { Id = 1, ExerciseName = "Push Up", IsDeleted = false };
            var queryable = new List<WorkoutRoutine> { routine }.AsQueryable().BuildMockDbSet();

            _mockWorkoutRoutineRepository.Setup(repo => repo.GetAllAttached())
                .Returns(queryable.Object);

            // Act
            var result = await _service.GetDeleteWorkoutRoutineByIdAsync(validId);

            // Assert
            Assert.That(result.Id, Is.EqualTo(validId));
        }

        #endregion

        #region DeleteWorkoutRoutineAsync

        [Test]
        public async Task DeleteWorkoutRoutineAsync_ShouldDeleteRoutine_WhenValidModel()
        {
            // Arrange
            var model = new WorkoutRoutineDeleteViewModel 
            { 
                Id = 1,
                ExerciseName = "Test"
            };
            var routine = new WorkoutRoutine { Id = 1, IsDeleted = false };
            var queryable = new List<WorkoutRoutine> { routine }.AsQueryable().BuildMockDbSet();

            _mockWorkoutRoutineRepository.Setup(repo => repo.GetAllAttached())
                .Returns(queryable.Object);
            _mockWorkoutRoutineRepository.Setup(repo => repo.UpdateAsync(It.IsAny<WorkoutRoutine>()))
                .ReturnsAsync(true);

            // Act
            await _service.DeleteWorkoutRoutineAsync(model);

            // Assert
            Assert.That(routine.IsDeleted, Is.True);
            _mockWorkoutRoutineRepository.Verify(repo => repo.UpdateAsync(It.IsAny<WorkoutRoutine>()), Times.Once);
        }

        #endregion

        #region GetRoutineDetailsAsync

        [Test]
        public void GetRoutineDetailsAsync_ShouldThrowException_WhenRoutineNotFound()
        {
            // Arrange
            var invalidId = 999;
            _mockWorkoutRoutineRepository.Setup(repo => repo.GetByIdAsync(invalidId))
                .ReturnsAsync((WorkoutRoutine)null!);

            // Act & Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await _service.GetRoutineDetailsAsync(invalidId));
        }

        [Test]
        public async Task GetRoutineDetailsAsync_ShouldReturnDetails_WhenRoutineExists()
        {
            // Arrange
            var validId = 1;
            var routine = new WorkoutRoutine { Id = validId, ExerciseName = "Push Up", IsDeleted = false };
            _mockWorkoutRoutineRepository.Setup(repo => repo.GetByIdAsync(validId))
                .ReturnsAsync(routine);

            // Act
            var result = await _service.GetRoutineDetailsAsync(validId);

            // Assert
            Assert.That(result.Id, Is.EqualTo(validId));
            Assert.That(result.ExerciseName, Is.EqualTo("Push Up"));
        }

        #endregion

        #region GetEditWorkoutPlanByIdAsync

        [Test]
        public void GetEditWorkoutPlanByIdAsync_ShouldThrowException_WhenRoutineNotFound()
        {
            // Arrange
            var invalidId = 999;
            _mockWorkoutRoutineRepository.Setup(repo => repo.GetByIdAsync(invalidId))
                .ReturnsAsync((WorkoutRoutine)null!);

            // Act & Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await _service.GetEditWorkoutPlanByIdAsync(invalidId));
        }

        [Test]
        public async Task GetEditWorkoutPlanByIdAsync_ShouldReturnRoutineForEdit_WhenRoutineExists()
        {
            // Arrange
            var validId = 1;
            var routine = new WorkoutRoutine { Id = validId, ExerciseName = "Push Up", IsDeleted = false };
            _mockWorkoutRoutineRepository.Setup(repo => repo.GetByIdAsync(validId))
                .ReturnsAsync(routine);

            // Act
            var result = await _service.GetEditWorkoutPlanByIdAsync(validId);

            // Assert
            Assert.That(result.Id, Is.EqualTo(validId));
            Assert.That(result.ExerciseName, Is.EqualTo("Push Up"));
        }

        #endregion

        #region EditWorkoutRoutine

        [Test]
        public async Task EditWorkoutRoutine_ShouldUpdateRoutine_WhenValidModel()
        {
            // Arrange
            var model = new WorkoutRoutineEditViewModel
            {
                ExerciseName = "Pull Up",
                ExerciseDescription = "Pull-ups for back",
                Weight = 10,
                Sets = 5,
                Reps = 12
            };
            var routine = new WorkoutRoutine { Id = 1, ExerciseName = "Push Up", IsDeleted = false };
            _mockWorkoutRoutineRepository.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(routine);
            _mockWorkoutRoutineRepository.Setup(repo => repo.UpdateAsync(It.IsAny<WorkoutRoutine>()))
                .ReturnsAsync(true);

            // Act
            var result = await _service.EditWorkoutRoutine(model, 1);

            // Assert
            Assert.That(result.ExerciseName, Is.EqualTo("Pull Up"));
            _mockWorkoutRoutineRepository.Verify(repo => repo.UpdateAsync(It.IsAny<WorkoutRoutine>()), Times.Once);
        }

        #endregion
    }
}

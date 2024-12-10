using EnerGym.Data.Models;
using EnerGym.Data.Repository.Interfaces;
using EnerGym.Services.Data.Implementations;
using EnerGym.ViewModels.GymClassViewModels;
using Microsoft.AspNetCore.Identity;
using MockQueryable.Moq;
using Moq;


namespace EnerGym.Tests.Services
{
    [TestFixture]
    public class GymClassServiceTests
    {
        private Mock<IRepository<GymClass, int>> _mockGymClassRepository;
        private Mock<IRepository<AttendantClass, object>> _mockAttendanceRepository;
        private Mock<IRepository<Schedule, int>> _mockScheduleRepository;
        private Mock<UserManager<ApplicationUser>> _mockUserManager;
        private GymClassService _service;

        [SetUp]
        public void Setup()
        {
            _mockGymClassRepository = new Mock<IRepository<GymClass, int>>();
            _mockAttendanceRepository = new Mock<IRepository<AttendantClass, object>>();
            _mockScheduleRepository = new Mock<IRepository<Schedule, int>>();
            _mockUserManager = MockUserManager();

            _service = new GymClassService(
                _mockGymClassRepository.Object,
                _mockAttendanceRepository.Object,
                _mockScheduleRepository.Object,
                _mockUserManager.Object);
        }

        private static Mock<UserManager<ApplicationUser>> MockUserManager()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            return new Mock<UserManager<ApplicationUser>>(store.Object, null!, null!, null!, null!, null!, null!, null!, null!);
        }

        [Test]
        public async Task AddGymClassAsync_ShouldAddClass()
        {
            // Arrange
            var viewModel = new GymClassAddViewModel
            {
                ClassName = "Yoga",
                InstructorName = "Jane Doe",
                Descripton = "Relaxing yoga session",
                Capacity = 10
            };

            _mockGymClassRepository.Setup(r => r.AddAsync(It.IsAny<GymClass>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Act
            await _service.AddGymClassAsync(viewModel);

            // Assert
            _mockGymClassRepository.Verify(r => r.AddAsync(It.Is<GymClass>(
                g => g.ClassName == "Yoga" &&
                     g.InstructorName == "Jane Doe" &&
                     g.Description == "Relaxing yoga session" &&
                     g.Capacity == 10)), Times.Once);
        }

        [Test]
        public async Task GetAllGymClassesAsync_ShouldReturnClasses()
        {
            // Arrange
            var gymClasses = new List<GymClass>
        {
            new GymClass { Id = 1, ClassName = "Yoga", InstructorName = "Jane Doe", Capacity = 10, IsActive = true },
            new GymClass { Id = 2, ClassName = "Pilates", InstructorName = "John Smith", Capacity = 15, IsActive = true }
        };

            var mockDbSet = gymClasses.AsQueryable().BuildMockDbSet();

            _mockGymClassRepository.Setup(r => r.GetAllAttached())
                .Returns(mockDbSet.Object);

            // Act
            var result = await _service.GetAllGymClassesAsync();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Count(), Is.EqualTo(2));
                Assert.That(result.First().ClassName, Is.EqualTo("Yoga"));
            });
        }

        [Test]
        public void GetGymClassDetailsAsync_ShouldThrowException_IfNotFound()
        {
            // Arrange
            _mockGymClassRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((GymClass)null!);

            // Act & Assert
            Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await _service.GetGymClassDetailsAsync(1);
            });
        }

        [Test]
        public async Task EnrollUserAsync_ShouldAddEnrollment()
        {
            // Arrange
            int gymClassId = 1;
            string userId = "user123";

            var existingAttendances = new List<AttendantClass>().AsQueryable();
            var mockDbSet = existingAttendances.BuildMockDbSet();

            _mockAttendanceRepository.Setup(r => r.GetAllAttached())
                .Returns(mockDbSet.Object);

            _mockAttendanceRepository.Setup(r => r.AddAsync(It.IsAny<AttendantClass>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Act: Enroll user
            await _service.EnrollUserAsync(gymClassId, userId);

            // Assert: Verify AddAsync was called with the correct parameters
            _mockAttendanceRepository.Verify(r => r.AddAsync(It.Is<AttendantClass>(
                e => e.GymClassId == gymClassId && e.AttendantId == userId)), Times.Once);
        }

        [Test]
        public async Task DeleteGymClassAsync_ShouldMarkClassAsInactive()
        {
            // Arrange
            var gymClass = new GymClass { Id = 1, IsActive = true };
            var gymClasses = new[] { gymClass }.AsQueryable();

            // Build the mock DbSet using the extension method
            var mockDbSet = gymClasses.BuildMockDbSet();

            // Set up repository methods
            _mockGymClassRepository.Setup(r => r.GetAllAttached())
                .Returns(mockDbSet.Object);

            _mockGymClassRepository.Setup(r => r.UpdateAsync(It.IsAny<GymClass>()))
                .ReturnsAsync(true)
                .Verifiable();

            var viewModel = new GymClassDeleteViewModel
            {
                Id = 1,
                ClassName = "HIIT Class",
                PublishedBy = "Stefan Dimitrov"
            };

            // Act
            await _service.DeleteGymClassAsync(viewModel);

            // Assert
            _mockGymClassRepository.Verify(r => r.UpdateAsync(It.Is<GymClass>(
                g => g.Id == 1 && g.IsActive == false)), Times.Once);
        }

        [Test]
        public async Task GetTotalPagesAsync_ShouldReturnCorrectTotalPages()
        {
            // Arrange
            var gymClasses = new List<GymClass>
            {
                new GymClass { Id = 1, IsActive = true },
                new GymClass { Id = 2, IsActive = true },
                new GymClass { Id = 3, IsActive = true },
                new GymClass { Id = 4, IsActive = true },
                new GymClass { Id = 5, IsActive = true },
                new GymClass { Id = 6, IsActive = true }
            };

            var queryable = gymClasses.AsQueryable().BuildMockDbSet();

            _mockGymClassRepository.Setup(r => r.GetAllAttached())
                .Returns(queryable.Object);

            // Act
            var result = await _service.GetTotalPagesAsync(pageSize: 3);

            // Assert
            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public async Task GetEditGymClassByIdAsync_ShouldReturnGymClassEditViewModel_WhenClassIsActive()
        {
            // Arrange
            var gymClass = new GymClass
            {
                Id = 1,
                ClassName = "Yoga",
                Capacity = 20,
                InstructorName = "Jane Doe",
                Description = "Yoga class for beginners",
                IsActive = true,
                Schedules = new List<Schedule> { new Schedule { Week = DateTime.Now } }
            };

            _mockGymClassRepository.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(gymClass);

            // Act
            var result = await _service.GetEditGymClassByIdAsync(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.ClassName, Is.EqualTo("Yoga"));
            Assert.That(result.Capacity, Is.EqualTo(20));
            Assert.That(result.InstructorName, Is.EqualTo("Jane Doe"));
            Assert.That(result.Descripton, Is.EqualTo("Yoga class for beginners"));
            Assert.That(result.Schedules.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetEditGymClassByIdAsync_ShouldThrowNullReferenceException_WhenClassDoesNotExist()
        {
            // Arrange
            _mockGymClassRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((GymClass)null!);

            // Act & Assert
            var ex = Assert.ThrowsAsync<NullReferenceException>(() => _service.GetEditGymClassByIdAsync(1));
            Assert.That(ex.Message, Is.EqualTo("Entity not found!"));
        }

        [Test]
        public void GetEditGymClassByIdAsync_ShouldThrowArgumentException_WhenClassIsInactive()
        {
            // Arrange
            var gymClass = new GymClass
            {
                Id = 1,
                ClassName = "Yoga",
                Capacity = 20,
                InstructorName = "Jane Doe",
                Description = "Yoga class for beginners",
                IsActive = false,
                Schedules = new List<Schedule> { new Schedule { Week = DateTime.Now } }
            };

            _mockGymClassRepository.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(gymClass);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(() => _service.GetEditGymClassByIdAsync(1));
            Assert.That(ex.Message, Is.EqualTo("Entity is already inactive!"));
        }

        [Test]
        public async Task EditGymClassAsync_ShouldUpdateGymClass_WhenClassIsActive()
        {
            // Arrange
            var gymClass = new GymClass
            {
                Id = 1,
                ClassName = "Yoga",
                Capacity = 20,
                InstructorName = "Jane Doe",
                Description = "Yoga class for beginners",
                IsActive = true
            };

            var model = new GymClassEditViewModel
            {
                ClassName = "Advanced Yoga",
                Capacity = 25,
                InstructorName = "John Smith",
                Descripton = "Advanced Yoga class"
            };

            _mockGymClassRepository.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(gymClass);
            _mockGymClassRepository.Setup(r => r.UpdateAsync(It.IsAny<GymClass>()))
                .ReturnsAsync(true);

            // Act
            var result = await _service.EditGymClassAsync(model, 1);

            // Assert
            Assert.That(result.ClassName, Is.EqualTo("Advanced Yoga"));
            Assert.That(result.Capacity, Is.EqualTo(25));
            Assert.That(result.InstructorName, Is.EqualTo("John Smith"));
            Assert.That(result.Description, Is.EqualTo("Advanced Yoga class"));

            // Verify that UpdateAsync was called once
            _mockGymClassRepository.Verify(r => r.UpdateAsync(It.Is<GymClass>(g => g.Id == 1)), Times.Once);
        }

        [Test]
        public void EditGymClassAsync_ShouldThrowNullReferenceException_WhenClassDoesNotExist()
        {
            // Arrange
            _mockGymClassRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((GymClass)null!);

            var model = new GymClassEditViewModel
            {
                ClassName = "Advanced Yoga",
                Capacity = 25,
                InstructorName = "John Smith",
                Descripton = "Advanced Yoga class"
            };

            // Act & Assert
            var ex = Assert.ThrowsAsync<NullReferenceException>(() => _service.EditGymClassAsync(model, 1));
            Assert.That(ex.Message, Is.EqualTo("Entity not found!"));
        }

        [Test]
        public void EditGymClassAsync_ShouldThrowArgumentException_WhenClassIsInactive()
        {
            // Arrange
            var gymClass = new GymClass
            {
                Id = 1,
                ClassName = "Yoga",
                Capacity = 20,
                InstructorName = "Jane Doe",
                Description = "Yoga class for beginners",
                IsActive = false
            };

            _mockGymClassRepository.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(gymClass);

            var model = new GymClassEditViewModel
            {
                ClassName = "Advanced Yoga",
                Capacity = 25,
                InstructorName = "John Smith",
                Descripton = "Advanced Yoga class"
            };

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(() => _service.EditGymClassAsync(model, 1));
            Assert.That(ex.Message, Is.EqualTo("Class already inactive!"));
        }

        [Test]
        public void GetDeleteGymClassByIdAsync_ShouldThrowNullReferenceException_WhenUserDoesNotExist()
        {
            // Arrange
            string invalidUserId = "invalidUserId";
            _mockUserManager.Setup(um => um.FindByIdAsync(invalidUserId))
                .ReturnsAsync((ApplicationUser)null!);  // User not found

            // Act & Assert
            var ex = Assert.ThrowsAsync<NullReferenceException>(async () =>
                await _service.GetDeleteGymClassByIdAsync(1, invalidUserId));
            Assert.That(ex.Message, Is.EqualTo("User Id was invalid!"));
        }

        [Test]
        public void GetDeleteGymClassByIdAsync_ShouldThrowNullReferenceException_WhenGymClassDoesNotExist()
        {
            // Arrange
            string userId = "user1";
            var user = new ApplicationUser { Id = userId, FirstName = "John", LastName = "Doe" };
            _mockUserManager.Setup(um => um.FindByIdAsync(userId))
                .ReturnsAsync(user); // Return valid user

            var gymClasses = Enumerable.Empty<GymClass>().AsQueryable();

            var mockGymClasses = gymClasses.BuildMockDbSet();

            _mockGymClassRepository.Setup(r => r.GetAllAttached())
                .Returns(mockGymClasses.Object);

            // Act & Assert
            var ex = Assert.ThrowsAsync<NullReferenceException>(async () =>
                await _service.GetDeleteGymClassByIdAsync(1, userId));
            Assert.That(ex.Message, Is.EqualTo("Entity not found!"));
        }


        [Test]
        public async Task GetDeleteGymClassByIdAsync_ShouldReturnGymClassDeleteViewModel_WhenGymClassExists()
        {
            // Arrange
            string userId = "user1";
            var user = new ApplicationUser { Id = userId, FirstName = "John", LastName = "Doe" };
            _mockUserManager.Setup(um => um.FindByIdAsync(userId))
                .ReturnsAsync(user);

            var gymClass = new GymClass
            {
                Id = 1,
                ClassName = "Yoga",
                IsActive = true
            };

            var gymClasses = new List<GymClass> { gymClass }.AsQueryable();

            var mockGymClasses = gymClasses.BuildMockDbSet();

            _mockGymClassRepository.Setup(r => r.GetAllAttached())
                .Returns(mockGymClasses.Object);

            // Act
            var result = await _service.GetDeleteGymClassByIdAsync(1, userId);

            // Assert
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.ClassName, Is.EqualTo("Yoga"));
            Assert.That(result.PublishedBy, Is.EqualTo("John Doe"));
        }

        [Test]
        public async Task GetDetailsGymClassByIdAsync_ShouldReturnGymClassDetailsViewModel_WhenGymClassExistsAndIsActive()
        {
            // Arrange
            var gymClassId = 1;
            var gymClass = new GymClass
            {
                Id = gymClassId,
                ClassName = "Yoga",
                InstructorName = "Jane Doe",
                Capacity = 20,
                Description = "A relaxing yoga class",
                IsActive = true
            };

            _mockGymClassRepository.Setup(r => r.GetByIdAsync(gymClassId))
                .ReturnsAsync(gymClass);

            // Act
            var result = await _service.GetDetailsGymClassByIdAsync(gymClassId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(gymClassId));
            Assert.That(result.ClassName, Is.EqualTo(gymClass.ClassName));
            Assert.That(result.InstructorName, Is.EqualTo(gymClass.InstructorName));
            Assert.That(result.Capacity, Is.EqualTo(gymClass.Capacity));
            Assert.That(result.Description, Is.EqualTo(gymClass.Description));
            Assert.That(result.IsActive, Is.True);
        }

        [Test]
        public void GetDetailsGymClassByIdAsync_ShouldThrowNullReferenceException_WhenGymClassDoesNotExist()
        {
            // Arrange
            var gymClassId = 1;

            _mockGymClassRepository.Setup(r => r.GetByIdAsync(gymClassId))
                .ReturnsAsync((GymClass)null!);

            // Act & Assert
            var ex = Assert.ThrowsAsync<NullReferenceException>(async () =>
                await _service.GetDetailsGymClassByIdAsync(gymClassId));

            Assert.That(ex.Message, Is.EqualTo("Entity not found!"));
        }

        [Test]
        public void GetDetailsGymClassByIdAsync_ShouldThrowArgumentException_WhenGymClassIsInactive()
        {
            // Arrange
            var gymClassId = 1;
            var gymClass = new GymClass
            {
                Id = gymClassId,
                ClassName = "Yoga",
                InstructorName = "Jane Doe",
                Capacity = 20,
                Description = "A relaxing yoga class",
                IsActive = false // Class is inactive
            };

            _mockGymClassRepository.Setup(r => r.GetByIdAsync(gymClassId))
                .ReturnsAsync(gymClass);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
                await _service.GetDetailsGymClassByIdAsync(gymClassId));

            Assert.That(ex.Message, Is.EqualTo("Class already inactive!"));
        }

        [Test]
        public async Task GetScheduletOfClassAsync_ShouldReturnScheduleAddViewModel_WhenGymClassExists()
        {
            // Arrange
            int gymClassId = 1;
            var gymClass = new GymClass
            {
                Id = gymClassId,
                ClassName = "Yoga",
                InstructorName = "Jane Doe",
                Capacity = 20,
                Description = "A relaxing yoga class",
                IsActive = true
            };

            _mockGymClassRepository.Setup(r => r.GetByIdAsync(gymClassId))
                .ReturnsAsync(gymClass);

            // Act
            var result = await _service.GetScheduletOfClassAsync(gymClassId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.GymClassId, Is.EqualTo(gymClassId));
        }

        [Test]
        public void GetScheduletOfClassAsync_ShouldThrowNullReferenceException_WhenGymClassDoesNotExist()
        {
            // Arrange
            int gymClassId = 1;

            _mockGymClassRepository.Setup(r => r.GetByIdAsync(gymClassId))
                .ReturnsAsync((GymClass)null!);

            // Act & Assert
            var ex = Assert.ThrowsAsync<NullReferenceException>(async () =>
                await _service.GetScheduletOfClassAsync(gymClassId));

            Assert.That(ex.Message, Is.EqualTo("Entity not found!"));
        }

        [Test]
        public async Task AddScheduleToClassAsync_ShouldAddSchedule_WhenGymClassExists()
        {
            // Arrange
            var gymClassId = 1;
            var gymClass = new GymClass
            {
                Id = gymClassId,
                ClassName = "Yoga",
                InstructorName = "Jane Doe",
                Capacity = 20,
                Description = "A relaxing yoga class",
                IsActive = true,
                Schedules = new List<Schedule>()
            };

            var scheduleModel = new ScheduleAddViewModel
            {
                GymClassId = gymClassId,
                Week = new DateTime(2024, 12, 11),
                TimeSchedule = new TimeOnly(12, 0),
                Monday = true,
                Tuesday = false,
                Wednesday = true,
                Thursday = false,
                Friday = true,
                Saturday = false,
                Sunday = true
            };

            _mockGymClassRepository.Setup(r => r.GetByIdAsync(gymClassId))
                .ReturnsAsync(gymClass);

            _mockScheduleRepository.Setup(r => r.AddAsync(It.IsAny<Schedule>()))
                .Returns(Task.CompletedTask);

            _mockGymClassRepository.Setup(r => r.UpdateAsync(It.IsAny<GymClass>()))
                .ReturnsAsync(true);

            // Act
            await _service.AddScheduleToClassAsync(scheduleModel);

            // Assert
            Assert.That(gymClass.Schedules.Count, Is.EqualTo(1));
            Assert.That(gymClass.Schedules.ToList()[0].Week, Is.EqualTo(new DateTime(2024, 12, 11)));
            _mockScheduleRepository.Verify(r => r.AddAsync(It.IsAny<Schedule>()), Times.Once);
            _mockGymClassRepository.Verify(r => r.UpdateAsync(gymClass), Times.Once);
        }

        [Test]
        public void AddScheduleToClassAsync_ShouldThrowNullReferenceException_WhenGymClassDoesNotExist()
        {
            // Arrange
            var gymClassId = 1;
            var scheduleModel = new ScheduleAddViewModel
            {
                GymClassId = gymClassId,
                Week = DateTime.Now,
                TimeSchedule = new TimeOnly(12, 0),
                Monday = true,
                Tuesday = false,
                Wednesday = true,
                Thursday = false,
                Friday = true,
                Saturday = false,
                Sunday = true
            };

            // Mock the repository to return null (class does not exist)
            _mockGymClassRepository.Setup(r => r.GetByIdAsync(gymClassId))
                .ReturnsAsync((GymClass)null!);

            // Act & Assert
            var ex = Assert.ThrowsAsync<NullReferenceException>(async () =>
                await _service.AddScheduleToClassAsync(scheduleModel));

            Assert.That(ex.Message, Is.EqualTo("Entity not found!"));
        }

        [Test]
        public async Task GetGymClassSchedulesAsync_ShouldReturnSchedules_WhenGymClassExists()
        {
            // Arrange
            var gymClassId = 1;
            var gymClass = new GymClass
            {
                Id = gymClassId,
                ClassName = "Yoga",
                Schedules = new List<Schedule>
        {
            new Schedule
            {
                Id = 1,
                Week = new DateTime(2024, 12, 1),
                Monday = true,
                Tuesday = false,
                Wednesday = true,
                Thursday = false,
                Friday = true,
                Saturday = false,
                Sunday = true
            },
            new Schedule
            {
                Id = 2,
                Week = new DateTime(2024, 12, 12),
                Monday = false,
                Tuesday = true,
                Wednesday = false,
                Thursday = true,
                Friday = false,
                Saturday = true,
                Sunday = false
            }
        }
            };

            // Use MockQueryable to build the mock DbSet with async capabilities
            var mockGymClassDbSet = new List<GymClass> { gymClass }.AsQueryable().BuildMockDbSet();

            _mockGymClassRepository.Setup(r => r.GetAllAttached())
                .Returns(mockGymClassDbSet.Object);

            // Act
            var result = await _service.GetGymClassSchedulesAsync(gymClassId);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));  // Two schedules should be returned
            Assert.That(result.First().ScheduleId, Is.EqualTo(1));  // First schedule has ID 1
            Assert.That(result.First().StartDate, Is.EqualTo(new DateTime(2024, 12, 1)));  // Correct week for first schedule
            Assert.That(result.First().Monday, Is.True);  // Monday is True for the first schedule
            Assert.That(result.Last().ScheduleId, Is.EqualTo(2));  // Last schedule has ID 2
            Assert.That(result.Last().StartDate, Is.EqualTo(new DateTime(2024, 12, 12)));  // Correct week for the last schedule
            Assert.That(result.Last().Tuesday, Is.True);  // Tuesday is True for the last schedule
        }
    }
}
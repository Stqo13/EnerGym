using EnerGym.Data.Models;
using EnerGym.Data.Repository.Interfaces;
using EnerGym.Services.Data.Interfaces;
using EnerGym.ViewModels.GymClassViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EnerGym.Services.Data.Implementations
{
    public class GymClassService(
        IRepository<GymClass, int> gymClassRepository,
        IRepository<AttendantClass, object> attendanceRepository,
        IRepository<Schedule, int> scheduleRepository,
        UserManager<ApplicationUser> userManager)
        : IGymClassService
    {
        public async Task AddGymClassAsync(GymClassAddViewModel model)
        {
            GymClass gymClass = new GymClass()
            {
                ClassName = model.ClassName,
                InstructorName = model.InstructorName,
                Description = model.Descripton,
                Capacity = model.Capacity,
            };

            await gymClassRepository.AddAsync(gymClass);
        }

        public async Task<IEnumerable<GymClassInfoViewModel>> GetAllGymClassesAsync(int pageNumber = 1, int pageSize = 3)
        {
            var classes = await gymClassRepository
                .GetAllAttached()
                .Where(p => p.IsActive)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new GymClassInfoViewModel()
                {
                    Id = c.Id,
                    ClassName = c.ClassName,
                    Capacity = c.Capacity,
                    IsActive = c.IsActive,
                    InstructorName = c.InstructorName
                })
                .AsNoTracking()
                .ToListAsync();

            return classes;
        }

        public async Task<int> GetTotalPagesAsync(int pageSize = 3)
        {
            var totalPlans = await gymClassRepository
                .GetAllAttached()
                .Where(c => c.IsActive)
                .CountAsync();

            return (int)Math.Ceiling(totalPlans / (double)pageSize);
        }

        public async Task<GymClassDetailsViewModel> GetGymClassDetailsAsync(int id)
        {
            var entity = await gymClassRepository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new NullReferenceException("Entity was not found!");
            }
            else if (entity.IsActive == false)
            {
                throw new ArgumentException("The class is inactive!");
            }

            var gymClass = new GymClassDetailsViewModel()
            {
                Id = id,
                ClassName= entity.ClassName,
                Capacity = entity.Capacity,
                IsActive = entity.IsActive,
                InstructorName = entity.InstructorName,
                Description = entity.Description
            };

            return gymClass;
        }

        public async Task<GymClassEditViewModel> GetEditGymClassByIdAsync(int id)
        {
            var entity = await gymClassRepository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new NullReferenceException("Entity not found!");
            }
            else if (entity.IsActive == false)
            {
                throw new ArgumentException("Entity is already inactive!");
            }

            var plan = new GymClassEditViewModel()
            {
                Id = id,
                ClassName = entity.ClassName,
                Capacity = entity.Capacity,
                InstructorName = entity.InstructorName,
                Descripton = entity.Description,
                Schedules = entity.Schedules.ToList()
            };

            return plan;
        }

        public async Task<GymClass> EditGymClassAsync(GymClassEditViewModel model, int id)
        {
            GymClass gymClass = await gymClassRepository.GetByIdAsync(id);

            if (gymClass == null)
            {
                throw new NullReferenceException("Entity not found!");
            }
            else if (gymClass.IsActive == false)
            {
                throw new ArgumentException("Class already inactive!");
            }

            gymClass.ClassName = model.ClassName;
            gymClass.InstructorName = model.InstructorName;
            gymClass.Description = model.Descripton;
            gymClass.Capacity = model.Capacity;

            await gymClassRepository.UpdateAsync(gymClass);

            return gymClass;
        }

        public async Task<GymClassDeleteViewModel> GetDeleteGymClassByIdAsync(int id, string publishedBy)
        {
            var user = await userManager.FindByIdAsync(publishedBy);

            if (user == null)
            {
                throw new NullReferenceException("User Id was invalid!");
            }

            string username = $"{user.FirstName} {user.LastName}";

            var gymClass = await gymClassRepository
                .GetAllAttached()
                .Where(p => p.Id == id && p.IsActive)
                .Select(p => new GymClassDeleteViewModel()
                {
                    Id = p.Id,
                    ClassName = p.ClassName,
                    PublishedBy = username ?? string.Empty
                })
                .FirstOrDefaultAsync();


            if (gymClass == null)
            {
                throw new NullReferenceException("Entity not found!");
            }

            return gymClass;
        }
        public async Task DeleteGymClassAsync(GymClassDeleteViewModel model)
        {
            GymClass? gymClass = await gymClassRepository
                .GetAllAttached()
                .Where(p => p.Id == model.Id && p.IsActive)
                .FirstOrDefaultAsync();

            if (gymClass != null)
            {
                gymClass.IsActive = false;

                await gymClassRepository.UpdateAsync(gymClass);
            }
        }

        public async Task<GymClassDetailsViewModel> GetDetailsGymClassByIdAsync(int id)
        {
            var entity = await gymClassRepository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new NullReferenceException("Entity not found!");
            }
            else if (entity.IsActive == false)
            {
                throw new ArgumentException("Class already inactive!");
            }

            var gymClass = new GymClassDetailsViewModel() 
            {
                Id = id,
                ClassName = entity.ClassName,
                InstructorName = entity.InstructorName,
                Capacity = entity.Capacity,
                Description = entity.Description,
                IsActive = entity.IsActive
            };

            return gymClass;
        }

        public async Task EnrollUserAsync(int id, string userId)
        {
            var existingEnrollment = await attendanceRepository
                .GetAllAttached()
                .FirstOrDefaultAsync(e => e.GymClassId == id && e.AttendantId == userId);

            if (existingEnrollment != null)
            {
                throw new ArgumentException("User is already enrolled!");
            }

            var enrollment = new AttendantClass()
            {
                AttendantId = userId,
                GymClassId = id
            };

            await attendanceRepository.AddAsync(enrollment);
        }

        public async Task<ScheduleAddViewModel> GetScheduletToClassAsync(int id)
        {
            var gymClass = await gymClassRepository.GetByIdAsync(id);

            if (gymClass == null)
            {
                throw new NullReferenceException("Entity not found!");
            }

            var schedule = new ScheduleAddViewModel()
            {
                GymClassId = id
            };

            return schedule;
        }

        public async Task AddScheduleToClassAsync(ScheduleAddViewModel model)
        {
            var gymClass = await gymClassRepository.GetByIdAsync(model.GymClassId);

            if (gymClass == null)
            {
                throw new NullReferenceException("Entity not found!");
            }

            var schedule = new Schedule()
            {
                GymClassId = model.GymClassId,
                Week = model.Week,
                TimeSchedule = model.TimeSchedule,
                Monday = model.Monday,
                Tuesday = model.Tuesday,
                Wednesday = model.Wednesday,
                Thursday = model.Thursday,
                Friday = model.Friday,
                Saturday = model.Saturday,
                Sunday = model.Sunday
            };

            gymClass.Schedules.Add(schedule);

            await scheduleRepository.AddAsync(schedule);

            await gymClassRepository.UpdateAsync(gymClass);
        }

        public async Task<IEnumerable<ScheduleInfoViewModel>> GetGymClassSchedulesAsync(int id)
        {
            var gymClass = await gymClassRepository
                .GetAllAttached()
                .Include(c => c.Schedules)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            if (gymClass == null)
            {
                throw new NullReferenceException("Entity not found");
            }

            var schedules = gymClass.Schedules
                .Select(s => new ScheduleInfoViewModel()
                {
                    ScheduleId = s.Id,
                    StartDate = s.Week,
                    Monday = s.Monday,
                    Tuesday = s.Tuesday,
                    Wednesday = s.Wednesday,
                    Thursday = s.Thursday,
                    Friday = s.Friday,
                    Saturday = s.Saturday,
                    Sunday = s.Sunday
                })
                .ToList();

            return schedules;
        }
    }
}


using EnerGym.Data.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EnerGym.Data.Models.Configurations
{
    public class DbSeeder
    {
        public static async Task SeedDatabase
            (UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, EnerGymDbContext context)
        {
            if (await userManager.Users.AnyAsync())
            {
                return;
            }

            var users = new List<ApplicationUser>()
            {
                new ApplicationUser
                {
                    FirstName = "Stefan",
                    LastName = "Dimitrov",
                    Email = "stefanadminemail@gmail.com",
                    UserName = "AdminStqo"
                },
                new ApplicationUser
                {
                    FirstName = "Georgi",
                    LastName = "Genov",
                    Email = "goshkopicha@abv.bg",
                    UserName = "Goshko"
                },
                new ApplicationUser
                {
                    FirstName = "Stilyan",
                    LastName = "Chanev",
                    Email = "stelkopostelko@gmail.com",
                    UserName = "AlenataVe"
                },
                new ApplicationUser
                {
                    FirstName = "Aleks",
                    LastName = "Stefanov",
                    Email = "rlgalexbrosta@abv.bg",
                    UserName = "AlexTuah"
                },
                new ApplicationUser
                {
                    FirstName = "Teodora",
                    LastName = "Nedkova",
                    Email = "tedimedisedi@gmail.com",
                    UserName = "tediPanda"
                },
                new ApplicationUser
                {
                    FirstName = "Hristo",
                    LastName = "Denev",
                    Email = "hristostyop69@gmail.com",
                    UserName = "IcakiYoichi"
                },
                new ApplicationUser
                {
                    FirstName = "Irem",
                    LastName = "Chaush",
                    Email = "iremkrem@gmail.com",
                    UserName = "KremBadem1"
                },
                new ApplicationUser
                {
                    FirstName = "Aneliq",
                    LastName = "Papazova",
                    Email = "aneliqponteva08@abv.com",
                    UserName = "Pontichka"
                }
            };

            //if (!await roleManager.RoleExistsAsync("Admin"))
            //{
            //    await roleManager.CreateAsync(new IdentityRole("Admin"));
            //}

            //if (!await roleManager.RoleExistsAsync("User"))
            //{
            //    await roleManager.CreateAsync(new IdentityRole("User"));
            //}

            //if (!await roleManager.RoleExistsAsync("Instructor"))
            //{
            //    await roleManager.CreateAsync(new IdentityRole("Instructor"));
            //}

            await userManager.CreateAsync(users[0], "StefkotoAdminaQkiq123");
            //await userManager.AddToRoleAsync(users[0], "Admin");

            await userManager.CreateAsync(users[1], "GoshkoPicha135");
            //await userManager.AddToRoleAsync(users[1], "Instructor");

            await userManager.CreateAsync(users[2], "StilkotoPostilkoto246");
            //await userManager.AddToRoleAsync(users[2], "User");

            await userManager.CreateAsync(users[3], "BroStarBest420");
            //await userManager.AddToRoleAsync(users[3], "Instructor");

            await userManager.CreateAsync(users[4], "VisokataTempetratura38");
            //await userManager.AddToRoleAsync(users[4], "User");

            await userManager.CreateAsync(users[5], "OurUaStoraikaDa69");
            //await userManager.AddToRoleAsync(users[5], "Instructor");

            await userManager.CreateAsync(users[6], "IremBademKrem120");
            //await userManager.AddToRoleAsync(users[6], "User");

            await userManager.CreateAsync(users[7], "PontitoPonitoPont123");
            //await userManager.AddToRoleAsync(users[7], "User");

            await context.ApplicationUsers.AddRangeAsync(users);

            await context.SaveChangesAsync();

            //Seeding gymClasses
            var gymClasses = new List<GymClass>()
            {
                new GymClass
                {
                    ClassName = "Yoga Basics",
                    Capacity = 20,
                    Description = "A beginner-friendly yoga class focusing on fundamental poses.",
                    IsActive = true,
                    InstructorId = users[1].Id,
                },
                new GymClass
                {
                    ClassName = "HIIT Training",
                    Capacity = 15,
                    Description = "High-Intensity Interval Training for a quick, powerful workout.",
                    IsActive = true,
                    InstructorId = users[5].Id,
                },
                new GymClass
                {
                    ClassName = "Strength and Conditioning",
                    Capacity = 25,
                    Description = "Strength-building exercises to improve overall muscle tone.",
                    IsActive = true,
                    InstructorId = users[1].Id,
                },
                new GymClass
                {
                    ClassName = "Pilates Core",
                    Capacity = 18,
                    Description = "A Pilates class focused on core stability and strength.",
                    IsActive = true,
                    InstructorId = users[2].Id,
                },
                new GymClass
                {
                    ClassName = "Cardio Kickboxing",
                    Capacity = 20,
                    Description = "A cardio-intensive class combining boxing moves with aerobic exercise.",
                    IsActive = true,
                    InstructorId = users[7].Id,
                }
            };
            await context.GymClasses.AddRangeAsync(gymClasses);
            await context.SaveChangesAsync();

            //Seeding Progress Records
            var progressRecords = new List<Progress>()
            {
                new Progress
                {
                    Weight = 70.5,
                    Date = new DateTime(2023, 1, 15),
                    TotalReps = 150,
                    TotalSets = 10,
                    Notes = "Increase endurance for next month.",
                    AttendantId = users[7].Id
                },
                new Progress
                {
                    Weight = 72.0,
                    Date = new DateTime(2023, 2, 15),
                    TotalReps = 160,
                    TotalSets = 12,
                    Notes = "Focus on strength training.",
                    AttendantId = users[6].Id
                },
                new Progress
                {
                    Weight = 68.3,
                    Date = new DateTime(2023, 3, 15),
                    TotalReps = 140,
                    TotalSets = 9,
                    Notes = "Work on flexibility and range of motion.",
                    AttendantId = users[5].Id
                },
                new Progress
                {
                    Weight = 69.7,
                    Date = new DateTime(2023, 4, 15),
                    TotalReps = 155,
                    TotalSets = 11,
                    Notes = "Increase weekly cardio sessions.",
                    AttendantId = users[4].Id
                },
                new Progress
                {
                    Weight = 71.2,
                    Date = new DateTime(2023, 5, 15),
                    TotalReps = 165,
                    TotalSets = 12,
                    Notes = "Improve lifting technique for deadlifts.",
                    AttendantId = users[3].Id
                }
            };
            await context.Progresses.AddRangeAsync(progressRecords);
            await context.SaveChangesAsync();

            //Seeding Schedules
            var schedules = new List<Schedule>()
            {
                new Schedule
                {
                    Monday = true,
                    Wednesday = true,
                    Friday = true,
                    Week = new DateTime(2023, 10, 2), // Starting week date
                    TimeSchedule = new TimeOnly(9, 0), // 9:00 AM
                    GymClassId = gymClasses[0].Id,
                },
                new Schedule
                {
                    Tuesday = true,
                    Thursday = true,
                    Saturday = true,
                    Week = new DateTime(2023, 10, 9),
                    TimeSchedule = new TimeOnly(11, 0), // 11:00 AM
                    GymClassId = gymClasses[1].Id
                },
                new Schedule
                {
                    Monday = true,
                    Thursday = true,
                    Week = new DateTime(2023, 10, 16),
                    TimeSchedule = new TimeOnly(7, 0), // 7:00 AM
                    GymClassId = gymClasses[2].Id
                },
                new Schedule
                {
                    Wednesday = true,
                    Friday = true,
                    Sunday = true,
                    Week = new DateTime(2023, 10, 23),
                    TimeSchedule = new TimeOnly(16, 0), // 4:00 PM
                    GymClassId = gymClasses[3].Id
                },
                new Schedule
                {
                    Tuesday = true,
                    Saturday = true,
                    Week = new DateTime(2023, 10, 30),
                    TimeSchedule = new TimeOnly(18, 30), // 6:30 PM
                    GymClassId = gymClasses[4].Id

                },
                new Schedule
                {
                    Wednesday = true,
                    Friday = true,
                    Week = new DateTime(2024, 7, 30),
                    TimeSchedule = new TimeOnly(16, 30), // 6:30 PM
                    GymClassId = gymClasses[1].Id
                }
            };
            await context.Schedules.AddRangeAsync(schedules);
            await context.SaveChangesAsync();

            //Seeding Workout Plans
            var workoutPlans = new List<WorkoutPlan>()
            {
                new WorkoutPlan
                {
                    Name = "Beginner Strength Training",
                    Description = "A strength-focused plan for beginners covering major muscle groups with basic exercises."
                },
                new WorkoutPlan
                {
                    Name = "Cardio Blast",
                    Description = "An intense cardio plan aimed at improving stamina and cardiovascular health."
                },
                new WorkoutPlan
                {
                    Name = "Flexibility and Mobility",
                    Description = "A plan designed to improve flexibility and range of motion with a series of daily stretches."
                },
                new WorkoutPlan
                {
                    Name = "Weight Loss Program",
                    Description = "A comprehensive plan combining cardio and strength training to aid in weight loss."
                },
                new WorkoutPlan
                {
                    Name = "Advanced Muscle Building",
                    Description = "An advanced workout plan focusing on muscle hypertrophy with compound and isolation exercises."
                }
            };
            await context.WorkoutPlans.AddRangeAsync(workoutPlans);
            await context.SaveChangesAsync();

            //Seeding Workout Routines
            var workoutRoutines = new List<WorkoutRoutine>()
            {
                new WorkoutRoutine
                {
                    ExerciseName = "Bench Press",
                    Description = "A compound upper-body exercise that targets the chest, shoulders, and triceps.",
                    Weight = 60.0,
                    Reps = 12,
                    WorkoutPlanId = workoutPlans[0].Id,
                },
                new WorkoutRoutine
                {
                    ExerciseName = "Squats",
                    Description = "A full-body exercise that primarily targets the quadriceps, hamstrings, and glutes.",
                    Weight = 80.0,
                    Reps = 10,
                    WorkoutPlanId = workoutPlans[0].Id
                },
                new WorkoutRoutine
                {
                    ExerciseName = "Deadlifts",
                    Description = "A strength exercise that works the lower back, glutes, and hamstrings.",
                    Weight = 100.0,
                    Reps = 8,
                    WorkoutPlanId = workoutPlans[1].Id
                },
                new WorkoutRoutine
                {
                    ExerciseName = "Jump Rope",
                    Description = "A cardio exercise that improves cardiovascular endurance and agility.",
                    Weight = 0.0,
                    Reps = 200,
                    WorkoutPlanId = workoutPlans[1].Id
                },
                new WorkoutRoutine
                {
                    ExerciseName = "Lat Pulldown",
                    Description = "Strength exercise focused on the back muscles",
                    Weight = 70.0,
                    Reps = 10,
                    WorkoutPlanId = workoutPlans[2].Id
                },
                new WorkoutRoutine
                {
                    ExerciseName = "Leg Press",
                    Description = "Lower body exercise targeting quads and glutes",
                    Weight = 150.0,
                    Reps = 12,
                    WorkoutPlanId = workoutPlans[2].Id
                },
                new WorkoutRoutine
                {
                    ExerciseName = "Push-Ups",
                    Description = "An upper-body exercise targeting chest, shoulders, and triceps, performed with bodyweight.",
                    Weight = 0.0,
                    Reps = 20,
                    WorkoutPlanId = workoutPlans[3].Id
                },
                new WorkoutRoutine
                {
                    ExerciseName = "Dumbbell Curls",
                    Description = "Upper body exercise focusing on the biceps",
                    Weight = 15.0,
                    Reps = 12,
                    WorkoutPlanId = workoutPlans[3].Id
                },
                new WorkoutRoutine
                {
                    ExerciseName = "Tricep Dips",
                    Description = "Bodyweight exercise targeting the triceps",
                    Weight = 0.0,  // Bodyweight exercise, so weight is 0
                    Reps = 15,
                    WorkoutPlanId = workoutPlans[4].Id
                }
            };
            await context.WorkoutRoutines.AddRangeAsync(workoutRoutines);
            await context.SaveChangesAsync();

            //Seeding Membership Plans
            var membershipPlans = new List<MembershipPlan>()
            {
                new MembershipPlan
                {
                    PlanType = PlanType.Standart,
                    Price = 29.99m,
                    Description = "Basic monthly plan with access to all gym facilities.",
                    Duration = 1,
                    AttendantId = users[2].Id
                },
                new MembershipPlan
                {
                    PlanType = PlanType.Premium,
                    Price = 79.99m,
                    Description = "Quarterly plan with a discount on personal training sessions.",
                    Duration = 3,
                    AttendantId = users[1].Id
                },
                new MembershipPlan
                {
                    PlanType = PlanType.VIP,
                    Price = 299.99m,
                    Description = "Yearly plan with unlimited access to all classes and facilities.",
                    Duration = 12,
                    AttendantId = users[0].Id
                },
                new MembershipPlan
                {
                    PlanType = PlanType.Student,
                    Price = 39.99m,
                    Description = "Monthly plan with access to premium equipment and classes.",
                    Duration = 1,
                    AttendantId = users[4].Id
                },
                new MembershipPlan
                {
                    PlanType = PlanType.Senior,
                    Price = 109.99m,
                    Description = "Quarterly plan for couples with a discounted price.",
                    Duration = 3,
                    AttendantId = users[6].Id
                }
            };
            await context.MembershipPlans.AddRangeAsync(membershipPlans);
            await context.SaveChangesAsync();

            //Seeding Mapping Table
            var attendantClasses = new List<AttendantClass>()
            {
                new AttendantClass
                {
                    GymClassId = 1,
                    AttendantId = users[6].Id
                },
                new AttendantClass
                {
                    GymClassId = 2,
                    AttendantId = users[3].Id
                },
                new AttendantClass
                {
                    GymClassId = 3,
                    AttendantId = users[7].Id
                },
                new AttendantClass
                {
                    GymClassId = 4,
                    AttendantId = users[0].Id
                },
                new AttendantClass
                {
                    GymClassId = 5,
                    AttendantId = users[5].Id
                }
            };
            await context.AttendantsClasses.AddRangeAsync(attendantClasses);

            await context.SaveChangesAsync();
        }
    }
}

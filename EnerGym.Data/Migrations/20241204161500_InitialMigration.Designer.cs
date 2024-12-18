﻿// <auto-generated />
using System;
using EnerGym.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EnerGym.Data.Migrations
{
    [DbContext(typeof(EnerGymDbContext))]
    [Migration("20241204161500_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EnerGym.Data.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)")
                        .HasComment("User's First Name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)")
                        .HasComment("User's Last Name");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("EnerGym.Data.Models.AttendantClass", b =>
                {
                    b.Property<int>("GymClassId")
                        .HasColumnType("int")
                        .HasComment("Gym Class Foreign Key");

                    b.Property<string>("AttendantId")
                        .HasColumnType("nvarchar(450)")
                        .HasComment("Attendant Foreign Key");

                    b.HasKey("GymClassId", "AttendantId");

                    b.HasIndex("AttendantId");

                    b.ToTable("AttendantsClasses");
                });

            modelBuilder.Entity("EnerGym.Data.Models.GymClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Gym Class Identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int")
                        .HasComment("Gym Class Attendants Capacity");

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasComment("Gym Class Name");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasComment("Gym Class Description");

                    b.Property<string>("InstructorName")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)")
                        .HasComment("Instructor Navigation Property");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasComment("Soft Delete Flag");

                    b.HasKey("Id");

                    b.ToTable("GymClasses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Capacity = 20,
                            ClassName = "Yoga Basics",
                            Description = "A beginner-friendly yoga class focusing on fundamental poses.",
                            InstructorName = "Pesho",
                            IsActive = true
                        },
                        new
                        {
                            Id = 2,
                            Capacity = 15,
                            ClassName = "HIIT Training",
                            Description = "High-Intensity Interval Training for a quick, powerful workout.",
                            InstructorName = "Teodor",
                            IsActive = true
                        },
                        new
                        {
                            Id = 3,
                            Capacity = 25,
                            ClassName = "Strength and Conditioning",
                            Description = "Strength-building exercises to improve overall muscle tone.",
                            InstructorName = "Aleksandar",
                            IsActive = true
                        });
                });

            modelBuilder.Entity("EnerGym.Data.Models.MembershipPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Membership Plan Identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AttendantId")
                        .HasColumnType("nvarchar(450)")
                        .HasComment("Attendat Identifier");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Duration")
                        .HasColumnType("int")
                        .HasComment("Membership Plan Duration");

                    b.Property<int>("PlanType")
                        .HasColumnType("int")
                        .HasComment("Membership Plan Type Options");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasComment("Membership Plan Price");

                    b.HasKey("Id");

                    b.HasIndex("AttendantId");

                    b.ToTable("MembershipPlans");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Basic monthly plan with access to all gym facilities.",
                            Duration = 1,
                            PlanType = 0,
                            Price = 29.99m
                        },
                        new
                        {
                            Id = 2,
                            Description = "Quarterly plan with a discount on personal training sessions.",
                            Duration = 3,
                            PlanType = 1,
                            Price = 79.99m
                        },
                        new
                        {
                            Id = 3,
                            Description = "Yearly plan with unlimited access to all classes and facilities.",
                            Duration = 12,
                            PlanType = 2,
                            Price = 299.99m
                        },
                        new
                        {
                            Id = 4,
                            Description = "Monthly plan with access to premium equipment and classes.",
                            Duration = 1,
                            PlanType = 4,
                            Price = 39.99m
                        },
                        new
                        {
                            Id = 5,
                            Description = "Quarterly plan for couples with a discounted price.",
                            Duration = 3,
                            PlanType = 5,
                            Price = 109.99m
                        });
                });

            modelBuilder.Entity("EnerGym.Data.Models.Progress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Attendant's Progress Identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AttendantId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasComment("Attendant's Progress Foreign Key");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2")
                        .HasComment("Attendant's Date Of Progress Record");

                    b.Property<string>("Notes")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasComment("List Of Future Goals");

                    b.Property<int>("TotalReps")
                        .HasColumnType("int")
                        .HasComment("Attedant's Total Reps For The Date's Plan");

                    b.Property<int>("TotalSets")
                        .HasColumnType("int")
                        .HasComment("Attedant's Total Sets For The Date's Plan");

                    b.Property<double>("Weight")
                        .HasColumnType("float")
                        .HasComment("Attendant's current weight");

                    b.HasKey("Id");

                    b.HasIndex("AttendantId");

                    b.ToTable("Progresses");
                });

            modelBuilder.Entity("EnerGym.Data.Models.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Schedule Identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Friday")
                        .HasColumnType("bit");

                    b.Property<int>("GymClassId")
                        .HasColumnType("int")
                        .HasComment("Gym Class Foreign Key");

                    b.Property<bool>("Monday")
                        .HasColumnType("bit");

                    b.Property<bool>("Saturday")
                        .HasColumnType("bit");

                    b.Property<bool>("Sunday")
                        .HasColumnType("bit");

                    b.Property<bool>("Thursday")
                        .HasColumnType("bit");

                    b.Property<TimeOnly>("TimeSchedule")
                        .HasColumnType("time")
                        .HasComment("The Gym Class time of conducting");

                    b.Property<bool>("Tuesday")
                        .HasColumnType("bit");

                    b.Property<bool>("Wednesday")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Week")
                        .HasColumnType("datetime2")
                        .HasComment("The Week Of The Schedule");

                    b.HasKey("Id");

                    b.HasIndex("GymClassId");

                    b.ToTable("Schedules");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Friday = true,
                            GymClassId = 1,
                            Monday = true,
                            Saturday = false,
                            Sunday = false,
                            Thursday = false,
                            TimeSchedule = new TimeOnly(9, 0, 0),
                            Tuesday = false,
                            Wednesday = true,
                            Week = new DateTime(2023, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            Friday = false,
                            GymClassId = 1,
                            Monday = false,
                            Saturday = true,
                            Sunday = false,
                            Thursday = true,
                            TimeSchedule = new TimeOnly(11, 0, 0),
                            Tuesday = true,
                            Wednesday = false,
                            Week = new DateTime(2023, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            Friday = false,
                            GymClassId = 2,
                            Monday = true,
                            Saturday = false,
                            Sunday = false,
                            Thursday = true,
                            TimeSchedule = new TimeOnly(7, 0, 0),
                            Tuesday = false,
                            Wednesday = false,
                            Week = new DateTime(2023, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4,
                            Friday = true,
                            GymClassId = 2,
                            Monday = false,
                            Saturday = false,
                            Sunday = true,
                            Thursday = false,
                            TimeSchedule = new TimeOnly(16, 0, 0),
                            Tuesday = false,
                            Wednesday = true,
                            Week = new DateTime(2023, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 5,
                            Friday = false,
                            GymClassId = 3,
                            Monday = false,
                            Saturday = true,
                            Sunday = false,
                            Thursday = false,
                            TimeSchedule = new TimeOnly(18, 30, 0),
                            Tuesday = true,
                            Wednesday = false,
                            Week = new DateTime(2023, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 6,
                            Friday = true,
                            GymClassId = 3,
                            Monday = false,
                            Saturday = false,
                            Sunday = false,
                            Thursday = false,
                            TimeSchedule = new TimeOnly(16, 30, 0),
                            Tuesday = false,
                            Wednesday = true,
                            Week = new DateTime(2024, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("EnerGym.Data.Models.WorkoutPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Workout Plant Identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)")
                        .HasComment("Workout Plan Description");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)")
                        .HasComment("Workout plan image url");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasComment("Soft delete");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)")
                        .HasComment("Workout Plan Name");

                    b.HasKey("Id");

                    b.ToTable("WorkoutPlans");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "A strength-focused plan for beginners covering major muscle groups with basic exercises.",
                            ImageUrl = "https://experiencelife.lifetime.life/wp-content/uploads/2022/07/etin22556950-card-getting-bigger-and-stronger-1136x640-1.jpg",
                            IsDeleted = false,
                            Name = "Beginner Strength Training"
                        },
                        new
                        {
                            Id = 2,
                            Description = "An intense cardio plan aimed at improving stamina and cardiovascular health.",
                            ImageUrl = "https://www.verywellfit.com/thmb/Y-pUPTgW0nQOwfBz7ahVRaTMHBg=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/everything-you-need-to-know-about-cardio-1229553-8e08847ffcfb4845b29c08fa27e76d32.jpg",
                            IsDeleted = false,
                            Name = "Cardio Blast"
                        },
                        new
                        {
                            Id = 3,
                            Description = "A plan designed to improve flexibility and range of motion with a series of daily stretches.",
                            ImageUrl = "https://media.glamour.com/photos/64b8316162c0e3f198870e20/4:3/w_1440,h_1080,c_limit/0718-flexibility.png",
                            IsDeleted = false,
                            Name = "Flexibility and Mobility"
                        },
                        new
                        {
                            Id = 4,
                            Description = "A comprehensive plan combining cardio and strength training to aid in weight loss.",
                            ImageUrl = "https://myauthentikspoon.com/wp-content/uploads/Strenght-Training-benefits-photo2_C-1024x1024.webp",
                            IsDeleted = false,
                            Name = "Weight Loss Program"
                        },
                        new
                        {
                            Id = 5,
                            Description = "An advanced workout plan focusing on muscle hypertrophy with compound and isolation exercises.",
                            ImageUrl = "https://i0.wp.com/www.muscleandfitness.com/wp-content/uploads/2019/01/Muscular-Fitness-Model-Looking-In-The-Mirror-Next-To-Dumbbell-Rack.jpg?quality=86&strip=all",
                            IsDeleted = false,
                            Name = "Advanced Muscle Building"
                        });
                });

            modelBuilder.Entity("EnerGym.Data.Models.WorkoutRoutine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Workout Routine Identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)")
                        .HasComment("Exercise Description");

                    b.Property<string>("ExerciseName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasComment("Workout Routine Exercise Name");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasComment("Soft delete");

                    b.Property<int>("Reps")
                        .HasColumnType("int")
                        .HasComment("Exercise Reps");

                    b.Property<int>("Sets")
                        .HasColumnType("int")
                        .HasComment("Exercise Sets");

                    b.Property<double?>("Weight")
                        .HasColumnType("float")
                        .HasComment("Exercise Equipment Weight");

                    b.Property<int?>("WorkoutPlanId")
                        .HasColumnType("int")
                        .HasComment("Workout Plan Foreign Key");

                    b.HasKey("Id");

                    b.HasIndex("WorkoutPlanId");

                    b.ToTable("WorkoutRoutines");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "A compound upper-body exercise that targets the chest, shoulders, and triceps.",
                            ExerciseName = "Bench Press",
                            IsDeleted = false,
                            Reps = 12,
                            Sets = 3,
                            Weight = 60.0,
                            WorkoutPlanId = 1
                        },
                        new
                        {
                            Id = 2,
                            Description = "A full-body exercise that primarily targets the quadriceps, hamstrings, and glutes.",
                            ExerciseName = "Squats",
                            IsDeleted = false,
                            Reps = 10,
                            Sets = 3,
                            WorkoutPlanId = 1
                        },
                        new
                        {
                            Id = 3,
                            Description = "A strength exercise that works the lower back, glutes, and hamstrings.",
                            ExerciseName = "Deadlifts",
                            IsDeleted = false,
                            Reps = 8,
                            Sets = 2,
                            Weight = 100.0,
                            WorkoutPlanId = 2
                        },
                        new
                        {
                            Id = 4,
                            Description = "A cardio exercise that improves cardiovascular endurance and agility.",
                            ExerciseName = "Jump Rope",
                            IsDeleted = false,
                            Reps = 200,
                            Sets = 4,
                            WorkoutPlanId = 2
                        },
                        new
                        {
                            Id = 5,
                            Description = "Strength exercise focused on the back muscles",
                            ExerciseName = "Lat Pulldown",
                            IsDeleted = false,
                            Reps = 12,
                            Sets = 3,
                            Weight = 70.0,
                            WorkoutPlanId = 3
                        },
                        new
                        {
                            Id = 6,
                            Description = "Lower body exercise targeting quads and glutes",
                            ExerciseName = "Leg Press",
                            IsDeleted = false,
                            Reps = 12,
                            Sets = 3,
                            Weight = 150.0,
                            WorkoutPlanId = 3
                        },
                        new
                        {
                            Id = 7,
                            Description = "An upper-body exercise targeting chest, shoulders, and triceps, performed with bodyweight.",
                            ExerciseName = "Push-Ups",
                            IsDeleted = false,
                            Reps = 20,
                            Sets = 5,
                            WorkoutPlanId = 4
                        },
                        new
                        {
                            Id = 8,
                            Description = "Upper body exercise focusing on the biceps",
                            ExerciseName = "Dumbbell Curls",
                            IsDeleted = false,
                            Reps = 12,
                            Sets = 3,
                            Weight = 15.0,
                            WorkoutPlanId = 4
                        },
                        new
                        {
                            Id = 9,
                            Description = "Bodyweight exercise targeting the triceps",
                            ExerciseName = "Tricep Dips",
                            IsDeleted = false,
                            Reps = 15,
                            Sets = 3,
                            WorkoutPlanId = 5
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("EnerGym.Data.Models.AttendantClass", b =>
                {
                    b.HasOne("EnerGym.Data.Models.ApplicationUser", "Attendant")
                        .WithMany("AttendantClasses")
                        .HasForeignKey("AttendantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EnerGym.Data.Models.GymClass", "GymClass")
                        .WithMany("AttendantClasses")
                        .HasForeignKey("GymClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attendant");

                    b.Navigation("GymClass");
                });

            modelBuilder.Entity("EnerGym.Data.Models.MembershipPlan", b =>
                {
                    b.HasOne("EnerGym.Data.Models.ApplicationUser", "Attendant")
                        .WithMany()
                        .HasForeignKey("AttendantId");

                    b.Navigation("Attendant");
                });

            modelBuilder.Entity("EnerGym.Data.Models.Progress", b =>
                {
                    b.HasOne("EnerGym.Data.Models.ApplicationUser", "Attendant")
                        .WithMany()
                        .HasForeignKey("AttendantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attendant");
                });

            modelBuilder.Entity("EnerGym.Data.Models.Schedule", b =>
                {
                    b.HasOne("EnerGym.Data.Models.GymClass", "GymClass")
                        .WithMany("Schedules")
                        .HasForeignKey("GymClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GymClass");
                });

            modelBuilder.Entity("EnerGym.Data.Models.WorkoutRoutine", b =>
                {
                    b.HasOne("EnerGym.Data.Models.WorkoutPlan", "WorkoutPlan")
                        .WithMany("WorkoutRoutines")
                        .HasForeignKey("WorkoutPlanId");

                    b.Navigation("WorkoutPlan");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("EnerGym.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("EnerGym.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EnerGym.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("EnerGym.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EnerGym.Data.Models.ApplicationUser", b =>
                {
                    b.Navigation("AttendantClasses");
                });

            modelBuilder.Entity("EnerGym.Data.Models.GymClass", b =>
                {
                    b.Navigation("AttendantClasses");

                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("EnerGym.Data.Models.WorkoutPlan", b =>
                {
                    b.Navigation("WorkoutRoutines");
                });
#pragma warning restore 612, 618
        }
    }
}

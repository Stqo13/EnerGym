using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EnerGym.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false, comment: "User's First Name"),
                    LastName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false, comment: "User's Last Name"),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GymClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Gym Class Identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false, comment: "Gym Class Name"),
                    Capacity = table.Column<int>(type: "int", nullable: false, comment: "Gym Class Attendants Capacity"),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "Gym Class Description"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "Soft Delete Flag"),
                    InstructorName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false, comment: "Instructor Navigation Property")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GymClasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Workout Plant Identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false, comment: "Workout Plan Name"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Workout plan image url"),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false, comment: "Workout Plan Description"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Soft delete")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MembershipPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Membership Plan Identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanType = table.Column<int>(type: "int", nullable: false, comment: "Membership Plan Type Options"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Membership Plan Price"),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: false, comment: "Membership Plan Duration"),
                    AttendantId = table.Column<string>(type: "nvarchar(450)", nullable: true, comment: "Attendat Identifier")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MembershipPlans_AspNetUsers_AttendantId",
                        column: x => x.AttendantId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Progresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Attendant's Progress Identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Weight = table.Column<double>(type: "float", nullable: false, comment: "Attendant's current weight"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Attendant's Date Of Progress Record"),
                    TotalReps = table.Column<int>(type: "int", nullable: false, comment: "Attedant's Total Reps For The Date's Plan"),
                    TotalSets = table.Column<int>(type: "int", nullable: false, comment: "Attedant's Total Sets For The Date's Plan"),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true, comment: "List Of Future Goals"),
                    AttendantId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Attendant's Progress Foreign Key")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Progresses_AspNetUsers_AttendantId",
                        column: x => x.AttendantId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttendantsClasses",
                columns: table => new
                {
                    GymClassId = table.Column<int>(type: "int", nullable: false, comment: "Gym Class Foreign Key"),
                    AttendantId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Attendant Foreign Key")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendantsClasses", x => new { x.GymClassId, x.AttendantId });
                    table.ForeignKey(
                        name: "FK_AttendantsClasses_AspNetUsers_AttendantId",
                        column: x => x.AttendantId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AttendantsClasses_GymClasses_GymClassId",
                        column: x => x.GymClassId,
                        principalTable: "GymClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Schedule Identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Monday = table.Column<bool>(type: "bit", nullable: false),
                    Tuesday = table.Column<bool>(type: "bit", nullable: false),
                    Wednesday = table.Column<bool>(type: "bit", nullable: false),
                    Thursday = table.Column<bool>(type: "bit", nullable: false),
                    Friday = table.Column<bool>(type: "bit", nullable: false),
                    Saturday = table.Column<bool>(type: "bit", nullable: false),
                    Sunday = table.Column<bool>(type: "bit", nullable: false),
                    Week = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The Week Of The Schedule"),
                    GymClassId = table.Column<int>(type: "int", nullable: false, comment: "Gym Class Foreign Key"),
                    TimeSchedule = table.Column<TimeOnly>(type: "time", nullable: false, comment: "The Gym Class time of conducting")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_GymClasses_GymClassId",
                        column: x => x.GymClassId,
                        principalTable: "GymClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutRoutines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Workout Routine Identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Workout Routine Exercise Name"),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false, comment: "Exercise Description"),
                    Weight = table.Column<double>(type: "float", nullable: true, comment: "Exercise Equipment Weight"),
                    Reps = table.Column<int>(type: "int", nullable: false, comment: "Exercise Reps"),
                    Sets = table.Column<int>(type: "int", nullable: false, comment: "Exercise Sets"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Soft delete"),
                    WorkoutPlanId = table.Column<int>(type: "int", nullable: true, comment: "Workout Plan Foreign Key")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutRoutines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutRoutines_WorkoutPlans_WorkoutPlanId",
                        column: x => x.WorkoutPlanId,
                        principalTable: "WorkoutPlans",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "GymClasses",
                columns: new[] { "Id", "Capacity", "ClassName", "Description", "InstructorName", "IsActive" },
                values: new object[,]
                {
                    { 1, 20, "Yoga Basics", "A beginner-friendly yoga class focusing on fundamental poses.", "Pesho", true },
                    { 2, 15, "HIIT Training", "High-Intensity Interval Training for a quick, powerful workout.", "Teodor", true },
                    { 3, 25, "Strength and Conditioning", "Strength-building exercises to improve overall muscle tone.", "Aleksandar", true }
                });

            migrationBuilder.InsertData(
                table: "MembershipPlans",
                columns: new[] { "Id", "AttendantId", "Description", "Duration", "PlanType", "Price" },
                values: new object[,]
                {
                    { 1, null, "Basic monthly plan with access to all gym facilities.", 1, 0, 29.99m },
                    { 2, null, "Quarterly plan with a discount on personal training sessions.", 3, 1, 79.99m },
                    { 3, null, "Yearly plan with unlimited access to all classes and facilities.", 12, 2, 299.99m },
                    { 4, null, "Monthly plan with access to premium equipment and classes.", 1, 4, 39.99m },
                    { 5, null, "Quarterly plan for couples with a discounted price.", 3, 5, 109.99m }
                });

            migrationBuilder.InsertData(
                table: "WorkoutPlans",
                columns: new[] { "Id", "Description", "ImageUrl", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, "A strength-focused plan for beginners covering major muscle groups with basic exercises.", "https://experiencelife.lifetime.life/wp-content/uploads/2022/07/etin22556950-card-getting-bigger-and-stronger-1136x640-1.jpg", false, "Beginner Strength Training" },
                    { 2, "An intense cardio plan aimed at improving stamina and cardiovascular health.", "https://www.verywellfit.com/thmb/Y-pUPTgW0nQOwfBz7ahVRaTMHBg=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/everything-you-need-to-know-about-cardio-1229553-8e08847ffcfb4845b29c08fa27e76d32.jpg", false, "Cardio Blast" },
                    { 3, "A plan designed to improve flexibility and range of motion with a series of daily stretches.", "https://media.glamour.com/photos/64b8316162c0e3f198870e20/4:3/w_1440,h_1080,c_limit/0718-flexibility.png", false, "Flexibility and Mobility" },
                    { 4, "A comprehensive plan combining cardio and strength training to aid in weight loss.", "https://myauthentikspoon.com/wp-content/uploads/Strenght-Training-benefits-photo2_C-1024x1024.webp", false, "Weight Loss Program" },
                    { 5, "An advanced workout plan focusing on muscle hypertrophy with compound and isolation exercises.", "https://i0.wp.com/www.muscleandfitness.com/wp-content/uploads/2019/01/Muscular-Fitness-Model-Looking-In-The-Mirror-Next-To-Dumbbell-Rack.jpg?quality=86&strip=all", false, "Advanced Muscle Building" }
                });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "Id", "Friday", "GymClassId", "Monday", "Saturday", "Sunday", "Thursday", "TimeSchedule", "Tuesday", "Wednesday", "Week" },
                values: new object[,]
                {
                    { 1, true, 1, true, false, false, false, new TimeOnly(9, 0, 0), false, true, new DateTime(2023, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, false, 1, false, true, false, true, new TimeOnly(11, 0, 0), true, false, new DateTime(2023, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, false, 2, true, false, false, true, new TimeOnly(7, 0, 0), false, false, new DateTime(2023, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, true, 2, false, false, true, false, new TimeOnly(16, 0, 0), false, true, new DateTime(2023, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, false, 3, false, true, false, false, new TimeOnly(18, 30, 0), true, false, new DateTime(2023, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, true, 3, false, false, false, false, new TimeOnly(16, 30, 0), false, true, new DateTime(2024, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "WorkoutRoutines",
                columns: new[] { "Id", "Description", "ExerciseName", "IsDeleted", "Reps", "Sets", "Weight", "WorkoutPlanId" },
                values: new object[,]
                {
                    { 1, "A compound upper-body exercise that targets the chest, shoulders, and triceps.", "Bench Press", false, 12, 3, 60.0, 1 },
                    { 2, "A full-body exercise that primarily targets the quadriceps, hamstrings, and glutes.", "Squats", false, 10, 3, null, 1 },
                    { 3, "A strength exercise that works the lower back, glutes, and hamstrings.", "Deadlifts", false, 8, 2, 100.0, 2 },
                    { 4, "A cardio exercise that improves cardiovascular endurance and agility.", "Jump Rope", false, 200, 4, null, 2 },
                    { 5, "Strength exercise focused on the back muscles", "Lat Pulldown", false, 12, 3, 70.0, 3 },
                    { 6, "Lower body exercise targeting quads and glutes", "Leg Press", false, 12, 3, 150.0, 3 },
                    { 7, "An upper-body exercise targeting chest, shoulders, and triceps, performed with bodyweight.", "Push-Ups", false, 20, 5, null, 4 },
                    { 8, "Upper body exercise focusing on the biceps", "Dumbbell Curls", false, 12, 3, 15.0, 4 },
                    { 9, "Bodyweight exercise targeting the triceps", "Tricep Dips", false, 15, 3, null, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AttendantsClasses_AttendantId",
                table: "AttendantsClasses",
                column: "AttendantId");

            migrationBuilder.CreateIndex(
                name: "IX_MembershipPlans_AttendantId",
                table: "MembershipPlans",
                column: "AttendantId");

            migrationBuilder.CreateIndex(
                name: "IX_Progresses_AttendantId",
                table: "Progresses",
                column: "AttendantId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_GymClassId",
                table: "Schedules",
                column: "GymClassId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutRoutines_WorkoutPlanId",
                table: "WorkoutRoutines",
                column: "WorkoutPlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AttendantsClasses");

            migrationBuilder.DropTable(
                name: "MembershipPlans");

            migrationBuilder.DropTable(
                name: "Progresses");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "WorkoutRoutines");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "GymClasses");

            migrationBuilder.DropTable(
                name: "WorkoutPlans");
        }
    }
}

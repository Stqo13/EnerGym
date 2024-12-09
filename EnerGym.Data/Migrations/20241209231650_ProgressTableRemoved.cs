using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnerGym.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProgressTableRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Progresses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Progresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Attendant's Progress Identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttendantId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Attendant's Progress Foreign Key"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Attendant's Date Of Progress Record"),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true, comment: "List Of Future Goals"),
                    TotalReps = table.Column<int>(type: "int", nullable: false, comment: "Attedant's Total Reps For The Date's Plan"),
                    TotalSets = table.Column<int>(type: "int", nullable: false, comment: "Attedant's Total Sets For The Date's Plan"),
                    Weight = table.Column<double>(type: "float", nullable: false, comment: "Attendant's current weight")
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

            migrationBuilder.CreateIndex(
                name: "IX_Progresses_AttendantId",
                table: "Progresses",
                column: "AttendantId");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnerGym.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedSoftDeleteToRoutines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "WorkoutRoutines",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Soft delete");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "WorkoutRoutines");
        }
    }
}

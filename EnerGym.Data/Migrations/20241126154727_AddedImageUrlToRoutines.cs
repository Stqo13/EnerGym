using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnerGym.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedImageUrlToRoutines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "WorkoutRoutines",
                type: "nvarchar(max)",
                nullable: true,
                comment: "Routine ImageURL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "WorkoutRoutines");
        }
    }
}

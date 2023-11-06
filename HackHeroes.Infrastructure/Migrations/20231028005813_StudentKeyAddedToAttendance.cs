using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackHeroes.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StudentKeyAddedToAttendance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StudentKey",
                table: "Attendance",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentKey",
                table: "Attendance");
        }
    }
}

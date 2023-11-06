using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackHeroes.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StudentsKeysMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StudentKey",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentKey",
                table: "Students");
        }
    }
}

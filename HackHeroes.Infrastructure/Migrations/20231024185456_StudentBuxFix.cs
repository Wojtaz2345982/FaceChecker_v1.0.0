using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackHeroes.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StudentBuxFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Students");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

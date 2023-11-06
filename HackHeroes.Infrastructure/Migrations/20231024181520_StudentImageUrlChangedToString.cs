using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackHeroes.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StudentImageUrlChangedToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageBytes",
                table: "Students");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Students");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageBytes",
                table: "Students",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}

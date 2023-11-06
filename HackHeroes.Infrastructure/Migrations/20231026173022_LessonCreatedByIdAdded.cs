using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackHeroes.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LessonCreatedByIdAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Lessons",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_CreatedById",
                table: "Lessons",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_AspNetUsers_CreatedById",
                table: "Lessons",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_AspNetUsers_CreatedById",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_CreatedById",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Lessons");
        }
    }
}

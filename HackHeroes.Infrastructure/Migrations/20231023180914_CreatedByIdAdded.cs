using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackHeroes.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreatedByIdAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Classes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Classes_CreatedById",
                table: "Classes",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_AspNetUsers_CreatedById",
                table: "Classes",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_AspNetUsers_CreatedById",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_CreatedById",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Classes");
        }
    }
}

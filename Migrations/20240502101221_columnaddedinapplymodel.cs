using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jobPortal.Migrations
{
    /// <inheritdoc />
    public partial class columnaddedinapplymodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameId",
                table: "ApplyModels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "appUserId",
                table: "ApplyModels",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplyModels_appUserId",
                table: "ApplyModels",
                column: "appUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplyModels_AspNetUsers_appUserId",
                table: "ApplyModels",
                column: "appUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplyModels_AspNetUsers_appUserId",
                table: "ApplyModels");

            migrationBuilder.DropIndex(
                name: "IX_ApplyModels_appUserId",
                table: "ApplyModels");

            migrationBuilder.DropColumn(
                name: "NameId",
                table: "ApplyModels");

            migrationBuilder.DropColumn(
                name: "appUserId",
                table: "ApplyModels");
        }
    }
}

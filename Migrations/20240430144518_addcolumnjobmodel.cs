using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jobPortal.Migrations
{
    /// <inheritdoc />
    public partial class addcolumnjobmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "JobModels",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "JobModels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_JobModels_AuthorId",
                table: "JobModels",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobModels_AspNetUsers_AuthorId",
                table: "JobModels",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobModels_AspNetUsers_AuthorId",
                table: "JobModels");

            migrationBuilder.DropIndex(
                name: "IX_JobModels_AuthorId",
                table: "JobModels");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "JobModels");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "JobModels");
        }
    }
}

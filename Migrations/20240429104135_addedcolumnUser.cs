using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jobPortal.Migrations
{
    /// <inheritdoc />
    public partial class addedcolumnUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                table: "AspNetUsers");
        }
    }
}

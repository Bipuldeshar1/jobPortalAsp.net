using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jobPortal.Migrations
{
    /// <inheritdoc />
    public partial class addcatimg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoryImg",
                table: "CategoriesModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryImg",
                table: "CategoriesModel");
        }
    }
}

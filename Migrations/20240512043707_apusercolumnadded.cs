﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jobPortal.Migrations
{
    /// <inheritdoc />
    public partial class apusercolumnadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Img",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Img",
                table: "AspNetUsers");
        }
    }
}

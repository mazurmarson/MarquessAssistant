using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MarqueesAssistant.API.Migrations
{
    public partial class UpdateMarqueeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DownDate",
                table: "Marquees");

            migrationBuilder.DropColumn(
                name: "UpDate",
                table: "Marquees");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Marquees",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "Marquees");

            migrationBuilder.AddColumn<DateTime>(
                name: "DownDate",
                table: "Marquees",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpDate",
                table: "Marquees",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

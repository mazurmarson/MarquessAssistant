using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MarqueesAssistant.API.Migrations
{
    public partial class AddMarqueeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Marquees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Event = table.Column<string>(nullable: true),
                    Width = table.Column<int>(nullable: false),
                    Length = table.Column<int>(nullable: false),
                    UpDate = table.Column<DateTime>(nullable: false),
                    DownDate = table.Column<DateTime>(nullable: false),
                    IsUp = table.Column<bool>(nullable: false),
                    IsDown = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marquees", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Marquees");
        }
    }
}

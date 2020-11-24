using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MarqueesAssistant.API.Migrations
{
    public partial class AddEquipmentBreakDownTables2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Typ = table.Column<int>(nullable: false),
                    Number = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Breakdowns",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EquipmentId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    AccitdentDate = table.Column<DateTime>(nullable: false),
                    RepairdDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Breakdowns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Breakdowns_Equipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Breakdowns_EquipmentId",
                table: "Breakdowns",
                column: "EquipmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Breakdowns");

            migrationBuilder.DropTable(
                name: "Equipments");
        }
    }
}

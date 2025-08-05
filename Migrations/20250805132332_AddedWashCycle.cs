using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarwashMonitor.Migrations
{
    /// <inheritdoc />
    public partial class AddedWashCycle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WashCycles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BoxId = table.Column<Guid>(type: "uuid", nullable: true),
                    WaterConsumption = table.Column<float>(type: "real", nullable: true),
                    DetergentConsumption = table.Column<float>(type: "real", nullable: true),
                    WaxConsumption = table.Column<float>(type: "real", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CoinAmount = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WashCycles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WashCycles_Boxes_BoxId",
                        column: x => x.BoxId,
                        principalTable: "Boxes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WashCycles_BoxId",
                table: "WashCycles",
                column: "BoxId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WashCycles");
        }
    }
}

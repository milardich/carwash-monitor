using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarwashMonitor.Migrations
{
    /// <inheritdoc />
    public partial class AddCoinTrayAmountToBox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CoinTrayAmount",
                table: "Boxes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CoinTrayLimit",
                table: "Boxes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoinTrayAmount",
                table: "Boxes");

            migrationBuilder.DropColumn(
                name: "CoinTrayLimit",
                table: "Boxes");
        }
    }
}

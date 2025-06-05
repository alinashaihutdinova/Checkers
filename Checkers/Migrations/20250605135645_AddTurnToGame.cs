using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Checkers.Migrations
{
    /// <inheritdoc />
    public partial class AddTurnToGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Turn",
                table: "Games",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Turn",
                table: "Games");
        }
    }
}

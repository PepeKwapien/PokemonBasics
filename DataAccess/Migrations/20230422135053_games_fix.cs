using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class gamesfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "MainRegion",
                table: "Games",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainRegion",
                table: "Games");

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Games",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);
        }
    }
}

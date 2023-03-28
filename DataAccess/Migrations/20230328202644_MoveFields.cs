using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MoveFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpecialEffect",
                table: "Moves");

            migrationBuilder.AddColumn<string>(
                name: "Effect",
                table: "Moves",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Moves",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SpecialEffectChance",
                table: "Moves",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Effect",
                table: "Moves");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Moves");

            migrationBuilder.DropColumn(
                name: "SpecialEffectChance",
                table: "Moves");

            migrationBuilder.AddColumn<string>(
                name: "SpecialEffect",
                table: "Moves",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);
        }
    }
}

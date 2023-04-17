using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class cascadetypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DamageMultipliers_Types_TypeId",
                table: "DamageMultipliers");

            migrationBuilder.AddForeignKey(
                name: "FK_DamageMultipliers_Types_TypeId",
                table: "DamageMultipliers",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DamageMultipliers_Types_TypeId",
                table: "DamageMultipliers");

            migrationBuilder.AddForeignKey(
                name: "FK_DamageMultipliers_Types_TypeId",
                table: "DamageMultipliers",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id");
        }
    }
}

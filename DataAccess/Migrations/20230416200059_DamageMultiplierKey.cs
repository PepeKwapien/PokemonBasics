using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DamageMultiplierKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DamageMultipliers",
                table: "DamageMultipliers");

            migrationBuilder.AddColumn<string>(
                name: "Target",
                table: "Moves",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "DamageMultipliers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_DamageMultipliers",
                table: "DamageMultipliers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DamageMultipliers_TypeId",
                table: "DamageMultipliers",
                column: "TypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DamageMultipliers",
                table: "DamageMultipliers");

            migrationBuilder.DropIndex(
                name: "IX_DamageMultipliers_TypeId",
                table: "DamageMultipliers");

            migrationBuilder.DropColumn(
                name: "Target",
                table: "Moves");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DamageMultipliers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DamageMultipliers",
                table: "DamageMultipliers",
                columns: new[] { "TypeId", "AgainstId" });
        }
    }
}

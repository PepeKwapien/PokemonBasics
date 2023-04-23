using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class removedcomplexkeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PokemonMoves",
                table: "PokemonMoves");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PokemonEntries",
                table: "PokemonEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PokemonAbilities",
                table: "PokemonAbilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AlternateForms",
                table: "AlternateForms");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "PokemonMoves",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "PokemonEntries",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "AlternateForms",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_PokemonMoves",
                table: "PokemonMoves",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PokemonEntries",
                table: "PokemonEntries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PokemonAbilities",
                table: "PokemonAbilities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AlternateForms",
                table: "AlternateForms",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonMoves_PokemonId",
                table: "PokemonMoves",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonEntries_PokemonId",
                table: "PokemonEntries",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonAbilities_PokemonId",
                table: "PokemonAbilities",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_AlternateForms_OriginalId",
                table: "AlternateForms",
                column: "OriginalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PokemonMoves",
                table: "PokemonMoves");

            migrationBuilder.DropIndex(
                name: "IX_PokemonMoves_PokemonId",
                table: "PokemonMoves");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PokemonEntries",
                table: "PokemonEntries");

            migrationBuilder.DropIndex(
                name: "IX_PokemonEntries_PokemonId",
                table: "PokemonEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PokemonAbilities",
                table: "PokemonAbilities");

            migrationBuilder.DropIndex(
                name: "IX_PokemonAbilities_PokemonId",
                table: "PokemonAbilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AlternateForms",
                table: "AlternateForms");

            migrationBuilder.DropIndex(
                name: "IX_AlternateForms_OriginalId",
                table: "AlternateForms");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PokemonMoves");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PokemonEntries");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AlternateForms");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PokemonMoves",
                table: "PokemonMoves",
                columns: new[] { "PokemonId", "MoveId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PokemonEntries",
                table: "PokemonEntries",
                columns: new[] { "PokemonId", "PokedexId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PokemonAbilities",
                table: "PokemonAbilities",
                columns: new[] { "PokemonId", "AbilityId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AlternateForms",
                table: "AlternateForms",
                columns: new[] { "OriginalId", "AlternateId" });
        }
    }
}

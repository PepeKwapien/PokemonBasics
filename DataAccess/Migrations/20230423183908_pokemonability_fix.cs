using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class pokemonabilityfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ability_Generations_GenerationId",
                table: "Ability");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonAbility_Ability_AbilityId",
                table: "PokemonAbility");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonAbility_Pokemons_PokemonId",
                table: "PokemonAbility");

            migrationBuilder.DropTable(
                name: "PokemonAvailabilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PokemonAbility",
                table: "PokemonAbility");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ability",
                table: "Ability");

            migrationBuilder.RenameTable(
                name: "PokemonAbility",
                newName: "PokemonAbilities");

            migrationBuilder.RenameTable(
                name: "Ability",
                newName: "Abilities");

            migrationBuilder.RenameColumn(
                name: "IsHidden",
                table: "PokemonAbilities",
                newName: "Hidden");

            migrationBuilder.RenameIndex(
                name: "IX_PokemonAbility_AbilityId",
                table: "PokemonAbilities",
                newName: "IX_PokemonAbilities_AbilityId");

            migrationBuilder.RenameIndex(
                name: "IX_Ability_GenerationId",
                table: "Abilities",
                newName: "IX_Abilities_GenerationId");

            migrationBuilder.AddColumn<int>(
                name: "Slot",
                table: "PokemonAbilities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PokemonAbilities",
                table: "PokemonAbilities",
                columns: new[] { "PokemonId", "AbilityId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Abilities",
                table: "Abilities",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PokemonEntries",
                columns: table => new
                {
                    PokemonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PokedexId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonEntries", x => new { x.PokemonId, x.PokedexId });
                    table.ForeignKey(
                        name: "FK_PokemonEntries_Pokedexes_PokedexId",
                        column: x => x.PokedexId,
                        principalTable: "Pokedexes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonEntries_Pokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PokemonEntries_PokedexId",
                table: "PokemonEntries",
                column: "PokedexId");

            migrationBuilder.AddForeignKey(
                name: "FK_Abilities_Generations_GenerationId",
                table: "Abilities",
                column: "GenerationId",
                principalTable: "Generations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonAbilities_Abilities_AbilityId",
                table: "PokemonAbilities",
                column: "AbilityId",
                principalTable: "Abilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonAbilities_Pokemons_PokemonId",
                table: "PokemonAbilities",
                column: "PokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Abilities_Generations_GenerationId",
                table: "Abilities");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonAbilities_Abilities_AbilityId",
                table: "PokemonAbilities");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonAbilities_Pokemons_PokemonId",
                table: "PokemonAbilities");

            migrationBuilder.DropTable(
                name: "PokemonEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PokemonAbilities",
                table: "PokemonAbilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Abilities",
                table: "Abilities");

            migrationBuilder.DropColumn(
                name: "Slot",
                table: "PokemonAbilities");

            migrationBuilder.RenameTable(
                name: "PokemonAbilities",
                newName: "PokemonAbility");

            migrationBuilder.RenameTable(
                name: "Abilities",
                newName: "Ability");

            migrationBuilder.RenameColumn(
                name: "Hidden",
                table: "PokemonAbility",
                newName: "IsHidden");

            migrationBuilder.RenameIndex(
                name: "IX_PokemonAbilities_AbilityId",
                table: "PokemonAbility",
                newName: "IX_PokemonAbility_AbilityId");

            migrationBuilder.RenameIndex(
                name: "IX_Abilities_GenerationId",
                table: "Ability",
                newName: "IX_Ability_GenerationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PokemonAbility",
                table: "PokemonAbility",
                columns: new[] { "PokemonId", "AbilityId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ability",
                table: "Ability",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PokemonAvailabilities",
                columns: table => new
                {
                    PokemonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PokedexId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonAvailabilities", x => new { x.PokemonId, x.PokedexId });
                    table.ForeignKey(
                        name: "FK_PokemonAvailabilities_Pokedexes_PokedexId",
                        column: x => x.PokedexId,
                        principalTable: "Pokedexes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonAvailabilities_Pokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PokemonAvailabilities_PokedexId",
                table: "PokemonAvailabilities",
                column: "PokedexId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ability_Generations_GenerationId",
                table: "Ability",
                column: "GenerationId",
                principalTable: "Generations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonAbility_Ability_AbilityId",
                table: "PokemonAbility",
                column: "AbilityId",
                principalTable: "Ability",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonAbility_Pokemons_PokemonId",
                table: "PokemonAbility",
                column: "PokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

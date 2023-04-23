using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class pokemonfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropForeignKey(
                name: "FK_Pokemons_Pokedexes_PokedexId",
                table: "Pokemons");

            migrationBuilder.DropIndex(
                name: "IX_Pokemons_PokedexId",
                table: "Pokemons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PokemonAbilities",
                table: "PokemonAbilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Abilities",
                table: "Abilities");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "PokedexId",
                table: "Pokemons");

            migrationBuilder.RenameTable(
                name: "PokemonAbilities",
                newName: "PokemonAbility");

            migrationBuilder.RenameTable(
                name: "Abilities",
                newName: "Ability");

            migrationBuilder.RenameColumn(
                name: "DexNumber",
                table: "Pokemons",
                newName: "Order");

            migrationBuilder.RenameIndex(
                name: "IX_PokemonAbilities_AbilityId",
                table: "PokemonAbility",
                newName: "IX_PokemonAbility_AbilityId");

            migrationBuilder.RenameIndex(
                name: "IX_Abilities_GenerationId",
                table: "Ability",
                newName: "IX_Ability_GenerationId");

            migrationBuilder.AddColumn<bool>(
                name: "Baby",
                table: "Pokemons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "EggGroups",
                table: "Pokemons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Genera",
                table: "Pokemons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Habitat",
                table: "Pokemons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "HasGenderDifferences",
                table: "Pokemons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Legendary",
                table: "Pokemons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Mythical",
                table: "Pokemons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Shape",
                table: "Pokemons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Pokedexes",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PokemonAbility",
                table: "PokemonAbility",
                columns: new[] { "PokemonId", "AbilityId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ability",
                table: "Ability",
                column: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_PokemonAbility",
                table: "PokemonAbility");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ability",
                table: "Ability");

            migrationBuilder.DropColumn(
                name: "Baby",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "EggGroups",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "Genera",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "Habitat",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "HasGenderDifferences",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "Legendary",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "Mythical",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "Shape",
                table: "Pokemons");

            migrationBuilder.RenameTable(
                name: "PokemonAbility",
                newName: "PokemonAbilities");

            migrationBuilder.RenameTable(
                name: "Ability",
                newName: "Abilities");

            migrationBuilder.RenameColumn(
                name: "Order",
                table: "Pokemons",
                newName: "DexNumber");

            migrationBuilder.RenameIndex(
                name: "IX_PokemonAbility_AbilityId",
                table: "PokemonAbilities",
                newName: "IX_PokemonAbilities_AbilityId");

            migrationBuilder.RenameIndex(
                name: "IX_Ability_GenerationId",
                table: "Abilities",
                newName: "IX_Abilities_GenerationId");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Pokemons",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PokedexId",
                table: "Pokemons",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Pokedexes",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PokemonAbilities",
                table: "PokemonAbilities",
                columns: new[] { "PokemonId", "AbilityId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Abilities",
                table: "Abilities",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_PokedexId",
                table: "Pokemons",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemons_Pokedexes_PokedexId",
                table: "Pokemons",
                column: "PokedexId",
                principalTable: "Pokedexes",
                principalColumn: "Id");
        }
    }
}

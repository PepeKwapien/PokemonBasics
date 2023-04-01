using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class GenerationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DamageMultiplier_Types_AgainstId",
                table: "DamageMultiplier");

            migrationBuilder.DropForeignKey(
                name: "FK_DamageMultiplier_Types_TypeId",
                table: "DamageMultiplier");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonAvailabilities_Games_GameId",
                table: "PokemonAvailabilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DamageMultiplier",
                table: "DamageMultiplier");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "Generation",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "ByEgg",
                table: "PokemonMoves");

            migrationBuilder.DropColumn(
                name: "ByLevelUp",
                table: "PokemonMoves");

            migrationBuilder.DropColumn(
                name: "ByTm",
                table: "PokemonMoves");

            migrationBuilder.DropColumn(
                name: "ByTutor",
                table: "PokemonMoves");

            migrationBuilder.DropColumn(
                name: "SignatureMove",
                table: "PokemonMoves");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Pokeballs");

            migrationBuilder.DropColumn(
                name: "Generation",
                table: "Games");

            migrationBuilder.RenameTable(
                name: "DamageMultiplier",
                newName: "DamageMultipliers");

            migrationBuilder.RenameColumn(
                name: "RegionalNumber",
                table: "PokemonAvailabilities",
                newName: "Number");

            migrationBuilder.RenameColumn(
                name: "GameId",
                table: "PokemonAvailabilities",
                newName: "PokedexId");

            migrationBuilder.RenameIndex(
                name: "IX_PokemonAvailabilities_GameId",
                table: "PokemonAvailabilities",
                newName: "IX_PokemonAvailabilities_PokedexId");

            migrationBuilder.RenameIndex(
                name: "IX_DamageMultiplier_AgainstId",
                table: "DamageMultipliers",
                newName: "IX_DamageMultipliers_AgainstId");

            migrationBuilder.AddColumn<Guid>(
                name: "GenerationId",
                table: "Pokemons",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PokedexId",
                table: "Pokemons",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GameId",
                table: "PokemonMoves",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Method",
                table: "PokemonMoves",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MinimalLevel",
                table: "PokemonMoves",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "SpecialEffectChance",
                table: "Moves",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Power",
                table: "Moves",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PP",
                table: "Moves",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Moves",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(16)",
                oldMaxLength: 16);

            migrationBuilder.AlterColumn<string>(
                name: "Effect",
                table: "Moves",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Moves",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(16)");

            migrationBuilder.AlterColumn<int>(
                name: "Accuracy",
                table: "Moves",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "GenerationId",
                table: "Moves",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "GenerationId",
                table: "Games",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Method",
                table: "Evolutions",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)");

            migrationBuilder.AddColumn<Guid>(
                name: "GenerationId",
                table: "Abilities",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_DamageMultipliers",
                table: "DamageMultipliers",
                columns: new[] { "TypeId", "AgainstId" });

            migrationBuilder.CreateTable(
                name: "Generations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Regions = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pokedexes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Region = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokedexes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GenerationPokeball",
                columns: table => new
                {
                    GenerationsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PokeballsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenerationPokeball", x => new { x.GenerationsId, x.PokeballsId });
                    table.ForeignKey(
                        name: "FK_GenerationPokeball_Generations_GenerationsId",
                        column: x => x.GenerationsId,
                        principalTable: "Generations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenerationPokeball_Pokeballs_PokeballsId",
                        column: x => x.PokeballsId,
                        principalTable: "Pokeballs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamePokedex",
                columns: table => new
                {
                    GamesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PokedexesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePokedex", x => new { x.GamesId, x.PokedexesId });
                    table.ForeignKey(
                        name: "FK_GamePokedex_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePokedex_Pokedexes_PokedexesId",
                        column: x => x.PokedexesId,
                        principalTable: "Pokedexes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_GenerationId",
                table: "Pokemons",
                column: "GenerationId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_PokedexId",
                table: "Pokemons",
                column: "PokedexId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonMoves_GameId",
                table: "PokemonMoves",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Moves_GenerationId",
                table: "Moves",
                column: "GenerationId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_GenerationId",
                table: "Games",
                column: "GenerationId");

            migrationBuilder.CreateIndex(
                name: "IX_Abilities_GenerationId",
                table: "Abilities",
                column: "GenerationId");

            migrationBuilder.CreateIndex(
                name: "IX_GamePokedex_PokedexesId",
                table: "GamePokedex",
                column: "PokedexesId");

            migrationBuilder.CreateIndex(
                name: "IX_GenerationPokeball_PokeballsId",
                table: "GenerationPokeball",
                column: "PokeballsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Abilities_Generations_GenerationId",
                table: "Abilities",
                column: "GenerationId",
                principalTable: "Generations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DamageMultipliers_Types_AgainstId",
                table: "DamageMultipliers",
                column: "AgainstId",
                principalTable: "Types",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DamageMultipliers_Types_TypeId",
                table: "DamageMultipliers",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Generations_GenerationId",
                table: "Games",
                column: "GenerationId",
                principalTable: "Generations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Moves_Generations_GenerationId",
                table: "Moves",
                column: "GenerationId",
                principalTable: "Generations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonAvailabilities_Pokedexes_PokedexId",
                table: "PokemonAvailabilities",
                column: "PokedexId",
                principalTable: "Pokedexes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonMoves_Games_GameId",
                table: "PokemonMoves",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemons_Generations_GenerationId",
                table: "Pokemons",
                column: "GenerationId",
                principalTable: "Generations",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemons_Pokedexes_PokedexId",
                table: "Pokemons",
                column: "PokedexId",
                principalTable: "Pokedexes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Abilities_Generations_GenerationId",
                table: "Abilities");

            migrationBuilder.DropForeignKey(
                name: "FK_DamageMultipliers_Types_AgainstId",
                table: "DamageMultipliers");

            migrationBuilder.DropForeignKey(
                name: "FK_DamageMultipliers_Types_TypeId",
                table: "DamageMultipliers");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Generations_GenerationId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Moves_Generations_GenerationId",
                table: "Moves");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonAvailabilities_Pokedexes_PokedexId",
                table: "PokemonAvailabilities");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonMoves_Games_GameId",
                table: "PokemonMoves");

            migrationBuilder.DropForeignKey(
                name: "FK_Pokemons_Generations_GenerationId",
                table: "Pokemons");

            migrationBuilder.DropForeignKey(
                name: "FK_Pokemons_Pokedexes_PokedexId",
                table: "Pokemons");

            migrationBuilder.DropTable(
                name: "GamePokedex");

            migrationBuilder.DropTable(
                name: "GenerationPokeball");

            migrationBuilder.DropTable(
                name: "Pokedexes");

            migrationBuilder.DropTable(
                name: "Generations");

            migrationBuilder.DropIndex(
                name: "IX_Pokemons_GenerationId",
                table: "Pokemons");

            migrationBuilder.DropIndex(
                name: "IX_Pokemons_PokedexId",
                table: "Pokemons");

            migrationBuilder.DropIndex(
                name: "IX_PokemonMoves_GameId",
                table: "PokemonMoves");

            migrationBuilder.DropIndex(
                name: "IX_Moves_GenerationId",
                table: "Moves");

            migrationBuilder.DropIndex(
                name: "IX_Games_GenerationId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Abilities_GenerationId",
                table: "Abilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DamageMultipliers",
                table: "DamageMultipliers");

            migrationBuilder.DropColumn(
                name: "GenerationId",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "PokedexId",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "PokemonMoves");

            migrationBuilder.DropColumn(
                name: "Method",
                table: "PokemonMoves");

            migrationBuilder.DropColumn(
                name: "MinimalLevel",
                table: "PokemonMoves");

            migrationBuilder.DropColumn(
                name: "GenerationId",
                table: "Moves");

            migrationBuilder.DropColumn(
                name: "GenerationId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "GenerationId",
                table: "Abilities");

            migrationBuilder.RenameTable(
                name: "DamageMultipliers",
                newName: "DamageMultiplier");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "PokemonAvailabilities",
                newName: "RegionalNumber");

            migrationBuilder.RenameColumn(
                name: "PokedexId",
                table: "PokemonAvailabilities",
                newName: "GameId");

            migrationBuilder.RenameIndex(
                name: "IX_PokemonAvailabilities_PokedexId",
                table: "PokemonAvailabilities",
                newName: "IX_PokemonAvailabilities_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_DamageMultipliers_AgainstId",
                table: "DamageMultiplier",
                newName: "IX_DamageMultiplier_AgainstId");

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Types",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Generation",
                table: "Pokemons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "ByEgg",
                table: "PokemonMoves",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ByLevelUp",
                table: "PokemonMoves",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ByTm",
                table: "PokemonMoves",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ByTutor",
                table: "PokemonMoves",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SignatureMove",
                table: "PokemonMoves",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Pokeballs",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SpecialEffectChance",
                table: "Moves",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Power",
                table: "Moves",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PP",
                table: "Moves",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Moves",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "Effect",
                table: "Moves",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Moves",
                type: "nvarchar(16)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<int>(
                name: "Accuracy",
                table: "Moves",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Generation",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Method",
                table: "Evolutions",
                type: "nvarchar(32)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DamageMultiplier",
                table: "DamageMultiplier",
                columns: new[] { "TypeId", "AgainstId" });

            migrationBuilder.AddForeignKey(
                name: "FK_DamageMultiplier_Types_AgainstId",
                table: "DamageMultiplier",
                column: "AgainstId",
                principalTable: "Types",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DamageMultiplier_Types_TypeId",
                table: "DamageMultiplier",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonAvailabilities_Games_GameId",
                table: "PokemonAvailabilities",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

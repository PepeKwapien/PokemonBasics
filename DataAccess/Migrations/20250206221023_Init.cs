using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Generations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Region = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pokedexes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Region = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokedexes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    Color = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Abilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    Effect = table.Column<string>(type: "text", nullable: false),
                    OverworldEffect = table.Column<string>(type: "text", nullable: true),
                    GenerationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Abilities_Generations_GenerationId",
                        column: x => x.GenerationId,
                        principalTable: "Generations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    PrettyName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    GenerationId = table.Column<Guid>(type: "uuid", nullable: false),
                    MainRegion = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Generations_GenerationId",
                        column: x => x.GenerationId,
                        principalTable: "Generations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DamageMultipliers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    AgainstId = table.Column<Guid>(type: "uuid", nullable: false),
                    Multiplier = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DamageMultipliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DamageMultipliers_Types_AgainstId",
                        column: x => x.AgainstId,
                        principalTable: "Types",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DamageMultipliers_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Moves",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Power = table.Column<int>(type: "integer", nullable: true),
                    Accuracy = table.Column<int>(type: "integer", nullable: true),
                    PP = table.Column<int>(type: "integer", nullable: true),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    TypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Category = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Effect = table.Column<string>(type: "text", nullable: false),
                    SpecialEffectChance = table.Column<int>(type: "integer", nullable: true),
                    Target = table.Column<string>(type: "text", nullable: false),
                    GenerationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Moves_Generations_GenerationId",
                        column: x => x.GenerationId,
                        principalTable: "Generations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Moves_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pokemons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    PrimaryTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    SecondaryTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    HP = table.Column<int>(type: "integer", nullable: false),
                    Attack = table.Column<int>(type: "integer", nullable: false),
                    SpecialAttack = table.Column<int>(type: "integer", nullable: false),
                    Defense = table.Column<int>(type: "integer", nullable: false),
                    SpecialDefense = table.Column<int>(type: "integer", nullable: false),
                    Speed = table.Column<int>(type: "integer", nullable: false),
                    Height = table.Column<double>(type: "double precision", nullable: false),
                    Weight = table.Column<double>(type: "double precision", nullable: false),
                    Habitat = table.Column<string>(type: "text", nullable: true),
                    EggGroups = table.Column<string>(type: "text", nullable: false),
                    Genera = table.Column<string>(type: "text", nullable: false),
                    HasGenderDifferences = table.Column<bool>(type: "boolean", nullable: false),
                    Baby = table.Column<bool>(type: "boolean", nullable: false),
                    Legendary = table.Column<bool>(type: "boolean", nullable: false),
                    Mythical = table.Column<bool>(type: "boolean", nullable: false),
                    Shape = table.Column<string>(type: "text", nullable: true),
                    Sprite = table.Column<string>(type: "text", nullable: true),
                    ShinySprite = table.Column<string>(type: "text", nullable: true),
                    GenerationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pokemons_Generations_GenerationId",
                        column: x => x.GenerationId,
                        principalTable: "Generations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pokemons_Types_PrimaryTypeId",
                        column: x => x.PrimaryTypeId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pokemons_Types_SecondaryTypeId",
                        column: x => x.SecondaryTypeId,
                        principalTable: "Types",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GamePokedex",
                columns: table => new
                {
                    GamesId = table.Column<Guid>(type: "uuid", nullable: false),
                    PokedexesId = table.Column<Guid>(type: "uuid", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "GameVersions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameVersions_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pokeballs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Sprite = table.Column<string>(type: "text", nullable: true),
                    GameId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokeballs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pokeballs_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AlternateForms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OriginalId = table.Column<Guid>(type: "uuid", nullable: false),
                    VariantId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlternateForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlternateForms_Pokemons_OriginalId",
                        column: x => x.OriginalId,
                        principalTable: "Pokemons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AlternateForms_Pokemons_VariantId",
                        column: x => x.VariantId,
                        principalTable: "Pokemons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Evolutions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PokemonId = table.Column<Guid>(type: "uuid", nullable: false),
                    IntoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Trigger = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: true),
                    HeldItem = table.Column<string>(type: "text", nullable: true),
                    Item = table.Column<string>(type: "text", nullable: true),
                    KnownMoveId = table.Column<Guid>(type: "uuid", nullable: true),
                    KnownMoveTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    Location = table.Column<string>(type: "text", nullable: true),
                    MinAffection = table.Column<int>(type: "integer", nullable: true),
                    MinBeauty = table.Column<int>(type: "integer", nullable: true),
                    MinHappiness = table.Column<int>(type: "integer", nullable: true),
                    MinLevel = table.Column<int>(type: "integer", nullable: true),
                    OverworldRain = table.Column<bool>(type: "boolean", nullable: false),
                    PartySpeciesId = table.Column<Guid>(type: "uuid", nullable: true),
                    PartyTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    RelativePhysicalStats = table.Column<string>(type: "text", nullable: true),
                    TimeOfDay = table.Column<string>(type: "text", nullable: true),
                    TradeSpeciesId = table.Column<Guid>(type: "uuid", nullable: true),
                    TurnUpsideDown = table.Column<bool>(type: "boolean", nullable: false),
                    BabyItem = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evolutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evolutions_Moves_KnownMoveId",
                        column: x => x.KnownMoveId,
                        principalTable: "Moves",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Evolutions_Pokemons_IntoId",
                        column: x => x.IntoId,
                        principalTable: "Pokemons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Evolutions_Pokemons_PartySpeciesId",
                        column: x => x.PartySpeciesId,
                        principalTable: "Pokemons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Evolutions_Pokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Evolutions_Pokemons_TradeSpeciesId",
                        column: x => x.TradeSpeciesId,
                        principalTable: "Pokemons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Evolutions_Types_KnownMoveTypeId",
                        column: x => x.KnownMoveTypeId,
                        principalTable: "Types",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Evolutions_Types_PartyTypeId",
                        column: x => x.PartyTypeId,
                        principalTable: "Types",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PokemonAbilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PokemonId = table.Column<Guid>(type: "uuid", nullable: false),
                    AbilityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Hidden = table.Column<bool>(type: "boolean", nullable: false),
                    Slot = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonAbilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonAbilities_Abilities_AbilityId",
                        column: x => x.AbilityId,
                        principalTable: "Abilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonAbilities_Pokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PokemonId = table.Column<Guid>(type: "uuid", nullable: false),
                    PokedexId = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonEntries", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "PokemonMoves",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PokemonId = table.Column<Guid>(type: "uuid", nullable: false),
                    MoveId = table.Column<Guid>(type: "uuid", nullable: false),
                    Method = table.Column<string>(type: "text", nullable: true),
                    MinimalLevel = table.Column<int>(type: "integer", nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonMoves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonMoves_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonMoves_Moves_MoveId",
                        column: x => x.MoveId,
                        principalTable: "Moves",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PokemonMoves_Pokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GenerationPokeball",
                columns: table => new
                {
                    GenerationsId = table.Column<Guid>(type: "uuid", nullable: false),
                    PokeballsId = table.Column<Guid>(type: "uuid", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Abilities_GenerationId",
                table: "Abilities",
                column: "GenerationId");

            migrationBuilder.CreateIndex(
                name: "IX_AlternateForms_OriginalId",
                table: "AlternateForms",
                column: "OriginalId");

            migrationBuilder.CreateIndex(
                name: "IX_AlternateForms_VariantId",
                table: "AlternateForms",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_DamageMultipliers_AgainstId",
                table: "DamageMultipliers",
                column: "AgainstId");

            migrationBuilder.CreateIndex(
                name: "IX_DamageMultipliers_TypeId",
                table: "DamageMultipliers",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Evolutions_IntoId",
                table: "Evolutions",
                column: "IntoId");

            migrationBuilder.CreateIndex(
                name: "IX_Evolutions_KnownMoveId",
                table: "Evolutions",
                column: "KnownMoveId");

            migrationBuilder.CreateIndex(
                name: "IX_Evolutions_KnownMoveTypeId",
                table: "Evolutions",
                column: "KnownMoveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Evolutions_PartySpeciesId",
                table: "Evolutions",
                column: "PartySpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_Evolutions_PartyTypeId",
                table: "Evolutions",
                column: "PartyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Evolutions_PokemonId",
                table: "Evolutions",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_Evolutions_TradeSpeciesId",
                table: "Evolutions",
                column: "TradeSpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_GamePokedex_PokedexesId",
                table: "GamePokedex",
                column: "PokedexesId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_GenerationId",
                table: "Games",
                column: "GenerationId");

            migrationBuilder.CreateIndex(
                name: "IX_GameVersions_GameId",
                table: "GameVersions",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GenerationPokeball_PokeballsId",
                table: "GenerationPokeball",
                column: "PokeballsId");

            migrationBuilder.CreateIndex(
                name: "IX_Moves_GenerationId",
                table: "Moves",
                column: "GenerationId");

            migrationBuilder.CreateIndex(
                name: "IX_Moves_TypeId",
                table: "Moves",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokeballs_GameId",
                table: "Pokeballs",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonAbilities_AbilityId",
                table: "PokemonAbilities",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonAbilities_PokemonId",
                table: "PokemonAbilities",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonEntries_PokedexId",
                table: "PokemonEntries",
                column: "PokedexId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonEntries_PokemonId",
                table: "PokemonEntries",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonMoves_GameId",
                table: "PokemonMoves",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonMoves_MoveId",
                table: "PokemonMoves",
                column: "MoveId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonMoves_PokemonId",
                table: "PokemonMoves",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_GenerationId",
                table: "Pokemons",
                column: "GenerationId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_PrimaryTypeId",
                table: "Pokemons",
                column: "PrimaryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_SecondaryTypeId",
                table: "Pokemons",
                column: "SecondaryTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlternateForms");

            migrationBuilder.DropTable(
                name: "DamageMultipliers");

            migrationBuilder.DropTable(
                name: "Evolutions");

            migrationBuilder.DropTable(
                name: "GamePokedex");

            migrationBuilder.DropTable(
                name: "GameVersions");

            migrationBuilder.DropTable(
                name: "GenerationPokeball");

            migrationBuilder.DropTable(
                name: "PokemonAbilities");

            migrationBuilder.DropTable(
                name: "PokemonEntries");

            migrationBuilder.DropTable(
                name: "PokemonMoves");

            migrationBuilder.DropTable(
                name: "Pokeballs");

            migrationBuilder.DropTable(
                name: "Abilities");

            migrationBuilder.DropTable(
                name: "Pokedexes");

            migrationBuilder.DropTable(
                name: "Moves");

            migrationBuilder.DropTable(
                name: "Pokemons");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropTable(
                name: "Generations");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class pokeballfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamePokeball");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Pokeballs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "GameId",
                table: "Pokeballs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pokeballs_GameId",
                table: "Pokeballs",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pokeballs_Games_GameId",
                table: "Pokeballs",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pokeballs_Games_GameId",
                table: "Pokeballs");

            migrationBuilder.DropIndex(
                name: "IX_Pokeballs_GameId",
                table: "Pokeballs");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Pokeballs");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Pokeballs");

            migrationBuilder.CreateTable(
                name: "GamePokeball",
                columns: table => new
                {
                    GamesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PokeballsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePokeball", x => new { x.GamesId, x.PokeballsId });
                    table.ForeignKey(
                        name: "FK_GamePokeball_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePokeball_Pokeballs_PokeballsId",
                        column: x => x.PokeballsId,
                        principalTable: "Pokeballs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GamePokeball_PokeballsId",
                table: "GamePokeball",
                column: "PokeballsId");
        }
    }
}

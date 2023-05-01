using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class evolutionfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlternateForms");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Evolutions");

            migrationBuilder.DropColumn(
                name: "Method",
                table: "Evolutions");

            migrationBuilder.AddColumn<string>(
                name: "BabyItem",
                table: "Evolutions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Evolutions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeldItem",
                table: "Evolutions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Item",
                table: "Evolutions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "KnownMoveId",
                table: "Evolutions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "KnownMoveTypeId",
                table: "Evolutions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Evolutions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinAffection",
                table: "Evolutions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinBeauty",
                table: "Evolutions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinHappiness",
                table: "Evolutions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinLevel",
                table: "Evolutions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OverworldRain",
                table: "Evolutions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "PartySpeciesId",
                table: "Evolutions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PartyTypeId",
                table: "Evolutions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RelativePhysicalStats",
                table: "Evolutions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TimeOfDay",
                table: "Evolutions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TradeSpeciesId",
                table: "Evolutions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Trigger",
                table: "Evolutions",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "TurnUpsideDown",
                table: "Evolutions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "RegionalVariants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OriginalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VariantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionalVariants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegionalVariants_Pokemons_OriginalId",
                        column: x => x.OriginalId,
                        principalTable: "Pokemons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RegionalVariants_Pokemons_VariantId",
                        column: x => x.VariantId,
                        principalTable: "Pokemons",
                        principalColumn: "Id");
                });

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
                name: "IX_Evolutions_TradeSpeciesId",
                table: "Evolutions",
                column: "TradeSpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_RegionalVariants_OriginalId",
                table: "RegionalVariants",
                column: "OriginalId");

            migrationBuilder.CreateIndex(
                name: "IX_RegionalVariants_VariantId",
                table: "RegionalVariants",
                column: "VariantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Evolutions_Moves_KnownMoveId",
                table: "Evolutions",
                column: "KnownMoveId",
                principalTable: "Moves",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Evolutions_Pokemons_PartySpeciesId",
                table: "Evolutions",
                column: "PartySpeciesId",
                principalTable: "Pokemons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Evolutions_Pokemons_TradeSpeciesId",
                table: "Evolutions",
                column: "TradeSpeciesId",
                principalTable: "Pokemons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Evolutions_Types_KnownMoveTypeId",
                table: "Evolutions",
                column: "KnownMoveTypeId",
                principalTable: "Types",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Evolutions_Types_PartyTypeId",
                table: "Evolutions",
                column: "PartyTypeId",
                principalTable: "Types",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evolutions_Moves_KnownMoveId",
                table: "Evolutions");

            migrationBuilder.DropForeignKey(
                name: "FK_Evolutions_Pokemons_PartySpeciesId",
                table: "Evolutions");

            migrationBuilder.DropForeignKey(
                name: "FK_Evolutions_Pokemons_TradeSpeciesId",
                table: "Evolutions");

            migrationBuilder.DropForeignKey(
                name: "FK_Evolutions_Types_KnownMoveTypeId",
                table: "Evolutions");

            migrationBuilder.DropForeignKey(
                name: "FK_Evolutions_Types_PartyTypeId",
                table: "Evolutions");

            migrationBuilder.DropTable(
                name: "RegionalVariants");

            migrationBuilder.DropIndex(
                name: "IX_Evolutions_KnownMoveId",
                table: "Evolutions");

            migrationBuilder.DropIndex(
                name: "IX_Evolutions_KnownMoveTypeId",
                table: "Evolutions");

            migrationBuilder.DropIndex(
                name: "IX_Evolutions_PartySpeciesId",
                table: "Evolutions");

            migrationBuilder.DropIndex(
                name: "IX_Evolutions_PartyTypeId",
                table: "Evolutions");

            migrationBuilder.DropIndex(
                name: "IX_Evolutions_TradeSpeciesId",
                table: "Evolutions");

            migrationBuilder.DropColumn(
                name: "BabyItem",
                table: "Evolutions");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Evolutions");

            migrationBuilder.DropColumn(
                name: "HeldItem",
                table: "Evolutions");

            migrationBuilder.DropColumn(
                name: "Item",
                table: "Evolutions");

            migrationBuilder.DropColumn(
                name: "KnownMoveId",
                table: "Evolutions");

            migrationBuilder.DropColumn(
                name: "KnownMoveTypeId",
                table: "Evolutions");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Evolutions");

            migrationBuilder.DropColumn(
                name: "MinAffection",
                table: "Evolutions");

            migrationBuilder.DropColumn(
                name: "MinBeauty",
                table: "Evolutions");

            migrationBuilder.DropColumn(
                name: "MinHappiness",
                table: "Evolutions");

            migrationBuilder.DropColumn(
                name: "MinLevel",
                table: "Evolutions");

            migrationBuilder.DropColumn(
                name: "OverworldRain",
                table: "Evolutions");

            migrationBuilder.DropColumn(
                name: "PartySpeciesId",
                table: "Evolutions");

            migrationBuilder.DropColumn(
                name: "PartyTypeId",
                table: "Evolutions");

            migrationBuilder.DropColumn(
                name: "RelativePhysicalStats",
                table: "Evolutions");

            migrationBuilder.DropColumn(
                name: "TimeOfDay",
                table: "Evolutions");

            migrationBuilder.DropColumn(
                name: "TradeSpeciesId",
                table: "Evolutions");

            migrationBuilder.DropColumn(
                name: "Trigger",
                table: "Evolutions");

            migrationBuilder.DropColumn(
                name: "TurnUpsideDown",
                table: "Evolutions");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Evolutions",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Method",
                table: "Evolutions",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AlternateForms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlternateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OriginalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlternateForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlternateForms_Pokemons_AlternateId",
                        column: x => x.AlternateId,
                        principalTable: "Pokemons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AlternateForms_Pokemons_OriginalId",
                        column: x => x.OriginalId,
                        principalTable: "Pokemons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlternateForms_AlternateId",
                table: "AlternateForms",
                column: "AlternateId");

            migrationBuilder.CreateIndex(
                name: "IX_AlternateForms_OriginalId",
                table: "AlternateForms",
                column: "OriginalId");
        }
    }
}

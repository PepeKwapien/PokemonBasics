using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class backToAlternateForms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegionalVariants");

            migrationBuilder.CreateTable(
                name: "AlternateForms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OriginalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VariantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_AlternateForms_OriginalId",
                table: "AlternateForms",
                column: "OriginalId");

            migrationBuilder.CreateIndex(
                name: "IX_AlternateForms_VariantId",
                table: "AlternateForms",
                column: "VariantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlternateForms");

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
                name: "IX_RegionalVariants_OriginalId",
                table: "RegionalVariants",
                column: "OriginalId");

            migrationBuilder.CreateIndex(
                name: "IX_RegionalVariants_VariantId",
                table: "RegionalVariants",
                column: "VariantId");
        }
    }
}

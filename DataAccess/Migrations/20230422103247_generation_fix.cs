using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class generationfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Regions",
                table: "Generations",
                newName: "Region");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Generations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Generations");

            migrationBuilder.RenameColumn(
                name: "Region",
                table: "Generations",
                newName: "Regions");
        }
    }
}

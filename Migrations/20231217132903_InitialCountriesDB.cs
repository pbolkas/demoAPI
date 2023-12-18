using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCountriesDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CommonName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Capital = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CommonName);
                });

            migrationBuilder.CreateTable(
                name: "Border",
                columns: table => new
                {
                    BorderName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CountryModelCommonName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Border", x => x.BorderName);
                    table.ForeignKey(
                        name: "FK_Border_Countries_CountryModelCommonName",
                        column: x => x.CountryModelCommonName,
                        principalTable: "Countries",
                        principalColumn: "CommonName");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Border_CountryModelCommonName",
                table: "Border",
                column: "CountryModelCommonName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Border");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}

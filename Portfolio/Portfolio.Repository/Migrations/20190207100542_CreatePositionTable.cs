using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Portfolios.Repository.Migrations
{
    public partial class CreatePositionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Portfolios",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Portfolios",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ISIN",
                table: "Portfolios",
                maxLength: 12,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "MarketValue",
                table: "Portfolios",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ISIN = table.Column<string>(maxLength: 12, nullable: false),
                    Currency = table.Column<string>(maxLength: 3, nullable: false),
                    MarketValue = table.Column<decimal>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: false),
                    Country = table.Column<string>(maxLength: 2, nullable: false),
                    SharePercentage = table.Column<float>(nullable: false),
                    PortfolioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Positions_Portfolios_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "Portfolios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Positions_PortfolioId",
                table: "Positions",
                column: "PortfolioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "ISIN",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "MarketValue",
                table: "Portfolios");
        }
    }
}

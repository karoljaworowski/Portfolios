using Microsoft.EntityFrameworkCore.Migrations;

namespace Portfolios.Repository.Migrations
{
    public partial class ChangeSharePercentageType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "SharePercentage",
                table: "Positions",
                nullable: false,
                oldClrType: typeof(float));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "SharePercentage",
                table: "Positions",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}

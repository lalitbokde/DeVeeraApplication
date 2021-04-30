using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class DashboardQuoteLevelID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "DashboardQuote",
                newName: "IsRandom");

            migrationBuilder.AddColumn<bool>(
                name: "IsDashboardQuote",
                table: "DashboardQuote",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LevelId",
                table: "DashboardQuote",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDashboardQuote",
                table: "DashboardQuote");

            migrationBuilder.DropColumn(
                name: "LevelId",
                table: "DashboardQuote");

            migrationBuilder.RenameColumn(
                name: "IsRandom",
                table: "DashboardQuote",
                newName: "IsActive");
        }
    }
}

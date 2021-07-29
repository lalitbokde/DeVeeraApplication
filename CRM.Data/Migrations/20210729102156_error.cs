using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class error : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Quote",
                table: "WeeklyUpdates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoHeader",
                table: "WeeklyUpdates",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quote",
                table: "WeeklyUpdates");

            migrationBuilder.DropColumn(
                name: "VideoHeader",
                table: "WeeklyUpdates");
        }
    }
}

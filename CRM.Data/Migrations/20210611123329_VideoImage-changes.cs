using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class VideoImagechanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BannerImageId",
                table: "WeeklyUpdates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BodyImageId",
                table: "WeeklyUpdates",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BannerImageId",
                table: "WeeklyUpdates");

            migrationBuilder.DropColumn(
                name: "BodyImageId",
                table: "WeeklyUpdates");
        }
    }
}

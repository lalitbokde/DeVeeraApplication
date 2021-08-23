using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class bannerinhome : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BannerImageId",
                table: "LayoutSetup",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShareBackgroundImageId",
                table: "LayoutSetup",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VideoThumbImageId",
                table: "LayoutSetup",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BannerImageId",
                table: "LayoutSetup");

            migrationBuilder.DropColumn(
                name: "ShareBackgroundImageId",
                table: "LayoutSetup");

            migrationBuilder.DropColumn(
                name: "VideoThumbImageId",
                table: "LayoutSetup");
        }
    }
}

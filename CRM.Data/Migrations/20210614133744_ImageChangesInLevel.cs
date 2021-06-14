using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class ImageChangesInLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BannerImageId",
                table: "Levels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShareBackgroundImageId",
                table: "Levels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VideoThumbImageId",
                table: "Levels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BannerImageId",
                table: "Levelmodules",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShareBackgroundImageId",
                table: "Levelmodules",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VideoThumbImageId",
                table: "Levelmodules",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BannerImageId",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "ShareBackgroundImageId",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "VideoThumbImageId",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "BannerImageId",
                table: "Levelmodules");

            migrationBuilder.DropColumn(
                name: "ShareBackgroundImageId",
                table: "Levelmodules");

            migrationBuilder.DropColumn(
                name: "VideoThumbImageId",
                table: "Levelmodules");
        }
    }
}

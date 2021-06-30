using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class QuickLinks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Link_1",
                table: "LayoutSetup",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Link_1_BannerImageId",
                table: "LayoutSetup",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Link_2",
                table: "LayoutSetup",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Link_2_BannerImageId",
                table: "LayoutSetup",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Link_3",
                table: "LayoutSetup",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Link_3_BannerImageId",
                table: "LayoutSetup",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link_1",
                table: "LayoutSetup");

            migrationBuilder.DropColumn(
                name: "Link_1_BannerImageId",
                table: "LayoutSetup");

            migrationBuilder.DropColumn(
                name: "Link_2",
                table: "LayoutSetup");

            migrationBuilder.DropColumn(
                name: "Link_2_BannerImageId",
                table: "LayoutSetup");

            migrationBuilder.DropColumn(
                name: "Link_3",
                table: "LayoutSetup");

            migrationBuilder.DropColumn(
                name: "Link_3_BannerImageId",
                table: "LayoutSetup");
        }
    }
}

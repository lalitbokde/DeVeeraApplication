using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class video : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HomeSubTitle",
                table: "LayoutSetup",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VideoId",
                table: "LayoutSetup",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HomeSubTitle",
                table: "LayoutSetup");

            migrationBuilder.DropColumn(
                name: "VideoId",
                table: "LayoutSetup");
        }
    }
}

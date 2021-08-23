using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class homefields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HomeDescription",
                table: "LayoutSetup",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeTitle",
                table: "LayoutSetup",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HomeDescription",
                table: "LayoutSetup");

            migrationBuilder.DropColumn(
                name: "HomeTitle",
                table: "LayoutSetup");
        }
    }
}

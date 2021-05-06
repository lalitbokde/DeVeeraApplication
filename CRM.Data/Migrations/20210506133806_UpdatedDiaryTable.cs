using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class UpdatedDiaryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Diaries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Diaries",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Diaries");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Diaries");
        }
    }
}

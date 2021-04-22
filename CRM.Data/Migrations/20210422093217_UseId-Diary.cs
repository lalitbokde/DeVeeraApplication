using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class UseIdDiary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Diaries",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Diaries");
        }
    }
}

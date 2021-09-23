using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class spanishvideo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SpanishKey",
                table: "videos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpanishVideoUrl",
                table: "videos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpanishKey",
                table: "videos");

            migrationBuilder.DropColumn(
                name: "SpanishVideoUrl",
                table: "videos");
        }
    }
}

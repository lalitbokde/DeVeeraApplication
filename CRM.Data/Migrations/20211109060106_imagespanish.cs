using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class imagespanish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SpanishImageUrl",
                table: "Images",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpanishKey",
                table: "Images",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpanishImageUrl",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "SpanishKey",
                table: "Images");
        }
    }
}

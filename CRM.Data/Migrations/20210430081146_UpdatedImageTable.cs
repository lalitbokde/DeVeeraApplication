using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class UpdatedImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Levels_LevelId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_LevelId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "LevelId",
                table: "Images");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LevelId",
                table: "Images",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_LevelId",
                table: "Images",
                column: "LevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Levels_LevelId",
                table: "Images",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

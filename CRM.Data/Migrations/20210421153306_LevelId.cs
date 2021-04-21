using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class LevelId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Levelmodules_Levels_VideoId",
                table: "Levelmodules");

            migrationBuilder.RenameColumn(
                name: "VideoId",
                table: "Levelmodules",
                newName: "LevelId");

            migrationBuilder.RenameIndex(
                name: "IX_Levelmodules_VideoId",
                table: "Levelmodules",
                newName: "IX_Levelmodules_LevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Levelmodules_Levels_LevelId",
                table: "Levelmodules",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Levelmodules_Levels_LevelId",
                table: "Levelmodules");

            migrationBuilder.RenameColumn(
                name: "LevelId",
                table: "Levelmodules",
                newName: "VideoId");

            migrationBuilder.RenameIndex(
                name: "IX_Levelmodules_LevelId",
                table: "Levelmodules",
                newName: "IX_Levelmodules_VideoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Levelmodules_Levels_VideoId",
                table: "Levelmodules",
                column: "VideoId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

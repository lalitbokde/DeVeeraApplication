using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class LevelEmotionId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Emotions",
                table: "Levels",
                newName: "EmotionId");

            migrationBuilder.CreateIndex(
                name: "IX_Levels_EmotionId",
                table: "Levels",
                column: "EmotionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Levels_Emotions_EmotionId",
                table: "Levels",
                column: "EmotionId",
                principalTable: "Emotions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Levels_Emotions_EmotionId",
                table: "Levels");

            migrationBuilder.DropIndex(
                name: "IX_Levels_EmotionId",
                table: "Levels");

            migrationBuilder.RenameColumn(
                name: "EmotionId",
                table: "Levels",
                newName: "Emotions");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class CurrentEmotion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Remember",
                table: "Emotion_Mapping",
                newName: "CurrentEmotion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrentEmotion",
                table: "Emotion_Mapping",
                newName: "Remember");
        }
    }
}

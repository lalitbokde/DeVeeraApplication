using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class updatedLevelTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Levels_videos_VideoId",
                table: "Levels");

            migrationBuilder.AlterColumn<int>(
                name: "VideoId",
                table: "Levels",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Levels_videos_VideoId",
                table: "Levels",
                column: "VideoId",
                principalTable: "videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Levels_videos_VideoId",
                table: "Levels");

            migrationBuilder.AlterColumn<int>(
                name: "VideoId",
                table: "Levels",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Levels_videos_VideoId",
                table: "Levels",
                column: "VideoId",
                principalTable: "videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

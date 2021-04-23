using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class VideoIdChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoName",
                table: "WeeklyUpdates");

            migrationBuilder.DropColumn(
                name: "VideoURL",
                table: "WeeklyUpdates");

            migrationBuilder.DropColumn(
                name: "VideoName",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "VideoURL",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "VideoURL",
                table: "Levelmodules");

            migrationBuilder.AddColumn<int>(
                name: "VideoId",
                table: "WeeklyUpdates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VideoId",
                table: "Levels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VideoId",
                table: "Levelmodules",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WeeklyUpdates_VideoId",
                table: "WeeklyUpdates",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_Levels_VideoId",
                table: "Levels",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_Levelmodules_VideoId",
                table: "Levelmodules",
                column: "VideoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Levelmodules_videos_VideoId",
                table: "Levelmodules",
                column: "VideoId",
                principalTable: "videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Levels_videos_VideoId",
                table: "Levels",
                column: "VideoId",
                principalTable: "videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_WeeklyUpdates_videos_VideoId",
                table: "WeeklyUpdates",
                column: "VideoId",
                principalTable: "videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Levelmodules_videos_VideoId",
                table: "Levelmodules");

            migrationBuilder.DropForeignKey(
                name: "FK_Levels_videos_VideoId",
                table: "Levels");

            migrationBuilder.DropForeignKey(
                name: "FK_WeeklyUpdates_videos_VideoId",
                table: "WeeklyUpdates");

            migrationBuilder.DropIndex(
                name: "IX_WeeklyUpdates_VideoId",
                table: "WeeklyUpdates");

            migrationBuilder.DropIndex(
                name: "IX_Levels_VideoId",
                table: "Levels");

            migrationBuilder.DropIndex(
                name: "IX_Levelmodules_VideoId",
                table: "Levelmodules");

            migrationBuilder.DropColumn(
                name: "VideoId",
                table: "WeeklyUpdates");

            migrationBuilder.DropColumn(
                name: "VideoId",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "VideoId",
                table: "Levelmodules");

            migrationBuilder.AddColumn<string>(
                name: "VideoName",
                table: "WeeklyUpdates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoURL",
                table: "WeeklyUpdates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoName",
                table: "Levels",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoURL",
                table: "Levels",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoURL",
                table: "Levelmodules",
                nullable: true);
        }
    }
}

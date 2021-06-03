using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class EmotionUpdateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BannerImageId",
                table: "Emotions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ContentImageId",
                table: "Emotions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Emotions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Quote",
                table: "Emotions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subtitle",
                table: "Emotions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Emotions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VideoId",
                table: "Emotions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Emotions_VideoId",
                table: "Emotions",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_Diaries_UserId",
                table: "Diaries",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Diaries_User_UserId",
                table: "Diaries",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Emotions_videos_VideoId",
                table: "Emotions",
                column: "VideoId",
                principalTable: "videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diaries_User_UserId",
                table: "Diaries");

            migrationBuilder.DropForeignKey(
                name: "FK_Emotions_videos_VideoId",
                table: "Emotions");

            migrationBuilder.DropIndex(
                name: "IX_Emotions_VideoId",
                table: "Emotions");

            migrationBuilder.DropIndex(
                name: "IX_Diaries_UserId",
                table: "Diaries");

            migrationBuilder.DropColumn(
                name: "BannerImageId",
                table: "Emotions");

            migrationBuilder.DropColumn(
                name: "ContentImageId",
                table: "Emotions");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Emotions");

            migrationBuilder.DropColumn(
                name: "Quote",
                table: "Emotions");

            migrationBuilder.DropColumn(
                name: "Subtitle",
                table: "Emotions");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Emotions");

            migrationBuilder.DropColumn(
                name: "VideoId",
                table: "Emotions");
        }
    }
}

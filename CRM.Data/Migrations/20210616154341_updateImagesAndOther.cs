using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class updateImagesAndOther : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ThumbnailImageId",
                table: "Emotions",
                newName: "EmotionThumbnailImageId");

            migrationBuilder.RenameColumn(
                name: "ContentImageId",
                table: "Emotions",
                newName: "EmotionHeaderImageId");

            migrationBuilder.RenameColumn(
                name: "BannerImageId",
                table: "Emotions",
                newName: "EmotionBannerImageId");

            migrationBuilder.AddColumn<int>(
                name: "BannerImageId",
                table: "Levels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShareBackgroundImageId",
                table: "Levels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VideoThumbImageId",
                table: "Levels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BannerImageId",
                table: "Levelmodules",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShareBackgroundImageId",
                table: "Levelmodules",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VideoThumbImageId",
                table: "Levelmodules",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompleteRegistrationHeaderImgId",
                table: "LayoutSetup",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ReasonToSubmit",
                table: "LayoutSetup",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BannerImageId",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "ShareBackgroundImageId",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "VideoThumbImageId",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "BannerImageId",
                table: "Levelmodules");

            migrationBuilder.DropColumn(
                name: "ShareBackgroundImageId",
                table: "Levelmodules");

            migrationBuilder.DropColumn(
                name: "VideoThumbImageId",
                table: "Levelmodules");

            migrationBuilder.DropColumn(
                name: "CompleteRegistrationHeaderImgId",
                table: "LayoutSetup");

            migrationBuilder.DropColumn(
                name: "ReasonToSubmit",
                table: "LayoutSetup");

            migrationBuilder.RenameColumn(
                name: "EmotionThumbnailImageId",
                table: "Emotions",
                newName: "ThumbnailImageId");

            migrationBuilder.RenameColumn(
                name: "EmotionHeaderImageId",
                table: "Emotions",
                newName: "ContentImageId");

            migrationBuilder.RenameColumn(
                name: "EmotionBannerImageId",
                table: "Emotions",
                newName: "BannerImageId");
        }
    }
}

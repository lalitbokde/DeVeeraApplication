using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class UpdatedStoryTableAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeelGoodStories_Images_ImageId",
                table: "FeelGoodStories");

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "FeelGoodStories",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_FeelGoodStories_Images_ImageId",
                table: "FeelGoodStories",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeelGoodStories_Images_ImageId",
                table: "FeelGoodStories");

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "FeelGoodStories",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FeelGoodStories_Images_ImageId",
                table: "FeelGoodStories",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

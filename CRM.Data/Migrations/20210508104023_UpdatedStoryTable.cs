using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class UpdatedStoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "FeelGoodStories",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FeelGoodStories_ImageId",
                table: "FeelGoodStories",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeelGoodStories_Images_ImageId",
                table: "FeelGoodStories",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeelGoodStories_Images_ImageId",
                table: "FeelGoodStories");

            migrationBuilder.DropIndex(
                name: "IX_FeelGoodStories_ImageId",
                table: "FeelGoodStories");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "FeelGoodStories");
        }
    }
}

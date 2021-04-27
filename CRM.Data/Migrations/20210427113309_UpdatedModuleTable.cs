using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class UpdatedModuleTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Levelmodules_videos_VideoId",
                table: "Levelmodules");

            migrationBuilder.AlterColumn<int>(
                name: "VideoId",
                table: "Levelmodules",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Levelmodules_videos_VideoId",
                table: "Levelmodules",
                column: "VideoId",
                principalTable: "videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Levelmodules_videos_VideoId",
                table: "Levelmodules");

            migrationBuilder.AlterColumn<int>(
                name: "VideoId",
                table: "Levelmodules",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Levelmodules_videos_VideoId",
                table: "Levelmodules",
                column: "VideoId",
                principalTable: "videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

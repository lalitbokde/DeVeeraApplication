using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class UpdatedSettingTableAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Setting",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Setting",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Setting_UserId",
                table: "Setting",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Setting_User_UserId",
                table: "Setting",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Setting_User_UserId",
                table: "Setting");

            migrationBuilder.DropIndex(
                name: "IX_Setting_UserId",
                table: "Setting");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Setting");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Setting");
        }
    }
}

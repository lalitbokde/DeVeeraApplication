using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class diaryTableUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diaries_Levels_LevelId",
                table: "Diaries");

            migrationBuilder.DropIndex(
                name: "IX_Diaries_LevelId",
                table: "Diaries");

            migrationBuilder.AlterColumn<int>(
                name: "LevelId",
                table: "Diaries",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ModuleId",
                table: "Diaries",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModuleId",
                table: "Diaries");

            migrationBuilder.AlterColumn<int>(
                name: "LevelId",
                table: "Diaries",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Diaries_LevelId",
                table: "Diaries",
                column: "LevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Diaries_Levels_LevelId",
                table: "Diaries",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

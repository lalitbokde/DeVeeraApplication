using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class UpdatedLanguageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "LanguageName",
                table: "Languages",
                newName: "UniqueSeoCode");

            migrationBuilder.RenameColumn(
                name: "Abbreviations",
                table: "Languages",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "Languages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FlagImageFileName",
                table: "Languages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LanguageCulture",
                table: "Languages",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "Languages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Rtl",
                table: "Languages",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "FlagImageFileName",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "LanguageCulture",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "Published",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "Rtl",
                table: "Languages");

            migrationBuilder.RenameColumn(
                name: "UniqueSeoCode",
                table: "Languages",
                newName: "LanguageName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Languages",
                newName: "Abbreviations");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Setting",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Setting",
                nullable: true);

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
                onDelete: ReferentialAction.Restrict);
        }
    }
}

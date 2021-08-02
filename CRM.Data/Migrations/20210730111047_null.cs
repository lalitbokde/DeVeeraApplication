using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class @null : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocaleStringResources_Languages_LanguageId",
                table: "LocaleStringResources");

            migrationBuilder.AlterColumn<int>(
                name: "LanguageId",
                table: "LocaleStringResources",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_LocaleStringResources_Languages_LanguageId",
                table: "LocaleStringResources",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocaleStringResources_Languages_LanguageId",
                table: "LocaleStringResources");

            migrationBuilder.AlterColumn<int>(
                name: "LanguageId",
                table: "LocaleStringResources",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LocaleStringResources_Languages_LanguageId",
                table: "LocaleStringResources",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class DiaryPasscodeDiaryLoginDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiaryLoginDate",
                table: "Diaries");

            migrationBuilder.AddColumn<DateTime>(
                name: "DiaryLoginDate",
                table: "DiaryPasscode",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiaryLoginDate",
                table: "DiaryPasscode");

            migrationBuilder.AddColumn<DateTime>(
                name: "DiaryLoginDate",
                table: "Diaries",
                nullable: true);
        }
    }
}

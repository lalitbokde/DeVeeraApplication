using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class likefield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Levels",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DisLikeId",
                table: "Levels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDisLike",
                table: "Levels",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLike",
                table: "Levels",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LikeId",
                table: "Levels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Levelmodules",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DisLikeId",
                table: "Levelmodules",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDisLike",
                table: "Levelmodules",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLike",
                table: "Levelmodules",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LikeId",
                table: "Levelmodules",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "DisLikeId",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "IsDisLike",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "IsLike",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "LikeId",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Levelmodules");

            migrationBuilder.DropColumn(
                name: "DisLikeId",
                table: "Levelmodules");

            migrationBuilder.DropColumn(
                name: "IsDisLike",
                table: "Levelmodules");

            migrationBuilder.DropColumn(
                name: "IsLike",
                table: "Levelmodules");

            migrationBuilder.DropColumn(
                name: "LikeId",
                table: "Levelmodules");
        }
    }
}

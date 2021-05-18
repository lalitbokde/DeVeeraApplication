using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class AddDeletedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "WeeklyUpdates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "videos",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "UserRole",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "UserPassword",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "StateProvince",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "PermissionRecord_Role_Mapping",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "PermissionRecord",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Levels",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Levelmodules",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "LevelImageLists",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Languages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Images",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "FeelGoodStories",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "DashboardQuote",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "DashboardMenus",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Country",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Address",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "WeeklyUpdates");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "videos");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "UserPassword");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "StateProvince");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "PermissionRecord_Role_Mapping");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "PermissionRecord");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Levelmodules");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "LevelImageLists");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "FeelGoodStories");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "DashboardQuote");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "DashboardMenus");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Address");
        }
    }
}

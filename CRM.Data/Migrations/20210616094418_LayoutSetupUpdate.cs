using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class LayoutSetupUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompleteRegistrationHeaderImgId",
                table: "LayoutSetup",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ReasonToSubmit",
                table: "LayoutSetup",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompleteRegistrationHeaderImgId",
                table: "LayoutSetup");

            migrationBuilder.DropColumn(
                name: "ReasonToSubmit",
                table: "LayoutSetup");
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class SliderChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DescriptionImageId",
                table: "WeeklyUpdates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LandingQuote",
                table: "WeeklyUpdates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SliderOneDescription",
                table: "WeeklyUpdates",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SliderOneImageId",
                table: "WeeklyUpdates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SliderOneTitle",
                table: "WeeklyUpdates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SliderThreeDescription",
                table: "WeeklyUpdates",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SliderThreeImageId",
                table: "WeeklyUpdates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SliderThreeTitle",
                table: "WeeklyUpdates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SliderTwoDescription",
                table: "WeeklyUpdates",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SliderTwoImageId",
                table: "WeeklyUpdates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SliderTwoTitle",
                table: "WeeklyUpdates",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ModuleImageLists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModuleId = table.Column<int>(nullable: false),
                    ImageId = table.Column<int>(nullable: false),
                    ModulesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleImageLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModuleImageLists_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModuleImageLists_Levelmodules_ModulesId",
                        column: x => x.ModulesId,
                        principalTable: "Levelmodules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModuleImageLists_ImageId",
                table: "ModuleImageLists",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleImageLists_ModulesId",
                table: "ModuleImageLists",
                column: "ModulesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModuleImageLists");

            migrationBuilder.DropColumn(
                name: "DescriptionImageId",
                table: "WeeklyUpdates");

            migrationBuilder.DropColumn(
                name: "LandingQuote",
                table: "WeeklyUpdates");

            migrationBuilder.DropColumn(
                name: "SliderOneDescription",
                table: "WeeklyUpdates");

            migrationBuilder.DropColumn(
                name: "SliderOneImageId",
                table: "WeeklyUpdates");

            migrationBuilder.DropColumn(
                name: "SliderOneTitle",
                table: "WeeklyUpdates");

            migrationBuilder.DropColumn(
                name: "SliderThreeDescription",
                table: "WeeklyUpdates");

            migrationBuilder.DropColumn(
                name: "SliderThreeImageId",
                table: "WeeklyUpdates");

            migrationBuilder.DropColumn(
                name: "SliderThreeTitle",
                table: "WeeklyUpdates");

            migrationBuilder.DropColumn(
                name: "SliderTwoDescription",
                table: "WeeklyUpdates");

            migrationBuilder.DropColumn(
                name: "SliderTwoImageId",
                table: "WeeklyUpdates");

            migrationBuilder.DropColumn(
                name: "SliderTwoTitle",
                table: "WeeklyUpdates");
        }
    }
}

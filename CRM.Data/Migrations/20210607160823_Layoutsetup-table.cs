using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class Layoutsetuptable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ThumbnailImageId",
                table: "Emotions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LayoutSetup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SliderOneImageId = table.Column<int>(nullable: false),
                    SliderTwoImageId = table.Column<int>(nullable: false),
                    SliderThreeImageId = table.Column<int>(nullable: false),
                    SliderOneTitle = table.Column<string>(nullable: true),
                    SliderOneDescription = table.Column<string>(nullable: true),
                    SliderTwoTitle = table.Column<string>(nullable: true),
                    SliderTwoDescription = table.Column<string>(nullable: true),
                    SliderThreeTitle = table.Column<string>(nullable: true),
                    SliderThreeDescription = table.Column<string>(nullable: true),
                    BannerOneImageId = table.Column<int>(nullable: false),
                    BannerTwoImageId = table.Column<int>(nullable: false),
                    DiaryHeaderImageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LayoutSetup", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LayoutSetup");

            migrationBuilder.DropColumn(
                name: "ThumbnailImageId",
                table: "Emotions");
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class renameTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoModules_Videos_VideoId",
                table: "VideoModules");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VideoModules",
                table: "VideoModules");

            migrationBuilder.RenameTable(
                name: "VideoModules",
                newName: "Levelmodules");

            migrationBuilder.RenameIndex(
                name: "IX_VideoModules_VideoId",
                table: "Levelmodules",
                newName: "IX_Levelmodules_VideoId");

            migrationBuilder.AddColumn<int>(
                name: "LastLevel",
                table: "User",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Levelmodules",
                table: "Levelmodules",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Subtitle = table.Column<string>(nullable: true),
                    Quote = table.Column<string>(nullable: true),
                    VideoURL = table.Column<string>(nullable: true),
                    VideoName = table.Column<string>(nullable: true),
                    FullDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Levelmodules_Levels_VideoId",
                table: "Levelmodules",
                column: "VideoId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Levelmodules_Levels_VideoId",
                table: "Levelmodules");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Levelmodules",
                table: "Levelmodules");

            migrationBuilder.DropColumn(
                name: "LastLevel",
                table: "User");

            migrationBuilder.RenameTable(
                name: "Levelmodules",
                newName: "VideoModules");

            migrationBuilder.RenameIndex(
                name: "IX_Levelmodules_VideoId",
                table: "VideoModules",
                newName: "IX_VideoModules_VideoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VideoModules",
                table: "VideoModules",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FullDescription = table.Column<string>(nullable: true),
                    Quote = table.Column<string>(nullable: true),
                    Subtitle = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    VideoName = table.Column<string>(nullable: true),
                    VideoURL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_VideoModules_Videos_VideoId",
                table: "VideoModules",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

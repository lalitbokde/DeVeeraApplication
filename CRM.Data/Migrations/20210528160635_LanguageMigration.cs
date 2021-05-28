using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class LanguageMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Emotions",
                table: "Levels",
                newName: "EmotionId");

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

            migrationBuilder.CreateTable(
                name: "DiaryPasscode",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    DiaryLoginDate = table.Column<DateTime>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaryPasscode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiaryPasscode_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Emotions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmotionNo = table.Column<int>(nullable: true),
                    EmotionName = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emotions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocaleStringResources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LanguageId = table.Column<int>(nullable: false),
                    ResourceName = table.Column<string>(nullable: true),
                    ResourceValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocaleStringResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocaleStringResources_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: true),
                    LanguageId = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Setting_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Level_Emotion_Mapping",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LevelId = table.Column<int>(nullable: false),
                    EmotionId = table.Column<int>(nullable: false),
                    CurrentEmotion = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Level_Emotion_Mapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Level_Emotion_Mapping_Emotions_EmotionId",
                        column: x => x.EmotionId,
                        principalTable: "Emotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Level_Emotion_Mapping_Levels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Levels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_Emotion_Mapping",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    EmotionId = table.Column<int>(nullable: false),
                    CurrentEmotion = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Emotion_Mapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Emotion_Mapping_Emotions_EmotionId",
                        column: x => x.EmotionId,
                        principalTable: "Emotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Emotion_Mapping_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiaryPasscode_UserId",
                table: "DiaryPasscode",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Level_Emotion_Mapping_EmotionId",
                table: "Level_Emotion_Mapping",
                column: "EmotionId");

            migrationBuilder.CreateIndex(
                name: "IX_Level_Emotion_Mapping_LevelId",
                table: "Level_Emotion_Mapping",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_LocaleStringResources_LanguageId",
                table: "LocaleStringResources",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Setting_UserId",
                table: "Setting",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Emotion_Mapping_EmotionId",
                table: "User_Emotion_Mapping",
                column: "EmotionId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Emotion_Mapping_UserId",
                table: "User_Emotion_Mapping",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiaryPasscode");

            migrationBuilder.DropTable(
                name: "Level_Emotion_Mapping");

            migrationBuilder.DropTable(
                name: "LocaleStringResources");

            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "User_Emotion_Mapping");

            migrationBuilder.DropTable(
                name: "Emotions");

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
                name: "EmotionId",
                table: "Levels",
                newName: "Emotions");

            migrationBuilder.RenameColumn(
                name: "UniqueSeoCode",
                table: "Languages",
                newName: "LanguageName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Languages",
                newName: "Abbreviations");
        }
    }
}

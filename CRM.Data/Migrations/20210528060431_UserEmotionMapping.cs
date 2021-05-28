using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class UserEmotionMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Levels_Emotions_EmotionId",
                table: "Levels");

            migrationBuilder.DropTable(
                name: "Emotion_Mapping");

            migrationBuilder.DropIndex(
                name: "IX_Levels_EmotionId",
                table: "Levels");

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
                name: "User_Emotion_Mapping");

            migrationBuilder.CreateTable(
                name: "Emotion_Mapping",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CurrentEmotion = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    EmotionId = table.Column<int>(nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emotion_Mapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Emotion_Mapping_Emotions_EmotionId",
                        column: x => x.EmotionId,
                        principalTable: "Emotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Emotion_Mapping_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Levels_EmotionId",
                table: "Levels",
                column: "EmotionId");

            migrationBuilder.CreateIndex(
                name: "IX_Emotion_Mapping_EmotionId",
                table: "Emotion_Mapping",
                column: "EmotionId");

            migrationBuilder.CreateIndex(
                name: "IX_Emotion_Mapping_UserId",
                table: "Emotion_Mapping",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Levels_Emotions_EmotionId",
                table: "Levels",
                column: "EmotionId",
                principalTable: "Emotions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

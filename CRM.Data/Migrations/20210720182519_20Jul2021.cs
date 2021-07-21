using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class _20Jul2021 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DashboardMenus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Menu_1 = table.Column<string>(nullable: true),
                    Menu_2 = table.Column<string>(nullable: true),
                    Menu_3 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DashboardMenus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DashboardQuote",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LevelId = table.Column<int>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    Level = table.Column<string>(nullable: true),
                    IsDashboardQuote = table.Column<bool>(nullable: false),
                    IsRandom = table.Column<bool>(nullable: false),
                    IsWeeklyInspiringQuotes = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DashboardQuote", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    Key = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    LanguageCulture = table.Column<string>(nullable: true),
                    UniqueSeoCode = table.Column<string>(nullable: true),
                    FlagImageFileName = table.Column<string>(nullable: true),
                    Published = table.Column<bool>(nullable: false),
                    Rtl = table.Column<bool>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

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
                    DiaryHeaderImageId = table.Column<int>(nullable: false),
                    CompleteRegistrationHeaderImgId = table.Column<int>(nullable: false),
                    ReasonToSubmit = table.Column<string>(nullable: true),
                    Link_1 = table.Column<string>(nullable: true),
                    Link_2 = table.Column<string>(nullable: true),
                    Link_3 = table.Column<string>(nullable: true),
                    Link_1_BannerImageId = table.Column<int>(nullable: false),
                    Link_2_BannerImageId = table.Column<int>(nullable: false),
                    Link_3_BannerImageId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    FooterDescription = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNo = table.Column<string>(nullable: true),
                    FooterImageId = table.Column<int>(nullable: false),
                    FooterImageUrl = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LayoutSetup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissionRecord",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    SystemName = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionRecord", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    IsSystemRole = table.Column<bool>(nullable: false),
                    SystemName = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "videos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    VideoUrl = table.Column<string>(nullable: true),
                    Key = table.Column<string>(nullable: true),
                    IsNew = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_videos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeelGoodStories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    Story = table.Column<string>(nullable: true),
                    ImageId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeelGoodStories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeelGoodStories_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "PermissionRecord_Role_Mapping",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PermissionRecordId = table.Column<int>(nullable: false),
                    UserRoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionRecord_Role_Mapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermissionRecord_Role_Mapping_PermissionRecord_PermissionRecordId",
                        column: x => x.PermissionRecordId,
                        principalTable: "PermissionRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionRecord_Role_Mapping_UserRole_UserRoleId",
                        column: x => x.UserRoleId,
                        principalTable: "UserRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserGuid = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(maxLength: 1000, nullable: true),
                    MobileNumber = table.Column<string>(nullable: true),
                    GenderType = table.Column<int>(nullable: true),
                    Age = table.Column<int>(nullable: true),
                    Occupation = table.Column<string>(nullable: true),
                    EducationType = table.Column<int>(nullable: true),
                    IncomeAboveOrBelow80K = table.Column<int>(nullable: true),
                    FamilyOrRelationshipType = table.Column<int>(nullable: true),
                    Alias = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 1000, nullable: true),
                    ImageURL = table.Column<string>(nullable: true),
                    EmailToRevalidate = table.Column<string>(maxLength: 1000, nullable: true),
                    RequireReLogin = table.Column<bool>(nullable: false),
                    FailedLoginAttempts = table.Column<int>(nullable: false),
                    CannotLoginUntilDateUtc = table.Column<DateTime>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    IsSystemAccount = table.Column<bool>(nullable: false),
                    SystemName = table.Column<string>(maxLength: 400, nullable: true),
                    UserRoleId = table.Column<int>(nullable: false),
                    ParentUserId = table.Column<int>(nullable: true),
                    LastLevel = table.Column<int>(nullable: true),
                    ActiveModule = table.Column<int>(nullable: true),
                    RegistrationComplete = table.Column<bool>(nullable: false),
                    TwoFactorAuthentication = table.Column<bool>(nullable: false),
                    IsAllow = table.Column<bool>(nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(nullable: false),
                    LastLoginDateUtc = table.Column<DateTime>(nullable: true),
                    LastActivityDateUtc = table.Column<DateTime>(nullable: false),
                    UserAvailabilityId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_UserRole_UserRoleId",
                        column: x => x.UserRoleId,
                        principalTable: "UserRole",
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
                    VideoId = table.Column<int>(nullable: false),
                    QuoteId = table.Column<int>(nullable: true),
                    EmotionHeaderImageId = table.Column<int>(nullable: false),
                    EmotionBannerImageId = table.Column<int>(nullable: false),
                    EmotionThumbnailImageId = table.Column<int>(nullable: false),
                    EmotionName = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Subtitle = table.Column<string>(nullable: true),
                    Quote = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsRandom = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emotions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Emotions_videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LevelNo = table.Column<int>(nullable: true),
                    VideoId = table.Column<int>(nullable: true),
                    EmotionId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Subtitle = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    FullDescription = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    BannerImageId = table.Column<int>(nullable: false),
                    VideoThumbImageId = table.Column<int>(nullable: false),
                    ShareBackgroundImageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Levels_videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WeeklyUpdates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VideoId = table.Column<int>(nullable: false),
                    BannerImageId = table.Column<int>(nullable: false),
                    BodyImageId = table.Column<int>(nullable: false),
                    QuoteId = table.Column<int>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Subtitle = table.Column<string>(nullable: true),
                    QuoteType = table.Column<int>(nullable: false),
                    SliderOneTitle = table.Column<string>(nullable: true),
                    SliderOneDescription = table.Column<string>(nullable: true),
                    SliderOneImageId = table.Column<int>(nullable: false),
                    SliderTwoTitle = table.Column<string>(nullable: true),
                    SliderTwoDescription = table.Column<string>(nullable: true),
                    SliderTwoImageId = table.Column<int>(nullable: false),
                    SliderThreeTitle = table.Column<string>(nullable: true),
                    SliderThreeDescription = table.Column<string>(nullable: true),
                    SliderThreeImageId = table.Column<int>(nullable: false),
                    DescriptionImageId = table.Column<int>(nullable: false),
                    LandingQuote = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsRandom = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeeklyUpdates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeeklyUpdates_videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Diaries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    LevelId = table.Column<int>(nullable: true),
                    ModuleId = table.Column<int>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    DiaryColor = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diaries_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "UserPassword",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    PasswordFormatId = table.Column<int>(nullable: false),
                    PasswordSalt = table.Column<string>(nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(nullable: false),
                    PasswordFormat = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPassword", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPassword_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
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
                name: "LevelImageLists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LevelId = table.Column<int>(nullable: false),
                    ImageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelImageLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LevelImageLists_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LevelImageLists_Levels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Levels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Levelmodules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LevelId = table.Column<int>(nullable: false),
                    VideoId = table.Column<int>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    FullDescription = table.Column<string>(nullable: true),
                    BannerImageId = table.Column<int>(nullable: false),
                    VideoThumbImageId = table.Column<int>(nullable: false),
                    ShareBackgroundImageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levelmodules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Levelmodules_Levels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Levels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Levelmodules_videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModuleId = table.Column<int>(nullable: false),
                    Question = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_Levelmodules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Levelmodules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions_Answers_Mapping",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false),
                    Answer = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions_Answers_Mapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Answers_Mapping_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Questions_Answers_Mapping_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Diaries_UserId",
                table: "Diaries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DiaryPasscode_UserId",
                table: "DiaryPasscode",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Emotions_VideoId",
                table: "Emotions",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_FeelGoodStories_ImageId",
                table: "FeelGoodStories",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Level_Emotion_Mapping_EmotionId",
                table: "Level_Emotion_Mapping",
                column: "EmotionId");

            migrationBuilder.CreateIndex(
                name: "IX_Level_Emotion_Mapping_LevelId",
                table: "Level_Emotion_Mapping",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_LevelImageLists_ImageId",
                table: "LevelImageLists",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_LevelImageLists_LevelId",
                table: "LevelImageLists",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Levelmodules_LevelId",
                table: "Levelmodules",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Levelmodules_VideoId",
                table: "Levelmodules",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_Levels_VideoId",
                table: "Levels",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_LocaleStringResources_LanguageId",
                table: "LocaleStringResources",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleImageLists_ImageId",
                table: "ModuleImageLists",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleImageLists_ModulesId",
                table: "ModuleImageLists",
                column: "ModulesId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionRecord_Role_Mapping_PermissionRecordId",
                table: "PermissionRecord_Role_Mapping",
                column: "PermissionRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionRecord_Role_Mapping_UserRoleId",
                table: "PermissionRecord_Role_Mapping",
                column: "UserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_ModuleId",
                table: "Question",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_Answers_Mapping_QuestionId",
                table: "Questions_Answers_Mapping",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_Answers_Mapping_UserId",
                table: "Questions_Answers_Mapping",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Setting_UserId",
                table: "Setting",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserRoleId",
                table: "User",
                column: "UserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Emotion_Mapping_EmotionId",
                table: "User_Emotion_Mapping",
                column: "EmotionId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Emotion_Mapping_UserId",
                table: "User_Emotion_Mapping",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPassword_UserId",
                table: "UserPassword",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WeeklyUpdates_VideoId",
                table: "WeeklyUpdates",
                column: "VideoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DashboardMenus");

            migrationBuilder.DropTable(
                name: "DashboardQuote");

            migrationBuilder.DropTable(
                name: "Diaries");

            migrationBuilder.DropTable(
                name: "DiaryPasscode");

            migrationBuilder.DropTable(
                name: "FeelGoodStories");

            migrationBuilder.DropTable(
                name: "LayoutSetup");

            migrationBuilder.DropTable(
                name: "Level_Emotion_Mapping");

            migrationBuilder.DropTable(
                name: "LevelImageLists");

            migrationBuilder.DropTable(
                name: "LocaleStringResources");

            migrationBuilder.DropTable(
                name: "ModuleImageLists");

            migrationBuilder.DropTable(
                name: "PermissionRecord_Role_Mapping");

            migrationBuilder.DropTable(
                name: "Questions_Answers_Mapping");

            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "User_Emotion_Mapping");

            migrationBuilder.DropTable(
                name: "UserPassword");

            migrationBuilder.DropTable(
                name: "WeeklyUpdates");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "PermissionRecord");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "Emotions");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Levelmodules");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropTable(
                name: "videos");
        }
    }
}

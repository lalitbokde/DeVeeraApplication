﻿// <auto-generated />
using System;
using CRM.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CRM.Data.Migrations
{
    [DbContext(typeof(dbContextCRM))]
    [Migration("20210823082704_video")]
    partial class video
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CRM.Core.Domain.DashboardMenus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Menu_1");

                    b.Property<string>("Menu_2");

                    b.Property<string>("Menu_3");

                    b.HasKey("Id");

                    b.ToTable("DashboardMenus");
                });

            modelBuilder.Entity("CRM.Core.Domain.DashboardQuote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author");

                    b.Property<bool>("IsDashboardQuote");

                    b.Property<bool>("IsRandom");

                    b.Property<bool>("IsWeeklyInspiringQuotes");

                    b.Property<string>("Level");

                    b.Property<int?>("LevelId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("DashboardQuote");
                });

            modelBuilder.Entity("CRM.Core.Domain.Emotions.Emotion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description");

                    b.Property<int>("EmotionBannerImageId");

                    b.Property<int>("EmotionHeaderImageId");

                    b.Property<string>("EmotionName");

                    b.Property<int?>("EmotionNo");

                    b.Property<int>("EmotionThumbnailImageId");

                    b.Property<bool>("IsRandom");

                    b.Property<DateTime>("LastUpdatedOn");

                    b.Property<string>("Quote");

                    b.Property<int?>("QuoteId");

                    b.Property<string>("Subtitle");

                    b.Property<string>("Title");

                    b.Property<int>("VideoId");

                    b.HasKey("Id");

                    b.HasIndex("VideoId");

                    b.ToTable("Emotions");
                });

            modelBuilder.Entity("CRM.Core.Domain.Emotions.Level_Emotion_Mapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<bool>("CurrentEmotion");

                    b.Property<bool>("Deleted");

                    b.Property<int>("EmotionId");

                    b.Property<DateTime>("LastUpdatedOn");

                    b.Property<int>("LevelId");

                    b.HasKey("Id");

                    b.HasIndex("EmotionId");

                    b.HasIndex("LevelId");

                    b.ToTable("Level_Emotion_Mapping");
                });

            modelBuilder.Entity("CRM.Core.Domain.Emotions.User_Emotion_Mapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<bool>("CurrentEmotion");

                    b.Property<bool>("Deleted");

                    b.Property<int>("EmotionId");

                    b.Property<DateTime>("LastUpdatedOn");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("EmotionId");

                    b.HasIndex("UserId");

                    b.ToTable("User_Emotion_Mapping");
                });

            modelBuilder.Entity("CRM.Core.Domain.FeelGoodStory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author");

                    b.Property<int?>("ImageId");

                    b.Property<string>("Story");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.ToTable("FeelGoodStories");
                });

            modelBuilder.Entity("CRM.Core.Domain.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Key");

                    b.Property<string>("Name");

                    b.Property<DateTime>("UpdatedOn");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("CRM.Core.Domain.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DisplayOrder");

                    b.Property<string>("FlagImageFileName");

                    b.Property<string>("LanguageCulture");

                    b.Property<string>("Name");

                    b.Property<bool>("Published");

                    b.Property<bool>("Rtl");

                    b.Property<string>("UniqueSeoCode");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("CRM.Core.Domain.LayoutSetups.LayoutSetup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BannerOneImageId");

                    b.Property<int>("BannerTwoImageId");

                    b.Property<int>("CompleteRegistrationHeaderImgId");

                    b.Property<string>("Description");

                    b.Property<int>("DiaryHeaderImageId");

                    b.Property<string>("Email");

                    b.Property<string>("FooterDescription");

                    b.Property<int>("FooterImageId");

                    b.Property<string>("FooterImageUrl");

                    b.Property<string>("HomeDescription");

                    b.Property<string>("HomeSubTitle");

                    b.Property<string>("HomeTitle");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Link_1");

                    b.Property<int>("Link_1_BannerImageId");

                    b.Property<string>("Link_2");

                    b.Property<int>("Link_2_BannerImageId");

                    b.Property<string>("Link_3");

                    b.Property<int>("Link_3_BannerImageId");

                    b.Property<string>("Location");

                    b.Property<string>("PhoneNo");

                    b.Property<string>("ReasonToSubmit");

                    b.Property<string>("SliderOneDescription");

                    b.Property<int>("SliderOneImageId");

                    b.Property<string>("SliderOneTitle");

                    b.Property<string>("SliderThreeDescription");

                    b.Property<int>("SliderThreeImageId");

                    b.Property<string>("SliderThreeTitle");

                    b.Property<string>("SliderTwoDescription");

                    b.Property<int>("SliderTwoImageId");

                    b.Property<string>("SliderTwoTitle");

                    b.Property<string>("Title");

                    b.Property<int?>("VideoId");

                    b.HasKey("Id");

                    b.ToTable("LayoutSetup");
                });

            modelBuilder.Entity("CRM.Core.Domain.Level", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<int>("BannerImageId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int>("EmotionId");

                    b.Property<string>("FullDescription");

                    b.Property<int?>("LevelNo");

                    b.Property<int>("ShareBackgroundImageId");

                    b.Property<string>("Subtitle");

                    b.Property<string>("Title");

                    b.Property<DateTime>("UpdatedOn");

                    b.Property<int?>("VideoId");

                    b.Property<int>("VideoThumbImageId");

                    b.HasKey("Id");

                    b.HasIndex("VideoId");

                    b.ToTable("Levels");
                });

            modelBuilder.Entity("CRM.Core.Domain.LevelImageList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ImageId");

                    b.Property<int>("LevelId");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.HasIndex("LevelId");

                    b.ToTable("LevelImageLists");
                });

            modelBuilder.Entity("CRM.Core.Domain.LocaleStringResource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive");

                    b.Property<int?>("LanguageId");

                    b.Property<string>("ResourceName");

                    b.Property<string>("ResourceValue");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.ToTable("LocaleStringResources");
                });

            modelBuilder.Entity("CRM.Core.Domain.ModuleImageList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ImageId");

                    b.Property<int>("ModuleId");

                    b.Property<int?>("ModulesId");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.HasIndex("ModulesId");

                    b.ToTable("ModuleImageLists");
                });

            modelBuilder.Entity("CRM.Core.Domain.Security.PermissionRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Category");

                    b.Property<string>("Name");

                    b.Property<string>("SystemName");

                    b.HasKey("Id");

                    b.ToTable("PermissionRecord");
                });

            modelBuilder.Entity("CRM.Core.Domain.Security.PermissionRecord_Role_Mapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PermissionRecordId");

                    b.Property<int>("UserRoleId");

                    b.HasKey("Id");

                    b.HasIndex("PermissionRecordId");

                    b.HasIndex("UserRoleId");

                    b.ToTable("PermissionRecord_Role_Mapping");
                });

            modelBuilder.Entity("CRM.Core.Domain.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LanguageId");

                    b.Property<int?>("ProjectId");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Setting");
                });

            modelBuilder.Entity("CRM.Core.Domain.Users.DiaryPasscode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<bool>("Deleted");

                    b.Property<DateTime?>("DiaryLoginDate");

                    b.Property<DateTime>("LastUpdatedOn");

                    b.Property<string>("Password");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("DiaryPasscode");
                });

            modelBuilder.Entity("CRM.Core.Domain.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<int?>("ActiveModule");

                    b.Property<int?>("Age");

                    b.Property<string>("Alias");

                    b.Property<DateTime?>("CannotLoginUntilDateUtc");

                    b.Property<DateTime>("CreatedOnUtc");

                    b.Property<bool>("Deleted");

                    b.Property<int?>("EducationType");

                    b.Property<string>("Email")
                        .HasMaxLength(1000);

                    b.Property<string>("EmailToRevalidate")
                        .HasMaxLength(1000);

                    b.Property<int>("FailedLoginAttempts");

                    b.Property<int?>("FamilyOrRelationshipType");

                    b.Property<int?>("GenderType");

                    b.Property<string>("ImageURL");

                    b.Property<int?>("IncomeAboveOrBelow80K");

                    b.Property<bool>("IsAllow");

                    b.Property<bool>("IsSystemAccount");

                    b.Property<DateTime>("LastActivityDateUtc");

                    b.Property<int?>("LastLevel");

                    b.Property<DateTime?>("LastLoginDateUtc");

                    b.Property<string>("MobileNumber");

                    b.Property<string>("Occupation");

                    b.Property<int?>("ParentUserId");

                    b.Property<bool>("RegistrationComplete");

                    b.Property<bool>("RequireReLogin");

                    b.Property<string>("SystemName")
                        .HasMaxLength(400);

                    b.Property<bool>("TwoFactorAuthentication");

                    b.Property<int?>("UserAvailabilityId");

                    b.Property<Guid>("UserGuid");

                    b.Property<int>("UserRoleId");

                    b.Property<string>("Username")
                        .HasMaxLength(1000);

                    b.HasKey("Id");

                    b.HasIndex("UserRoleId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("CRM.Core.Domain.Users.UserPassword", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOnUtc");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<int>("PasswordFormat");

                    b.Property<int>("PasswordFormatId");

                    b.Property<string>("PasswordSalt");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserPassword");
                });

            modelBuilder.Entity("CRM.Core.Domain.Users.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<bool>("IsSystemRole");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("SystemName")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("CRM.Core.Domain.Video", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<bool>("IsNew");

                    b.Property<string>("Key");

                    b.Property<string>("Name");

                    b.Property<DateTime>("UpdatedOn");

                    b.Property<string>("VideoUrl");

                    b.HasKey("Id");

                    b.ToTable("videos");
                });

            modelBuilder.Entity("CRM.Core.Domain.VideoModules.Diary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description");

                    b.Property<string>("DiaryColor");

                    b.Property<DateTime>("LastUpdatedOn");

                    b.Property<int?>("LevelId");

                    b.Property<int?>("ModuleId");

                    b.Property<string>("Title");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Diaries");
                });

            modelBuilder.Entity("CRM.Core.Domain.VideoModules.Modules", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BannerImageId");

                    b.Property<string>("FullDescription");

                    b.Property<int>("LevelId");

                    b.Property<int>("ShareBackgroundImageId");

                    b.Property<string>("Title");

                    b.Property<int?>("VideoId");

                    b.Property<int>("VideoThumbImageId");

                    b.HasKey("Id");

                    b.HasIndex("LevelId");

                    b.HasIndex("VideoId");

                    b.ToTable("Levelmodules");
                });

            modelBuilder.Entity("CRM.Core.Domain.VideoModules.Question_Answer_Mapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Answer");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<bool>("Deleted");

                    b.Property<int>("QuestionId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("UserId");

                    b.ToTable("Questions_Answers_Mapping");
                });

            modelBuilder.Entity("CRM.Core.Domain.VideoModules.Questions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<bool>("Deleted");

                    b.Property<int>("ModuleId");

                    b.Property<string>("Question");

                    b.HasKey("Id");

                    b.HasIndex("ModuleId");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("CRM.Core.Domain.WeeklyUpdate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BannerImageId");

                    b.Property<int>("BodyImageId");

                    b.Property<int>("DescriptionImageId");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsRandom");

                    b.Property<string>("LandingQuote");

                    b.Property<string>("Quote");

                    b.Property<int?>("QuoteId");

                    b.Property<int>("QuoteType");

                    b.Property<string>("SliderOneDescription");

                    b.Property<int>("SliderOneImageId");

                    b.Property<string>("SliderOneTitle");

                    b.Property<string>("SliderThreeDescription");

                    b.Property<int>("SliderThreeImageId");

                    b.Property<string>("SliderThreeTitle");

                    b.Property<string>("SliderTwoDescription");

                    b.Property<int>("SliderTwoImageId");

                    b.Property<string>("SliderTwoTitle");

                    b.Property<string>("Subtitle");

                    b.Property<string>("Title");

                    b.Property<string>("VideoHeader");

                    b.Property<int>("VideoId");

                    b.HasKey("Id");

                    b.HasIndex("VideoId");

                    b.ToTable("WeeklyUpdates");
                });

            modelBuilder.Entity("CRM.Core.Domain.Emotions.Emotion", b =>
                {
                    b.HasOne("CRM.Core.Domain.Video", "Video")
                        .WithMany()
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CRM.Core.Domain.Emotions.Level_Emotion_Mapping", b =>
                {
                    b.HasOne("CRM.Core.Domain.Emotions.Emotion", "Emotion")
                        .WithMany()
                        .HasForeignKey("EmotionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CRM.Core.Domain.Level")
                        .WithMany("Level_Emotion_Mappings")
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CRM.Core.Domain.Emotions.User_Emotion_Mapping", b =>
                {
                    b.HasOne("CRM.Core.Domain.Emotions.Emotion", "Emotion")
                        .WithMany()
                        .HasForeignKey("EmotionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CRM.Core.Domain.Users.User")
                        .WithMany("User_Emotion_Mappings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CRM.Core.Domain.FeelGoodStory", b =>
                {
                    b.HasOne("CRM.Core.Domain.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId");
                });

            modelBuilder.Entity("CRM.Core.Domain.Level", b =>
                {
                    b.HasOne("CRM.Core.Domain.Video", "Video")
                        .WithMany()
                        .HasForeignKey("VideoId");
                });

            modelBuilder.Entity("CRM.Core.Domain.LevelImageList", b =>
                {
                    b.HasOne("CRM.Core.Domain.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CRM.Core.Domain.Level", "Level")
                        .WithMany()
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CRM.Core.Domain.LocaleStringResource", b =>
                {
                    b.HasOne("CRM.Core.Domain.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId");
                });

            modelBuilder.Entity("CRM.Core.Domain.ModuleImageList", b =>
                {
                    b.HasOne("CRM.Core.Domain.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CRM.Core.Domain.VideoModules.Modules", "Modules")
                        .WithMany()
                        .HasForeignKey("ModulesId");
                });

            modelBuilder.Entity("CRM.Core.Domain.Security.PermissionRecord_Role_Mapping", b =>
                {
                    b.HasOne("CRM.Core.Domain.Security.PermissionRecord", "PermissionRecord")
                        .WithMany("PermissionRecord_Role_Mapping")
                        .HasForeignKey("PermissionRecordId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CRM.Core.Domain.Users.UserRole", "UserRole")
                        .WithMany("PermissionRecord_Role_Mapping")
                        .HasForeignKey("UserRoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CRM.Core.Domain.Setting", b =>
                {
                    b.HasOne("CRM.Core.Domain.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("CRM.Core.Domain.Users.DiaryPasscode", b =>
                {
                    b.HasOne("CRM.Core.Domain.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CRM.Core.Domain.Users.User", b =>
                {
                    b.HasOne("CRM.Core.Domain.Users.UserRole", "UserRole")
                        .WithMany()
                        .HasForeignKey("UserRoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CRM.Core.Domain.Users.UserPassword", b =>
                {
                    b.HasOne("CRM.Core.Domain.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CRM.Core.Domain.VideoModules.Diary", b =>
                {
                    b.HasOne("CRM.Core.Domain.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CRM.Core.Domain.VideoModules.Modules", b =>
                {
                    b.HasOne("CRM.Core.Domain.Level", "Level")
                        .WithMany()
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CRM.Core.Domain.Video", "Video")
                        .WithMany()
                        .HasForeignKey("VideoId");
                });

            modelBuilder.Entity("CRM.Core.Domain.VideoModules.Question_Answer_Mapping", b =>
                {
                    b.HasOne("CRM.Core.Domain.VideoModules.Questions", "Question")
                        .WithMany("Question_Answer_Mapping")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CRM.Core.Domain.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CRM.Core.Domain.VideoModules.Questions", b =>
                {
                    b.HasOne("CRM.Core.Domain.VideoModules.Modules", "Module")
                        .WithMany()
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CRM.Core.Domain.WeeklyUpdate", b =>
                {
                    b.HasOne("CRM.Core.Domain.Video", "Video")
                        .WithMany()
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
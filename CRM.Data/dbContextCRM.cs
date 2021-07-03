using CRM.Core.Domain.Users;

using CRM.Core.Domain.Security;
using CRM.Data.Mapping.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using CRM.Core.Domain;
using CRM.Core.Domain.VideoModules;
using CRM.Core.Domain.Emotions;
using CRM.Core.Domain.LayoutSetups;
using CRM.Core.ViewModels;

namespace CRM.Data
{
    public class dbContextCRM : DbContext
    {
        public dbContextCRM(DbContextOptions<dbContextCRM> options)
           : base(options)
        {
            Database.SetCommandTimeout(1500000);
        }

     

        //User
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<UserPassword> UserPassword { get; set; }

        //Security
        public DbSet<PermissionRecord> PermissionRecord { get; set; }

        //Video
        public DbSet<Video> videos { get; set; }

        //Image
        public DbSet<Image> Images { get; set; }
        public DbSet<LevelImageList> LevelImageLists {get; set;}
        //Level
        public DbSet<Level> Levels { get; set; }
        public DbSet<Modules> Levelmodules { get; set; }
        public DbSet<Diary> Diaries { get; set; }
        public DbSet<ModuleImageList> ModuleImageLists { get; set; }

        //Quote
        public DbSet<WeeklyUpdate> WeeklyUpdates { get; set; }
        public DbSet<DashboardQuote> DashboardQuote { get; set; }
        public DbSet<DashboardMenus> DashboardMenus { get; set; }

        //Questions
        public DbSet<Questions> Question { get; set; }
        public DbSet<Question_Answer_Mapping> Questions_Answers_Mapping { get; set; }

        //feelGoodStory

        public DbSet<FeelGoodStory> FeelGoodStories { get; set; }


        //Language

        public DbSet<Language> Languages { get; set; }

        //setting
        public DbSet<Setting> Setting { get; set; }

        public DbSet<LocaleStringResource> LocaleStringResources { get; set; }


        //Emotion

        public DbSet<Emotion> Emotions { get; set; }

        //Diary Passcode

        public DbSet<DiaryPasscode> DiaryPasscode { get; set; }
        //Diary Layout Setup

        public DbSet<LayoutSetup> LayoutSetup { get; set; }

        public virtual DbQuery<DiaryViewModel> DiaryViewModel { get; set; }
        public virtual DbQuery<VideoViewModel> VideoViewModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
             base.OnModelCreating(modelBuilder);
         

            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => !string.IsNullOrEmpty(type.Namespace))
            .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
                type.BaseType.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
       
            //User
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new UserRoleMap());

          
           
            base.OnModelCreating(modelBuilder);
        }
    }
}

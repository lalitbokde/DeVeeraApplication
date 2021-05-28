using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;
using AutoMapper;
using CRM.Core;
using CRM.Core.Domain.Security;
using CRM.Core.Infrastructure;
using CRM.Data;
using CRM.Data.Interfaces;
using CRM.Services;
using CRM.Services.Authentication;
using CRM.Services.Common;
using CRM.Services.Customers;
using CRM.Services.DashboardMenu;
using CRM.Services.DashboardQuotes;
using CRM.Services.Directory;
using CRM.Services.Emotions;
using CRM.Services.Helpers;
using CRM.Services.Message;
using CRM.Services.QuestionsAnswer;
using CRM.Services.Security;
using CRM.Services.Users;
using CRM.Services.VideoModules;
using DeVeeraApp.Factories;
using DeVeeraApp.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace DeVeeraApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
          
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDbContext<dbContextCRM>(options =>
          options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSession();
            services.AddTransient(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddTransient(typeof(IAsyncRepository<>), typeof(EfRepository<>));

            services.AddScoped<IWorkContext, WebWorkContext>();
            services.AddScoped<IAuthenticationService, CookieAuthenticationService>();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<HttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<IUserAgentHelper, UserAgentHelper>();
            services.AddScoped<IUserPasswordService, UserPasswordService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IStateProvinceService, StateProvinceService>();
            services.AddScoped<DateTimeSettings>();
            services.AddScoped<IDateTimeHelper, DateTimeHelper>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IUserRegistrationService, UserRegistrationService>();
            services.AddScoped<IEncryptionService, EncryptionService>();
            services.AddScoped<SecuritySettings, SecuritySettings>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserModelFactory, UserModelFactory>();
            services.AddScoped<IDiaryMasterService, DiaryMasterService>();
            services.AddScoped<IFeelGoodStoryServices, FeelGoodStoryServices>();
            
            services.AddScoped<IModuleService, ModuleService>();

            services.AddScoped<ILevelServices, LevelServices>();
            services.AddScoped<IVideoMasterService, VideoMasterService>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<IWeeklyUpdateServices, WeeklyUpdateServices>();
            services.AddScoped<IDashboardQuoteService, DashboardQuoteService>();
            services.AddScoped<IDashboardMenuService, DashboardMenuService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IImageMasterService, ImageMasterService>();
            services.AddScoped<ILevelImageListServices, LevelImageListServices>();
            services.AddScoped<IS3BucketService, S3BucketService>();
            services.AddScoped<IQuestionAnswerService, QuestionAnswerService>();
            services.AddScoped<IQuestionAnswerMappingService, QuestionAnswerMappingService>();
            services.AddScoped<IEmotionService, EmotionService>();
            services.AddScoped<IEmotionMappingService, EmotionMappingService>();
            services.AddScoped<IDiaryPasscodeService, DiaryPasscodeService>();

            var authenticationBuilder = services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = AutoDataImportCookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = AutoDataImportCookieAuthenticationDefaults.ExternalAuthenticationScheme;
            });

            //add main cookie authentication
            authenticationBuilder.AddCookie(AutoDataImportCookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.Cookie.Name = AutoDataImportCookieAuthenticationDefaults.CookiePrefix + AutoDataImportCookieAuthenticationDefaults.AuthenticationScheme;
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromSeconds(180);
                //options.Cookie.Expiration = TimeSpan.FromMinutes(3);
                options.SlidingExpiration = true;
                options.LoginPath = AutoDataImportCookieAuthenticationDefaults.LoginPath;
                options.AccessDeniedPath = AutoDataImportCookieAuthenticationDefaults.AccessDeniedPath;
            });

            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue;
                x.MultipartHeadersLengthLimit = int.MaxValue;
            });

            services.AddMvc();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddControllersWithViews();
            AddAutoMapper(services);
        }

        protected virtual void AddAutoMapper(IServiceCollection services)
        {
            //create and sort instances of mapper configurations

            //create AutoMapper configuration
            var mappingProfile = new AdminMapperConfiguration();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(mappingProfile);
                cfg.ValidateInlineMaps = false;
            });

            //register AutoMapper
            services.AddAutoMapper();

            //register
            AutoMapperConfiguration.Init(config);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.Use(async (context, next) =>
            {
                context.Features.Get<IHttpMaxRequestBodySizeFeature>().MaxRequestBodySize = null; // unlimited I guess
                await next.Invoke();
            });
        }
    }



    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<dbContextCRM>
    {
        public dbContextCRM CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            var builder = new DbContextOptionsBuilder<dbContextCRM>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new dbContextCRM(builder.Options);
        }
    }
}

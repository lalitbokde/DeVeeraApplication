using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DeVeeraApp.Filters;
using DeVeeraApp.Utils;
using CRM.Services.Users;
using CRM.Services;
using DeVeeraApp.ViewModels;
using CRM.Core.Domain;
using CRM.Services.DashboardQuotes;
using CRM.Core;
using Microsoft.AspNetCore.Http;
using CRM.Services.Authentication;
using CRM.Services.DashboardMenu;
using CRM.Services.Localization;
using CRM.Services.VideoModules;

namespace DeVeeraApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthorizeAdmin]
    public class HomeController : BaseController
    {
        #region fields

        private readonly ILogger<HomeController> _logger;
        private readonly ILevelServices _levelServices;
        private readonly IWeeklyUpdateServices _weeklyUpdateServices;
        private readonly IDashboardQuoteService _dashboardQuoteService;
        private readonly IDashboardMenuService _dashboardMenuService;
        private readonly IUserService _UserService;
        private readonly IWorkContext _workContext;
        private readonly IFeelGoodStoryServices _feelGoodStoryServices;
        private readonly IImageMasterService _imageMasterService;
        private readonly IVideoMasterService _videoMasterService;
        private readonly IS3BucketService _s3BucketService;
        private readonly ITranslationService _translationService;
        private readonly IModuleService _moduleService;
        public string key = "AIzaSyC2wpcQiQQ7ASdt4vcJHfmly8DwE3l3tqE";

        #endregion


        #region ctor
        public HomeController(ILogger<HomeController> logger,
                              ILevelServices levelServices,
                              IWeeklyUpdateServices weeklyUpdateServices,
                              IDashboardQuoteService dashboardQuoteService,
                              IDashboardMenuService dashboardMenuService,
                              IWorkContext workContext,
                              IHttpContextAccessor httpContextAccessor,
                              IAuthenticationService authenticationService,
                              IFeelGoodStoryServices feelGoodStoryServices,
                              IImageMasterService imageMasterService,
                              IVideoMasterService videoMasterService,
                              IS3BucketService s3BucketService,
                              ITranslationService translationService,
                              IModuleService moduleService,
                              IUserService userService
                              ) : base(workContext: workContext,
                                                                                  httpContextAccessor: httpContextAccessor,
                                                                                  authenticationService: authenticationService)
        {
            _logger = logger;
            _levelServices = levelServices;
            _weeklyUpdateServices = weeklyUpdateServices;
            _dashboardQuoteService = dashboardQuoteService;
            _dashboardMenuService = dashboardMenuService;
            _UserService = userService;
            _workContext = workContext;
            _feelGoodStoryServices = feelGoodStoryServices;
            _imageMasterService = imageMasterService;
            _videoMasterService = videoMasterService;
            _s3BucketService = s3BucketService;
            _translationService = translationService;
            _moduleService = moduleService;
        }

        #endregion

        #region Method
        public IActionResult Index()
        {

            AddBreadcrumbs("Application", "Dashboard", "/Home/Index", "/Admin");

            var currentUser = _UserService.GetUserById(_workContext.CurrentUser.Id);

            var model = new DashboardQuoteModel();

            var quote = _dashboardQuoteService.GetAllDashboardQuotes().Where(a => a.IsDashboardQuote == true).FirstOrDefault();

            model.Title = quote?.Title;
            model.Author = quote?.Author;

            model.Menus = _dashboardMenuService.GetAllDashboardMenus().FirstOrDefault();
            _translationService.Translate(model.Title, key);
            _translationService.Translate(model.Author, key);
            var data = _levelServices.GetAllLevels().OrderBy(l => l.LevelNo);
            ///all count data
            model.TotalModuleCount = _moduleService.GetAllModules().Count();
            model.TotalLevelCount = _levelServices.GetAllLevels().Count();
            model.TotalUserCount = _UserService.GetAllUsers().Count();
            model.TotalVisitorsCount = _UserService.GetAllUsers().Where(a => a.RegistrationComplete == true).Count();
            //int lastlevel = data.LastOrDefault().Id;
            var lastLevelData = data.LastOrDefault();
            int lastlevel = lastLevelData != null ? lastLevelData.Id : 0;
            if (data.Count() != 0)
            {
                if (!(_workContext.CurrentUser.UserRole.Name == "Admin"))
                {
                    var activeLevel = data.Where(l => l.Active == true).ToList();

                    if (activeLevel.Count() != 0)
                    {
                        var LevelOne = data.FirstOrDefault();

                        var lastLevelForNewUser = data.Where(a => a.Id == LevelOne.Id).FirstOrDefault();

                        var lastLevelForOldUser = data.Where(a => a.Id == currentUser.LastLevel).FirstOrDefault();


                        lastlevel = (currentUser.LastLevel == null || currentUser.LastLevel == 0) ? lastlevel = lastLevelForNewUser.Id : lastlevel = lastLevelForOldUser.Id;

                        foreach (var item in data)
                        {
                            if (item.Active == true && item.Id <= lastlevel)
                            {
                                model.VideoModelList.Add(item.ToModel<LevelModel>());

                                if (item.Id == lastlevel)
                                {
                                    break;
                                }

                            }

                        }

                    }



                }
                else
                {
                    foreach (var item in data)
                    {
                        model.VideoModelList.Add(item.ToModel<LevelModel>());

                    }
                }


                return View(model);
            }
            return View();
        }

        public IActionResult ExistingUser(int QuoteType,DateTime LastLoginDateUtc)
        {
            ViewBag.LastLoginDateUtc = LastLoginDateUtc;

            var level = new Level();
            var currentUser = _UserService.GetUserById(_workContext.CurrentUser.Id);

            var data = _weeklyUpdateServices.GetWeeklyUpdateByQuoteType((int)ViewModels.Quote.Login);

            var model = data?.ToModel<WeeklyUpdateModel>();
            _translationService.Translate(model.Title,key);
            _translationService.Translate(model.Subtitle, key);
            if (model != null)
            {
                model.VideoUrl = data?.Video?.VideoUrl;

                if (currentUser.LastLevel != null)
                    level = _levelServices.GetLevelById((int)currentUser.LastLevel);

                var firstlevel = _levelServices.GetAllLevels().FirstOrDefault().Id;

                model.LastLevel = (currentUser.LastLevel == null || currentUser.LastLevel == 0) ? firstlevel : (level != null ? (int)currentUser.LastLevel : firstlevel);


                var lastLevel = _levelServices.GetAllLevels().Where(a => a.Id <= model.LastLevel && a.Active == true).ToList();

                if (lastLevel.Count() != 0)
                {
                    ViewBag.SrNo = lastLevel.Count();
                }
                else
                {
                    ViewBag.SrNo = 0;
                }

            }

            return View(model);
        }

        public IActionResult NewUser(int QuoteType)
        {
            var currentUser = _UserService.GetUserById(_workContext.CurrentUser.Id);

            var data = _weeklyUpdateServices.GetWeeklyUpdateByQuoteType((int)ViewModels.Quote.Registration);

            var model = data?.ToModel<WeeklyUpdateModel>();
            if(model != null)
            {
                model.VideoUrl = data?.Video?.VideoUrl;
                var firstLevel = _levelServices.GetAllLevels().Where(a => a.Active == true).FirstOrDefault();
                if (firstLevel != null)
                {
                    model.LastLevel = firstLevel.Id;
                    ViewBag.SrNo = 1;

                }
                currentUser.LastLevel = model.LastLevel;
                _UserService.UpdateUser(currentUser);

            }
            _translationService.Translate(model.Title,key);
            _translationService.Translate(model.Subtitle, key);
            return View(model);



        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Help()
        {
            return View();
        }
        public IActionResult WeeklyInspiringQuotes()
        {
            AddBreadcrumbs("Home", "WeeklyInspiringQuotes", "/Admin", "/Admin/Home/WeeklyInspiringQuotes");
            var model = new List<DashboardQuoteModel>();

            var WeeklyQuoteData = _dashboardQuoteService.GetAllDashboardQuotes().Where(q => q.IsWeeklyInspiringQuotes == true).ToList();
            if (WeeklyQuoteData.Count != 0)
            {
                foreach (var item in WeeklyQuoteData)
                {
                    model.Add(item.ToModel<DashboardQuoteModel>());
                }
            }
            return View(model);
        }
        public IActionResult NewVideos()
        {
            AddBreadcrumbs("Home", "NewVideos", "/Admin", "/Admin/Home/NewVideos");
            var model = new List<VideoModel>();

            var newVideoList = _videoMasterService.GetAllVideos().Where(v => v.IsNew == true).ToList();

            if (newVideoList.Count != 0)
            {
                foreach (var item in newVideoList)
                {
                    model.Add(item.ToModel<VideoModel>());
                }
            }
            return View(model);

        }
        

        public IActionResult Play(int Id)
        {
    
            if (Id != 0)
            {
                var data = _videoMasterService.GetVideoById(Id);

                var model = data.ToModel<VideoModel>();
                return View(model);
            }
            return RedirectToAction("List");
            

        }
        
        public IActionResult FeelGoodStories()
        {
            AddBreadcrumbs("Home", "FeelGoodStories", "/Admin", "/Admin/Home/FeelGoodStories");
            var data = _feelGoodStoryServices.GetAllFeelGoodStorys();

            var model = new List<FeelGoodStoryModel>();
         

            if (data.Count() != 0)
            {
                foreach (var item in data)
                {
                    if (item.ImageId != null)
                    {
                        var imagedata = _imageMasterService.GetImageById((int)item.ImageId);

                        if (imagedata != null)
                        {
                            imagedata.ImageUrl = _s3BucketService.GetPreSignedURL(imagedata.Key);

                            _imageMasterService.UpdateImage(imagedata);

                            item.Image.ImageUrl = imagedata.ImageUrl;
                        }

                    }
                    _translationService.Translate(item.Title,key);
                    _translationService.Translate(item.Story, key);
                    model.Add(item.ToModel<FeelGoodStoryModel>());
                }
            }
            return View(model);
        }

        #endregion
    }
}

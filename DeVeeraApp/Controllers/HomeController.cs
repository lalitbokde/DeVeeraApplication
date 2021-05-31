using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DeVeeraApp.Models;
using DeVeeraApp.Filters;
using CRM.Core.Domain.Users;
using DeVeeraApp.ViewModels.User;
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

using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using static DeVeeraApp.ViewModels.HappynessLevelModel;


namespace DeVeeraApp.Controllers
{
   
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
        private readonly ILanguageService _languageService;

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
                              ILanguageService languageService,
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
            _languageService = languageService;
        }

        #endregion

        #region Utilities



        #endregion

        #region Method
        public IActionResult Index()
        {
            var model = new UserModel();

            var data = _weeklyUpdateServices.GetWeeklyUpdateByQuoteType((int)ViewModels.Quote.Landing);

            model.LandingPageModel.WeeklyUpdate = data.ToModel<WeeklyUpdateModel>();

            ViewBag.VideoUrl = data?.Video?.VideoUrl;

            return View(model);
        }

        
        public IActionResult AskHappynessLevel()
        {
            return View();
        }
       
        [HttpPost]
        public IActionResult AskHappynessLevel(HappynessLevelModel model)
        {
            if (ModelState.IsValid)
            {

                if (model.HappynessLevelTypeId > (int)HappynessLevelType.LevelSix)
                {
                    var data = _levelServices.GetAllLevels().Where(l => l.Level_Emotion_Mappings.Where(a => a.Emotion?.EmotionName == "Happy").Count() > 0 && l.Active == true).FirstOrDefault();
                    if (data != null)
                    {
                        return RedirectToAction("Index", "Lesson", new { id = data.Id });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("Create", "Diary");
                }
            }
            else
            {
                return View(model);
            }

        }


        [HttpPost]
        public IActionResult CultureManagement(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.Now.AddDays(30) });

            return RedirectToAction(nameof(Index));
        }

      
        public IActionResult ExistingUser(int QuoteType,DateTime LastLoginDateUtc)
        {
            ViewBag.LastLoginDateUtc = LastLoginDateUtc;

            var level = new Level();
            var currentUser = _UserService.GetUserById(_workContext.CurrentUser.Id);

            var data = _weeklyUpdateServices.GetWeeklyUpdateByQuoteType((int)ViewModels.Quote.Login);

            var model = data?.ToModel<WeeklyUpdateModel>();

            if(model != null)
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
            AddBreadcrumbs("Home", "WeeklyInspiringQuotes", "/Home/WeeklyInspiringQuotes", "/Home/WeeklyInspiringQuotes");
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
            AddBreadcrumbs("Home", "NewVideos", "/Home/NewVideos", "/Home/NewVideos");
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
      
        public IActionResult FeelGoodStories()
        {
            AddBreadcrumbs("Home", "FeelGoodStories", "/Home/FeelGoodStories", "/Home/FeelGoodStories");
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
                            imagedata.ImageUrl = _s3BucketService.GetPreSignedURL(imagedata.Key).Result;

                            _imageMasterService.UpdateImage(imagedata);

                            item.Image.ImageUrl = imagedata.ImageUrl;
                        }

                    }

                    model.Add(item.ToModel<FeelGoodStoryModel>());
                }
            }
            return View(model);
        }
   

        #endregion
    }
}

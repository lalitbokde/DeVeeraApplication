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

namespace DeVeeraApp.Controllers
{
    [AuthorizeAdmin]
    public class HomeController : BaseController
    {
        #region fields

        private readonly ILogger<HomeController> _logger;
        private readonly ILevelServices _levelServices;
        private readonly IWeeklyUpdateServices _weeklyUpdateServices;
        private readonly IDashboardQuoteService _dashboardQuoteService;
        private readonly IUserService _UserService;
        private readonly IWorkContext _workContext;
        private readonly IFeelGoodStoryServices _feelGoodStoryServices;
        private readonly IImageMasterService _imageMasterService;
        private readonly IVideoMasterService _videoMasterService;
        private readonly IS3BucketService _s3BucketService;

        #endregion


        #region ctor
        public HomeController(ILogger<HomeController> logger,
                              ILevelServices levelServices,
                              IWeeklyUpdateServices weeklyUpdateServices,
                              IDashboardQuoteService dashboardQuoteService,
                              IWorkContext workContext,
                              IHttpContextAccessor httpContextAccessor,
                              IAuthenticationService authenticationService,
                              IFeelGoodStoryServices feelGoodStoryServices,
                              IImageMasterService imageMasterService,
                              IVideoMasterService videoMasterService,
                              IS3BucketService s3BucketService,
                              IUserService userService                             
                              ) :base(workContext: workContext,
                                                                                  httpContextAccessor: httpContextAccessor,
                                                                                  authenticationService: authenticationService)
        {
            _logger = logger;
            _levelServices = levelServices;
            _weeklyUpdateServices = weeklyUpdateServices;
            _dashboardQuoteService = dashboardQuoteService;
            _UserService = userService;
            _workContext = workContext;
            _feelGoodStoryServices = feelGoodStoryServices;
            _imageMasterService = imageMasterService;
            _videoMasterService = videoMasterService;
            _s3BucketService = s3BucketService;
        }

        #endregion

        #region Method
        public IActionResult Index()
        {
            AddBreadcrumbs("Application", "Dashboard","/Home/Index", "/Home/Index");

            var currentUser = _UserService.GetUserById(_workContext.CurrentUser.Id);

            var model = new DashboardQuoteModel();

            var quote = _dashboardQuoteService.GetAllDashboardQutoes().Where(a => a.IsDashboardQuote == true).FirstOrDefault();

            model.Title = quote?.Title;
            model.Author = quote?.Author;


            var data = _levelServices.GetAllLevels().OrderBy(l => l.LevelNo); 

            int lastlevel = data.LastOrDefault().Id;

            if (data.Count() != 0)
            {
                if(!(_workContext.CurrentUser.UserRole.Name == "Admin"))
                {
                    var activeLevel = data.Where(l => l.Active == true).ToList();

                    if(activeLevel.Count() != 0)
                    {
                        var LevelOne = data.FirstOrDefault();

                        var lastLevelForNewUser = data.Where(a => a.Id == LevelOne.Id).FirstOrDefault();

                        var lastLevelForOldUser = data.Where(a => a.Id == currentUser.LastLevel).FirstOrDefault();


                        lastlevel = (currentUser.LastLevel == null || currentUser.LastLevel == 0) ? lastlevel = lastLevelForNewUser.Id : lastlevel = lastLevelForOldUser.Id;

                        foreach (var item in data)
                        {
                            if(item.Active == true && item.Id <= lastlevel)
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

        public IActionResult ExistingUser(int QuoteType)
        {
            var level = new Level();
            var currentUser = _UserService.GetUserById(_workContext.CurrentUser.Id);
            
               var data = _weeklyUpdateServices.GetWeeklyUpdateByQuoteType((int)ViewModels.Quote.Login);

                var model = data.ToModel<WeeklyUpdateModel>();
                model.VideoUrl = data?.Video?.VideoUrl;
                if(currentUser.LastLevel!=null)
                level = _levelServices.GetLevelById((int)currentUser.LastLevel);
            
            var firstlevel = _levelServices.GetAllLevels().FirstOrDefault().Id;
            model.LastLevel = (currentUser.LastLevel == null || currentUser.LastLevel  == 0)? firstlevel : (level!= null? (int)currentUser.LastLevel : firstlevel);


            var lastLevel = _levelServices.GetAllLevels().Where(a => a.Id <= model.LastLevel);

            if (lastLevel != null)
            {
                ViewBag.SrNo = lastLevel.Count();
            }
            else
            {
                ViewBag.SrNo = 1;
            }

            return View(model);
        }

        public IActionResult NewUser(int QuoteType)
        {
            var currentUser = _UserService.GetUserById(_workContext.CurrentUser.Id);
       
                var data = _weeklyUpdateServices.GetWeeklyUpdateByQuoteType((int)ViewModels.Quote.Registration);
            
                var model = data.ToModel<WeeklyUpdateModel>();
            model.VideoUrl = data?.Video?.VideoUrl;
            var firstLevel = _levelServices.GetAllLevels().FirstOrDefault();
                if (firstLevel != null)
                    model.LastLevel = firstLevel.Id;
                ViewBag.SrNo = 1;
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

            var WeeklyQuoteData = _dashboardQuoteService.GetAllDashboardQutoes().Where(q => q.IsWeeklyInspiringQuotes == true).ToList();
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

            if(newVideoList.Count != 0)
            {
                foreach(var item in newVideoList)
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

            if(data.Count() != 0)
            {
                foreach(var item in data)
                {
                    if(item.ImageId != null)
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

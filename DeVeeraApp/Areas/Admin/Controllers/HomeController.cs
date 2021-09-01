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
using DeVeeraApp.ViewModels.LayoutSetups;
using CRM.Services.Layoutsetup;
using Newtonsoft.Json;
using CRM.Core.Domain.LayoutSetups;
using Microsoft.AspNetCore.Mvc.Rendering;
using CRM.Services.Message;
using DeVeeraApp.ViewModels.Common;

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
        private readonly ILayoutSetupService _layoutSetupService;
        private readonly INotificationService _notificationService;
        private readonly IVideoMasterService _videoServices;
        private readonly ILocalStringResourcesServices _localStringResourcesServices;
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
                              IUserService userService,
                              ILayoutSetupService layoutSetupService,
                              INotificationService notificationService,
                              IVideoMasterService videoService,
                              ILocalStringResourcesServices localStringResourcesServices
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
            _layoutSetupService = layoutSetupService;
            _notificationService = notificationService;
            _videoServices = videoService;
            _localStringResourcesServices = localStringResourcesServices;
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

        public IActionResult ExistingUser(int QuoteType, DateTime LastLoginDateUtc)
        {
            ViewBag.LastLoginDateUtc = LastLoginDateUtc;

            var level = new Level();
            var currentUser = _UserService.GetUserById(_workContext.CurrentUser.Id);

            var data = _weeklyUpdateServices.GetWeeklyUpdateByQuoteType((int)ViewModels.Quote.Login);

            var model = data?.ToModel<WeeklyUpdateModel>();
            _translationService.Translate(model.Title, key);
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
            if (model != null)
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
            _translationService.Translate(model.Title, key);
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
                    _translationService.Translate(item.Title, key);
                    _translationService.Translate(item.Story, key);
                    model.Add(item.ToModel<FeelGoodStoryModel>());
                }
            }
            return View(model);
        }

        #endregion

        #region Home Master
        public virtual void PrepareImages(LayoutSetupModel model)
        {
            model.AvailableVideo.Add(new SelectListItem { Text = "Select Video", Value = "0" });
            var AvailableVideoUrl = _videoServices.GetAllVideos();
            foreach (var url in AvailableVideoUrl)
            {
                model.AvailableVideo.Add(new SelectListItem
                {
                    Value = url.Id.ToString(),
                    Text = url.Name,
                    Selected = url.Id == model.VideoId
                });
            }
            //prepare available images
            var AvailableImages = _imageMasterService.GetAllImages();
            foreach (var item in AvailableImages)
            {
                model.AvailableImages.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name,
                });
            }
        }
        public virtual void PrepareImageUrls(LayoutSetupModel model)
        {
            model.AvailableVideo.Add(new SelectListItem { Text = "Select Video", Value = "0" });
            var AvailableVideoUrl = _videoServices.GetAllVideos();
            foreach (var url in AvailableVideoUrl)
            {
                model.AvailableVideo.Add(new SelectListItem
                {
                    Value = url.Id.ToString(),
                    Text = url.Name,
                    Selected = url.Id == model.VideoId
                });
            }
            model.SliderOneImageUrl = model.SliderOneImageId > 0 ? _imageMasterService.GetImageById(model.SliderOneImageId)?.ImageUrl : null;
            model.SliderTwoImageUrl = model.SliderTwoImageId > 0 ? _imageMasterService.GetImageById(model.SliderTwoImageId)?.ImageUrl : null;
            model.SliderThreeImageUrl = model.SliderThreeImageId > 0 ? _imageMasterService.GetImageById(model.SliderThreeImageId)?.ImageUrl : null;
            model.BannerOneImageUrl = model.BannerOneImageId > 0 ? _imageMasterService.GetImageById(model.BannerOneImageId)?.ImageUrl : null;
            model.BannerTwoImageUrl = model.BannerTwoImageId > 0 ? _imageMasterService.GetImageById(model.BannerTwoImageId)?.ImageUrl : null;
            model.DiaryHeaderImageUrl = model.DiaryHeaderImageId > 0 ? _imageMasterService.GetImageById(model.DiaryHeaderImageId)?.ImageUrl : null;
            model.CompleteRegistrationHeaderImgUrl = model.CompleteRegistrationHeaderImgId > 0 ? _imageMasterService.GetImageById(model.CompleteRegistrationHeaderImgId)?.ImageUrl : null;
            model.Link_1_BannerImageUrl = model.Link_1_BannerImageId > 0 ? _imageMasterService.GetImageById(model.Link_1_BannerImageId)?.ImageUrl : null;
            model.Link_2_BannerImageUrl = model.Link_2_BannerImageId > 0 ? _imageMasterService.GetImageById(model.Link_2_BannerImageId)?.ImageUrl : null;
            model.Link_3_BannerImageUrl = model.Link_3_BannerImageId > 0 ? _imageMasterService.GetImageById(model.Link_3_BannerImageId)?.ImageUrl : null;
            model.FooterImageUrl = model.FooterImageId > 0 ? _imageMasterService.GetImageById(model.FooterImageId)?.ImageUrl : null;
        }
        public IActionResult List()
        {
            AddBreadcrumbs("Home", "List", "/Admin/Home/List", "/Admin/Home/List");
            var model = new List<LayoutSetupModel>();
            var data = _layoutSetupService.GetAllLayoutSetups();
            if (data.Count() != 0)
            {
                model = data.ToList().ToModelList<LayoutSetup, LayoutSetupModel>(model);
                ViewBag.LayOut = JsonConvert.SerializeObject(model);
                return View(model);
            }
            return View(model);
        }

        public IActionResult Create()
        {
            AddBreadcrumbs("Home", "Create", "/Admin/Home/List", "/Admin/Home/Create");
            LayoutSetupModel model = new LayoutSetupModel();
            PrepareImages(model);
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(LayoutSetupModel model)
        {
            AddBreadcrumbs("Home", "Create", "/Admin/Home/Create", "/Admin/Home/Create");

            if (ModelState.IsValid)
            {
                model.VideoId = (model.VideoId == 0) ? model.VideoId = null : model.VideoId;
                var data = model.ToEntity<LayoutSetup>();

                _layoutSetupService.InsertLayoutSetup(data);
                _translationService.Translate(model.Title, model.ModuleSpanishTitle);
                _translationService.Translate(model.Description, model.ModuleSpanishDescription);
                _translationService.Translate(model.Title, model.HomeTitleSpanish);
                _translationService.Translate(model.HomeSubTitle, model.HomeSubTitleSpanish);
                _translationService.Translate(model.Description, model.HomeSpanishDescription);

                _notificationService.SuccessNotification("Home Created Successfully.");
                return RedirectToAction("List", "Home");
            }

            PrepareImages(model);
            return View(model);
        }

        public IActionResult Edit(int Id)
        {
            AddBreadcrumbs("Home", "Edit", $"/Admin/Home/List", $"/Admin/Home/Edit/{Id}");

            if (Id != 0)
            {
                var data = _layoutSetupService.GetLayoutSetupById(Id);
                if (data != null)
                {
                    var model = data.ToModel<LayoutSetupModel>();
                    model.BannerImageUrl = _imageMasterService.GetImageById(data.BannerImageId)?.ImageUrl;
                    model.VideoThumbImageUrl = _imageMasterService.GetImageById(data.VideoThumbImageId)?.ImageUrl;
                    model.ShareBackgroundImageUrl = _imageMasterService.GetImageById(data.ShareBackgroundImageId)?.ImageUrl;
                    model.ModuleSpanishTitle = _localStringResourcesServices.GetResourceValueByResourceNameScreen(model.Title);
                    model.ModuleSpanishDescription = _localStringResourcesServices.GetResourceValueByResourceNameScreen(model.Description);
                    model.HomeTitleSpanish = _localStringResourcesServices.GetResourceValueByResourceNameScreen(model.HomeTitle);
                    model.HomeSubTitleSpanish = _localStringResourcesServices.GetResourceValueByResourceNameScreen(model.HomeSubTitle);
                    model.HomeSpanishDescription = _localStringResourcesServices.GetResourceValueByResourceNameScreen(model.HomeDescription);
                    model.FooterDescriptionSpanish = _localStringResourcesServices.GetResourceValueByResourceNameScreen(model.FooterDescription);
                    model.LocationSpanish = _localStringResourcesServices.GetResourceValueByResourceNameScreen(model.Location);
                    PrepareImageUrls(model);
                    return View(model);
                }

            }
            return View();
        }

        [HttpPost]
        public IActionResult Edit(LayoutSetupModel model)
        {
            AddBreadcrumbs("Home", "Edit", $"/Admin/Home/List", $"/Home/Edit/{model.Id}");

            if (ModelState.IsValid)
            {
                model.VideoId = (model.VideoId == 0) ? model.VideoId = null : model.VideoId;
                var data = _layoutSetupService.GetLayoutSetupById(model.Id);
 
                data.IsActive = model.IsActive;
                data.Location = model.Location;
                data.Description = model.Description;
                data.PhoneNo = model.PhoneNo;
                data.FooterImageId = model.FooterImageId;
                data.FooterDescription = model.FooterDescription;
                data.Title = model.Title;
                data.HomeDescription = model.HomeDescription;
                data.HomeTitle = model.HomeTitle;
                data.HomeSubTitle = model.HomeSubTitle;
                data.VideoId = model.VideoId;
                data.BannerImageId = model.BannerImageId;
                data.VideoThumbImageId = model.VideoThumbImageId;
                data.ShareBackgroundImageId = model.ShareBackgroundImageId;

                _layoutSetupService.UpdateLayoutSetup(data);
                _translationService.Translate(model.Title, model.ModuleSpanishTitle);
                _translationService.Translate(model.Description, model.ModuleSpanishDescription);
                _translationService.Translate(model.HomeTitle, model.HomeTitleSpanish);
                _translationService.Translate(model.HomeSubTitle, model.HomeSubTitleSpanish);
                _translationService.Translate(model.HomeDescription, model.HomeSpanishDescription);
                _translationService.Translate(model.Location, model.LocationSpanish);
                _translationService.Translate(model.FooterDescription, model.FooterDescriptionSpanish);
                _notificationService.SuccessNotification("Home Page Updated Successfully.");
                return RedirectToAction("List");
            }

            PrepareImageUrls(model);
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            ResponseModel response = new ResponseModel();

            if (id != 0)
            {
                var Data = _layoutSetupService.GetLayoutSetupById(id);
                if (Data == null)
                {
                    response.Success = false;
                    response.Message = "No data found";
                }
                _layoutSetupService.DeleteLayoutSetup(Data);

                response.Success = true;
            }
            else
            {
                response.Success = false;
                response.Message = "Id is 0";

            }
            return Json(response);
        }


        #endregion
    }

}

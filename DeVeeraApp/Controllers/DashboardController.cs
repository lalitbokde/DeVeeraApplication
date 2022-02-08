using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DeVeeraApp.Utils;
using CRM.Services.Users;
using CRM.Services;
using DeVeeraApp.ViewModels;
using CRM.Services.DashboardQuotes;
using CRM.Core;
using Microsoft.AspNetCore.Http;
using CRM.Services.Authentication;
using CRM.Services.DashboardMenu;
using CRM.Services.Layoutsetup;
using DeVeeraApp.ViewModels.LayoutSetups;
using CRM.Services.Settings;
using System;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeVeeraApp.Controllers
{

    public class DashboardController : BaseController
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
        private readonly ILevelImageListServices _levelImageListServices;
        private readonly IS3BucketService _s3BucketService;
        private readonly ILayoutSetupService _LayoutSetupService;
        private readonly ISettingService _settingService;
        private readonly ILocalStringResourcesServices _localStringResourcesServices;
        #endregion


        #region ctor
        public DashboardController(ILogger<HomeController> logger,
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
                              ILevelImageListServices levelImageListServices,
                              IS3BucketService s3BucketService,
                              IUserService userService,
                              ISettingService settingService,
                              ILayoutSetupService layoutSetupService,
                                ILocalStringResourcesServices localStringResourcesServices
                              ) : base(workContext: workContext,
                                                                                  httpContextAccessor: httpContextAccessor,
                                                                                  authenticationService: authenticationService)
        {
            _logger = logger;
            _localStringResourcesServices = localStringResourcesServices;
            _levelServices = levelServices;
            _weeklyUpdateServices = weeklyUpdateServices;
            _dashboardQuoteService = dashboardQuoteService;
            _dashboardMenuService = dashboardMenuService;
            _UserService = userService;
            _workContext = workContext;
            _feelGoodStoryServices = feelGoodStoryServices;
            _imageMasterService = imageMasterService;
            _videoMasterService = videoMasterService;
            _levelImageListServices = levelImageListServices;
            _s3BucketService = s3BucketService;
            _LayoutSetupService = layoutSetupService;
            _settingService = settingService;
        }

        #endregion

        // GET: /<controller>/

        //(Changes in redirecting new/exististing user to home page)
        public IActionResult Index()
        {
            try { 
            var currentUser = _UserService.GetUserById(_workContext.CurrentUser.Id);

            var model = new DashboardQuoteModel();

            var result = _LayoutSetupService.GetAllLayoutSetups().FirstOrDefault();
            var userLanguage = _settingService.GetAllSetting().Where(s => s.UserId == currentUser.Id).FirstOrDefault();
            if (result != null)
            {
                
                model.layoutSetup = result.ToModel<LayoutSetupModel>();
                model.layoutSetup.SliderOneImageUrl = result.SliderOneImageId > 0 ? _imageMasterService.GetImageById(result.SliderOneImageId)?.ImageUrl : null;
                model.layoutSetup.SliderTwoImageUrl = result.SliderTwoImageId > 0 ? _imageMasterService.GetImageById(result.SliderTwoImageId)?.ImageUrl : null;
                model.layoutSetup.SliderThreeImageUrl = result.SliderThreeImageId > 0 ? _imageMasterService.GetImageById(result.SliderThreeImageId)?.ImageUrl : null;
                var imagesRecord = _imageMasterService.GetImageById(result.BannerImageId);
                if (userLanguage.LanguageId == 5) { 
                model.layoutSetup.BannerImageUrl = imagesRecord?.SpanishImageUrl!=null?imagesRecord?.SpanishImageUrl:imagesRecord?.ImageUrl;
                }
                else
                {
                    model.layoutSetup.BannerImageUrl = imagesRecord?.ImageUrl;
                }
                var imagesRecord1 = _imageMasterService.GetImageById(result.VideoThumbImageId);
                if (userLanguage.LanguageId == 5)
                {
                    model.layoutSetup.VideoThumbImageUrl = imagesRecord1?.SpanishImageUrl!=null ? imagesRecord1?.SpanishImageUrl : imagesRecord1?.ImageUrl;
                }
                else
                {
                    model.layoutSetup.VideoThumbImageUrl = imagesRecord1?.ImageUrl;
                }
               

                var imagesRecord2 = _imageMasterService.GetImageById(result.ShareBackgroundImageId);
                if (userLanguage.LanguageId == 5)
                {
                    model.layoutSetup.ShareBackgroundImageUrl = imagesRecord2?.SpanishImageUrl!=null? imagesRecord2?.SpanishImageUrl: imagesRecord2?.ImageUrl;
                }
                else
                {
                    model.layoutSetup.ShareBackgroundImageUrl = imagesRecord2?.ImageUrl;
                }
               
                model.layoutSetup.HomeTitle = result.HomeTitle;
                model.layoutSetup.HomeSubTitle = result.HomeSubTitle;
                if (model.layoutSetup.VideoId != null)
                {
                    var videoRecord = _videoMasterService.GetVideoById((int)model.layoutSetup.VideoId);
                    var videoUrl = _s3BucketService.GetPreSignedURL(videoRecord.Key);
                    model.VideoUrl = videoUrl;
                    model.VideoId = (int)model.layoutSetup.VideoId;
                }
               
                if (userLanguage != null)
                {
                    if (userLanguage.LanguageId == 5)
                    {
                        model.layoutSetup.HomeDescription = _localStringResourcesServices.GetResourceValueByResourceName(result.HomeDescription);

                    }
                    else
                    {
                        model.layoutSetup.HomeDescription = result.HomeDescription;
                    }
                }
                else if (_workContext.CurrentUser.UserRole.Name == "User")
                {
                    model.layoutSetup.HomeDescription = result.HomeDescription;
                }
                else if (_workContext.CurrentUser.UserRole.Name == "Admin")
                {
                    model.layoutSetup.HomeDescription = result.HomeDescription;
                }
            }

            var quote = _dashboardQuoteService.GetAllDashboardQuotes().Where(a => a.IsDashboardQuote == true).FirstOrDefault();

            model.Title = quote?.Title;

            model.Author = quote?.Author;

            model.Menus = _dashboardMenuService.GetAllDashboardMenus().FirstOrDefault();

            var data = _levelServices.GetAllLevels().OrderBy(l => l.LevelNo);

            if (data.Count() != 0)
            {
                if (currentUser.LastLevel == null)
                {
                    foreach (var item in data)
                    {
                        if (item.LevelNo <= 1)
                        {

                            model.VideoModelList.Add(item.ToModel<LevelModel>());

                            if (model.VideoModelList.Count() > 0)
                            {
                                foreach (var level in model.VideoModelList)
                                {
                                    var leveldata = _levelServices.GetLevelByLevelNo(level.LevelNo ?? 1);
                                    if (leveldata != null)
                                    {
                                        var img = _imageMasterService.GetImageById(leveldata.BannerImageId);
                                        if (img != null)
                                        {
                                            if (userLanguage?.LanguageId == 5)
                                            {
                                                level.BannerImageUrl = img.SpanishImageUrl!=null? img.SpanishImageUrl: img.ImageUrl;
                                            }
                                            else
                                            {
                                                level.BannerImageUrl = img.ImageUrl;
                                            }
                                               
                                        }

                                    }

                                }
                            }

                            if (item.LevelNo == 1)
                            {
                                break;
                            }

                        }

                    }
                    return View(model);
                } 
                else
                {
                    var LevelOne = data.FirstOrDefault();
                    var lastLevel = _levelServices.GetLevelById((int)currentUser.LastLevel)?.LevelNo;
                    if (lastLevel != null)
                    {
                        var levelget = _levelServices.GetLevelByLevelNo(lastLevel ?? 1);
                       
                        foreach (var item in data)
                        {
                            if (item.LevelNo <= lastLevel)
                            {

                                model.VideoModelList.Add(item.ToModel<LevelModel>());

                                if (model.VideoModelList.Count() > 0)
                                {
                                    foreach (var level in model.VideoModelList)
                                    {
                                        var leveldata = _levelServices.GetLevelByLevelNo(level.LevelNo ?? 1);
                                        if (leveldata != null)
                                        {
                                            var img = _imageMasterService.GetImageById(leveldata.BannerImageId);
                                            if (img != null)
                                            {
                                                if (userLanguage?.LanguageId == 5)
                                                {
                                                    level.BannerImageUrl = img.SpanishImageUrl!=null? img.SpanishImageUrl: img.ImageUrl;
                                                }
                                                else
                                                {
                                                    level.BannerImageUrl = img.ImageUrl;
                                                }
                                                
                                            }

                                        }

                                    }
                                }

                                if (item.LevelNo == lastLevel)
                                {
                                    break;
                                }

                            }

                        }
                    }
                }
                return View(model);
            }


            }
            catch(Exception ex)
            {

            }
            return View();
        }



        #region oldCode
        //public IActionResult Index()
        //{

        //    var currentUser = _UserService.GetUserById(_workContext.CurrentUser.Id);

        //    var model = new DashboardQuoteModel();

        //    var result = _LayoutSetupService.GetAllLayoutSetups().FirstOrDefault();

        //    if (result != null)
        //    {
        //        model.layoutSetup = result.ToModel<LayoutSetupModel>();
        //        model.layoutSetup.SliderOneImageUrl = result.SliderOneImageId > 0 ? _imageMasterService.GetImageById(result.SliderOneImageId)?.ImageUrl : null;
        //        model.layoutSetup.SliderTwoImageUrl = result.SliderTwoImageId > 0 ? _imageMasterService.GetImageById(result.SliderTwoImageId)?.ImageUrl : null;
        //        model.layoutSetup.SliderThreeImageUrl = result.SliderThreeImageId > 0 ? _imageMasterService.GetImageById(result.SliderThreeImageId)?.ImageUrl : null;
        //    }

        //    var quote = _dashboardQuoteService.GetAllDashboardQuotes().Where(a => a.IsDashboardQuote == true).FirstOrDefault();

        //    model.Title = quote?.Title;
        //    model.Author = quote?.Author;

        //    model.Menus = _dashboardMenuService.GetAllDashboardMenus().FirstOrDefault();

        //    var data = _levelServices.GetAllLevels().OrderBy(l => l.LevelNo);

        //    if (data.Count() != 0)
        //    {
        //        var LevelOne = data.FirstOrDefault();

        //        var lastLevel = _levelServices.GetLevelById((int)currentUser.LastLevel)?.LevelNo;

        //        foreach (var item in data)
        //        {
        //            if (item.LevelNo <= lastLevel)
        //            {

        //                model.VideoModelList.Add(item.ToModel<LevelModel>());

        //                if (model.VideoModelList.Count() > 0)
        //                {
        //                    foreach (var level in model.VideoModelList)
        //                    {
        //                        var leveldata = _levelServices.GetLevelByLevelNo(level.LevelNo ?? 1);
        //                        if (leveldata != null)
        //                        {
        //                            var img = _imageMasterService.GetImageById(leveldata.BannerImageId);
        //                            if (img != null)
        //                            {
        //                                level.BannerImageUrl = img.ImageUrl;
        //                            }

        //                        }

        //                    }
        //                }

        //                if (item.LevelNo == lastLevel)
        //                {
        //                    break;
        //                }

        //            }

        //        }


        //        return View(model);
        //    }
        //    return View();
        //}
        #endregion
    }
}

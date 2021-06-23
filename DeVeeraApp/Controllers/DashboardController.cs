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
using CRM.Services.Layoutsetup;
using DeVeeraApp.ViewModels.LayoutSetups;

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
                              ILayoutSetupService layoutSetupService
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
            _levelImageListServices = levelImageListServices;
            _s3BucketService = s3BucketService;
            _LayoutSetupService = layoutSetupService;
        }

        #endregion

        // GET: /<controller>/
        public IActionResult Index()
        {
           
            var currentUser = _UserService.GetUserById(_workContext.CurrentUser.Id);

            var model = new DashboardQuoteModel();

            var result = _LayoutSetupService.GetAllLayoutSetups().FirstOrDefault();

            if (result != null)
            {
                model.layoutSetup = result.ToModel<LayoutSetupModel>();
                model.layoutSetup.SliderOneImageUrl= result.SliderOneImageId > 0 ? _imageMasterService.GetImageById(result.SliderOneImageId)?.ImageUrl : null;
                model.layoutSetup.SliderTwoImageUrl = result.SliderTwoImageId > 0 ? _imageMasterService.GetImageById(result.SliderTwoImageId)?.ImageUrl : null;
                model.layoutSetup.SliderThreeImageUrl = result.SliderThreeImageId > 0 ? _imageMasterService.GetImageById(result.SliderThreeImageId)?.ImageUrl : null;
            }

            var quote = _dashboardQuoteService.GetAllDashboardQuotes().Where(a => a.IsDashboardQuote == true).FirstOrDefault();

            model.Title = quote?.Title;
            model.Author = quote?.Author;

            model.Menus = _dashboardMenuService.GetAllDashboardMenus().FirstOrDefault();

            var data = _levelServices.GetAllLevels().OrderBy(l => l.LevelNo);
           
            if (data.Count() != 0)
            {                
                        var LevelOne = data.FirstOrDefault();

                        var lastLevel = _levelServices.GetLevelById((int)currentUser.LastLevel)?.LevelNo;

                        foreach (var item in data)
                        {
                            if (item.LevelNo <= lastLevel)
                            {
                                
                                model.VideoModelList.Add(item.ToModel<LevelModel>());

                                if(model.VideoModelList.Count() > 0)
                                {
                                    foreach(var level in model.VideoModelList)
                                    {
                                        var leveldata = _levelServices.GetLevelByLevelNo(level.LevelNo??1);
                                        if(leveldata != null)
                                        {

                                                    var img = _imageMasterService.GetImageById(leveldata.BannerImageId);
                                            if (img != null) {
                                                var levelImage = new SelectedImage();
                                                levelImage.ImageId = img.Id;
                                                levelImage.ImageUrl = img.ImageUrl;
                                                levelImage.Key = img.Key;
                                                levelImage.Name = img.Name;
                                                level.SelectedImages.Add(levelImage);
                                            }
                                            var img1 = _imageMasterService.GetImageById(leveldata.VideoThumbImageId);
                                            if (img1 != null)
                                            {
                                                var levelImage1 = new SelectedImage();
                                                levelImage1.ImageId = img1.Id;
                                                levelImage1.ImageUrl = img1.ImageUrl;
                                                levelImage1.Key = img1.Key;
                                                levelImage1.Name = img1.Name;
                                                level.SelectedImages.Add(levelImage1);
                                            }

                                            var img2 = _imageMasterService.GetImageById(leveldata.ShareBackgroundImageId);
                                            if (img2 != null)
                                            {
                                                var levelImage2 = new SelectedImage();
                                                levelImage2.ImageId = img2.Id;
                                                levelImage2.ImageUrl = img2.ImageUrl;
                                                levelImage2.Key = img2.Key;
                                                levelImage2.Name = img2.Name;
                                                level.SelectedImages.Add(levelImage2);
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
              
                
                return View(model);
            }
            return View();
        }
    }
}

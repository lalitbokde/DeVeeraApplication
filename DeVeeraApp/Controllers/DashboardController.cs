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
        private readonly IS3BucketService _s3BucketService;

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
                              IS3BucketService s3BucketService,
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
        }

        #endregion

        // GET: /<controller>/
        public IActionResult Index()
        {
           
            var currentUser = _UserService.GetUserById(_workContext.CurrentUser.Id);

            var model = new DashboardQuoteModel();

            var quote = _dashboardQuoteService.GetAllDashboardQuotes().Where(a => a.IsDashboardQuote == true).FirstOrDefault();

            model.Title = quote?.Title;
            model.Author = quote?.Author;

            model.Menus = _dashboardMenuService.GetAllDashboardMenus().FirstOrDefault();

            var data = _levelServices.GetAllLevels().OrderBy(l => l.LevelNo);

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
    }
}

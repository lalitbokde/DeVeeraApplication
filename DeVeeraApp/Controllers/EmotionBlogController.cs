using CRM.Core;
using CRM.Services;
using CRM.Services.Authentication;
using CRM.Services.Customers;
using CRM.Services.DashboardQuotes;
using CRM.Services.Emotions;
using CRM.Services.Message;
using CRM.Services.Users;
using CRM.Services.VideoModules;
using DeVeeraApp.Utils;
using DeVeeraApp.ViewModels.Emotions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace DeVeeraApp.Controllers
{

    public class EmotionBlogController : BaseController
    {
        #region field

        private readonly INotificationService _notificationService;
        private readonly IDiaryMasterService _DiaryMasterService;
        private readonly IWorkContext _workContext;
        private readonly ILevelServices _levelServices;
        private readonly IModuleService _moduleService;
        private readonly IUserService _userService;
        private readonly IEmotionService _emotionService;
        private readonly IEmotionMappingService _emotionMappingService;
        private readonly IDiaryPasscodeService _diaryPasscodeService;
        private readonly IVideoMasterService _videoMasterService;
        private readonly IImageMasterService _imageMasterService;
        private readonly IDashboardQuoteService _dashboardQuoteService;

        #endregion


        #region ctor

        public EmotionBlogController(INotificationService notificationService,
                       IDiaryMasterService DiaryMasterService,
                       IWorkContext workContext,
                       ILevelServices levelServices,
                       IModuleService moduleService,
                       IUserService userService,
                       IEmotionService emotionService,
                       IImageMasterService imageMasterService,
                       IEmotionMappingService emotionMappingService,
                       IDiaryPasscodeService diaryPasscodeService,
                        IHttpContextAccessor httpContextAccessor,
                        IVideoMasterService videoMasterService,
                        IDashboardQuoteService dashboardQuoteService,
                               IAuthenticationService authenticationService
                               ) : base(workContext: workContext,
                                    httpContextAccessor: httpContextAccessor,
                                    authenticationService: authenticationService)
        {
            _notificationService = notificationService;
            _DiaryMasterService = DiaryMasterService;
            _workContext = workContext;
            _levelServices = levelServices;
            _moduleService = moduleService;
            _userService = userService;
            _emotionService = emotionService;
            _emotionMappingService = emotionMappingService;
            _diaryPasscodeService = diaryPasscodeService;
            _videoMasterService = videoMasterService;
            _imageMasterService = imageMasterService;
            _dashboardQuoteService = dashboardQuoteService;
        }

        #endregion



        #region Method

        public IActionResult Index(int emotionid)
        {
            var random = new Random();
            var emotion = _emotionService.GetEmotionById(emotionid);
            var model = new EmotionModel();
            if (emotion != null)
            {
                 model = emotion.ToModel<EmotionModel>();
                 model.EmotionHeaderImageUrl = emotion.EmotionHeaderImageId > 0 ?_imageMasterService.GetImageById(emotion.EmotionHeaderImageId)?.ImageUrl:null;
                 model.EmotionBannerImageUrl = emotion.EmotionBannerImageId > 0 ?_imageMasterService.GetImageById(emotion.EmotionBannerImageId)?.ImageUrl:null;
                 model.EmotionThumbnailImageUrl = emotion.EmotionThumbnailImageId> 0 ? _imageMasterService.GetImageById(emotion.EmotionThumbnailImageId)?.ImageUrl:null;
                 model.Video = _videoMasterService.GetVideoById(emotion.VideoId);
                var quoteList = _dashboardQuoteService.GetAllDashboardQuotes().Where(a => a.IsRandom == true).ToList();
                if (quoteList != null && quoteList.Count > 0 && emotion.IsRandom == true)
                {
                    int index = random.Next(quoteList.Count);
                    model.Quote = quoteList[index].Title + " -- " + quoteList[index].Author;
                }
            }
            return View(model);
        }

        #endregion


    }
}

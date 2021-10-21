using CRM.Core;
using CRM.Services;
using CRM.Services.Authentication;
using CRM.Services.Customers;
using CRM.Services.DashboardQuotes;
using CRM.Services.Emotions;
using CRM.Services.Message;
using CRM.Services.Settings;
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
        private readonly ILocalStringResourcesServices _localStringResourcesServices;
        private readonly ISettingService _settingService;
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
                        ISettingService settingService,
                        ILocalStringResourcesServices localStringResourcesServices,
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
            _settingService = settingService;
            _localStringResourcesServices = localStringResourcesServices;
        }

        #endregion



        #region Method

        public IActionResult Index(int emotionid)
        {
            var random = new Random();
            int UserId = _workContext.CurrentUser.Id;

            var user = _userService.GetUserById(UserId);
            var userLanguage = _settingService.GetAllSetting().Where(s => s.UserId == UserId).FirstOrDefault();
            var passcode = _diaryPasscodeService.GetDiaryPasscodeByUserId(UserId).FirstOrDefault();
            var emotion = _emotionService.GetEmotionById(emotionid);
            var model = new EmotionModel();
            if (emotion != null)
            {

                 model = emotion.ToModel<EmotionModel>();
                 model.EmotionHeaderImageUrl = emotion.EmotionHeaderImageId > 0 ?_imageMasterService.GetImageById(emotion.EmotionHeaderImageId)?.ImageUrl:null;
                 model.EmotionBannerImageUrl = emotion.EmotionBannerImageId > 0 ?_imageMasterService.GetImageById(emotion.EmotionBannerImageId)?.ImageUrl:null;
                 model.EmotionThumbnailImageUrl = emotion.EmotionThumbnailImageId> 0 ? _imageMasterService.GetImageById(emotion.EmotionThumbnailImageId)?.ImageUrl:null;
                 model.Video = _videoMasterService.GetVideoById(emotion.VideoId);
                if (userLanguage != null)
                {
                    if (userLanguage.LanguageId == 5)
                    {
                        model.Title = _localStringResourcesServices.GetResourceValueByResourceName(model.Title);
                        model.Subtitle = _localStringResourcesServices.GetResourceValueByResourceName(model.Subtitle);
                        model.Description = _localStringResourcesServices.GetResourceValueByResourceName(model.Description);

                    }
                    else
                    {
                        model.Title = emotion.Title;
                        model.Subtitle = emotion.Subtitle;
                        model.Description = emotion.Description;
                    }
                }
               
                var quoteList = _dashboardQuoteService.GetAllDashboardQuotes().Where(a => a.IsRandom == true).ToList();
                if (quoteList != null && quoteList.Count > 0 && emotion.IsRandom == true)
                {
                    int index = random.Next(quoteList.Count);
                    model.Quote = quoteList[index].Title + " -- " + quoteList[index].Author;
                }
                foreach ( var diar in diary) { 
                diar.EmotionId = emotion.Id;
                _DiaryMasterService.UpdateDiary(diar);
                }
            }
            return View(model);
        }

        #endregion


    }
}

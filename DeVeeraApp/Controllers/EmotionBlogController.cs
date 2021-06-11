using CRM.Core;
using CRM.Core.Domain;
using CRM.Core.Domain.Emotions;
using CRM.Core.Domain.VideoModules;
using CRM.Services;
using CRM.Services.Authentication;
using CRM.Services.Customers;
using CRM.Services.Emotions;
using CRM.Services.Message;
using CRM.Services.Users;
using CRM.Services.VideoModules;
using DeVeeraApp.Filters;
using DeVeeraApp.Utils;
using DeVeeraApp.ViewModels;
using DeVeeraApp.ViewModels.Common;
using DeVeeraApp.ViewModels.Diaries;
using DeVeeraApp.ViewModels.Emotions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        }

        #endregion



        #region Method

        public IActionResult Index(int emotionid)
        {

            var emotion = _emotionService.GetEmotionById(emotionid);
            var model = new EmotionModel();
            if (emotion != null)
            {
                 model = emotion.ToModel<EmotionModel>();
                 model.ContentImageUrl = emotion.ContentImageId > 0 ?_imageMasterService.GetImageById(emotion.ContentImageId)?.ImageUrl:null;
                 model.BannerImageUrl = emotion.BannerImageId > 0 ?_imageMasterService.GetImageById(emotion.BannerImageId)?.ImageUrl:null;
                 model.ThumbnailImageUrl = emotion.ThumbnailImageId> 0 ? _imageMasterService.GetImageById(emotion.ThumbnailImageId)?.ImageUrl:null;
                 model.Video = _videoMasterService.GetVideoById(emotion.VideoId);

            }
            return View(model);
        }

        #endregion


    }
}

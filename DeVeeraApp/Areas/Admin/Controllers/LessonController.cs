using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DeVeeraApp.Filters;
using CRM.Services;
using DeVeeraApp.ViewModels;
using DeVeeraApp.Utils;
using CRM.Core;
using CRM.Services.Authentication;
using Microsoft.AspNetCore.Http;

using CRM.Services.VideoModules;
using CRM.Services.Users;
using CRM.Core.Domain.VideoModules;
using CRM.Services.DashboardQuotes;
using CRM.Services.Localization;

namespace DeVeeraApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthorizeAdmin]
    public class LessonController : BaseController
    {
        #region fields
        private readonly ILogger<LessonController> _logger;
        private readonly ILevelServices _levelServices;
        private readonly IVideoMasterService _videoMasterService;
        private readonly IImageMasterService _imageMasterService;
        private readonly IModuleService _moduleServices;
        private readonly IS3BucketService _s3BucketService;
        private readonly IUserService _userService;
        private readonly IWorkContext _workContext;
        private readonly ILevelImageListServices _levelImageListServices;
        private readonly IDiaryMasterService _diaryMasterService;
        private readonly IDashboardQuoteService _dashboardQuoteService;
        private readonly ITranslationService _translationService;

        public string key = "AIzaSyC2wpcQiQQ7ASdt4vcJHfmly8DwE3l3tqE";
        #endregion


        #region ctor
        public LessonController(ILogger<LessonController> logger,
                                ILevelServices levelServices,
                                IVideoMasterService videoMasterService,
                                IImageMasterService imageMasterService,
                                IModuleService moduleService,
                                IS3BucketService s3BucketService,
                                IUserService userService,
                                IDashboardQuoteService dashboardQuoteService,
                                IWorkContext workContext,
                                IHttpContextAccessor httpContextAccessor,
                                IAuthenticationService authenticationService,
                                ILevelImageListServices levelImageListServices,
                                ITranslationService translationService,
                                IDiaryMasterService diaryMasterService) : base(workContext: workContext,
                                                                                  httpContextAccessor: httpContextAccessor,
                                                                                  authenticationService: authenticationService)
        {
            _logger = logger;
            _levelServices = levelServices;
            _videoMasterService = videoMasterService;
            _imageMasterService = imageMasterService;
            _moduleServices = moduleService;
            _s3BucketService = s3BucketService;
            _userService = userService;
            _workContext = workContext;
            _levelImageListServices = levelImageListServices;
            _diaryMasterService = diaryMasterService;
            _dashboardQuoteService = dashboardQuoteService;
            _translationService = translationService;

        }

        #endregion

        #region Utilities     
        public bool IsUserFirstLoginOnDay(DateTime? lastLoginDateUtc)
        {
            var currentUser = _userService.GetUserById(_workContext.CurrentUser.Id);

            var currentDate = DateTime.UtcNow.ToShortDateString();

          //  var lastLoginDate = lastLoginDateUtc.Value.ToShortDateString();

            if (currentUser.UserRole.Name != "Admin")
            {
                if( lastLoginDateUtc != null  && currentDate != lastLoginDateUtc.Value.ToShortDateString() )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            
        }
        #endregion

        #region Method
        public IActionResult Index(int levelno, DateTime? lastLoginDateUtc)
        {
            var random = new Random();
           
            ViewBag.TotalLevels = _levelServices.GetAllLevels().Where(a => a.Active == true).ToList().Count;
            var videoData = new LevelModel
            {
                SelectedImages = new List<SelectedImage>()
            };
            AddBreadcrumbs("Level", "Index", $"/Lesson/Index/{levelno}", $"/Lesson/Index/{levelno}");

            var result= IsUserFirstLoginOnDay(lastLoginDateUtc);
            if (result == true) 
            {
                return RedirectToAction("AskUserEmotion", "Diary");
            }
            var data = _levelServices.GetLevelByLevelNo(levelno);
            var levelImages = _levelImageListServices.GetLevelImageListByLevelId(data.Id);

            if(levelImages.Count != 0)
            {
                foreach(var item in levelImages)
                {
                    var seletedImages = new SelectedImage();
                    var imagesRecord = _imageMasterService.GetImageById(item.Image.Id);
                    var imageUrl = _s3BucketService.GetPreSignedURL(imagesRecord.Key);
                    imagesRecord.ImageUrl = imageUrl;
                    _imageMasterService.UpdateImage(imagesRecord);
                    seletedImages.ImageUrl = imagesRecord.ImageUrl;
                    seletedImages.Key = imagesRecord.Key;
                    seletedImages.Name = imagesRecord.Name;
                    seletedImages.ImageId = imagesRecord.Id;
                    videoData.SelectedImages.Add(seletedImages);

                }
            }

            if (data.VideoId != null) { 
            var videoRecord = _videoMasterService.GetVideoById((int)data.VideoId);

            var videoUrl =  _s3BucketService.GetPreSignedURL(videoRecord.Key);

            videoRecord.VideoUrl = videoUrl;

            _videoMasterService.UpdateVideo(videoRecord);
            }
            var updatedVideoData = _levelServices.GetLevelByLevelNo(levelno);
            videoData.Id = updatedVideoData.Id;
            videoData.FullDescription = updatedVideoData.FullDescription;
            videoData.Video = updatedVideoData.Video;
          
            videoData.Subtitle = updatedVideoData.Subtitle;
            videoData.Title = updatedVideoData.Title;
            videoData.LevelNo = updatedVideoData.LevelNo;


            var quoteList = _dashboardQuoteService.GetAllDashboardQuotes().Where(a => a.IsRandom == true).ToList();
            quoteList = quoteList.Where(a => a.LevelId == data.Id || a.Level == "All Level").ToList();

            if (quoteList != null && quoteList.Count > 0)
            {
                int index = random.Next(quoteList.Count);
                videoData.Quote = quoteList[index].Title;
                videoData.Author = quoteList[index].Author;
            }
            Diary diary = new Diary();
            if (_workContext.CurrentUser.UserRole.Name == "Admin")
            {
                diary = _diaryMasterService.GetAllDiarys().OrderByDescending(a => a.Id).FirstOrDefault();
            }
            else
            {
                diary = _diaryMasterService.GetAllDiarys().Where(a=>a.UserId == _workContext.CurrentUser.Id).OrderByDescending(a => a.Id).FirstOrDefault();

            }
            videoData.DiaryText = diary != null ? diary.Comment : "";
            videoData.DiaryLatestUpdateDate = diary != null ? diary.CreatedOn.ToShortDateString() : "";
            var moduleList = _moduleServices.GetModulesByLevelId(data.Id);
            videoData.ModuleList = moduleList.ToList().ToModelList<Modules, ModulesModel>(videoData.ModuleList.ToList());
            _translationService.Translate(videoData.Title,key);
            _translationService.Translate(videoData.Subtitle, key);
            _translationService.Translate(videoData.Quote, key);
            _translationService.Translate(videoData.Author, key);
            _translationService.Translate(videoData.FullDescription, key);
            return View(videoData);
        }

        #endregion
    }
}

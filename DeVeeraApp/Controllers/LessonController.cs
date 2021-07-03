using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CRM.Services;
using DeVeeraApp.ViewModels;
using DeVeeraApp.Utils;
using ErrorViewModel = DeVeeraApp.Models.ErrorViewModel;
using CRM.Core;
using CRM.Services.Authentication;
using Microsoft.AspNetCore.Http;

using CRM.Services.VideoModules;
using CRM.Services.Users;
using CRM.Core.Domain.VideoModules;
using CRM.Services.DashboardQuotes;

namespace DeVeeraApp.Controllers
{


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
        private readonly IModuleImageListService _moduleImageListService;
        private readonly IDiaryMasterService _diaryMasterService;
        private readonly IDashboardQuoteService _dashboardQuoteService;

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
                                IModuleImageListService moduleImageListService,
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
            _moduleImageListService = moduleImageListService;
            _diaryMasterService = diaryMasterService;
            _dashboardQuoteService = dashboardQuoteService;

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
                if (lastLoginDateUtc != null && currentDate != lastLoginDateUtc.Value.ToShortDateString())
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
        public IActionResult Index(int levelno,DateTime? lastLoginDateUtc)
        {
            var random = new Random();
           
            var AllLevels = _levelServices.GetAllLevels().ToList();
            ViewBag.TotalLevels = AllLevels.Count;
            var videoData = new LevelModel
            {
                SelectedImages = new List<SelectedImage>()
            };
            AddBreadcrumbs("Level", "Index", $"/Lesson/Index/{levelno}", $"/Lesson/Index/{levelno}");
            var result = IsUserFirstLoginOnDay(lastLoginDateUtc);
            if (result == true)
            {
                return RedirectToAction("AskHappynessLevel", "Home");
            }
            var data = _levelServices.GetLevelByLevelNo(levelno);
            var imagesRecord = _imageMasterService.GetImageById(data.BannerImageId);
            videoData.BannerImageUrl = imagesRecord?.ImageUrl;

            var imagesRecord1 = _imageMasterService.GetImageById(data.VideoThumbImageId);
            videoData.VideoThumbImageUrl = imagesRecord1?.ImageUrl;

            var imagesRecord2 = _imageMasterService.GetImageById(data.ShareBackgroundImageId);
            videoData.ShareBackgroundImageUrl = imagesRecord2?.ImageUrl;

            if (data.VideoId != null)
            {
                var videoRecord = _videoMasterService.GetVideoById((int)data.VideoId);

                var videoUrl = _s3BucketService.GetPreSignedURL(videoRecord.Key);

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
                diary = _diaryMasterService.GetAllDiarys().Where(a => a.UserId == _workContext.CurrentUser.Id).OrderByDescending(a => a.Id).FirstOrDefault();

            }
            videoData.DiaryText = diary != null ? diary.Comment : "";
            videoData.DiaryLatestUpdateDate = diary != null ? diary.CreatedOn.ToShortDateString() : "";
            var moduleList = _moduleServices.GetModulesByLevelId(data.Id);
            videoData.ModuleList = moduleList.ToList().ToModelList<Modules, ModulesModel>(videoData.ModuleList.ToList());
            foreach (var module in videoData.ModuleList)
            {
                var seletedImages5 = new SelectedImage();
                var imagesRecord5 = _imageMasterService.GetImageById(data.BannerImageId);
                if (imagesRecord5 != null)
                {
                    seletedImages5.ImageUrl = imagesRecord5.ImageUrl;
                    seletedImages5.Key = imagesRecord5.Key;
                    seletedImages5.Name = imagesRecord5.Name;
                    seletedImages5.ImageId = imagesRecord5.Id;
                    module.SelectedModuleImages.Add(seletedImages5);
                }

            }

            var userNextLevel = AllLevels.Where(a => a.LevelNo > levelno).FirstOrDefault();

            if (userNextLevel != null)
            {
                videoData.NextTitle = userNextLevel?.Title;
                var level = _imageMasterService.GetImageById(userNextLevel.BannerImageId);
                videoData.NextImageUrl = level?.ImageUrl;

            }

            var userPreviousLevel = AllLevels.OrderByDescending(a => a.LevelNo).Where(a => a.LevelNo < levelno).FirstOrDefault();

            if (userPreviousLevel != null)
            {
                videoData.PrevTitle = userPreviousLevel?.Title;
                var level = _imageMasterService.GetImageById(userPreviousLevel.BannerImageId);
                videoData.PrevImageUrl = level?.ImageUrl;
            }


            return View(videoData);
        }



        public IActionResult Previous(int levelno)
        {
            var currentUser = _userService.GetUserById(_workContext.CurrentUser.Id);

            var data = _levelServices.GetAllLevels().OrderByDescending(a => a.LevelNo);

            var level = (currentUser.UserRole.Name == "Admin") ? data.Where(a => a.LevelNo < levelno).FirstOrDefault() : data.Where(a => a.LevelNo < levelno).FirstOrDefault();

            if (level != null)
            {
                return RedirectToAction("Index", new { levelno = level.LevelNo });
            }
            return RedirectToAction("Index", new { levelno = levelno });


            //if (!(currentUser.UserRole.Name == "Admin"))
            //{
            //    var userPreviousLevel = data.OrderByDescending(a => a.Id).Where(a => a.Id < id && a.Active == true).FirstOrDefault();

            //    if (userPreviousLevel != null)
            //    {
            //        return RedirectToAction("Index", new { id = userPreviousLevel.Id, srno = srno - 1 });
            //    }
            //    return RedirectToAction("Index", new { id = id, srno = srno - 1 });

            //}
            //else
            //{
            //    var adminPreviousLevel = data.OrderByDescending(a => a.Id).Where(a => a.Id < id).FirstOrDefault();

            //    if(adminPreviousLevel != null)
            //    {
            //        return RedirectToAction("Index", new { id = adminPreviousLevel.Id, srno = srno - 1 });
            //    }
            //    return RedirectToAction("Index", new { id = id, srno = srno - 1 });

            //}

        }

        public IActionResult Next(int levelno)
        {
            var currentUser = _userService.GetUserById(_workContext.CurrentUser.Id);
            var data = _levelServices.GetAllLevels();

            if (!(currentUser.UserRole.Name == "Admin"))
            {
                if (currentUser.RegistrationComplete == false)
                {
                    return RedirectToAction("CompleteRegistration", "User", new { levelno = levelno, userId = currentUser.Id });
                }
                else
                {
                 
                    var userNextLevel = data.OrderBy(a=>a.LevelNo).Where(a => a.LevelNo > levelno).FirstOrDefault();

                    if (userNextLevel != null)
                    {
                        if (userNextLevel.LevelNo > levelno)
                        {
                            currentUser.LastLevel = userNextLevel.Id;

                            _userService.UpdateUser(currentUser);
                        }
                        return RedirectToAction("Index", new { levelno = userNextLevel.LevelNo });
                    }
                    return RedirectToAction("Index", new { levelno = levelno });

                }

            }
            else
            {
              

                var adminNextLevel = data.Where(a => a.LevelNo > levelno).FirstOrDefault();

                if (adminNextLevel != null)
                {
                    return RedirectToAction("Index", new { levelno = adminNextLevel.LevelNo });

                }
                return RedirectToAction("Index", new { levelno = levelno});

            }

        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }
}

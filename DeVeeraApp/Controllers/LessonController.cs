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
using CRM.Services.Settings;
using CRM.Core.Domain;
using CRM.Services.Likes;

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
        private readonly ILocalStringResourcesServices _localStringResourcesServices;
        private readonly ISettingService _settingService;
        private readonly ILikesService _likesService;
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
                                IDiaryMasterService diaryMasterService,
                                        ISettingService settingService,
                                        ILikesService likesService,
                                ILocalStringResourcesServices localStringResourcesServices) : base(workContext: workContext,
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
            _localStringResourcesServices = localStringResourcesServices;
            _settingService = settingService;
            _likesService = likesService;

        }

        #endregion

        #region Utilities     
        public bool IsUserFirstLoginOnDay(DateTime? lastLoginDateUtc)
        {
            var currentUser = _userService.GetUserById(_workContext.CurrentUser.Id);

            var currentDate = DateTime.UtcNow.ToShortDateString();

            //var lastLoginDate = currentUser.LastLoginDateUtc.ToString();

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
        public IActionResult Index(int levelno, DateTime? lastLoginDateUtc)
        {
            var random = new Random();
           var ch= TempData["LangaugeId"];
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
            var currentUser = _userService.GetUserById(_workContext.CurrentUser.Id);
            var userLanguage = _settingService.GetAllSetting().Where(s => s.UserId == currentUser.Id).FirstOrDefault();
            var imagesRecord = _imageMasterService.GetImageById(data?.BannerImageId);
            if (userLanguage.LanguageId == 5) { 
            videoData.BannerImageUrl = imagesRecord?.SpanishImageUrl!=null? imagesRecord?.SpanishImageUrl: imagesRecord?.ImageUrl;
            }
            else
            {
                videoData.BannerImageUrl = imagesRecord?.ImageUrl;
            }

            var imagesRecord1 = _imageMasterService.GetImageById(data?.VideoThumbImageId);
            if (userLanguage.LanguageId == 5) { 
            videoData.VideoThumbImageUrl = imagesRecord1?.SpanishImageUrl!=null? imagesRecord1?.SpanishImageUrl: imagesRecord1?.ImageUrl;
            }
            else
            {
                videoData.VideoThumbImageUrl = imagesRecord1?.ImageUrl;
            }
            var imagesRecord2 = _imageMasterService.GetImageById(data?.ShareBackgroundImageId);
            if (userLanguage.LanguageId == 5)
            {
                videoData.ShareBackgroundImageUrl = imagesRecord2?.SpanishImageUrl!=null? imagesRecord2?.SpanishImageUrl: imagesRecord2?.ImageUrl;
            }
            else
            {
                videoData.ShareBackgroundImageUrl = imagesRecord2?.ImageUrl;
            }

            

            if (data?.VideoId != null)
            {
                var videoRecord = _videoMasterService.GetVideoById((int)data?.VideoId);

                var videoUrl = _s3BucketService.GetPreSignedURL(videoRecord?.Key);

                videoRecord.VideoUrl = videoUrl;

                _videoMasterService.UpdateVideo(videoRecord);
                videoData.VideoId = data.VideoId;
            }
            var updatedVideoData = _levelServices.GetLevelByLevelNo(levelno);
            if (updatedVideoData != null) { 
            videoData.Id = updatedVideoData.Id;
            }
            if (userLanguage != null)
            {
                if (userLanguage.LanguageId == 5)
                {
                    videoData.FullDescription = _localStringResourcesServices.GetResourceValueByResourceName(updatedVideoData?.FullDescription);

                }
                else
                {
                    videoData.FullDescription = updatedVideoData?.FullDescription;
                }
            }
            else if (_workContext.CurrentUser.UserRole.Name == "User")
            {
                videoData.FullDescription = updatedVideoData?.FullDescription;
            }
            else if (_workContext.CurrentUser.UserRole.Name == "Admin")
            {
                videoData.FullDescription = updatedVideoData?.FullDescription;
            }
            videoData.Video = updatedVideoData?.Video;
            videoData.Subtitle = updatedVideoData?.Subtitle;
            videoData.Title = updatedVideoData?.Title;
            
            
            var likesdata = _likesService.GetAllLikes().Where(a => a.UserId == currentUser.Id && a.LevelId== (data?.Id==null?1:data.Id)).ToList();
            if (likesdata.Count() != 0)
            {
                videoData.IsLike = likesdata[0].IsLike;
                videoData.IsDisLike = likesdata[0].IsDisLike;
                //videoData.Comments = likesdata[0].Comments;
            }
            videoData.LevelNo = updatedVideoData?.LevelNo;

            videoData.LikeComments = likesdata;
            foreach (var com in videoData.LikeComments)
            {
                videoData.CreatedOn = com.CreatedDate;
            }

            var quoteListlevel = _dashboardQuoteService.GetAllDashboardQuotes().Where(a => a.IsRandom == true && a.LevelId==videoData.LevelNo).ToList().FirstOrDefault();
            if (quoteListlevel != null)
            {
                videoData.Quote = quoteListlevel.Title;
                videoData.Author = quoteListlevel.Author;
            }
            else { 
            var quoteList = _dashboardQuoteService.GetAllDashboardQuotes().Where(a => a.IsRandom == true).ToList();
            //quoteList = quoteList.Where(a => a.LevelId == data.Id || a.Level == "All Level").ToList();

            if (quoteList != null && quoteList.Count > 0)
            {
                int index = random.Next(quoteList.Count);
                videoData.Quote = quoteList[index].Title;
                videoData.Author = quoteList[index].Author;
            }

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
            var moduleList = _moduleServices.GetModulesByLevelId((data==null)?0:data.Id);
            videoData.ModuleList = moduleList?.ToList().ToModelList<Modules, ModulesModel>(videoData.ModuleList.ToList());
            if (videoData.ModuleList != null) { 
            
            foreach (var module in videoData.ModuleList)
            {
                if (userLanguage != null)
                {
                    if (userLanguage.LanguageId == 5)
                    {
                        module.Title = _localStringResourcesServices.GetResourceValueByResourceName(module.Title);

                    }
                }
                var seletedImages5 = new SelectedImage();
                var imagesRecord5 = _imageMasterService.GetImageById(module.BannerImageId);
                if (imagesRecord5 != null)
                {
                    seletedImages5.ImageUrl = imagesRecord5.ImageUrl;
                    seletedImages5.Key = imagesRecord5.Key;
                    seletedImages5.Name = imagesRecord5.Name;
                    seletedImages5.ImageId = imagesRecord5.Id;
                    seletedImages5.SpanishKey = imagesRecord5.SpanishKey;
                    seletedImages5.SpanishImageUrl = imagesRecord5.SpanishImageUrl;
                    module.SelectedModuleImages.Add(seletedImages5);
                }

            }
            }

            var userNextLevel = AllLevels.Where(a => a.LevelNo > levelno).FirstOrDefault();

            if (userNextLevel != null)
            {
                videoData.NextTitle = userNextLevel?.Title;
                var level = _imageMasterService.GetImageById(userNextLevel.BannerImageId);
                if (userLanguage?.LanguageId == 5) { 
                videoData.NextImageUrl = level?.SpanishImageUrl!=null? level?.SpanishImageUrl: level?.ImageUrl;
                }
                else
                {
                    videoData.NextImageUrl = level?.ImageUrl;
                }

            }

            var userPreviousLevel = AllLevels.OrderByDescending(a => a.LevelNo).Where(a => a.LevelNo < levelno).FirstOrDefault();

            if (userPreviousLevel != null)
            {
                videoData.PrevTitle = userPreviousLevel?.Title;
                var level = _imageMasterService.GetImageById(userPreviousLevel.BannerImageId);
                if (userLanguage?.LanguageId == 5) { 
                videoData.PrevImageUrl = level?.SpanishImageUrl!=null? level?.SpanishImageUrl: level?.ImageUrl;
                }
                else
                {
                    videoData.PrevImageUrl = level?.ImageUrl;
                }
            }
            _localStringResourcesServices.GetResourceValueByResourceName(videoData.Subtitle);
            _localStringResourcesServices.GetResourceValueByResourceName(videoData.Title);
            _localStringResourcesServices.GetResourceValueByResourceName(videoData.Quote);
            _localStringResourcesServices.GetResourceValueByResourceName(videoData.Author);

             
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

                    var userNextLevel = data.OrderBy(a => a.LevelNo).Where(a => a.LevelNo > levelno).FirstOrDefault();

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
                return RedirectToAction("Index", new { levelno = levelno });

            }

        }

        #region like/dislike
        [HttpPost]
        public IActionResult Like(int id, bool islike, string value)
        {
            var currentUser = _userService.GetUserById(_workContext.CurrentUser.Id);
            var likesdata = new LikesUnlikess();
            var levelData = _levelServices.GetLevelById(id);
            var likesbylevelid = _likesService.GetLikesByLevelIdandUserId(levelData.Id, currentUser.Id);
            var model = levelData.ToModel<LevelModel>();
            if (levelData != null)
            {
                if (islike==true) { 
                if (likesbylevelid== null)
                {
                        likesdata.IsLike = true;
                        likesdata.LikeId = 1;
                        likesdata.IsDisLike = false;
                        likesdata.UserId = currentUser.Id;
                        likesdata.LevelId = levelData.Id;
                        //likesdata.CreatedDate = DateTime.Now;
                        _likesService.InsertLikes(likesdata);
                    }
                    else
                    {
                        likesbylevelid.IsLike = true;
                        likesbylevelid.DisLikeId = 0;
                        likesbylevelid.LikeId =  1;
                        likesbylevelid.IsDisLike = false;
                        likesbylevelid.UserId = currentUser.Id;
                        likesbylevelid.LevelId = levelData.Id;
                        //likesbylevelid.CreatedDate = DateTime.Now;
                        _likesService.UpdateLikes(likesbylevelid);
                    }
               }
                else
                {
                    if (likesbylevelid == null)
                    {
                        likesdata.IsLike = false;
                        likesdata.DisLikeId = 1;
                        likesdata.IsDisLike = true;
                        likesdata.UserId = currentUser.Id;
                        likesdata.LevelId = levelData.Id;
                        //likesdata.CreatedDate = DateTime.Now;
                        _likesService.InsertLikes(likesdata);
                    }
                    else
                    {
                        likesbylevelid.IsLike = false;
                        likesbylevelid.LikeId = 0;
                        likesbylevelid.DisLikeId = 1;
                        likesbylevelid.IsDisLike = true;
                        likesbylevelid.UserId = currentUser.Id;
                        likesbylevelid.LevelId = levelData.Id;
                        //likesbylevelid.CreatedDate = DateTime.Now;
                        _likesService.UpdateLikes(likesbylevelid);
                    }
                }
            }
          
         
            return Json(model);
        }
        [HttpPost]
        public IActionResult Comments(int id, string comments)
        {
           
            var currentUser = _userService.GetUserById(_workContext.CurrentUser.Id);
            var likesdata = new LikesUnlikess();
            var levelData = _levelServices.GetLevelById(id);
            var likesbylevelid = _likesService.GetLikesByLevelIdandUserId(levelData.Id, currentUser.Id);
            
            var model = levelData.ToModel<LevelModel>();
            if (levelData != null)
            {
                if (comments != null)
                {
                    //if (likesbylevelid == null) 
                    //{
                        likesdata.UserId = currentUser.Id;
                        likesdata.LevelId = levelData.Id;
                        likesdata.Comments = comments;
                        likesdata.IsLike = false;
                        likesdata.IsDisLike = false;
                    likesdata.CreatedDate = DateTime.Now;
                    _likesService.InsertLikes(likesdata);
                    //}
                    //else
                    //{
                    //    likesbylevelid.UserId = currentUser.Id;
                    //    likesbylevelid.LevelId = model.Id;
                    //    likesbylevelid.Comments = comments;
                    //    likesbylevelid.IsDisLike = likesbylevelid.IsDisLike;
                    //    likesbylevelid.IsLike = likesbylevelid.IsLike;
                    //    _likesService.UpdateLikes(likesbylevelid);
                    //}
                }
            }
           
            return Json(model);
        }
        #endregion


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

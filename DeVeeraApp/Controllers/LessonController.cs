using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DeVeeraApp.Models;
using DeVeeraApp.Filters;
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
        public IActionResult Index(int id, int srno, DateTime? lastLoginDateUtc)
        {
            var random = new Random();
            ViewBag.SrNo = srno;
            ViewBag.TotalLevels = _levelServices.GetAllLevels().Where(a => a.Active == true).ToList().Count;
            var videoData = new LevelModel();
            videoData.SelectedImages = new List<SelectedImage>();
            AddBreadcrumbs("Level", "Index", $"/Lesson/Index/{id}", $"/Lesson/Index/{id}");

            var result= IsUserFirstLoginOnDay(lastLoginDateUtc);
            if (result == true) 
            {
                return RedirectToAction("AskUserEmotion", "Diary");
            }
            var data = _levelServices.GetLevelById(id);
            var levelImages = _levelImageListServices.GetLevelImageListByLevelId(data.Id);

            if(levelImages.Count != 0)
            {
                foreach(var item in levelImages)
                {
                    var seletedImages = new SelectedImage();
                    var imagesRecord = _imageMasterService.GetImageById(item.Image.Id);
                    var imageUrl = _s3BucketService.GetPreSignedURL(imagesRecord.Key);
                    imagesRecord.ImageUrl = imageUrl.Result;
                    _imageMasterService.UpdateImage(imagesRecord);
                    seletedImages.ImageUrl = imagesRecord.ImageUrl;
                    seletedImages.Key = imagesRecord.Key;
                    seletedImages.Name = imagesRecord.Name;
                    seletedImages.id = imagesRecord.Id;
                    videoData.SelectedImages.Add(seletedImages);

                }
            }

            if (data.VideoId != null) { 
            var videoRecord = _videoMasterService.GetVideoById((int)data.VideoId);

            var videoUrl =  _s3BucketService.GetPreSignedURL(videoRecord.Key);

            videoRecord.VideoUrl = videoUrl.Result;

            _videoMasterService.UpdateVideo(videoRecord);
            }
            var updatedVideoData = _levelServices.GetLevelById(id);
            videoData.Id = updatedVideoData.Id;
            videoData.FullDescription = updatedVideoData.FullDescription;
            videoData.Video = updatedVideoData.Video;
            videoData.srno = srno;
            videoData.Subtitle = updatedVideoData.Subtitle;
            videoData.Title = updatedVideoData.Title;
            videoData.LevelNo = updatedVideoData.LevelNo;


            var quoteList = _dashboardQuoteService.GetAllDashboardQuotes().Where(a => a.IsRandom == true).ToList();
            quoteList = quoteList.Where(a => a.LevelId == id || a.Level == "All Level").ToList();

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
            videoData.ModuleList = _moduleServices.GetModulesByLevelId(id);
            return View(videoData);
        }



        public IActionResult Previous(int id, int srno)
        {
            var currentUser = _userService.GetUserById(_workContext.CurrentUser.Id);

            var data = _levelServices.GetAllLevels().OrderByDescending(a => a.Id);

            var level = (currentUser.UserRole.Name == "Admin") ? data.Where(a => a.Id < id).FirstOrDefault() : data.Where(a => a.Id < id && a.Active == true).FirstOrDefault();

            if(level != null)
            {
                return RedirectToAction("Index", new { id = level.Id, srno = srno - 1 });
            }
            return RedirectToAction("Index", new { id = id, srno = srno - 1 });


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

        public IActionResult Next(int id, int srno)
        {
            var currentUser = _userService.GetUserById(_workContext.CurrentUser.Id);

            var data = _levelServices.GetAllLevels();

            if(!(currentUser.UserRole.Name == "Admin"))
            {
                if (currentUser.RegistrationComplete == false)
                {
                    return RedirectToAction("CompleteRegistration", "User", new { Id = id, SrNo = srno, userId = currentUser.Id });
                }
                else
                {
                    ViewBag.SrNo = srno;
                    var userNextLevel = data.Where(a => a.Id > id && a.Active == true).FirstOrDefault();

                    if (userNextLevel != null)
                    {
                        if (userNextLevel.Id > currentUser.LastLevel)
                        {
                            currentUser.LastLevel = userNextLevel.Id;

                            _userService.UpdateUser(currentUser);
                        }
                        return RedirectToAction("Index", new { id = userNextLevel.Id, srno = srno + 1 });
                    }
                    return RedirectToAction("Index", new { id = id, srno = srno + 1 });

                }

            }
            else
            {
                ViewBag.SrNo = srno;

                var adminNextLevel = data.Where(a => a.Id > id).FirstOrDefault();

                if(adminNextLevel != null)
                {
                    return RedirectToAction("Index", new { id = adminNextLevel.Id, srno = srno + 1 });

                }
                return RedirectToAction("Index", new { id = id, srno = srno + 1 });

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

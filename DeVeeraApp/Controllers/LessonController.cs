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
        private readonly IModuleService _moduleServices;
        private readonly IS3BucketService _s3BucketService;
        private readonly IUserService _userService;
        private readonly IWorkContext _workContext;
        private readonly IDiaryMasterService _diaryMasterService;
        private readonly IDashboardQuoteService _dashboardQuoteService;

        #endregion


        #region ctor
        public LessonController(ILogger<LessonController> logger,
                                ILevelServices levelServices,
                                IVideoMasterService videoMasterService,
                                IModuleService moduleService,
                                IS3BucketService s3BucketService,
                                IUserService userService,
                                IDashboardQuoteService dashboardQuoteService,
                                IWorkContext workContext,
                                IHttpContextAccessor httpContextAccessor,
                                IAuthenticationService authenticationService,
                                IDiaryMasterService diaryMasterService) : base(workContext: workContext,
                                                                                  httpContextAccessor: httpContextAccessor,
                                                                                  authenticationService: authenticationService)
        {
            _logger = logger;
            _levelServices = levelServices;
            _videoMasterService = videoMasterService;
            _moduleServices = moduleService;
            _s3BucketService = s3BucketService;
            _userService = userService;
            _workContext = workContext;
            _diaryMasterService = diaryMasterService;
            _dashboardQuoteService = dashboardQuoteService;

        }

        #endregion


        #region Method
        public IActionResult Index(int id, int srno)
        {
            var random = new Random();
            ViewBag.SrNo = srno;
            ViewBag.TotalLevels = _levelServices.GetAllLevels().Count;

            AddBreadcrumbs("Level", "Index", $"/Lesson/Index/{id}", $"/Lesson/Index/{id}");

            var data = _levelServices.GetLevelById(id);

            var videoRecord = _videoMasterService.GetVideoById((int)data.VideoId);

            var videoUrl =  _s3BucketService.GetPreSignedURL(videoRecord.Key);

            videoRecord.VideoUrl = videoUrl.Result;

            _videoMasterService.UpdateVideo(videoRecord);

            var updatedVideoData = _levelServices.GetLevelById(id);
            var videoData = updatedVideoData.ToModel<LevelModel>();

            var quoteList = _dashboardQuoteService.GetDashboardQuoteByLevelId(id).Where(a => a.IsRandom == true).ToList();
            if (quoteList != null && quoteList.Count > 0)
            {
                int index = random.Next(quoteList.Count);
                videoData.Quote = quoteList[index].Title;
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

           
            var data = _levelServices.GetAllLevels().OrderByDescending(a => a.Id).Where(a=>a.Id < id).FirstOrDefault();

            if (data != null)
            {
                return RedirectToAction("Index", new { id = data.Id, srno = srno - 1 });
            }
            return RedirectToAction("Index", new { id = id, srno = srno - 1 });
        }

        public IActionResult Next(int id, int srno)
        {
            var currentUser = _userService.GetUserById(_workContext.CurrentUser.Id);

            if (currentUser.RegistrationComplete == false)
            {
                return RedirectToAction("CompleteRegistration", "User", new { Id = id, SrNo = srno, userId = currentUser.Id });
            }
            else
            {
                ViewBag.SrNo = srno;
                var data = _levelServices.GetAllLevels().Where(a => a.Id > id).FirstOrDefault();
                if (data != null)
                {
                    if (data.Id > currentUser.LastLevel)
                    {
                        currentUser.LastLevel = data.Id;
                        _userService.UpdateUser(currentUser);
                    }
                    return RedirectToAction("Index", new { id = data.Id, srno = srno + 1 });
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

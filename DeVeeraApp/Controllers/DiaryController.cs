using CRM.Core;
using CRM.Core.Domain;

using CRM.Core.Domain.VideoModules;
using CRM.Services;
using CRM.Services.Authentication;
using CRM.Services.Message;
using CRM.Services.Users;
using CRM.Services.VideoModules;
using DeVeeraApp.Utils;
using DeVeeraApp.ViewModels;
using DeVeeraApp.ViewModels.Common;
using DeVeeraApp.ViewModels.Diaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.Controllers
{
    public class DiaryController :BaseController
    {
        #region field

        private readonly INotificationService _notificationService;
        private readonly IDiaryMasterService _DiaryMasterService;
        private readonly IWorkContext _workContext;
        private readonly ILevelServices _levelServices;
        private readonly IModuleService _moduleService;
        private readonly IUserService _userService;

        #endregion


        #region ctor

        public DiaryController(INotificationService notificationService,
                       IDiaryMasterService DiaryMasterService,
                       IWorkContext workContext,
                       ILevelServices levelServices,
                       IModuleService moduleService,
                       IUserService userService,
                        IHttpContextAccessor httpContextAccessor,
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
        }

        #endregion


        #region Method

        public IActionResult Create()
        {
            AddBreadcrumbs("Diary", "Create", "/Diary/Create", "/Diary/Create");
            DiaryModel model = new DiaryModel();
            var currentUser = _userService.GetUserById(_workContext.CurrentUser.Id);

            if (currentUser.TwoFactorAuthentication == false && currentUser.UserRole.Name != "Admin")
            {
                return RedirectToAction("TwoFactorAuthentication", "User", new { UserId = currentUser.Id });
            }
            else
            {
                var diary = _DiaryMasterService.GetAllDiarys().Where(a => a.UserId == currentUser.Id && a.CreatedOn.ToShortDateString() == DateTime.UtcNow.ToShortDateString()).FirstOrDefault();
                model.DiaryDate = DateTime.UtcNow;
                model.Title = diary?.Title;
                model.Comment = diary?.Comment;
                model.Id = diary==null ? 0:diary.Id;
                model.DiaryColor = diary.DiaryColor;

                //#region Diary 
                //List<DiaryModel> DiaryList = new List<DiaryModel>();

                //    var item = _DiaryMasterService.GetAllDiarys().Where(a => a.UserId == currentUser.Id).ToList();

                //    DiaryList = item.ToModelList<Diary, DiaryModel>(DiaryList);

                //#endregion

                //model.diaryModels = DiaryList;
                //model.DiaryDate = DateTime.UtcNow;

                return View(model);

            }

        }

        [HttpPost]
        public IActionResult Create(DiaryModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    var currentUser = _userService.GetUserById(_workContext.CurrentUser.Id);
                    var todayDiary = _DiaryMasterService.GetAllDiarys().Where(a => a.UserId == currentUser.Id && a.CreatedOn.ToShortDateString() == DateTime.UtcNow.ToShortDateString()).FirstOrDefault();
                    var data = model.ToEntity<Diary>();
                    data.UserId = _workContext.CurrentUser.Id;
                    data.CreatedOn = DateTime.UtcNow;
                    _DiaryMasterService.InsertDiary(data);
                    _notificationService.SuccessNotification("Diary added successfully.");
                    if (todayDiary == null && currentUser.UserRole.Name != "Admin") 
                    { 
                        return RedirectToAction(nameof(AskUserEmotion)); 
                    }
                   
                }
                else
                {
                    var diary = _DiaryMasterService.GetDiaryById(model.Id);
                    diary.Title = model.Title;
                    diary.Comment = model.Comment;
                    diary.LastUpdatedOn = DateTime.UtcNow;
                    diary.DiaryColor = model.DiaryColor;
                    _DiaryMasterService.UpdateDiary(diary);
                    _notificationService.SuccessNotification("Diary updated successfully.");

                }
                //#region Diary 
                //List<DiaryModel> DiaryList = new List<DiaryModel>();
                
                //    var item = _DiaryMasterService.GetAllDiarys().Where(a => a.UserId == _workContext.CurrentUser.Id).ToList();

                //    DiaryList = item.ToModelList<Diary, DiaryModel>(DiaryList);
                
                //#endregion

                //model.diaryModels = DiaryList;

                return RedirectToAction("Create", "Diary");

            }

             model.DiaryDate = DateTime.UtcNow;          
            return View(model);

        }

    
        public IActionResult Edit(int Id)
        {
            AddBreadcrumbs("Diary", "Edit", $"/Diary/Edit/{Id}", $"/Diary/Edit/{Id}");
            DiaryModel model = new DiaryModel();

            if (Id > 0) 
            {
                var diary = _DiaryMasterService.GetDiaryById(Id);
                 model = diary.ToModel<DiaryModel>();
            }

                #region Diary 
                List<DiaryModel> DiaryList = new List<DiaryModel>();

                var item = _DiaryMasterService.GetAllDiarys().Where(a => a.UserId == _workContext.CurrentUser.Id).ToList();

                DiaryList = item.ToModelList<Diary, DiaryModel>(DiaryList);

                #endregion

                model.diaryModels = DiaryList;
                return View(model);
            
        }
        [HttpPost]
        public IActionResult Edit(DiaryModel model)
        {
            if (ModelState.IsValid)
            {
                var diary = _DiaryMasterService.GetDiaryById(model.Id);
                diary.Title = model.Title;
                diary.Comment = model.Comment;
                diary.LastUpdatedOn= DateTime.UtcNow;
                _DiaryMasterService.UpdateDiary(diary);
                _notificationService.SuccessNotification("Diary updated successfully.");

                return RedirectToAction("Create", "Diary", new { levelid = model.LevelId });
            }

            return View(model);

        }

        [HttpPost]
        public IActionResult GetDiaryByDate(DateTime Date)
        {
            var currentUser = _userService.GetUserById(_workContext.CurrentUser.Id);
            var diary = _DiaryMasterService.GetAllDiarys().Where(a => a.UserId == currentUser.Id && a.CreatedOn.ToShortDateString() == Date.ToShortDateString()).FirstOrDefault();
            return Json(diary);
        }

        public IActionResult AskUserEmotion(string QuoteType)
        {
            Emotion model = new Emotion();
            model.QuoteType = QuoteType;
            return View(model);
        }

        [HttpPost]
        public IActionResult AskUserEmotion(Emotion model)
        {
            if (ModelState.IsValid)
            {
                if (model.QuoteType == ViewModels.Quote.Login.ToString() || model.QuoteType == ViewModels.Quote.Registration.ToString())
                {
                    return RedirectToAction("Index", "Home");
                }
                var data = _levelServices.GetAllLevels().Where(l => l.Emotions == (CRM.Core.Domain.EmotionType)model.Emotions && l.Active == true).FirstOrDefault();
                if (data != null)
                {
                    return RedirectToAction("Index", "Lesson", new { id = data.Id });
                }
            }
            return View();
        }
        #endregion


    }
}

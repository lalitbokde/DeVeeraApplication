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
    public class DiaryController :BaseController
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

        #endregion


        #region ctor

        public DiaryController(INotificationService notificationService,
                       IDiaryMasterService DiaryMasterService,
                       IWorkContext workContext,
                       ILevelServices levelServices,
                       IModuleService moduleService,
                       IUserService userService,
                       IEmotionService emotionService,
                       IEmotionMappingService emotionMappingService,
                       IDiaryPasscodeService diaryPasscodeService,
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
            _emotionService = emotionService;
            _emotionMappingService = emotionMappingService;
            _diaryPasscodeService = diaryPasscodeService;
        }

        #endregion

        #region Utilities     
        public bool IsUserFirstLoginOnDay(DateTime lastLoginDateUtc)
        {
            var currentUser = _userService.GetUserById(_workContext.CurrentUser.Id);

            var currentDate = DateTime.UtcNow.ToShortDateString();

            var lastLoginDate = lastLoginDateUtc.ToShortDateString();

            if (currentUser.UserRole.Name != "Admin")
            {
                if (currentDate != lastLoginDate)
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
                var passcode = _diaryPasscodeService.GetDiaryPasscodeByUserId(currentUser.Id).FirstOrDefault();
                var result = IsUserFirstLoginOnDay(Convert.ToDateTime(passcode?.DiaryLoginDate));
                if (result == true)
                {
                    return RedirectToAction("EnterPasscode", "Diary");
                }

                var diary = _DiaryMasterService.GetAllDiarys().Where(a => a.UserId == currentUser.Id && a.CreatedOn.ToShortDateString() == DateTime.UtcNow.ToShortDateString()).FirstOrDefault();
                model.DiaryDate = DateTime.UtcNow;
                model.Title = diary?.Title;
                model.Comment = diary?.Comment;
                model.Id = diary==null ? 0:diary.Id;
                model.DiaryColor = diary?.DiaryColor;

                #region Diary 
                List<DiaryModel> DiaryList = new List<DiaryModel>();

                var item = _DiaryMasterService.GetAllDiarys().ToList();

                DiaryList = item.ToModelList<Diary, DiaryModel>(DiaryList);

                #endregion

                model.diaryModels = DiaryList;
                
                

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
                #region Diary 
                List<DiaryModel> DiaryList = new List<DiaryModel>();

                var item = _DiaryMasterService.GetAllDiarys().ToList();

                DiaryList = item.ToModelList<Diary, DiaryModel>(DiaryList);

                #endregion

                model.diaryModels = DiaryList;

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

        public IActionResult AskUserEmotion()
        {
            EmotionModel model = new EmotionModel();

            var list = _emotionService.GetAllEmotions().ToList();

            if (list.Count != 0)
            {
                model.EmotionList = list.ToModelList<Emotion, EmotionModel>(model.EmotionList);
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult AskUserEmotion(EmotionModel model)
        {
            if (ModelState.IsValid)
            {
                int UserId = _workContext.CurrentUser.Id;

                var user = _userService.GetUserById(UserId);

                //var emotion = _emotionService.GetEmotionById(model.Id);

               _emotionMappingService.InActiveUserEmotion(UserId);

                user.User_Emotion_Mappings.Add(new User_Emotion_Mapping
                {
                    EmotionId = model.Id,
                    UserId = UserId,
                   CreatedOn = DateTime.UtcNow,
                    CurrentEmotion = true
                });
                var emotionname=_emotionService.GetEmotionById(model.Id).EmotionName;

                _userService.UpdateUser(user);

                var data = _levelServices.GetAllLevels().Where(l => l.Level_Emotion_Mappings.Where(a=>a.EmotionId== model.Id).Count() > 0 && l.Active == true).FirstOrDefault();
                if (data != null)
                {
                    return RedirectToAction("GotEmotionPage", "diary", new { Emotion=emotionname });
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
               
            }
            return View();
        }
        [HttpGet]
        public IActionResult GotEmotionPage(string Emotion)
        {
           @ViewBag.emotion = Emotion;
            return View();
        }
        public IActionResult EnterPasscode()
        {
            AddBreadcrumbs("Diary", "Passcode", $"/Diary/EnterPasscode", $"/Diary/EnterPasscode");
            return View();
        }

        [HttpPost]
        public IActionResult EnterPasscode(EnterPasscodeModel model)
        {
            AddBreadcrumbs("Diary", "Passcode", $"/Diary/EnterPasscode", $"/Diary/EnterPasscode");

            if (ModelState.IsValid)
            {
                var currentUser = _workContext.CurrentUser;

                var passcode =_diaryPasscodeService.GetDiaryPasscodeByUserId(currentUser.Id).FirstOrDefault();

                if (model.Passcode != passcode.Password)
                {
                    ModelState.AddModelError("Passcode", "Passcode Doesn't match");
                }
                else
                {
                    passcode.DiaryLoginDate = DateTime.UtcNow;
                    _diaryPasscodeService.UpdateDiaryPasscode(passcode);

                    return RedirectToAction("Create");

                }
            }
            return View(model);
        }
        public IActionResult ChangePasscode()
        {
            AddBreadcrumbs("Diary", "Change Passcode", $"/Diary/ChangePasscode", $"/Diary/ChangePasscode");

            PasscodeModel model = new PasscodeModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult ChangePasscode(PasscodeModel model)
        {
            AddBreadcrumbs("Diary", "Change Passcode", $"/Diary/ChangePasscode", $"/Diary/ChangePasscode");

            if (ModelState.IsValid)
            {
                var currentUser = _workContext.CurrentUser;
                var diaryPasscode = _diaryPasscodeService.GetDiaryPasscodeByUserId(currentUser.Id).FirstOrDefault();
    
                if (model.Password != model.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "Passcode Doesn't match");
                }
                else
                {
                    diaryPasscode.Password = model.Password;
                    diaryPasscode.LastUpdatedOn = DateTime.UtcNow;
                    _diaryPasscodeService.UpdateDiaryPasscode(diaryPasscode);

                    _notificationService.SuccessNotification("Passcode change successfully.");

                    return RedirectToAction("Create");
                }

            }

            return View(model);
        }


        #endregion


    }
}

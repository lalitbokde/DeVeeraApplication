using CRM.Core;
using CRM.Core.Domain;
using CRM.Core.Domain.Emotions;
using CRM.Core.Domain.VideoModules;
using CRM.Core.Infrastructure;
using CRM.Core.ViewModels;
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
using DeVeeraApp.ViewModels.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthorizeAdmin]
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

        public IActionResult Create(DataSourceRequest command)
        {
            AddBreadcrumbs("Diary", "Create", "/Diary/Create", "/Diary/Create");
            DiaryListModel model = new DiaryListModel();
            var currentUser = _userService.GetUserById(_workContext.CurrentUser.Id);

            #region DiaryList

            command.PageSize = (command.PageSize == 0) ? 10 : command.PageSize;
            var list = _DiaryMasterService.GetAllDiaries(page_size: command.PageSize, page_num: command.Page, GetAll: command.GetAll, SortBy: "", SearchByDate: "", UserId: 0);
            model.DiaryList = list.FirstOrDefault() != null ? list.GetPaged(command.Page, command.PageSize, list.FirstOrDefault().TotalRecords) : new PagedResult<DiaryViewModel>();
            #endregion

            return View(model);

        }

        [HttpPost]
        public IActionResult Create(DiaryListModel model , DataSourceRequest command)
        {
            AddBreadcrumbs("Diary", "Create", "/Diary/Create", "/Diary/Create");

            #region DiaryList
            command.SortBy = model.SortTypeId == 0 ? "" : EnumDescription.GetDisplayName((SortType)model.SortTypeId).ToString();
            command.PageSize = (command.PageSize == 0) ? 10 : command.PageSize;
            var list = _DiaryMasterService.GetAllDiaries( page_size: command.PageSize, page_num: command.Page, GetAll: command.GetAll, command.SortBy, SearchByDate: model.SearchByDate, UserId: 0);
            model.DiaryList = list.FirstOrDefault() != null ? list.GetPaged(command.Page, command.PageSize, list.FirstOrDefault().TotalRecords) : new PagedResult<DiaryViewModel>();
            #endregion
            
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

                _userService.UpdateUser(user);

                var data = _levelServices.GetAllLevels().Where(l => l.Level_Emotion_Mappings.Where(a=>a.EmotionId== model.Id).Count() > 0 && l.Active == true).FirstOrDefault();
                if (data != null)
                {
                    return RedirectToAction("Index", "Lesson", new { id = data.Id });
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
               
            }
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

using CRM.Core;
using CRM.Core.Domain.Emotions;
using CRM.Core.Domain.VideoModules;
using CRM.Core.Infrastructure;
using CRM.Core.ViewModels;
using CRM.Services;
using CRM.Services.Authentication;
using CRM.Services.Customers;
using CRM.Services.Emotions;
using CRM.Services.Layoutsetup;
using CRM.Services.Localization;
using CRM.Services.Message;
using CRM.Services.TwilioConfiguration;
using CRM.Services.Users;
using CRM.Services.VideoModules;
using DeVeeraApp.Utils;
using DeVeeraApp.ViewModels.Diaries;
using DeVeeraApp.ViewModels.Emotions;
using DeVeeraApp.ViewModels.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.Controllers
{

    public class DiaryController : BaseController
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
        private readonly ILayoutSetupService _LayoutSetupService;
        private readonly IImageMasterService _imageMasterService;
        private readonly ITranslationService _translationService;

        public string key = "AIzaSyC2wpcQiQQ7ASdt4vcJHfmly8DwE3l3tqE";
        private readonly IVerificationService _verificationService;
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
                       ILayoutSetupService layoutSetupService,
                       IImageMasterService imageMasterService,
                       IVerificationService verificationService,
                        IHttpContextAccessor httpContextAccessor,
                        ITranslationService translationService,
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
            _LayoutSetupService = layoutSetupService;
            _imageMasterService = imageMasterService;
            _translationService = translationService;
            _verificationService = verificationService;
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
            var verifymobno = currentUser?.MobileNumber;
            if (currentUser.TwoFactorAuthentication == false && currentUser.UserRole.Name != "Admin")
            {
                //var verification =
                //   await _verificationService.StartVerificationAsync(verifymobno, "sms");
                // if (verification.IsValid == true)
                //{

                //}
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

                var data = _LayoutSetupService.GetAllLayoutSetups().FirstOrDefault();
                model.Diary.DiaryHeaderImageUrl = data?.DiaryHeaderImageId > 0 ? _imageMasterService.GetImageById(data.DiaryHeaderImageId)?.ImageUrl : null;

                var diary = _DiaryMasterService.GetAllDiarys().Where(a => a.UserId == currentUser.Id && a.CreatedOn.ToShortDateString() == DateTime.UtcNow.ToShortDateString()).FirstOrDefault();
                if (diary != null) {
                    model.Diary.DiaryDate = DateTime.UtcNow;
                    model.Diary.Title = diary.Title;
                    model.Diary.Comment = diary.Comment;
                    model.Diary.Id = diary == null ? 0 : diary.Id;
                    model.Diary.DiaryColor = diary.DiaryColor;
                    model.Diary.CreatedOn = diary.CreatedOn;
                    model.Diary.LastUpdatedOn = diary.LastUpdatedOn;
                }
                #region DiaryList

                command.PageSize = (command.PageSize == 0) ? 5 : command.PageSize;
                var list = _DiaryMasterService.GetAllDiaries(page_num: command.Page, page_size: command.PageSize, GetAll: command.GetAll, SortBy: "", SearchByDate: "", UserId: currentUser.Id);
                model.DiaryList = list.FirstOrDefault() != null ? list.GetPaged(command.Page, command.PageSize, list.FirstOrDefault().TotalRecords) : new PagedResult<DiaryViewModel>();
                #endregion

                return View(model);

            }

        }

        [HttpPost]
        public IActionResult Create(DiaryListModel DiaryListModel, DataSourceRequest command, string SubmitButton)
        {
            var model = DiaryListModel?.Diary;
            var currentUser = _userService.GetUserById(_workContext.CurrentUser.Id);

            if (SubmitButton != "SaveDiary")
            {

                var data = _LayoutSetupService.GetAllLayoutSetups().FirstOrDefault();
                model.DiaryHeaderImageUrl = data?.DiaryHeaderImageId > 0 ? _imageMasterService.GetImageById(data.DiaryHeaderImageId)?.ImageUrl : null;

                var diary = _DiaryMasterService.GetAllDiarys().Where(a => a.UserId == currentUser.Id && a.CreatedOn.ToShortDateString() == DateTime.UtcNow.ToShortDateString()).FirstOrDefault();
                model.DiaryDate = DateTime.UtcNow;
                model.Title = diary?.Title;
                model.Comment = diary?.Comment;
                model.Id = diary == null ? 0 : diary.Id;
                model.DiaryColor = diary?.DiaryColor;
                #region DiaryList
                command.SortBy = DiaryListModel.SortTypeId == 0 ? "" : EnumDescription.GetDisplayName((SortType)DiaryListModel.SortTypeId).ToString();
                command.PageSize = (command.PageSize == 0) ? 5 : command.PageSize;
                var list = _DiaryMasterService.GetAllDiaries(page_num: command.Page, page_size: command.PageSize, GetAll: command.GetAll, SortBy: command.SortBy, SearchByDate: DiaryListModel.SearchByDate, UserId: currentUser.Id);
                DiaryListModel.DiaryList = list.FirstOrDefault() != null ? list.GetPaged(command.Page, command.PageSize, list.FirstOrDefault().TotalRecords) : new PagedResult<DiaryViewModel>();
                #endregion

                return View(DiaryListModel);
            }
            else
            {

                if (ModelState.IsValid)
                {
                    
                    var diary = _DiaryMasterService.GetAllDiarys().Where(a => a.UserId == currentUser.Id && a.CreatedOn.ToShortDateString() == DateTime.UtcNow.ToShortDateString()).FirstOrDefault();
                
                    if (diary == null)
                    {
                        var newdiary = new Diary
                        {
                            Title = model.Title,
                            Comment = model.Comment,
                            DiaryColor = model.DiaryColor,
                            UserId = _workContext.CurrentUser.Id,
                            CreatedOn = DateTime.UtcNow
                        };
                        _DiaryMasterService.InsertDiary(newdiary);
                        _translationService.Translate(newdiary.Title,key);
                        _translationService.Translate(newdiary.Comment, key);
                        _notificationService.SuccessNotification("Diary added successfully.");                

                        if (diary == null )
                        {
                            return RedirectToAction(nameof(AskUserEmotion));
                        }

                    }
                    else
                    {
                        diary.Title = model.Title;
                        diary.Comment = model.Comment;
                        diary.LastUpdatedOn = DateTime.UtcNow;
                        diary.DiaryColor = model.DiaryColor;
                        _DiaryMasterService.UpdateDiary(diary);
                        _translationService.Translate(diary.Title, key);
                        _translationService.Translate(diary.Comment, key);
                        _notificationService.SuccessNotification("Diary updated successfully.");
                    }
                    return RedirectToAction("Create", "Diary");

                }

                #region DiaryList
                command.PageSize = (command.PageSize == 0) ? 5 : command.PageSize;
                var list = _DiaryMasterService.GetAllDiaries(page_num: command.Page, page_size: command.PageSize, GetAll: command.GetAll, SortBy: "", SearchByDate:"", UserId: currentUser.Id);
                DiaryListModel.DiaryList = list.FirstOrDefault() != null ? list.GetPaged(command.Page, command.PageSize, list.FirstOrDefault().TotalRecords) : new PagedResult<DiaryViewModel>();
                #endregion
                model.DiaryDate = DateTime.UtcNow;
                return View(DiaryListModel);
            }



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
                diary.LastUpdatedOn = DateTime.UtcNow;
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
            var result = diary?.ToModel<DiaryModel>();
            return Json(result);
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
                var emotionname = _emotionService.GetEmotionById(model.Id).EmotionName;
                _translationService.Translate(emotionname,key);
                _userService.UpdateUser(user);

                var data = _levelServices.GetAllLevels().Where(l => l.Level_Emotion_Mappings.Where(a => a.EmotionId == model.Id).Count() > 0 && l.Active == true).FirstOrDefault();
                if (data != null)
                {
                    return RedirectToAction("GotEmotionPage", "diary", new { Emotion = emotionname });
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
        public async Task <IActionResult> EnterPasscode()
        {
            AddBreadcrumbs("Diary", "Passcode", $"/Diary/EnterPasscode", $"/Diary/EnterPasscode");
            var model = new EnterPasscodeModel();
            var currentUser = _userService.GetUserById(_workContext.CurrentUser.Id);
            var verifymobno = currentUser?.MobileNumber;
            model.MobileNumber = verifymobno;
            var verification =
                   await _verificationService.StartVerificationAsync(verifymobno, "sms");
            if (verification.IsValid == true)
            {

            }
            else
            {
                ModelState.AddModelError("Passcode", "Enter Passcode!!");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EnterPasscode(EnterPasscodeModel model)
        {
            AddBreadcrumbs("Diary", "Passcode", $"/Diary/EnterPasscode", $"/Diary/EnterPasscode");

            if (ModelState.IsValid) 
            {
                var currentUser = _workContext.CurrentUser;
                var enterpass = model.Passcode.Length;
                if (enterpass == 6) { 
                var passcode = _diaryPasscodeService.GetDiaryPasscodeByUserId(currentUser.Id).FirstOrDefault();
                var result = await _verificationService.CheckVerificationAsync(currentUser.MobileNumber, model.Passcode);
                if (result.IsValid==false)
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
                else
                {
                    ModelState.AddModelError("Passcode", "Add Proper Passcode !! ");
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

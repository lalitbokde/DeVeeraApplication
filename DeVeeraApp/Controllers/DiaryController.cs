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

        public IActionResult Create(int levelid, int moduleid)
        {

            AddBreadcrumbs("Diary", "Create", "/Diary/Create", "/Diary/Create");
            DiaryModel model = new DiaryModel();

            var currentUser = _userService.GetUserById(_workContext.CurrentUser.Id);

            if (currentUser.TwoFactorAuthentication == false && currentUser.UserRole.Name != "Admin")
            {
                return RedirectToAction("TwoFactorAuthentication", "User", new { LevelId = levelid, ModuleId = moduleid, UserId = currentUser.Id });
            }
            else
            {
                #region Diary 
                List<DiaryModel> DiaryList = new List<DiaryModel>();
                if (_workContext.CurrentUser.UserRole.Name != "Admin")
                {
                    var item = _DiaryMasterService.GetAllDiarys().Where(a => a.UserId == _workContext.CurrentUser.Id).ToList();

                    DiaryList = item.ToModelList<Diary, DiaryModel>(DiaryList);
                }
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
                var data = model.ToEntity<Diary>();
                data.UserId = _workContext.CurrentUser.Id;
                data.CreatedOn = DateTime.UtcNow;
                _DiaryMasterService.InsertDiary(data);
                _notificationService.SuccessNotification("Diary added successfully.");


                #region Diary 
                List<DiaryModel> DiaryList = new List<DiaryModel>();
                if (_workContext.CurrentUser.UserRole.Name != "Admin")
                {
                    var item = _DiaryMasterService.GetAllDiarys().Where(a => a.UserId == _workContext.CurrentUser.Id).ToList();

                    DiaryList = item.ToModelList<Diary, DiaryModel>(DiaryList);
                }
                #endregion

                model.diaryModels = DiaryList;
                return RedirectToAction("Create", "Diary", new { levelid = model.LevelId });

            }

            return View(model);

        }


        #endregion


    }
}

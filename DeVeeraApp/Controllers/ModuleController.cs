using CRM.Core;
using CRM.Core.Domain.VideoModules;
using CRM.Services;
using CRM.Services.Authentication;
using CRM.Services.QuestionsAnswer;
using CRM.Services.Settings;
using CRM.Services.Users;
using CRM.Services.VideoModules;
using DeVeeraApp.Utils;
using DeVeeraApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DeVeeraApp.Controllers
{

    public class ModuleController : BaseController
    {
        private readonly IModuleService _moduleService;
        private readonly ILevelServices _levelServices;
        private readonly IWorkContext _workContext;
        private readonly IUserService _userService;
        private readonly IDiaryMasterService _diaryMasterService;
        private readonly IModuleImageListService _moduleImageListService;
        private readonly IImageMasterService _imageMasterService;
        private readonly IS3BucketService _s3BucketService;
        private readonly IQuestionAnswerService _QuestionAnswerService;
        private readonly ILocalStringResourcesServices _localStringResourcesServices;
        private readonly ISettingService _settingService;
        public ModuleController(IModuleService moduleService,
                                ILevelServices levelServices,
                                IQuestionAnswerService questionAnswerService,
                                IWorkContext workContext,
                                IUserService userService,
                                IDiaryMasterService diaryMasterService,
                                IHttpContextAccessor httpContextAccessor,
                                IModuleImageListService moduleImageListService,
                                IImageMasterService imageMasterService,
                                IS3BucketService s3BucketService,
                               IAuthenticationService authenticationService,
                                       ISettingService settingService,
                               ILocalStringResourcesServices localStringResourcesServices) : base(workContext: workContext,
                                                                                  httpContextAccessor: httpContextAccessor,
                                                                                  authenticationService: authenticationService)
        {
            _moduleService = moduleService;
            _levelServices = levelServices;
            _workContext = workContext;
            _userService = userService;
            _diaryMasterService = diaryMasterService;
            _moduleImageListService = moduleImageListService;
            _imageMasterService = imageMasterService;
            _s3BucketService = s3BucketService;
            _QuestionAnswerService = questionAnswerService;
            _localStringResourcesServices = localStringResourcesServices;
            _settingService = settingService;
        }

        #region methods

        public IActionResult Index(int id, int srno, int levelSrno)
        {
            var currentUser = _userService.GetUserById(_workContext.CurrentUser.Id);
            AddBreadcrumbs("Module", "Index", "/Module/Index", "/Module/Index");
            ViewBag.SrNo = srno;
            ViewBag.LevelSrNo = levelSrno;
            var data = _moduleService.GetModuleById(id);
            ViewBag.TotalModules = _moduleService.GetAllModules().Where(a=>a.LevelId == data.LevelId).Count();      
            var moduleData = data.ToModel<ModulesModel>();
           
            var userLanguage = _settingService.GetAllSetting().Where(s => s.UserId == currentUser.Id).FirstOrDefault();
            if (userLanguage !=null) 
            {
                if (userLanguage.LanguageId == 5)
            {
                 moduleData.FullDescription = _localStringResourcesServices.GetResourceValueByResourceName(moduleData.FullDescription);
            }
            }
            Diary diary = new Diary();
            if (_workContext.CurrentUser.UserRole.Name == "Admin")
            {
                diary = _diaryMasterService.GetAllDiarys().OrderByDescending(a => a.Id).FirstOrDefault();
            }
            else
            {
                diary = _diaryMasterService.GetAllDiarys().Where(a => a.UserId == _workContext.CurrentUser?.Id).OrderByDescending(a => a.Id).FirstOrDefault();

            }
            moduleData.DiaryText = diary != null ? diary.Comment : "";
            moduleData.DiaryLatestUpdateDate = diary != null ? diary.CreatedOn.ToShortDateString() : "";
            ViewBag.LevelName = _levelServices.GetLevelById(data.LevelId).Title;
            moduleData.QuestionsList = _QuestionAnswerService.GetQuestionsByModuleId(id).ToList();
            if(currentUser.UserRole.Name != "Admin")
            {
                currentUser.ActiveModule = id;
                _userService.UpdateUser(currentUser);

            }
            var moduleImages = _moduleImageListService.GetModuleImageListByModuleId(data.Id);

            var imagesRecord = _imageMasterService.GetImageById(data.BannerImageId);
            moduleData.BannerImageUrl = imagesRecord?.ImageUrl;

            var imagesRecord1 = _imageMasterService.GetImageById(data.VideoThumbImageId);
            moduleData.VideoThumbImageUrl = imagesRecord1?.ImageUrl;

            var imagesRecord2 = _imageMasterService.GetImageById(data.ShareBackgroundImageId);
            moduleData.ShareBackgroundImageUrl = imagesRecord2?.ImageUrl;

            var leveldata = _levelServices.GetLevelById(data.LevelId);
            var AllmoduleList = _moduleService.GetModulesByLevelId(leveldata.Id);
            var alllevel = _levelServices.GetAllLevels();
            var usernextmodule = AllmoduleList.Where(a => a.Id > id).FirstOrDefault();           
            var userprevmodule = AllmoduleList.OrderByDescending(a=>a.Id).Where(a => a.Id < id).FirstOrDefault();

            if (usernextmodule != null)
            {
                moduleData.NextTitle = usernextmodule?.Title;
                var module = _imageMasterService.GetImageById(usernextmodule.BannerImageId);
                moduleData.NextImageUrl = module?.ImageUrl;

            }
            if (userprevmodule != null)
            {
                moduleData.PrevTitle = userprevmodule?.Title;
                var module = _imageMasterService.GetImageById(userprevmodule.BannerImageId);
                moduleData.PrevImageUrl = module?.ImageUrl;

            }
       
            var nextlevel = alllevel.Where(a => a.LevelNo > leveldata?.LevelNo).FirstOrDefault();
            if (nextlevel != null)
            {
                moduleData.NextLeveltitle = nextlevel?.Title;
                var level = _imageMasterService.GetImageById(nextlevel.BannerImageId);
                moduleData.NextLevelUrl = level?.ImageUrl;

            }

            //var userNextLevel = AllLevels.Where(a => a.LevelNo > levelno).FirstOrDefault();

            //if (userNextLevel != null)
            //{
            //    videoData.NextTitle = userNextLevel?.Title;
            //    var level = _imageMasterService.GetImageById(userNextLevel.BannerImageId);
            //    videoData.NextImageUrl = level?.ImageUrl;

            //}

            //var userPreviousLevel = AllLevels.OrderByDescending(a => a.LevelNo).Where(a => a.LevelNo < levelno).FirstOrDefault();

            //if (userPreviousLevel != null)
            //{
            //    videoData.PrevTitle = userPreviousLevel?.Title;
            //    var level = _imageMasterService.GetImageById(userPreviousLevel.BannerImageId);
            //    videoData.PrevImageUrl = level?.ImageUrl;
            //}

            return View(moduleData);
        }



        public IActionResult Previous(int id, int srno, int levelSrno)
        {
            var level = _levelServices.GetLevelByLevelNo(levelSrno);
            var data = _moduleService.GetAllModules().OrderByDescending(a => a.Id).Where(a => a.Id < id && a.LevelId == level.Id).FirstOrDefault();
            if (data != null)
            {
                return RedirectToAction("Index", new { id = data.Id, srno = srno - 1, levelsrno = levelSrno });
            }
            return RedirectToAction("Index", new { id = id, srno = srno - 1, levelsrno = levelSrno });
        }

        public IActionResult Next(int id, int srno, int levelSrno)
        {
            ViewBag.SrNo = srno;
            var currentUser = _userService.GetUserById(_workContext.CurrentUser.Id);

            var level = _levelServices.GetLevelByLevelNo(levelSrno);
            if (srno==1)
            {
                return RedirectToAction("Index", new { id = id, srno = srno + 1, levelsrno = levelSrno });
            }
            var data = _moduleService.GetAllModules().Where(a => a.Id > id && a.LevelId == level.Id).FirstOrDefault();
            if (data != null)
            {
                return RedirectToAction("Index", new { id = data.Id, srno = srno + 1, levelsrno = levelSrno });
            }
            return RedirectToAction("Index", new { id = id, srno = srno + 1, levelsrno = levelSrno });
        }

        #endregion
    }
}

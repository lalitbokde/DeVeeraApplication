using CRM.Core;
using CRM.Core.Domain.VideoModules;
using CRM.Services;
using CRM.Services.Authentication;
using CRM.Services.QuestionsAnswer;
using CRM.Services.Users;
using CRM.Services.VideoModules;
using DeVeeraApp.Filters;
using DeVeeraApp.Utils;
using DeVeeraApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                               IAuthenticationService authenticationService) : base(workContext: workContext,
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
            Diary diary = new Diary();
            if (_workContext.CurrentUser.UserRole.Name == "Admin")
            {
                diary = _diaryMasterService.GetAllDiarys().OrderByDescending(a => a.Id).FirstOrDefault();
            }
            else
            {
                diary = _diaryMasterService.GetAllDiarys().Where(a => a.UserId == _workContext.CurrentUser.Id).OrderByDescending(a => a.Id).FirstOrDefault();

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

          var seletedImages = new SelectedImage();
                    var imagesRecord = _imageMasterService.GetImageById(data.BannerImageId);
            if (imagesRecord != null)
            {
                seletedImages.ImageUrl = imagesRecord.ImageUrl;
                seletedImages.Key = imagesRecord.Key;
                seletedImages.Name = imagesRecord.Name;
                seletedImages.ImageId = imagesRecord.Id;
                moduleData.SelectedModuleImages.Add(seletedImages);
            }

            var seletedImages1 = new SelectedImage();
            var imagesRecord1 = _imageMasterService.GetImageById(data.VideoThumbImageId);
            if (imagesRecord1!=null)
            {
                seletedImages1.ImageUrl = imagesRecord1.ImageUrl;
                seletedImages1.Key = imagesRecord1.Key;
                seletedImages1.Name = imagesRecord1.Name;
                seletedImages1.ImageId = imagesRecord1.Id;
                moduleData.SelectedModuleImages.Add(seletedImages1);
            }
            var seletedImages2 = new SelectedImage();
            
                var imagesRecord2 = _imageMasterService.GetImageById(data.ShareBackgroundImageId);
            if (imagesRecord2 != null)
            {
                seletedImages2.ImageUrl = imagesRecord2.ImageUrl;
                seletedImages2.Key = imagesRecord2.Key;
                seletedImages2.Name = imagesRecord2.Name;
                seletedImages2.ImageId = imagesRecord2.Id;
                moduleData.SelectedModuleImages.Add(seletedImages2);
            }
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

﻿using CRM.Core;
using CRM.Core.Domain;
using CRM.Core.Domain.VideoModules;
using CRM.Services;
using CRM.Services.Authentication;
using CRM.Services.Likes;
using CRM.Services.QuestionsAnswer;
using CRM.Services.Settings;
using CRM.Services.Users;
using CRM.Services.VideoModules;
using DeVeeraApp.Utils;
using DeVeeraApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
        private readonly ILikesService _likesService;
        private readonly IVideoMasterService _videoMasterService;
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
                               ILikesService likesService,
                               IVideoMasterService videoMasterService,
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
            _likesService = likesService;
            _videoMasterService = videoMasterService;
        }

        #region methods

        public IActionResult Index(int moduleNo, int levelSrno,int PreviewLangId)
        {
            var currentUser = _userService.GetUserById(_workContext.CurrentUser.Id);
            AddBreadcrumbs("Module", "Index", "/Module/Index", "/Module/Index");
           
            ViewBag.LevelSrNo = levelSrno;
            var level = _levelServices.GetLevelByLevelNo(levelSrno);
            
            var data = _moduleService.GetModuleByModuleNo(moduleNo, level.Id);
            if (data!=null) {
            ViewBag.TotalModules = _moduleService.GetAllModules().Where(a => a.LevelId == data.LevelId).Count();
            }
            var moduleData = data.ToModel<ModulesModel>();
           
            var likesdata = _likesService.GetAllLikes().Where(a => a.UserId == currentUser.Id && a.ModuleId==data.Id ).ToList();
            if (likesdata.Count()!=0)
            {
                moduleData.IsLike = likesdata[0].IsLike;
                moduleData.IsDisLike = likesdata[0].IsDisLike;
            //  moduleData.Comments = likesdata.Comments;
            }
            //if (lastlevel!=null) { 
            //  moduleData.IsLast = lastlevel?.LevelNo;
            //}
            var userLanguage = _settingService.GetAllSetting().Where(s => s.UserId == currentUser.Id).FirstOrDefault();
            if (userLanguage != null)
            {
                if (userLanguage.LanguageId == 5)
                {
                    moduleData.FullDescription = _localStringResourcesServices.GetResourceValueByResourceName(moduleData.FullDescription);
                }
            }
            if (PreviewLangId != 0)
            {
                userLanguage.LanguageId = PreviewLangId;
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
            moduleData.QuestionsList = _QuestionAnswerService.GetQuestionsByModuleId(data.Id).ToList();
            if (currentUser.UserRole.Name != "Admin")
            {
                currentUser.ActiveModule = data.Id;
                _userService.UpdateUser(currentUser);

            }
            var moduleImages = _moduleImageListService.GetModuleImageListByModuleId(data.Id);

            var imagesRecord = _imageMasterService.GetImageById(data.BannerImageId);
            if (userLanguage?.LanguageId == 5)
            {
                moduleData.BannerImageUrl = imagesRecord?.SpanishImageUrl!=null ? imagesRecord?.SpanishImageUrl : imagesRecord?.ImageUrl;
            }
            else { 
            moduleData.BannerImageUrl = imagesRecord?.ImageUrl != null ? imagesRecord?.ImageUrl : imagesRecord?.SpanishImageUrl;
            }
            var imagesRecord1 = _imageMasterService.GetImageById(data.VideoThumbImageId);
            if (userLanguage?.LanguageId == 5)
            {
                moduleData.VideoThumbImageUrl = imagesRecord1?.SpanishImageUrl!=null? imagesRecord1?.SpanishImageUrl: imagesRecord1?.ImageUrl;
            }
            else
            {
                moduleData.VideoThumbImageUrl = imagesRecord1?.ImageUrl != null ? imagesRecord1?.ImageUrl : imagesRecord1?.SpanishImageUrl;
            }
           

            var imagesRecord2 = _imageMasterService.GetImageById(data.ShareBackgroundImageId);
            if (userLanguage?.LanguageId == 5)
            {
                moduleData.ShareBackgroundImageUrl = imagesRecord2?.SpanishImageUrl!=null? imagesRecord2?.SpanishImageUrl: imagesRecord2?.ImageUrl;
            }
            else
            {
                moduleData.ShareBackgroundImageUrl = imagesRecord2?.ImageUrl != null ? imagesRecord2?.ImageUrl : imagesRecord2?.SpanishImageUrl;
            }

            var leveldata = _levelServices.GetLevelById(data.LevelId);
            var AllmoduleList = _moduleService.GetModulesByLevelId(leveldata.Id);
            var alllevel = _levelServices.GetAllLevels();
            var lastlevels = alllevel.LastOrDefault();
            var levelss = alllevel.Where(a => a.LevelNo == leveldata.LevelNo && a.Id==lastlevels.Id).FirstOrDefault();
            if (levelss != null)
            {
                moduleData.IsLast = levelss?.LevelNo;
            }
            var usernextmodule = AllmoduleList.Where(a => a.ModuleNo > moduleNo).FirstOrDefault();
            var userprevmodule = AllmoduleList.OrderByDescending(a => a.Id).Where(a => a.ModuleNo < moduleNo).FirstOrDefault();
            ViewBag.Previousmodules = userprevmodule;
            if (usernextmodule != null)
            {
                moduleData.NextTitle = usernextmodule?.Title;
                var module = _imageMasterService.GetImageById(usernextmodule.BannerImageId);
                if (userLanguage?.LanguageId == 5) { 
                moduleData.NextImageUrl = module?.SpanishImageUrl!=null ? module?.SpanishImageUrl : module?.ImageUrl;
                }
                else
                {
                    moduleData.NextImageUrl = module?.ImageUrl != null ? module?.ImageUrl : module?.SpanishImageUrl;
                }
            }
            if (userprevmodule != null)
            {
                moduleData.PrevTitle = userprevmodule?.Title;
                var module = _imageMasterService.GetImageById(userprevmodule.BannerImageId);
                if (userLanguage?.LanguageId == 5)
                {
                    moduleData.PrevImageUrl = module?.SpanishImageUrl!=null? module?.SpanishImageUrl: module?.ImageUrl;
                }
                else
                {
                    moduleData.PrevImageUrl = module?.ImageUrl != null ? module?.ImageUrl : module?.SpanishImageUrl;
                }
            }

            var nextlevel = alllevel.Where(a => a.LevelNo > leveldata?.LevelNo).FirstOrDefault();
            if (nextlevel != null)
            {
                moduleData.NextLeveltitle = nextlevel?.Title;
                var levelImage = _imageMasterService.GetImageById(nextlevel.BannerImageId);
                if (userLanguage?.LanguageId == 5)
                {
                    moduleData.NextLevelUrl = levelImage?.SpanishImageUrl!=null? levelImage?.SpanishImageUrl: levelImage?.ImageUrl;
                }
                else
                {
                    moduleData.NextLevelUrl = levelImage?.ImageUrl != null ? levelImage?.ImageUrl : levelImage?.SpanishImageUrl;
                }
            }

            if (moduleData.VideoId != null)
            {
                var videoRecord = _videoMasterService.GetVideoById((int)moduleData.VideoId);

                var videoUrl = _s3BucketService.GetPreSignedURL(videoRecord.Key);

                videoRecord.VideoUrl = videoUrl;

                _videoMasterService.UpdateVideo(videoRecord);
                moduleData.VideoId = (int)moduleData.VideoId;
            }

            var likesdatacomment = _likesService.GetAllLikes().Where(a => a.UserId == currentUser.Id && a.ModuleId == data.Id).ToList();
           
            foreach (var datacomment in likesdata)
            {
                moduleData.LikeCommentslModulelist.Add(datacomment);
            }
            ViewBag.totalmodulefirstidcount = 1;
            return View(moduleData);
        }


        public IActionResult Previous(int moduleNo, int levelSrno)
        {
            var level = _levelServices.GetLevelByLevelNo(levelSrno);
            var data = _moduleService.GetAllModules().OrderByDescending(a => a.ModuleNo).Where(a => a.ModuleNo < moduleNo && a.LevelId == level.Id).FirstOrDefault();
            if (data != null)
                return RedirectToAction("Index", new { moduleNo = data.ModuleNo, levelsrno = levelSrno });
            else
                return RedirectToAction("Index", new { moduleNo = moduleNo, levelsrno = levelSrno });
        }

        public IActionResult Next(int moduleNo, int levelSrno)
        {
            var level = _levelServices.GetLevelByLevelNo(levelSrno);
            var data = _moduleService.GetAllModules().OrderBy(a => a.ModuleNo).Where(a => a.ModuleNo > moduleNo && a.LevelId == level.Id).FirstOrDefault();
            if (data != null)
                return RedirectToAction("Index", new { moduleNo = data.ModuleNo, levelsrno = levelSrno });
            else
                return RedirectToAction("Index", new { moduleNo = moduleNo, levelsrno = levelSrno });
        }

        #endregion
        #region like/dislike
        [HttpPost]
        public IActionResult ModuleLike(int id, bool islike, string value)
        {
            var currentUser = _userService.GetUserById(_workContext.CurrentUser.Id);
            var likesdata = new LikesUnlikess();
            var data = _moduleService.GetModuleById(id);
            var likesbylid = _likesService.GetLikesByModuleIdandUserId(data.Id, currentUser.Id);
            var model = data.ToModel<ModulesModel>();
            if (data != null)
            {
                if (islike == true)
                {
                    if (likesbylid == null)
                    {
                        likesdata.IsLike = true;
                        likesdata.IsDisLike = false;
                        likesdata.UserId = currentUser.Id;
                        likesdata.ModuleId = data.Id;
                        _likesService.InsertLikes(likesdata);
                    }
                    else
                    {
                        likesbylid.IsLike = true;
                        likesbylid.IsDisLike = false;
                        likesbylid.UserId = currentUser.Id;
                        likesbylid.ModuleId = data.Id;
                        _likesService.UpdateLikes(likesbylid);
                    }
                }
                else
                {
                    if (likesbylid == null)
                    {
                        likesdata.IsLike = false;
                        likesdata.IsDisLike = true;
                        likesdata.UserId = currentUser.Id;
                        likesdata.ModuleId = data.Id;
                        _likesService.InsertLikes(likesdata);
                    }
                    else
                    {
                        likesbylid.IsLike = false;
                        likesbylid.IsDisLike = true;
                        likesbylid.UserId = currentUser.Id;
                        likesbylid.ModuleId = data.Id;
                        _likesService.UpdateLikes(likesbylid);
                    }
                }
            }
            return Json(model);
        }
        [HttpPost]
        public IActionResult ModuleComments(int id, string comments)
        {

            var currentUser = _userService.GetUserById(_workContext.CurrentUser.Id);
            var likesdata = new LikesUnlikess();
            var data = _moduleService.GetModuleById(id);
            var likesbylid = _likesService.GetLikesByModuleIdandUserId(data.Id, currentUser.Id);
            var model = data.ToModel<ModulesModel>();
            if (data != null)
            {
                if (comments != null)
                {
                    //if (likesbylid == null) 
                    //{
                        likesdata.UserId = currentUser.Id;
                        likesdata.ModuleId = data.Id;
                        likesdata.Comments = comments;
                        likesdata.IsLike = false;
                        likesdata.IsDisLike = false;
                      likesdata.CreatedDate = DateTime.Now;
                      _likesService.InsertLikes(likesdata);

                    //}
                    //else
                    //{
                    //    likesbylid.UserId = currentUser.Id;
                    //    likesbylid.ModuleId = data.Id;
                    //    likesbylid.Comments = comments;
                    //    likesbylid.IsDisLike = likesbylid.IsDisLike;
                    //    likesbylid.IsLike = likesbylid.IsLike;
                    //    _likesService.UpdateLikes(likesbylid);
                    //}
                    
                }
            }
            return Json(model);
        }
        #endregion
    }
}

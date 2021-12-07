using CRM.Core;
using CRM.Core.Domain;
using CRM.Core.Domain.Emotions;
using CRM.Core.Domain.VideoModules;
using CRM.Core.ViewModels;
using CRM.Services;
using CRM.Services.Authentication;
using CRM.Services.DashboardQuotes;
using CRM.Services.Emotions;
using CRM.Services.Likes;
using CRM.Services.Localization;
using CRM.Services.Message;
using CRM.Services.VideoModules;
using DeVeeraApp.Filters;
using DeVeeraApp.Utils;
using DeVeeraApp.ViewModels;
using DeVeeraApp.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeVeeraApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthorizeAdmin]
    public class LevelController : BaseController
    {
        #region fields

        private readonly IWeeklyUpdateServices _weeklyVideoServices;
        private readonly IModuleService _moduleServices;
        private readonly ILevelServices _levelServices;
        private readonly IVideoMasterService _videoServices;
        private readonly IImageMasterService _imageMasterService;
        private readonly ILevelImageListServices _levelImageListServices;
        private readonly IEmotionService _emotionService;
        private readonly INotificationService _notificationService;
        private readonly IModuleImageListService _moduleImageListService;
        private readonly ITranslationService _translationService;
        private readonly ILikesService _likesService;
        private readonly ILocalStringResourcesServices _localStringResourcesServices;
        private readonly IDashboardQuoteService _dashboardQuoteService;
        public string key = "AIzaSyC2wpcQiQQ7ASdt4vcJHfmly8DwE3l3tqE";

        #endregion


        #region ctor
        public LevelController(IWeeklyUpdateServices weeklyVideoServices,
                                     IModuleService moduleService,
                                     ILevelServices levelServices,
                                     IVideoMasterService videoService,
                                     IImageMasterService imageMasterService,
                                     IWorkContext workContext,
                                     IHttpContextAccessor httpContextAccessor,
                                     IAuthenticationService authenticationService,
                                     ILevelImageListServices levelImageListServices,
                                     IEmotionService emotionService,
                                     INotificationService notificationService,
                                     IModuleImageListService moduleImageListService,
                                     ITranslationService translationService,
                                     ILikesService likesService,
                                       ILocalStringResourcesServices localStringResourcesServices,
                                        IDashboardQuoteService dashboardQuoteService) : base(workContext: workContext,
                                                                                  httpContextAccessor: httpContextAccessor,
                                                                                  authenticationService: authenticationService)
        {
            _weeklyVideoServices = weeklyVideoServices;
            _moduleServices = moduleService;
            _levelServices = levelServices;
            _videoServices = videoService;
            _imageMasterService = imageMasterService;
            _levelImageListServices = levelImageListServices;
            _emotionService = emotionService;
            _likesService = likesService;
            _notificationService = notificationService;
            this._moduleImageListService = moduleImageListService;
            _translationService = translationService;
            _localStringResourcesServices = localStringResourcesServices;
            _dashboardQuoteService = dashboardQuoteService; 
        }
        #endregion

        #region Utilities
        public virtual void PrepareLevelModel(LevelModel model)
        {
            //prepare available url
            model.AvailableVideo.Add(new SelectListItem { Text = "Select Video", Value = "0" });
            var AvailableVideoUrl = _videoServices.GetAllVideos();
            foreach (var url in AvailableVideoUrl)
            {
                model.AvailableVideo.Add(new SelectListItem
                {
                    Value = url.Id.ToString(),
                    Text = url.Name,
                    Selected = url.Id == model.VideoId
                });
            }

            //prepare available images
            model.ImageLists = _imageMasterService.GetAllImages().ToList();
            foreach (var item in model.ImageLists)
            {
                model.AvailableImages.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name,
                });
            }

            //prepare available Emotions
            model.AvailableEmotions.Add(new SelectListItem { Text = "Select Emotion", Value = "0" });
            var AvailableEmotions = _emotionService.GetAllEmotions();
            foreach (var item in AvailableEmotions)
            {
                model.AvailableEmotions.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.EmotionName,
                });
            }

            model.AvilableQuotelevel.Add(new SelectListItem { Text = "Select Quote", Value = "0" });
            var AvilableQuotelevel = _dashboardQuoteService.GetAllDashboardQuotes();
            foreach (var quote in AvilableQuotelevel)
            {
                model.AvilableQuotelevel.Add(new SelectListItem
                {
                    Value = quote.Id.ToString(),
                    Text = quote.Title + " - " + quote.Author,
                    Selected = quote.Id == model.QuoteId
                });
            }


        }


        #endregion

        #region Methods
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            AddBreadcrumbs("Level", "Create", "/Admin/Level/List", "/Admin/Level/Create");
            LevelModel model = new LevelModel();
            
            PrepareLevelModel(model);

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(LevelModel model)
        {
            AddBreadcrumbs("Level", "Create", "/Admin/Level/List", "/Admin/Level/Create");

            if (ModelState.IsValid)
            {
                model.VideoId = (model.VideoId == 0) ? model.VideoId = null : model.VideoId;

                var data = model.ToEntity<Level>();
                data.CreatedOn = DateTime.Now;
                data.Title = model.Title;

                if (model.SelectedEmotions.Count() != 0)
                {
                    foreach (var item in model.SelectedEmotions)
                    {
                        data.Level_Emotion_Mappings.Add(new Level_Emotion_Mapping
                        {
                            EmotionId = Convert.ToInt32(item),
                            LevelId = data.Id,
                            CreatedOn = DateTime.UtcNow,
                        });
                    }
                }

                _levelServices.InsertLevel(data);

                var quotedata = _dashboardQuoteService.GetDashboardQuoteById(model.QuoteId);
                quotedata.IsRandom = true; 
                quotedata.LevelId = data.LevelNo;
                _dashboardQuoteService.UpdateDashboardQuote(quotedata);


                /// This is used for translate English To spanish
                _translationService.Translate(model.Title, model.SpanishTitle);
                _translationService.Translate(model.Subtitle, model.SpanishSubtitle);
                _translationService.Translate(model.FullDescription, model.SpanishFullDescription);

                //foreach (var result in model.ImageLists.Where(a => a.Selected == true))
                //{
                //    var record = new LevelImageList
                //    {
                //        LevelId = data.Id,
                //        ImageId = result.Id
                //    };

                //    _levelImageListServices.InsertLevelImage(record);
                //}

                model.AvilableQuotelevel.Add(new SelectListItem { Text = "Select Quote", Value = "0" });
                var AvilableQuotelevel = _dashboardQuoteService.GetAllDashboardQuotes();
                foreach (var quote in AvilableQuotelevel)
                {
                    model.AvilableQuotelevel.Add(new SelectListItem
                    {
                        Value = quote.Id.ToString(),
                        Text = quote.Title + " - " + quote.Author,
                        Selected = quote.Id == model.QuoteId
                    });
                }




                _notificationService.SuccessNotification("New video lesson has been created successfully.");
                return RedirectToAction("List");
            }

            PrepareLevelModel(model);


            return View(model);
        }

        public IActionResult List(DataSourceRequest command)
        {
            var model = new LevelModel();
            var list = _levelServices.GetAllLevelsDataSp(page_size: command.PageSize, page_num: command.Page, GetAll: command.GetAll, SortBy: "", Title: model.Title, Subtitle: model.Subtitle, VideoName: model.VideoName, LikeId: model.LikeId, DisLikeId: model.DisLikeId).OrderBy(a => a.LevelNo).ToList();
            model.LevelListPaged = list.FirstOrDefault() != null ? list : new List<LevelViewModel>(); 

            if (model.LevelListPaged != null)
            {
               
                return View(model);
            }
            return View(model);

        }




        public IActionResult Edit(int id, int ModuleId, int srno)
        {
            AddBreadcrumbs("Level", "Edit", "/Admin/Level/List", $"/Admin/Level/Edit/{id}");
            ViewBag.ActiveTab = "Level";
            List<string> Emotions = new List<string>();
            DataSourceRequest command = new DataSourceRequest();
            if (id != 0)
            {
                var data = _levelServices.GetLevelById(id);
                var model = data.ToModel<LevelModel>();

                if (data.Level_Emotion_Mappings.Count != 0)
                {
                    foreach (var item in data.Level_Emotion_Mappings)
                    {
                        Emotions.Add(item.EmotionId.ToString());
                    }

                    model.SelectedEmotions = Emotions;
                }
                model.LikeUser = _likesService.GetLikesByLevelId(data.Id);
                ViewBag.likes = model.LikeUser.Count();
                if (ViewBag.likes== null) { ViewBag.likes = 0; }
                foreach (var result in model.LikeUser)
                {
                    var record = new LevelModel
                    {
                        UserName = result.User.Email,
                        
                    };
                }
                model.DisLikeUser = _likesService.GetDislikesByLevelId(data.Id);
                ViewBag.dislikes = model.DisLikeUser.Count();
                if (ViewBag.dislikes == null) { ViewBag.dislikes = 0; }
                foreach (var result in model.DisLikeUser)
                {
                    var record = new LevelModel
                    {
                        UserName = result.User.Email,

                    };
                }
                model.LikeComments= _likesService.GetCommenntsByLevelId(data.Id);
                model.srno = srno;
                
                model.BannerImageUrl = _imageMasterService.GetImageById(data.BannerImageId)?.ImageUrl;
                model.VideoThumbImageUrl = _imageMasterService.GetImageById(data.VideoThumbImageId)?.ImageUrl;
                model.ShareBackgroundImageUrl = _imageMasterService.GetImageById(data.ShareBackgroundImageId)?.ImageUrl;
                model.SpanishFullDescription = _localStringResourcesServices.GetResourceValueByResourceName(model.FullDescription);
                model.SpanishSubtitle = _localStringResourcesServices.GetResourceValueByResourceName(model.Subtitle);
                model.SpanishTitle = _localStringResourcesServices.GetResourceValueByResourceName(model.Title);
                var moduleList = _moduleServices.GetModulesByLevelId(id);
               
                var list = _levelServices.GetAllModulesDataSp(id).OrderBy(a => a.Id).ToList();
                model.ModuleDataList = list.FirstOrDefault() != null ? list : new List<ModulesViewModel>().OrderBy(a => a.Id).ToList();
                if (ModuleId > 0 && ModuleId != 0)
                {
                    var module = _moduleServices.GetModuleById(ModuleId);
                    model.Modules.Title = module.Title;
                    model.Modules.VideoId = module.VideoId;
                    model.Modules.BannerImageId = module.BannerImageId;
                    model.Modules.VideoThumbImageId = module.VideoThumbImageId;
                    model.Modules.ShareBackgroundImageId = module.ShareBackgroundImageId;
                    model.Modules.ModuleNo = module.ModuleNo;
                    model.Modules.FullDescription = module.FullDescription;
                    model.Modules.Id = module.Id;
                    ViewBag.ActiveTab = "Add Module";

                    model.Modules.BannerImageUrl = _imageMasterService.GetImageById(module.BannerImageId)?.ImageUrl;
                    model.Modules.VideoThumbImageUrl = _imageMasterService.GetImageById(module.VideoThumbImageId)?.ImageUrl;
                    model.Modules.ShareBackgroundImageUrl = _imageMasterService.GetImageById(module.ShareBackgroundImageId)?.ImageUrl;
                    model.SpanishTitleModule = _localStringResourcesServices.GetResourceValueByResourceName(model.Modules.Title);
                    model.SpanishFullDescriptionModule = _localStringResourcesServices.GetResourceValueByResourceName(model.Modules.FullDescription);
                    model.LikeModule = _likesService.GetLikesByModuleId(module.Id);
                    if (model.LikeModule != null)
                    {
                        ViewBag.modulelikes = model.LikeModule.Count();
                        if (ViewBag.modulelikes == null) { ViewBag.modulelikes = 0; }
                        foreach (var result in model.LikeModule)
                        {
                            var record = new LevelModel
                            {
                                UserName = result.User.Email,

                            };
                        }
                    }
                    model.DisLikeModule = _likesService.GetDisLikesByModuleId(module.Id);
                    if (model.LikeModule != null)
                    {
                        ViewBag.moduledislikes = model.DisLikeModule.Count();
                        if (ViewBag.moduledislikes == null) { ViewBag.moduledislikes = 0; }
                        foreach (var result in model.DisLikeModule)
                        {
                            var record = new LevelModel
                            {
                                UserName = result.User.Email,

                            };
                        }
                    }
                    model.CommentsModule = _likesService.GetCommenntsByModuleId(module.Id);
                    

                }
               
                PrepareLevelModel(model);

             
               
                var quotelevel = _dashboardQuoteService.GetAllDashboardQuotes().Where(a=>a.LevelId== (int)model.LevelNo).ToList().FirstOrDefault();
                if (quotelevel != null){ 
                model.QuoteId = quotelevel.Id;
                }
                //foreach (var result in imagedata)
                //{
                //    model.ImageLists.Where(a => a.Id == result.ImageId).ToList().ForEach(c => c.Selected = true);
                //}

                return View(model);
            }
            return RedirectToAction("List");

        }

        [HttpPost]
        public IActionResult Edit(LevelModel model, IFormCollection form)
        {
            AddBreadcrumbs("Level", "Edit", "/Admin/Level/List", $"/Admin/Level/Edit/{model.Id}");
            _ = Request.Form["selectedImages"];

            if (ModelState.IsValid)
            {
                string name = Request.Form["selectcheck"];
                model.VideoId = (model.VideoId == 0) ? model.VideoId = null : model.VideoId;

                var levelData = _levelServices.GetLevelById(model.Id);
                levelData.Title = model.Title;
                levelData.Subtitle = model.Subtitle;
                levelData.LevelNo = model.LevelNo;
                levelData.FullDescription = model.FullDescription;
                levelData.VideoId = model.VideoId;
                levelData.BannerImageId = model.BannerImageId;
                levelData.VideoThumbImageId = model.VideoThumbImageId;
                levelData.ShareBackgroundImageId = model.ShareBackgroundImageId;
                levelData.Active = model.Active;
                levelData.EmotionId = model.EmotionId;
                levelData.UpdatedOn = DateTime.Now;
                _levelImageListServices.DeleteLevelImagesByLevelId(levelData.Id);

                /// This is used for translate English To spanish
                _translationService.Translate(levelData.Title, model.SpanishTitle);
                //_translationService.Translate(levelData.Subtitle, model.SpanishTitleModule);
                _translationService.Translate(levelData.FullDescription, model.SpanishFullDescription);
                //foreach (var result in model.ImageLists.Where(a=>a.Selected==true))
                //    {
                //        var record = new LevelImageList
                //        {
                //            LevelId = levelData.Id,
                //            ImageId = result.Id
                //        };

                //        _levelImageListServices.InsertLevelImage(record);
                //    }




                if (levelData.Level_Emotion_Mappings.Count() != 0)
                {
                    levelData.Level_Emotion_Mappings.Clear();
                }

                if (model.SelectedEmotions.Count() != 0)
                {
                    foreach (var item in model.SelectedEmotions)
                    {
                        levelData.Level_Emotion_Mappings.Add(new Level_Emotion_Mapping
                        {
                            EmotionId = Convert.ToInt32(item),
                            LevelId = levelData.Id,
                            CreatedOn = DateTime.UtcNow,
                        });
                    }
                }

                model.AvilableQuotelevel.Add(new SelectListItem { Text = "Select Quote", Value = "0" });
                var AvilableQuotelevel = _dashboardQuoteService.GetAllDashboardQuotes();
                foreach (var quote in AvilableQuotelevel)
                {
                    model.AvilableQuotelevel.Add(new SelectListItem
                    {
                        Value = quote.Id.ToString(),
                        Text = quote.Title + " - " + quote.Author,
                        Selected = quote.Id == model.QuoteId
                    });
                }


                _levelServices.UpdateLevel(levelData);
                //// first set all level to zero with same same level set for other quotes /////
                var level = _dashboardQuoteService.GetAllDashboardQuotes().Where(a => a.LevelId == model.LevelNo).ToList();

                foreach(var datalevel in level)
                {
                    datalevel.LevelId = 0;
                    _dashboardQuoteService.UpdateDashboardQuote(datalevel);
                }
                //setting for respected quote to that level /////////
                var quotelevel = _dashboardQuoteService.GetDashboardQuoteById(model.QuoteId);
                if (name !="2") { 
                quotelevel.IsRandom=true;
                quotelevel.LevelId = model.Id;               
                quotelevel.LevelId = model.LevelNo;     
                }                
                _dashboardQuoteService.UpdateDashboardQuote(quotelevel);
                ////////  end of setting level ////
                _notificationService.SuccessNotification("video lesson has been edited successfully.");

                return RedirectToAction("Edit", "Level", model.Id);
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddModule(LevelModel model)
        {
            AddBreadcrumbs("Level", "Edit", "/Admin/Level/List", $"/Admin/Level/Edit/{model.Id}");

            if (model.Modules.FullDescription != null)
            {
                if (model.Modules.Id == 0)
                {
                    model.Modules.VideoId = (model.Modules.VideoId == 0) ? model.Modules.VideoId = null : model.Modules.VideoId;

                    var modules = new Modules
                    {
                        LevelId = model.Id,
                        Title = model.Modules.Title,
                        VideoId = model.Modules.VideoId,
                        BannerImageId = model.Modules.BannerImageId,
                        VideoThumbImageId = model.Modules.VideoThumbImageId,
                        ShareBackgroundImageId = model.Modules.ShareBackgroundImageId,
                        FullDescription = model.Modules.FullDescription,
                        ModuleNo = model.Modules.ModuleNo
                    };
                    _moduleServices.InsertModule(modules);
                    _translationService.Translate(modules.Title, model.SpanishTitleModule);
                    _translationService.Translate(modules.FullDescription, model.SpanishFullDescriptionModule);
                    _notificationService.SuccessNotification("Module has been added successfully");

                    if (model.SelectedModuleImg.Count() != 0)
                    {
                        for (int i = 0; i < model.SelectedModuleImg.Count(); i++)
                        {
                            var record = new ModuleImageList
                            {
                                ModuleId = modules.Id,
                                ImageId = Convert.ToInt32(model.SelectedModuleImg[i])
                            };

                            _moduleImageListService.InsertModuleImageList(record);
                        }
                    }

                }
                else
                {
                    model.Modules.VideoId = (model.Modules.VideoId == 0) ? model.Modules.VideoId = null : model.Modules.VideoId;

                    var modules = _moduleServices.GetModuleById(model.Modules.Id);
                    modules.LevelId = model.Id;
                    modules.Title = model.Modules.Title;
                    modules.VideoId = model.Modules.VideoId;
                    modules.BannerImageId = model.Modules.BannerImageId;
                    modules.VideoThumbImageId = model.Modules.VideoThumbImageId;
                    modules.ShareBackgroundImageId = model.Modules.ShareBackgroundImageId;
                    modules.FullDescription = model.Modules.FullDescription;
                    modules.ModuleNo = model.Modules.ModuleNo;
                    _moduleServices.UpdateModule(modules);
                    _translationService.Translate(modules.Title, model.SpanishTitleModule);
                    _translationService.Translate(modules.FullDescription, model.SpanishFullDescriptionModule);
                    _notificationService.SuccessNotification("Module has been updated successfully");

                    _moduleImageListService.DeleteModuleImageListByModuleId(modules.Id);

                    if (model.SelectedModuleImg.Count() != 0)
                    {
                        for (int i = 0; i < model.SelectedModuleImg.Count(); i++)
                        {
                            var record = new ModuleImageList
                            {
                                ModuleId = modules.Id,
                                ImageId = Convert.ToInt32(model.SelectedModuleImg[i])
                            };

                            _moduleImageListService.InsertModuleImageList(record);
                        }
                    }


                    return RedirectToAction("Edit", "Level", new { id = model.Id, ModuleId = model.Modules.Id });
                }

                return RedirectToAction("Edit", "Level", new { id = model.Id });

            }
            _notificationService.ErrorNotification("The filed Full Description is required.");
            return RedirectToAction("Edit", "Level", new { id = model.Id });


        }


        [HttpPost]
        public IActionResult SaveLevelImages(LevelModel model)
        {
            //model.ImageLists = model.ImageLists.Where(a=>a.Selected==true).ToList();

            if (model.ImageLists.Count() > 0)
            {
                foreach (var item in model.ImageLists)
                {
                    model.SelectedImg.Add(item.Id.ToString());
                }

                return Json(new { Success = true, Images = model.ImageLists, SelectedImages = model.SelectedImg });
            }

            return Json("Ok");
        }


        public IActionResult Delete(int videoId)
        {

            ResponseModel response = new ResponseModel();

            if (videoId != 0)
            {
                var videoData = _levelServices.GetLevelById(videoId);
                if (videoData == null)
                {
                    response.Success = false;
                    response.Message = "No user found";
                }
                _levelServices.DeleteLevel(videoData);

                response.Success = true;
            }
            else
            {
                response.Success = false;
                response.Message = "videoId is 0";

            }
            return Json(response);
        }

        public IActionResult DeleteModule(int id)
        {

            ResponseModel response = new ResponseModel();

            if (id != 0)
            {
                var module = _moduleServices.GetModuleById(id);
                if (module == null)
                {
                    response.Success = false;
                    response.Message = "No user found";
                }
                _moduleServices.DeleteModule(module);

                response.Success = true;
            }
            else
            {
                response.Success = false;
                response.Message = "";

            }
            return Json(response);
        }

        #region Translate

        [HttpPost]
        public IActionResult TranslateSpanish(LevelModel level)
        {
            LevelModel model = new LevelModel();
            model.Title = level.Title != null ? _translationService.TranslateLevelSpanish(level.Title, key) : "";
            model.Subtitle = level.Subtitle != null ? _translationService.TranslateLevel(level.Subtitle, key):"";
            model.FullDescription = level.FullDescription != null ? _translationService.TranslateLevel(level.FullDescription, key) : ""; 
            return Json(model);
        
        }
        [HttpPost]
        public IActionResult TranslateSpanishModule(LevelModel level)
        {
            LevelModel model = new LevelModel();
            model.Title = level.Title != null ? _translationService.TranslateLevel(level.Title, key):"" ;
            model.FullDescription = level.FullDescription!= null ? _translationService.TranslateLevel(level.FullDescription, key) :"";
            return Json(model);
        }

        [HttpPost]
        public IActionResult TranslateSpanishCreate(LevelModel level)
        {
            LevelModel model = new LevelModel();
            model.Title = level.Title!=null? _translationService.TranslateLevel(level.Title, key):"";
            model.Subtitle = level.Subtitle != null ? _translationService.TranslateLevel(level.Subtitle, key) :"";
            model.FullDescription = level.FullDescription!=null ? _translationService.TranslateLevel(level.FullDescription, key) :"";
            return Json(model);
           
        }
        [HttpPost]
        public IActionResult TranslateEnglishCreate(LevelModel level)
        {
            LevelModel model = new LevelModel();
            model.Title = level.Title != null ? _translationService.TranslateLevel(level.Title, key) : ""; 
            model.Subtitle = level.Subtitle!=null? _translationService.TranslateLevel(level.Subtitle, key):"";
            model.FullDescription = level.FullDescription!=null? _translationService.TranslateLevel(level.FullDescription, key):"";
            return Json(model);
        }
        [HttpPost]
        public IActionResult TranslateEnglish(LevelModel level)
        {
            LevelModel model = new LevelModel();
            model.Title = level.Title!=null? _translationService.TranslateLevel(level.Title, key):"";
            model.Subtitle = level.Subtitle!=null?_translationService.TranslateLevel(level.Subtitle, key):"";
            model.FullDescription = level.FullDescription!=null? _translationService.TranslateLevel(level.FullDescription, key):"";
            return Json(model);
           
        }
        [HttpPost]
        public IActionResult TranslateEnglishModule(LevelModel level)
        {
            LevelModel model = new LevelModel();
            model.Title = level.Title!=null? _translationService.TranslateLevelSpanish(level.Title, key):"";
            model.FullDescription = level.FullDescription!=null? _translationService.TranslateLevelSpanish(level.FullDescription, key):"";
            return Json(model);
        }
        #endregion
        #endregion
    }
}

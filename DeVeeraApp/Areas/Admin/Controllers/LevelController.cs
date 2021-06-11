 using CRM.Core;
using CRM.Core.Domain;
using CRM.Core.Domain.Emotions;
using CRM.Core.Domain.VideoModules;
using CRM.Services;
using CRM.Services.Authentication;
using CRM.Services.Emotions;
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
using System.Threading.Tasks;

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
                                     IModuleImageListService moduleImageListService) : base(workContext: workContext,
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
            _notificationService = notificationService;
            this._moduleImageListService = moduleImageListService;
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
                model.VideoId =  (model.VideoId == 0) ? model.VideoId = null : model.VideoId;

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

                foreach (var result in model.ImageLists.Where(a => a.Selected == true))
                {
                    var record = new LevelImageList
                    {
                        LevelId = data.Id,
                        ImageId = result.Id
                    };

                    _levelImageListServices.InsertLevelImage(record);
                }

                _notificationService.SuccessNotification("New video lesson has been created successfully.");
                return RedirectToAction("Index", "Home");
            }

            PrepareLevelModel(model);


            return View(model);
        }

        public IActionResult List()
        {
            AddBreadcrumbs("Level", "List", "/Admin/Level/List", "/Admin/Level/List");

            var model = new List<LevelModel>();
            var data = _levelServices.GetAllLevels();
            if(data.Count() != 0)
            {
                model = data.ToList().ToModelList<Level, LevelModel>(model);


                ViewBag.TableData = JsonConvert.SerializeObject(model);
                return View(model);
            }
            return RedirectToAction("Index", "Home");

        }


       

        public IActionResult Edit(int id,int ModuleId, int srno)
        {
            AddBreadcrumbs("Level", "Edit", "/Admin/Level/List", $"/Admin/Level/Edit/{id}");        
            ViewBag.ActiveTab = "Level";
            List<string> Emotions = new List<string>(); 

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

                model.srno = srno;

                var imagedata = _levelImageListServices.GetLevelImageListByLevelId(data.Id);

                if(imagedata.Count != 0)
                {
                    foreach(var item in imagedata)
                    {
                        model.SelectedImg.Add(item.ImageId.ToString());
                    }
                }


                var moduleList = _moduleServices.GetModulesByLevelId(id);
                model.ModuleList = moduleList.ToList().ToModelList<Modules, ModulesModel>(model.ModuleList.ToList());
                if ( ModuleId > 0 && ModuleId != 0)
                {
                    var module = _moduleServices.GetModuleById(ModuleId);
                    model.Modules.Title = module.Title;
                    model.Modules.VideoId = module.VideoId;
                    model.Modules.FullDescription = module.FullDescription;
                    model.Modules.Id = module.Id;
                    ViewBag.ActiveTab = "Add Module";

                    var moduleIMageList = _moduleImageListService.GetModuleImageListByModuleId(ModuleId);

                    if (moduleIMageList.Count > 0)
                    {
                        foreach (var item in moduleIMageList)
                        {
                            model.SelectedModuleImg.Add(item.ImageId.ToString());
                        }
                    }
                }

                

                PrepareLevelModel(model);

                foreach (var result in imagedata)
                {
                    model.ImageLists.Where(a => a.Id == result.ImageId).ToList().ForEach(c => c.Selected = true);
                }

                return View(model);
            }
            return RedirectToAction("List");

        }

        [HttpPost]
        public IActionResult Edit(LevelModel model)
        {
            AddBreadcrumbs("Level", "Edit", "/Admin/Level/List", $"/Admin/Level/Edit/{model.Id}");

            var data = Request.Form["selectedImages"];

            if (ModelState.IsValid)
            {
                model.VideoId = (model.VideoId == 0) ? model.VideoId = null : model.VideoId;

                var levelData = _levelServices.GetLevelById(model.Id);
                levelData.Title = model.Title;
                levelData.Subtitle = model.Subtitle;
                levelData.LevelNo = model.LevelNo;
                levelData.FullDescription = model.FullDescription;
                levelData.VideoId = model.VideoId;
                levelData.Active = model.Active;
                levelData.EmotionId = model.EmotionId;
                levelData.UpdatedOn = DateTime.Now;
                _levelImageListServices.DeleteLevelImagesByLevelId(levelData.Id);


                foreach (var result in model.ImageLists.Where(a=>a.Selected==true))
                    {
                        var record = new LevelImageList
                        {
                            LevelId = levelData.Id,
                            ImageId = result.Id
                        };

                        _levelImageListServices.InsertLevelImage(record);
                    }
                

                

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

                _levelServices.UpdateLevel(levelData);
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

                    var modules = new Modules();
                    modules.LevelId = model.Id;
                    modules.Title = model.Modules.Title;
                    modules.VideoId = model.Modules.VideoId;
                    modules.FullDescription = model.Modules.FullDescription;
                    _moduleServices.InsertModule(modules);
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
                    modules.FullDescription = model.Modules.FullDescription;
                    _moduleServices.UpdateModule(modules);
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


                    return RedirectToAction("Edit", "Level", new { id = model.Id , ModuleId = model.Modules.Id });
                }

                return RedirectToAction("Edit", "Level", new { id = model.Id });

            }
            _notificationService.ErrorNotification("The filed Full Description is required.");
            return RedirectToAction("Edit", "Level", new { id = model.Id });


        }

     
        [HttpPost]
        public IActionResult SaveLevelImages(LevelModel model)
        {
            model.ImageLists = model.ImageLists.Where(a=>a.Selected==true).ToList();

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

        #endregion
    }
}

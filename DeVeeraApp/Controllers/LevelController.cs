using CRM.Core;
using CRM.Core.Domain;
using CRM.Core.Domain.VideoModules;
using CRM.Services;
using CRM.Services.Authentication;
using CRM.Services.Message;
using CRM.Services.VideoModules;
using DeVeeraApp.Utils;
using DeVeeraApp.ViewModels;
using DeVeeraApp.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.Controllers
{
    public class LevelController : BaseController
    {
        #region fields

        private readonly IWeeklyUpdateServices _weeklyVideoServices;
        private readonly IModuleService _moduleServices;
        private readonly ILevelServices _levelServices;
        private readonly IVideoMasterService _videoServices;
        private readonly IImageMasterService _imageMasterService;
        private readonly INotificationService _notificationService;


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
                                     INotificationService notificationService) : base(workContext: workContext,
                                                                                  httpContextAccessor: httpContextAccessor,
                                                                                  authenticationService: authenticationService)
        {
            _weeklyVideoServices = weeklyVideoServices;
            _moduleServices = moduleService;
            _levelServices = levelServices;
            _videoServices = videoService;
            _imageMasterService = imageMasterService;
            _notificationService = notificationService;
        }
        #endregion

        #region Utilities
        public virtual void PrepareVideoUrl(LevelModel model)
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
        }

        public virtual void PrepareImageUrl(LevelModel model)
        {
            model.AvailableImages.Add(new SelectListItem { Text = "Select Image", Value = "0" });
            var AvailableImageUrl = _imageMasterService.GetAllImages();
            foreach(var url in AvailableImageUrl)
            {
                model.AvailableImages.Add(new SelectListItem
                {
                    Value = url.Id.ToString(),
                    Text = url.Name,
                    //Selected = url.Id == model.Image.Id
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
            AddBreadcrumbs("Level", "Create", "/Level/List", "/Level/Create");
            LevelModel model = new LevelModel();
            PrepareVideoUrl(model);
            PrepareImageUrl(model);
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(LevelModel model)
        {
            AddBreadcrumbs("Level", "Create", "/Level/List", "/Level/Create");

            if (ModelState.IsValid)
            {
                model.VideoId =  (model.VideoId == 0) ? model.VideoId = null : model.VideoId;
                var data = model.ToEntity<Level>();
                data.Title = "Level " + (_levelServices.GetAllLevels().Count + 1);
                _levelServices.InsertLevel(data);
                _notificationService.SuccessNotification("New video lesson has been created successfully.");
                return RedirectToAction("Index", "Home");
            }
            PrepareVideoUrl(model);
            PrepareImageUrl(model);

            return View(model);
        }

        public IActionResult List()
        {
            AddBreadcrumbs("Level", "List", "/Level/List", "/Level/List");

            var model = new List<LevelModel>();
            var data = _levelServices.GetAllLevels();
            if(data.Count() != 0)
            {
                foreach(var item in data)
                {
                    model.Add(item.ToModel<LevelModel>());
                }
                return View(model);
            }
            return RedirectToAction("Index", "Home");

        }


        public IActionResult Play(string url)
        {
            var model = new LevelModel();
            model.VideoUrl = url;
            return View(model);
        }

        public IActionResult Edit(int id,int ModuleId, int srno)
        {
            AddBreadcrumbs("Level", "Edit", "/Level/List", $"/Level/Edit/{id}");

           
            ViewBag.ActiveTab = "Level";

            if (id != 0)
            {
                var data = _levelServices.GetLevelById(id);
                
                var model = data.ToModel<LevelModel>();
                
                model.srno = srno;
                model.ModuleList = _moduleServices.GetModulesByLevelId(id);

                if( ModuleId > 0 && ModuleId != 0)
                {
                    var module = _moduleServices.GetModuleById(ModuleId);
                    model.Modules.VideoId = module.VideoId;
                    model.Modules.FullDescription = module.FullDescription;
                    model.Modules.Id = module.Id;
                    ViewBag.ActiveTab = "Add Module";
                }

                PrepareVideoUrl(model);
                PrepareImageUrl(model);

                return View(model);
            }
            return RedirectToAction("List");

        }

        [HttpPost]
        public IActionResult Edit(LevelModel model)
        {
            AddBreadcrumbs("Level", "Edit", "/Level/List", $"/Level/Edit/{model.Id}");

            if (ModelState.IsValid)
            {
                model.VideoId = (model.VideoId == 0) ? model.VideoId = null : model.VideoId;

                var levelData = _levelServices.GetLevelById(model.Id);
                levelData.Title = "Level " + model.srno;
                levelData.Subtitle = model.Subtitle;
                levelData.Quote = model.Quote;
                levelData.FullDescription = model.FullDescription;
                levelData.VideoId = model.VideoId;
                _levelServices.UpdateLevel(levelData);
                _notificationService.SuccessNotification("video lesson has been edited successfully.");

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddModule(LevelModel model)
        {
           AddBreadcrumbs("Level", "Edit", "/Level/List", $"/Level/Edit/{model.Id}");

            if (model.Modules.FullDescription != null)
            {
                if (model.Modules.Id == 0)
                {
                    model.Modules.VideoId = (model.Modules.VideoId == 0) ? model.Modules.VideoId = null : model.Modules.VideoId;

                    var modules = new Modules();
                    modules.LevelId = model.Id;
                    modules.VideoId = model.Modules.VideoId;
                    modules.FullDescription = model.Modules.FullDescription;
                    _moduleServices.InsertModule(modules);
                    _notificationService.SuccessNotification("Module has been added successfully");

                }
                else
                {
                    model.Modules.VideoId = (model.Modules.VideoId == 0) ? model.Modules.VideoId = null : model.Modules.VideoId;

                    var modules = _moduleServices.GetModuleById(model.Modules.Id);
                    modules.LevelId = model.Id;
                    modules.VideoId = model.Modules.VideoId;
                    modules.FullDescription = model.Modules.FullDescription;
                    _moduleServices.UpdateModule(modules);
                    _notificationService.SuccessNotification("Module has been updated successfully");

                }

                return RedirectToAction("Edit", "Level", new { id = model.Id });

            }
            _notificationService.ErrorNotification("The filed Full Description is required.");
            return RedirectToAction("Edit", "Level", new { id = model.Id });


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

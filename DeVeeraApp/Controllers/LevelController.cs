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
        private readonly INotificationService _notificationService;


        #endregion


        #region ctor
        public LevelController(IWeeklyUpdateServices weeklyVideoServices,
                                     IModuleService moduleService,
                                     ILevelServices levelServices,
                                     IVideoMasterService videoService,
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
            _notificationService = notificationService;
        }
        #endregion

        #region Utilities
        public virtual void PrepareVideoUrl(LevelModel model)
        {
            //prepare available url
            model.AvailableVideoUrl.Add(new SelectListItem { Text = "Select VideoUrl", Value = "0" });
            var AvailableVideoUrl = _videoServices.GetAllVideos();
            foreach (var url in AvailableVideoUrl)
            {
                model.AvailableVideoUrl.Add(new SelectListItem
                {
                    Value = url.VideoUrl,
                    Text = url.VideoUrl,
                    Selected = url.Id == model.VideoId
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
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(LevelModel model)
        {
            AddBreadcrumbs("Level", "Create", "/Level/List", "/Level/Create");

            if (ModelState.IsValid)
            {

                var data = model.ToEntity<Level>();
                data.Title = "Level " + (_levelServices.GetAllLevels().Count + 1);
                _levelServices.InsertLevel(data);
                _notificationService.SuccessNotification("New video lesson has been created successfully.");
                return RedirectToAction("Edit", "Level", new { id = data.Id });
            }
            PrepareVideoUrl(model);
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
                    model.Modules.VideoURL = module.VideoURL;
                    model.Modules.FullDescription = module.FullDescription;
                    model.Modules.Id = module.Id;
                    ViewBag.ActiveTab = "Add Module";
                }

                PrepareVideoUrl(model);
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
                var data = model.ToEntity<Level>();
                data.Title = "Level " + model.srno;
                _levelServices.UpdateLevel(data);
                _notificationService.SuccessNotification("video lesson has been edited successfully.");

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddModule(LevelModel model)
        {
           AddBreadcrumbs("Level", "Edit", "/Level/List", $"/Level/Edit/{model.Id}");

            if (model.Modules.Id == 0)
            { 
                var modules = new Modules();
                modules.LevelId = model.Id;
                modules.VideoURL = model.Modules.VideoURL;
                modules.FullDescription = model.Modules.FullDescription;
                _moduleServices.InsertModule(modules);
            }
            else
            {
                var modules = _moduleServices.GetModuleById(model.Modules.Id);
                modules.LevelId = model.Id;
                modules.VideoURL = model.Modules.VideoURL;
                modules.FullDescription = model.Modules.FullDescription;
                _moduleServices.UpdateModule(modules);
            }

            return RedirectToAction("Edit","Level", new { id = model.Id });
           
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

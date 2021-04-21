using CRM.Core;
using CRM.Core.Domain;
using CRM.Services;
using CRM.Services.Authentication;
using CRM.Services.Message;
using DeVeeraApp.Utils;
using DeVeeraApp.ViewModels;
using DeVeeraApp.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IVideoServices _videoServices;
        private readonly INotificationService _notificationService;


        #endregion


        #region ctor
        public LevelController(IWeeklyUpdateServices weeklyVideoServices,
                                     IVideoServices videoServices,
                                     IWorkContext workContext,
                                     IHttpContextAccessor httpContextAccessor,
                                     IAuthenticationService authenticationService,
                                     INotificationService notificationService) : base(workContext: workContext,
                                                                                  httpContextAccessor: httpContextAccessor,
                                                                                  authenticationService: authenticationService)
        {
            _weeklyVideoServices = weeklyVideoServices;
            _videoServices = videoServices;
            _notificationService = notificationService;
        }
        #endregion


        #region Methods
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Create()
        {
            AddBreadcrumbs("Level", "Create", "/UploadVideo/List", "/UploadVideo/Create");

            return View();
        }

        [HttpPost]
        public IActionResult Create(LevelModel model)
        {
            AddBreadcrumbs("Level", "Create", "/UploadVideo/List", "/UploadVideo/Create");

            if (ModelState.IsValid)
            {
                var data = model.ToEntity<Level>();
                _videoServices.InsertVideo(data);
                _notificationService.SuccessNotification("New video lesson has been created successfully.");
                return RedirectToAction("List");
            }
            return View();
        }

        public IActionResult List()
        {
            AddBreadcrumbs("Level", "List", "/UploadVideo/List", "/UploadVideo/List");

            var model = new List<LevelModel>();
            var data = _videoServices.GetAllVideos();
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

        public IActionResult Edit(int id)
        {
            AddBreadcrumbs("Level", "Edit", "/UploadVideo/List", $"/UploadVideo/Edit/{id}");

            if (id != 0)
            {
                var data = _videoServices.GetVideoById(id);
                var model = data.ToModel<LevelModel>();
                return View(model);
            }
            return RedirectToAction("List");

        }

        [HttpPost]
        public IActionResult Edit(LevelModel model)
        {
            AddBreadcrumbs("Level", "Edit", "/UploadVideo/List", $"/UploadVideo/Edit/{model.Id}");

            if (ModelState.IsValid)
            {
                var data = model.ToEntity<Level>();
                _videoServices.UpdateVideo(data);
                _notificationService.SuccessNotification("video lesson has been edited successfully.");

                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        public IActionResult Delete(int videoId)
        {
           
            ResponseModel response = new ResponseModel();

            if (videoId != 0)
            {
                var videoData = _videoServices.GetVideoById(videoId);
                if (videoData == null)
                {
                    response.Success = false;
                    response.Message = "No user found";
                }
                _videoServices.DeleteVideo(videoData);

                response.Success = true;
            }
            else
            {
                response.Success = false;
                response.Message = "videoId is 0";

            }
            return Json(response);
        }


        #endregion
    }
}

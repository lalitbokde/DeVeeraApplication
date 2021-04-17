using CRM.Core;
using CRM.Core.Domain;
using CRM.Services;
using CRM.Services.Authentication;
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
    public class UploadVideoController : BaseController
    {
        #region fields

        private readonly IWeeklyUpdateServices _weeklyVideoServices;
        private readonly IVideoServices _videoServices;


        #endregion


        #region ctor
        public UploadVideoController(IWeeklyUpdateServices weeklyVideoServices,
                                     IVideoServices videoServices,
                                     IWorkContext workContext,
                                     IHttpContextAccessor httpContextAccessor,
                                     IAuthenticationService authenticationService) : base(workContext: workContext,
                                                                                  httpContextAccessor: httpContextAccessor,
                                                                                  authenticationService: authenticationService)
        {
            _weeklyVideoServices = weeklyVideoServices;
            _videoServices = videoServices;
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
        public IActionResult Create(VideoModel model)
        {
            AddBreadcrumbs("Level", "Create", "/UploadVideo/List", "/UploadVideo/Create");

            if (ModelState.IsValid)
            {
                var data = model.ToEntity<Video>();
                _videoServices.InsertVideo(data);
                return RedirectToAction("List");
            }
            return View();
        }

        public IActionResult List()
        {
            AddBreadcrumbs("Level", "List", "/UploadVideo/List", "/UploadVideo/List");

            var model = new List<VideoModel>();
            var data = _videoServices.GetAllVideos();
            if(data.Count() != 0)
            {
                foreach(var item in data)
                {
                    model.Add(item.ToModel<VideoModel>());
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
                var model = data.ToModel<VideoModel>();
                return View(model);
            }
            return RedirectToAction("List");

        }

        [HttpPost]
        public IActionResult Edit(VideoModel model)
        {
            AddBreadcrumbs("Level", "Edit", "/UploadVideo/List", $"/UploadVideo/Edit/{model.Id}");

            if (ModelState.IsValid)
            {
                var data = model.ToEntity<Video>();
                _videoServices.UpdateVideo(data);
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

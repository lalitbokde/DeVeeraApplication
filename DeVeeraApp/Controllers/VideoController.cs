using CRM.Core.Domain;
using CRM.Services;
using CRM.Services.Message;
using DeVeeraApp.Utils;
using DeVeeraApp.ViewModels;
using DeVeeraApp.ViewModels.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.Controllers
{
    public class VideoController : Controller
    {
        #region field

        private readonly INotificationService _notificationService;
        private readonly IVideoMasterService _videoMasterService;
        private readonly IVideoUploadService _videoUploadService;
        private readonly IHostingEnvironment _hostingEnvironment;

        #endregion


        #region ctor

        public VideoController(INotificationService notificationService,
                       IVideoMasterService videoMasterService,
                       IVideoUploadService videoUploadService,
                       IHostingEnvironment hostingEnvironment)
        {
            _notificationService = notificationService;
            _videoMasterService = videoMasterService;
            _videoUploadService = videoUploadService;
            _hostingEnvironment = hostingEnvironment;
        }

        #endregion


        #region Method

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Create(VideoModel model)
        {
            if (ModelState.IsValid)
            {
                var data = model.ToEntity<Video>();
                _videoMasterService.InsertVideo(data);
                _notificationService.SuccessNotification("Video url added successfully.");
                return RedirectToAction("List");
            }
            return View();

        }

        public IActionResult Edit(int id)
        {
            if(id != 0)
            {
                var data = _videoMasterService.GetVideoById(id);
                if(data != null)
                {
                    var model = data.ToModel<VideoModel>();
                    return View(model);
                }
            }
            return RedirectToAction("List");

        }

        [HttpPost]
        public IActionResult Edit(VideoModel model)
        {

            if (ModelState.IsValid)
            {
                var videoData = _videoMasterService.GetVideoById(model.Id);
                videoData.Name = model.Name;
                videoData.VideoUrl = model.VideoUrl;
                _videoMasterService.UpdateVideo(videoData);
                _notificationService.SuccessNotification("Video url updated successfully.");
                return RedirectToAction("List");
            }
            return View();

        }

        public IActionResult List()
        {
            var videoList = _videoMasterService.GetAllVideos();
            var model = new List<VideoModel>();
            if(videoList.Count != 0)
            {
                foreach(var item in videoList)
                {
                    model.Add(item.ToModel<VideoModel>());
                }
                return View(model);

            }
            return RedirectToAction("Index", "Home");

        }

        public IActionResult Delete(int videoId)
        {
            ResponseModel response = new ResponseModel();

            if (videoId != 0)
            {
                var videoData = _videoMasterService.GetVideoById(videoId);
                if (videoData == null)
                {
                    response.Success = false;
                    response.Message = "No video found";
                }
                _videoMasterService.DeleteVideo(videoData);

                response.Success = true;
            }
            else
            {
                response.Success = false;
                response.Message = "videoId is 0";

            }
            return Json(response);
        }


        public async Task UploadVideo()
        {
            var path = Path.Combine( _hostingEnvironment.WebRootPath + "\\Video", "THE SEED __ Inspirational Short Film.mp4");
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var val = await _videoUploadService.UploadFileAsync(stream, path);

                await stream.CopyToAsync(memory);
            }
        }
        #endregion


    }
}

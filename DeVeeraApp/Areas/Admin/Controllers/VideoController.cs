﻿using CRM.Core;
using CRM.Core.Domain;
using CRM.Services;
using CRM.Services.Authentication;
using CRM.Services.Message;
using DeVeeraApp.Filters;
using DeVeeraApp.Utils;
using DeVeeraApp.ViewModels;
using DeVeeraApp.ViewModels.Common;
using Microsoft.AspNetCore.Hosting;

using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthorizeAdmin]
    public class VideoController :BaseController
    {
        #region field

        private readonly INotificationService _notificationService;
        private readonly IVideoMasterService _videoMasterService;

        private readonly IS3BucketService _videoUploadService;
        private readonly IWebHostEnvironment _hostingEnvironment;


        #endregion


        #region ctor

        public VideoController(INotificationService notificationService,
                       IVideoMasterService videoMasterService,

                       IS3BucketService videoUploadService,

                       IWebHostEnvironment hostingEnvironment, IWorkContext workContext,
                               IHttpContextAccessor httpContextAccessor,
                               IAuthenticationService authenticationService) : base(workContext: workContext,
                                                                                  httpContextAccessor: httpContextAccessor,
                                                                                  authenticationService: authenticationService)
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
            AddBreadcrumbs("Video", "Create", "/Admin/Video/List", "/Admin/Video/Create");
            return View();

        }
        [HttpPost]
        [Obsolete]
        public IActionResult Create(VideoModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var videonameCheck = _videoMasterService.GetAllVideos().Where(a=>a.Name==model.Name).ToList().FirstOrDefault();
                    if (videonameCheck!= null)
                    {
                        _notificationService.SuccessNotification("Video Name already Exist .");
                        return RedirectToAction("Create");
                    }

                    var data = new Video();
                    var url = ""; var url2 = "";
                    if (model.FileName != null) { 
                     url = UploadVideo(model.FileName).Result.ToString();
                    }
                    if (model.SpanishFileName != null)
                    {
                        url2 = UploadVideo(model.SpanishFileName).Result.ToString();
                    }
                    if (model.SpanishFileName==null && url2 ==null) 
                    {
                        model.SpanishFileName = model.FileName;
                        url2 = url;
                    }
                    data.Key = model.FileName;
                    if (url!=null && url!="") {
                    data.VideoUrl = url;
                    }
                    data.Name = model.Name;
                    //spanish
                    data.SpanishKey = model.SpanishFileName;
                    if (url2 != null && url2 != "")
                    {
                        data.SpanishVideoUrl = url2;
                    }
                    data.IsNew = model.IsNew;
                    data.CreatedOn = DateTime.Now;
                    data.UpdatedOn = DateTime.Now;
                    _videoMasterService.InsertVideo(data);
                    _notificationService.SuccessNotification("Video url added successfully.");
                    return RedirectToAction("List");
                }

            }
            catch (Exception ex)
            {
                _notificationService.ErrorNotification(ex.Message);
                return RedirectToAction("List");
            }
            return View();

        }

        //[Obsolete]
        //public async Task<bool> ConvertVideo(string OriginalFileName, string CompressedFileName)
        //{


        //    var originalFile = Path.Combine(_hostingEnvironment.WebRootPath, OriginalFileName);
        //    var CompressedFile = Path.Combine(_hostingEnvironment.WebRootPath + "/Files", CompressedFileName);
        //    //linux
        //   // FFmpeg.ExecutablesPath = Path.Combine("/usr/bin");
        //    //windows
        //     FFmpeg.ExecutablesPath = Path.Combine(_hostingEnvironment.WebRootPath, "FFmpeg");
           
        //    var info = await MediaInfo.Get(originalFile);

        //    var videoStream = info.VideoStreams.First().SetCodec(VideoCodec.H264).SetSize(VideoSize.Hd480);
        //    var audioStream = info.AudioStreams.First().SetCodec(AudioCodec.Aac);
        //    await Conversion.New().AddStream(audioStream).AddStream(videoStream).SetOutput(CompressedFile).Start();

        //    FileInfo file = new FileInfo(OriginalFileName);
        //    if (file.Exists)//check file exsit or not  
        //    {
        //        file.Delete();

        //    }
        //    return true;

        //}

        public IActionResult Edit(int id)        
        {
            AddBreadcrumbs("Video", "Edit", "/Admin/Video/List", $"/Admin/Video/Edit/{id}");
           

            if (id != 0)
            {
                var data = _videoMasterService.GetVideoById(id);
                if (data != null)
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
                try
                {
                    if(model.FileName != null)
                    {
                        var url = UploadVideo(model.FileName);
                      
                        videoData.VideoUrl = url.Result;
                        
                        videoData.Key = model.FileName;
                      
                        videoData.UpdatedOn = DateTime.Now;

                    }

                    if (model.SpanishFileName != null)
                    {
                       
                        var url2 = UploadVideo(model.SpanishFileName);
                      
                        videoData.SpanishVideoUrl = url2.Result;
                
                        videoData.SpanishKey = model.SpanishFileName;
                        videoData.UpdatedOn = DateTime.Now;

                    }
                }
                catch
                {

                }
               
                videoData.Name = model.Name;
                videoData.IsNew = model.IsNew;

                _videoMasterService.UpdateVideo(videoData);
                _notificationService.SuccessNotification("Video url updated successfully.");
                return RedirectToAction("List");
            }
            return View();

        }

        public IActionResult List()
        {
            AddBreadcrumbs("Video", "List", "/Admin/Video/List", "/Admin/Video/List");
           
            var videoList = _videoMasterService.GetAllVideos();
            var model = new List<VideoModel>();
            if (videoList.Count != 0)
            {
                model = videoList.ToList().ToModelList<Video, VideoModel>(model);


               ViewBag.Video = JsonConvert.SerializeObject(model);
                return View(model);

            }
            return View(model);
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public bool Upload(IFormFile file)
        {

            var FileDic = "Files";


            string FilePath = Path.Combine(_hostingEnvironment.WebRootPath, FileDic);

            if (!Directory.Exists(FilePath))

                Directory.CreateDirectory(FilePath);

            string fileName =  file.FileName;
    

            var filePath = Path.Combine(FilePath, fileName);

            using (FileStream fs = System.IO.File.Create(filePath))
            {
                file.CopyTo(fs);
            }

            _ = Path.Combine(FilePath, fileName);
            _ = file.FileName;
            //if (file.Length >= 104857600)
            //{
            //    await ConvertVideo(OriginalFileName, CompressedFileName);
            //}
            return true;
        }

        [HttpGet]
        public IActionResult Play(int Id,int srno)
        {
                var data = _videoMasterService.GetVideoById(Id);
              
                var model = data.ToModel<VideoModel>();
            ViewBag.srno = srno;
            if(srno==1 && model.VideoUrl == null)
            {
                TempData["VideoUrl"] = 1;
            }
            if (srno == 2 && model.SpanishVideoUrl == null)
            {
                TempData["SpanishVideoUrl"] = 2;
            }
            return View(model);
        
        }
        public IActionResult DeleteVideo(int videoId,int keyId)
        {
            ResponseModel response = new ResponseModel();
            //,string keyId,string language
            if (videoId != 0)
            {
                var data = _videoMasterService.GetVideoById(videoId);

                if (data == null)
                {
                    response.Success = false;
                    response.Message = "No video found";
                }

            if (keyId == 1) 
            {
                 _videoUploadService.DeleteFile(data.Key);

                data.Key = null;
                data.VideoUrl = null;
            }
            else
            {
                    _videoUploadService.DeleteFile(data.SpanishKey);

                    data.SpanishKey = null;
                    data.SpanishVideoUrl = null;
                }

                _videoMasterService.UpdateVideo(data);
                response.Success = true;

            }
            else
            {
                response.Success = false;
                response.Message = "videoId is 0";

            }
            return Json(response);
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
                _videoUploadService.DeleteFile(videoData.Key);
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


        public async Task<string> UploadVideo(string fileName)
        {
            string val;
            var path = Path.Combine(_hostingEnvironment.WebRootPath + "//Files", fileName);
            _ = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                val = await _videoUploadService.UploadFileAsync(stream, path, fileName);


            }
            return val;
        }
        #endregion


    }
}

using CRM.Core.Domain;
using CRM.Services;
using CRM.Services.Message;
using DeVeeraApp.Utils;
using DeVeeraApp.ViewModels;
using DeVeeraApp.ViewModels.Common;
using Microsoft.AspNetCore.Hosting;

using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Enums;

namespace DeVeeraApp.Controllers
{
    public class VideoController : Controller
    {
        #region field

        private readonly INotificationService _notificationService;
        private readonly IVideoMasterService _videoMasterService;

        private readonly IS3BucketService _videoUploadService;
        private readonly IHostingEnvironment _hostingEnvironment;



        #endregion


        #region ctor

        public VideoController(INotificationService notificationService,
                       IVideoMasterService videoMasterService,

                       IS3BucketService videoUploadService,

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
        [Obsolete]
        public IActionResult Create(VideoModel model)
        {
            if (ModelState.IsValid)
            {
                var url = UploadVideo(model.FileName);
                var data = new Video();
                data.Key = model.FileName;
                data.VideoUrl = url.Result.ToString();
                data.Name = model.Name;
                _videoMasterService.InsertVideo(data);
                _notificationService.SuccessNotification("Video url added successfully.");
                return RedirectToAction("List");
            }
            return View();

        }

        [Obsolete]
        public async Task<bool> ConvertVideo(string OriginalFileName, string CompressedFileName)
        {
            try
            {


                var originalFile = Path.Combine(_hostingEnvironment.WebRootPath, OriginalFileName);
                var CompressedFile = Path.Combine(_hostingEnvironment.WebRootPath + "/Files", CompressedFileName);
                //linux
                FFmpeg.ExecutablesPath = Path.Combine("/usr/bin");
               //windows
                //FFmpeg.ExecutablesPath = Path.Combine(_hostingEnvironment.WebRootPath, "FFmpeg");

                var info = await MediaInfo.Get(originalFile);

                var videoStream = info.VideoStreams.First().SetCodec(VideoCodec.H264).SetSize(VideoSize.Hd480);
                var audioStream = info.AudioStreams.First().SetCodec(AudioCodec.Aac);
                await Conversion.New().AddStream(audioStream).AddStream(videoStream).SetOutput(CompressedFile).Start();

                FileInfo file = new FileInfo(OriginalFileName);
                if (file.Exists)//check file exsit or not  
                {
                    file.Delete();

                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return true;
        }

        public IActionResult Edit(int id)
        {
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
                var url = UploadVideo(model.FileName);
                videoData.Name = model.Name;
                videoData.VideoUrl = url.Result;
                videoData.Key = model.FileName;
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
            if (videoList.Count != 0)
            {
                foreach (var item in videoList)
                {
                    model.Add(item.ToModel<VideoModel>());
                }
                return View(model);

            }
            return RedirectToAction("Index", "Home");

        }

        [Obsolete]
        public async Task<bool> Upload(IFormFile file)
        {

            var FileDic = "Files";

            string FilePath = Path.Combine(_hostingEnvironment.WebRootPath, FileDic);

            if (!Directory.Exists(FilePath))

                Directory.CreateDirectory(FilePath);

            var fileName = file.FileName;

            var filePath = Path.Combine(FilePath, "UploadedVideo.mp4");

            using (FileStream fs = System.IO.File.Create(filePath))
            {
                file.CopyTo(fs);
            }

            var OriginalFileName = Path.Combine(FilePath, "UploadedVideo.mp4");

            var CompressedFileName = fileName;

            await ConvertVideo(OriginalFileName, CompressedFileName);

            return true;
        }


        public IActionResult Play(int Id)
        {
            if(Id != 0)
            {
                var data = _videoMasterService.GetVideoById(Id);
                data.VideoUrl = _videoUploadService.GetPreSignedURL(data.Key).Result;
                _videoMasterService.UpdateVideo(data);
                var model = data.ToModel<VideoModel>();
                return View(model);
            }
            return RedirectToAction("List");
          
        }
        public IActionResult DeleteVideo(int videoId)
        {
            ResponseModel response = new ResponseModel();

            if (videoId != 0)
            {
                var data = _videoMasterService.GetVideoById(videoId);

                if(data == null)
                {
                    response.Success = false;
                    response.Message = "No video found";
                }
                _videoUploadService.DeleteFile(data.Key);

                data.Key = null;
                data.VideoUrl = null;

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
            var path = Path.Combine( _hostingEnvironment.WebRootPath + "\\Files", fileName);
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                 val = await _videoUploadService.UploadFileAsync(stream, path, fileName);

               
            }
            return val;
        }
        #endregion


    }
}

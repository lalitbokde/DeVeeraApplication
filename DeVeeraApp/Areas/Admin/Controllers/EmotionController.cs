using CRM.Core;
using CRM.Core.Domain.Emotions;
using CRM.Services;
using CRM.Services.Authentication;
using CRM.Services.Emotions;
using CRM.Services.Message;
using DeVeeraApp.Filters;
using DeVeeraApp.Utils;
using DeVeeraApp.ViewModels.Common;
using DeVeeraApp.ViewModels.Emotions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthorizeAdmin]
    public class EmotionController : BaseController
    {
        #region fields

        private readonly INotificationService _notificationService;
        private readonly IEmotionService _emotionService;
        private readonly IImageMasterService _imageMasterService;
        private readonly IVideoMasterService _videoServices;

        #endregion

        #region ctor

        public EmotionController(IEmotionService emotionService,
                                 INotificationService notificationService,
                                 IImageMasterService imageMasterService,
                                 IVideoMasterService videoMasterService,
                                 IWorkContext workContext,
                                 IHttpContextAccessor httpContextAccessor,
                                 IAuthenticationService authenticationService) : base(workContext: workContext,
                                                                                  httpContextAccessor: httpContextAccessor,
                                                                                  authenticationService: authenticationService)

        {
            _emotionService = emotionService;
            _notificationService = notificationService;
            _imageMasterService = imageMasterService;
            _videoServices = videoMasterService;
        }
        #endregion
        #region Utilities
        public virtual void PrepareEmotionModel(EmotionModel model)
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
            var AvailableImages = _imageMasterService.GetAllImages();
            foreach (var item in AvailableImages)
            {
                model.AvailableImages.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name,
                });
            }

        }

        #endregion
        #region Methods
        public IActionResult List()
        {
            AddBreadcrumbs("Emotion", "List", "/Admin/Emotion/List", "/Admin/Emotion/List");
            var List = _emotionService.GetAllEmotions();
            var model = new List<EmotionModel>();
            if (List.Count != 0)
            {
                model = List.ToList().ToModelList<Emotion, EmotionModel>(model);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            AddBreadcrumbs("Emotion", "Create", "/Admin/Emotion/List", "/Admin/Emotion/Create");
            EmotionModel model = new EmotionModel();
            PrepareEmotionModel(model);
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(EmotionModel model)
        {
            AddBreadcrumbs("Emotion", "Create", "/Emotion/List", "/Emotion/Create");
            if (ModelState.IsValid)
            {
                var emotion = model.ToEntity<Emotion>();
                emotion.CreatedOn = DateTime.UtcNow;
                _emotionService.InsertEmotion(emotion);
                _notificationService.SuccessNotification("Emotion added successfully.");
                return RedirectToAction("List");
            }
            PrepareEmotionModel(model);
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            AddBreadcrumbs("Emotion", "Edit", "/Admin/Emotion/List", $"/Admin/Emotion/Edit/{id}");
            var emotion = _emotionService.GetEmotionById(id);
            var model = emotion.ToModel<EmotionModel>();
            model.EmotionHeaderImageUrl = emotion.EmotionHeaderImageId > 0 ? _imageMasterService.GetImageById(emotion.EmotionHeaderImageId)?.ImageUrl : null;
            model.EmotionBannerImageUrl = emotion.EmotionBannerImageId > 0 ? _imageMasterService.GetImageById(emotion.EmotionBannerImageId)?.ImageUrl : null;
            model.EmotionThumbnailImageUrl = emotion.EmotionThumbnailImageId > 0 ? _imageMasterService.GetImageById(emotion.EmotionThumbnailImageId)?.ImageUrl : null;
            PrepareEmotionModel(model);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EmotionModel model)
        {
            AddBreadcrumbs("Emotion", "Edit", "/Admin/Emotion/List", $"/Admin/Emotion/Edit/{model.Id}");
            if (ModelState.IsValid)
            {
                var emotion = _emotionService.GetEmotionById(model.Id);
                emotion.EmotionName = model.EmotionName;
                emotion.EmotionNo = model.EmotionNo;
                emotion.Title = model.Title;
                emotion.Subtitle = model.Subtitle;
                emotion.Quote = model.Quote;
                emotion.Description = model.Description;
                emotion.VideoId = model.VideoId;
                emotion.IsRandom = model.IsRandom;
                emotion.EmotionHeaderImageId = model.EmotionHeaderImageId;
                emotion.EmotionBannerImageId = model.EmotionBannerImageId;
                emotion.EmotionThumbnailImageId = model.EmotionThumbnailImageId;
                emotion.LastUpdatedOn = DateTime.UtcNow;

                _emotionService.UpdateEmotion(emotion);
                _notificationService.SuccessNotification("Emotion updated successfully.");

                return RedirectToAction("List");

            }
            PrepareEmotionModel(model);
            return View(model);
        }

        public IActionResult Delete(int emotionId)
        {
            ResponseModel response = new ResponseModel();

            if (emotionId != 0)
            {
                var Data = _emotionService.GetEmotionById(emotionId);
                if (Data == null)
                {
                    response.Success = false;
                    response.Message = "No Data found";
                }
                _emotionService.DeleteEmotion(Data);

                response.Success = true;
            }
            else
            {
                response.Success = false;
                response.Message = "No Data found";

            }
            return Json(response);
        }
        #endregion
    }
}
﻿using CRM.Core;
using CRM.Core.Domain.Emotions;
using CRM.Services;
using CRM.Services.Authentication;
using CRM.Services.DashboardQuotes;
using CRM.Services.Emotions;
using CRM.Services.Localization;
using CRM.Services.Message;
using DeVeeraApp.Filters;
using DeVeeraApp.Utils;
using DeVeeraApp.ViewModels.Common;
using DeVeeraApp.ViewModels.Emotions;
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
    public class EmotionController : BaseController
    {
        #region fields

        private readonly INotificationService _notificationService;
        private readonly IEmotionService _emotionService;
        private readonly IImageMasterService _imageMasterService;
        private readonly IVideoMasterService _videoServices;
        private readonly IDashboardQuoteService _dashboardQuoteService;
        private readonly ITranslationService _translationService;
        private readonly ILocalStringResourcesServices _localStringResourcesServices;
        public string key = "AIzaSyC2wpcQiQQ7ASdt4vcJHfmly8DwE3l3tqE";
        #endregion

        #region ctor

        public EmotionController(IEmotionService emotionService,
                                 INotificationService notificationService,
                                 IImageMasterService imageMasterService,
                                 IVideoMasterService videoMasterService,
                                 IWorkContext workContext,
                                 IHttpContextAccessor httpContextAccessor,
                                 IAuthenticationService authenticationService,
                                 ITranslationService translationService,
                                  ILocalStringResourcesServices localStringResourcesServices,
                                  IDashboardQuoteService dashboardQuoteService) : base(workContext: workContext,
                                                                                  httpContextAccessor: httpContextAccessor,
                                                                                  authenticationService: authenticationService)

        {
            _emotionService = emotionService;
            _notificationService = notificationService;
            _imageMasterService = imageMasterService;
            _videoServices = videoMasterService;
            _dashboardQuoteService = dashboardQuoteService;
            _translationService = translationService;
            _localStringResourcesServices = localStringResourcesServices;
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


            //prepare Available Quotes

            model.AvilableQuote.Add(new SelectListItem { Text = "Select Quote", Value = "0" });
            var AvailableQuote = _dashboardQuoteService.GetAllDashboardQuotes();
            foreach (var quote in AvailableQuote)
            {
                model.AvilableQuote.Add(new SelectListItem
                {
                    Value = quote.Id.ToString(),
                    Text = quote.Title + " - " + quote.Author,
                    Selected = quote.Id == model.QuoteId
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

                ViewBag.Emotion = JsonConvert.SerializeObject(model);
                return View(model);
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
                _translationService.Translate(model.EmotionName, model.SpanishEmotionName);
                _translationService.Translate(model.Title, model.SpanishTitle);
                _translationService.Translate(model.Subtitle, model.SpanishSubtitle);
                _translationService.Translate(model.Description, model.SpanishDescription);
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
            model.SpanishEmotionName = _localStringResourcesServices.GetResourceValueByResourceName(model.EmotionName);
            model.SpanishTitle = _localStringResourcesServices.GetResourceValueByResourceName(model.Title);
            model.SpanishSubtitle = _localStringResourcesServices.GetResourceValueByResourceName(model.Subtitle);
            model.SpanishDescription = _localStringResourcesServices.GetResourceValueByResourceName(model.Description);
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
                emotion.QuoteId = model.QuoteId;

                _emotionService.UpdateEmotion(emotion);
                _translationService.Translate(model.EmotionName, model.SpanishEmotionName);
                _translationService.Translate(model.Title, model.SpanishTitle);
                _translationService.Translate(model.Subtitle, model.SpanishSubtitle);
                _translationService.Translate(model.Description, model.SpanishDescription);
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


        #region Translate

        [HttpPost]
        public IActionResult TranslateSpanish(EmotionModel level)
        {
            EmotionModel model = new EmotionModel();
            model.EmotionName = level.EmotionName != null ? _translationService.TranslateLevel(level.EmotionName, key) : "";
            model.Title = level.Title != null ? _translationService.TranslateLevel(level.Title, key) : "";
            model.Subtitle = level.Subtitle != null ? _translationService.TranslateLevel(level.Subtitle, key) : "";
            model.Description = level.Description != null ? _translationService.TranslateLevel(level.Description, key) : "";
            return Json(model);

        }
        [HttpPost]
        public IActionResult TranslateEnglish(EmotionModel level)
        {
            EmotionModel model = new EmotionModel();
            model.SpanishEmotionName = level.SpanishEmotionName != null ? _translationService.TranslateLevel(level.SpanishEmotionName, key) : "";
            model.SpanishTitle = level.SpanishTitle != null ? _translationService.TranslateLevel(level.SpanishTitle, key) : "";
            model.SpanishSubtitle = level.SpanishSubtitle != null ? _translationService.TranslateLevel(level.SpanishSubtitle, key) : "";
            model.SpanishDescription = level.SpanishDescription != null ? _translationService.TranslateLevel(level.SpanishDescription, key) : "";
            return Json(model);
        }

       
        #endregion
    }
}
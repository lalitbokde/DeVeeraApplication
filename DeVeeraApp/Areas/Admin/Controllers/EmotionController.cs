using CRM.Core;
using CRM.Core.Domain.Emotions;
using CRM.Services.Authentication;
using CRM.Services.Emotions;
using CRM.Services.Message;
using DeVeeraApp.Utils;
using DeVeeraApp.ViewModels.Common;
using DeVeeraApp.ViewModels.Emotions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmotionController : BaseController
    {
        #region fields

        private readonly INotificationService _notificationService;
        private readonly IEmotionService _emotionService;

        #endregion

        #region ctor

        public EmotionController(IEmotionService emotionService,
                                 INotificationService notificationService,
                                 IWorkContext workContext,
                                 IHttpContextAccessor httpContextAccessor,
                                 IAuthenticationService authenticationService) : base(workContext: workContext,
                                                                                  httpContextAccessor: httpContextAccessor,
                                                                                  authenticationService: authenticationService)

        {
            _emotionService = emotionService;
            _notificationService = notificationService;
        }
        #endregion

        #region Methods
        public IActionResult List()
        {
            AddBreadcrumbs("Emotion", "List", "/Emotion/List", "/Emotion/List");
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
            AddBreadcrumbs("Emotion", "Create", "/Emotion/List", "/Emotion/Create");
            EmotionModel model = new EmotionModel();
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
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            AddBreadcrumbs("Emotion", "Edit", "/Emotion/List", $"/Emotion/Edit/{id}");
            var emotion = _emotionService.GetEmotionById(id);
            var model = emotion.ToModel<EmotionModel>();
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EmotionModel model)
        {
            AddBreadcrumbs("Emotion", "Edit", "/Emotion/List", $"/Emotion/Edit/{model.Id}");
            if (ModelState.IsValid)
            {
                var emotion = _emotionService.GetEmotionById(model.Id);
                emotion.EmotionName = model.EmotionName;
                emotion.EmotionNo = model.EmotionNo;
                emotion.LastUpdatedOn = DateTime.UtcNow;

                _emotionService.UpdateEmotion(emotion);
                _notificationService.SuccessNotification("Emotion updated successfully.");

                return RedirectToAction("List");

            }

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
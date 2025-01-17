﻿using CRM.Core;
using CRM.Core.Domain;
using CRM.Services;

using CRM.Services.Message;

using CRM.Services.Authentication;

using DeVeeraApp.Utils;
using DeVeeraApp.ViewModels;
using DeVeeraApp.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using DeVeeraApp.Filters;
using CRM.Services.Localization;

namespace DeVeeraApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthorizeAdmin]
    public class FeelGoodStoryController : BaseController
    {

        #region fields
        private readonly IFeelGoodStoryServices _feelGoodStoryServices;
        private readonly IImageMasterService _imageMasterService;
        private readonly INotificationService _notificationService;
        private readonly ITranslationService _translationService;

        public string key = "AIzaSyC2wpcQiQQ7ASdt4vcJHfmly8DwE3l3tqE";
        #endregion

        #region ctor

        public FeelGoodStoryController(IFeelGoodStoryServices feelGoodStoryServices,

                                       INotificationService notificationService,

                                       IImageMasterService imageMasterService,                                      
                                       IWorkContext workContext,
                                       IHttpContextAccessor httpContextAccessor,
                                        ITranslationService translationService,
                                       IAuthenticationService authenticationService) : base(workContext: workContext,
                                                                                  httpContextAccessor: httpContextAccessor,
                                                                                  authenticationService: authenticationService)

        {
            _feelGoodStoryServices = feelGoodStoryServices;
            _imageMasterService = imageMasterService;
            _notificationService = notificationService;
            _translationService = translationService;
        }
        #endregion

        #region Utilities

        public virtual void PrepareImages(FeelGoodStoryModel model)
        {
            //prepare available url
            model.AvailableImages.Add(new SelectListItem { Text = "Select Image", Value = "0" });
            var AvailableVideoUrl = _imageMasterService.GetAllImages();
            foreach (var item in AvailableVideoUrl)
            {
                model.AvailableImages.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
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
            AddBreadcrumbs("FeelGoodStory", "Create", "/Admin/FeelGoodStory/List", "/Admin/FeelGoodStory/Create");
            FeelGoodStoryModel model = new FeelGoodStoryModel();
            PrepareImages(model);
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(FeelGoodStoryModel model)
        {
            if (ModelState.IsValid)
            {
                model.ImageId = (model.ImageId == 0) ? model.ImageId = null : model.ImageId;
                var data = new FeelGoodStory
                {
                    Author = model.Author,
                    Title = model.Title,
                    Story = model.Story,
                    ImageId = model.ImageId
                };

                _feelGoodStoryServices.InsertFeelGoodStory(data);
                _translationService.Translate(data.Title,key);
                _translationService.Translate(data.Story, key);
                _notificationService.SuccessNotification("Story added successfully");
                return RedirectToAction("List");
            }
            PrepareImages(model);

            return View();
        }


        public IActionResult Edit(int Id)
        {

            AddBreadcrumbs("FeelGoodStory", "Edit", $"/Admin/FeelGoodStory/List", $"/Admin/FeelGoodStory/Edit/{Id}");
            var data = _feelGoodStoryServices.GetFeelGoodStoryById(Id);
            var model = data.ToModel<FeelGoodStoryModel>();
            //PrepareImages(model);
            PrepareImageUrls(model);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(FeelGoodStoryModel model)
        {
            if (ModelState.IsValid)
            {
                model.ImageId = (model.ImageId == 0) ? model.ImageId = null : model.ImageId;

                var data = _feelGoodStoryServices.GetFeelGoodStoryById(model.Id);
                data.Author = model.Author;
                data.Title = model.Title;
                data.Story = model.Story;
                data.ImageId = model.ImageId;


                _feelGoodStoryServices.UpdateFeelGoodStory(data);
                _translationService.Translate(data.Title, key);
                _translationService.Translate(data.Story, key);
                _notificationService.SuccessNotification("Story updated successfully");

                return RedirectToAction("List");
            }
            PrepareImages(model);

            return View(model);
        }


        public IActionResult List()
        {
            AddBreadcrumbs("FeelGoodStory", "List", "/Admin/FeelGoodStory/List", "/Admin/FeelGoodStory/List");
            var model = new List<FeelGoodStoryModel>();
            var data = _feelGoodStoryServices.GetAllFeelGoodStorys();

            if(data.Count != 0)
            {
                model = data.ToList().ToModelList<FeelGoodStory, FeelGoodStoryModel>(model);


            }

            return View(model);
        }

        public IActionResult Delete(int storyId)
        {
            ResponseModel response = new ResponseModel();

            if (storyId != 0)
            {
                var storyData = _feelGoodStoryServices.GetFeelGoodStoryById(storyId);
                if (storyData == null)
                {
                    response.Success = false;
                    response.Message = "No video found";
                }
                _feelGoodStoryServices.DeleteFeelGoodStory(storyData);

                response.Success = true;
            }
            else
            {
                response.Success = false;
                response.Message = "videoId is 0";

            }
            return Json(response);
        }

        public virtual void PrepareImageUrls(FeelGoodStoryModel model)
        {

          model.Image.ImageUrl = model.ImageId > 0 ? _imageMasterService.GetImageById(model.Image.Id)?.ImageUrl : null;
           
        }

        #endregion
    }
}

using CRM.Core;
using CRM.Core.Domain;
using CRM.Core.Infrastructure;
using CRM.Services;
using CRM.Services.Authentication;
using CRM.Services.Message;
using DeVeeraApp.Filters;
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

namespace DeVeeraApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthorizeAdmin]
    public class WeeklyUpdateController : BaseController
    {

        #region fields
        private readonly IWeeklyUpdateServices _weeklyUpdateServices;
        private readonly INotificationService _notificationService;
        private readonly IVideoMasterService _videoServices;
        private readonly IImageMasterService _imageMasterService;

        #endregion

        #region ctor
        public WeeklyUpdateController(IWeeklyUpdateServices weeklyUpdateServices,
                                      IVideoMasterService videoService,
                                      IImageMasterService imageMasterService,
                                      IWorkContext workContext,
                                      IHttpContextAccessor httpContextAccessor,
                                      IAuthenticationService authenticationService,
                                      INotificationService notificationService) : base(workContext: workContext,
                                                                                  httpContextAccessor: httpContextAccessor,
                                                                                  authenticationService: authenticationService)
        {
            _weeklyUpdateServices = weeklyUpdateServices;
            _notificationService = notificationService;
            _videoServices = videoService;
            _imageMasterService = imageMasterService;
        }
        #endregion
        #region Utilities
        public virtual void PrepareVideo(WeeklyUpdateModel model)
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
        #region methods

        public IActionResult Create(CRM.Core.Domain.Quote type)
        {
            AddBreadcrumbs( $"{type} Page", "Create", $"/WeeklyUpdate/List?typeId={(int)type}", $"/WeeklyUpdate/Create?type={type}");
            WeeklyUpdateModel model = new WeeklyUpdateModel();
            ViewBag.QuoteType = type.ToString();
            PrepareVideo(model);
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(WeeklyUpdateModel model)
        {
            AddBreadcrumbs($"{model.QuoteType} Page", "Create", $"/WeeklyUpdate/List?typeId={(int)model.QuoteType}", $"/WeeklyUpdate/Create?type={model.QuoteType}");

            if (ModelState.IsValid)
            {
                _weeklyUpdateServices.InActiveAllActiveQuotes((int)model.QuoteType);

                var data = model.ToEntity<WeeklyUpdate>();
                _weeklyUpdateServices.InsertWeeklyUpdate(data);
                _notificationService.SuccessNotification("Video created successfully.");
                return RedirectToAction("List", "WeeklyUpdate", new { typeId = (int)model.QuoteType });
            }
            PrepareVideo(model);
            return View(model);
        }


        public IActionResult Edit(int id, CRM.Core.Domain.Quote type)
        {
            AddBreadcrumbs($"{type} Page", "Edit", $"/WeeklyUpdate/List?typeId={(int)type}", $"/WeeklyUpdate/Edit/{id}?type={type}");

            if (id != 0)
            {
                var data = _weeklyUpdateServices.GetWeeklyUpdateById(id);

                if (data != null)
                {
                    var model = data.ToModel<WeeklyUpdateModel>();
                    PrepareVideo(model);
                    return View(model);
                }

                return View();
            }
            return View();
        }

        [HttpPost]
        public IActionResult Edit(WeeklyUpdateModel model)
        {
            AddBreadcrumbs($"{model.QuoteType} Page", "Edit", $"/WeeklyUpdate/List?typeId={(int)model.QuoteType}", $"/WeeklyUpdate/Edit/{model.Id}?type={model.QuoteType}");

            if (ModelState.IsValid)
            {
                _weeklyUpdateServices.InActiveAllActiveQuotes((int)model.QuoteType);
                
                var val = _weeklyUpdateServices.GetWeeklyUpdateById(model.Id);
                val.IsActive = model.IsActive;
                val.VideoId = model.VideoId;
                val.Title = model.Title;
                val.Subtitle = model.Subtitle;
                val.QuoteType = (CRM.Core.Domain.Quote)model.QuoteType;
                val.LandingQuote = model.LandingQuote;
                val.DescriptionImageId = model.DescriptionImageId;

                val.SliderOneTitle = model.SliderOneTitle;
                val.SliderOneDescription = model.SliderOneDescription;
                val.SliderOneImageId = model.SliderOneImageId;
                val.SliderTwoTitle = model.SliderTwoTitle;
                val.SliderTwoDescription = model.SliderTwoDescription;
                val.SliderTwoImageId = model.SliderTwoImageId;
                val.SliderThreeTitle = model.SliderThreeTitle;
                val.SliderThreeDescription = model.SliderThreeDescription;
                val.SliderThreeImageId = model.SliderThreeImageId;

                _weeklyUpdateServices.UpdateWeeklyUpdate(val);
                _notificationService.SuccessNotification("Video edited successfully.");

                return RedirectToAction("List", "WeeklyUpdate",new { typeId = (int)model.QuoteType });
            }
            PrepareVideo(model);
            return View(model);
        }

        public IActionResult List(int typeId)
        {
            AddBreadcrumbs($"{(CRM.Core.Domain.Quote)typeId} Page", "List", $"/WeeklyUpdate/List?typeId={typeId}", $"/WeeklyUpdate/List?typeId={typeId}");

            ViewBag.QuoteType = EnumDescription.GetDescription((CRM.Core.Domain.Quote)typeId);
            ViewBag.Quote = (CRM.Core.Domain.Quote)typeId;
            var model = new List<WeeklyUpdateModel>();
            var data = _weeklyUpdateServices.GetWeeklyUpdatesByQuoteType(typeId);
            if (data.Count() != 0)
            {
                model = data.ToList().ToModelList<WeeklyUpdate, WeeklyUpdateModel>(model);


            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            ResponseModel response = new ResponseModel();

            if (id != 0)
            {
                var Data = _weeklyUpdateServices.GetWeeklyUpdateById(id);
                if (Data == null)
                {
                    response.Success = false;
                    response.Message = "No user found";
                }
                _weeklyUpdateServices.DeleteWeeklyUpdate(Data);

                response.Success = true;
            }
            else
            {
                response.Success = false;
                response.Message = "Id is 0";

            }
            return Json(response);
        }

        #endregion

    }
}

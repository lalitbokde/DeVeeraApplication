using CRM.Core;
using CRM.Core.Domain;
using CRM.Core.Infrastructure;
using CRM.Services;
using CRM.Services.Authentication;
using CRM.Services.DashboardQuotes;
using CRM.Services.Localization;
using CRM.Services.Message;
using DeVeeraApp.Filters;
using DeVeeraApp.Utils;
using DeVeeraApp.ViewModels;
using DeVeeraApp.ViewModels.Common;
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
    public class WeeklyUpdateController : BaseController
    {

        #region fields
        private readonly IWeeklyUpdateServices _weeklyUpdateServices;
        private readonly INotificationService _notificationService;
        private readonly IVideoMasterService _videoServices;
        private readonly IImageMasterService _imageMasterService;
        private readonly IDashboardQuoteService _dashboardQuoteService;
        private readonly ITranslationService _translationService;
        private readonly ILocalStringResourcesServices _localStringResourcesServices;
        public string key = "AIzaSyC2wpcQiQQ7ASdt4vcJHfmly8DwE3l3tqE";
        #endregion

        #region ctor
        public WeeklyUpdateController(IWeeklyUpdateServices weeklyUpdateServices,
                                      IVideoMasterService videoService,
                                      IImageMasterService imageMasterService,
                                      IWorkContext workContext,
                                      IHttpContextAccessor httpContextAccessor,
                                      IAuthenticationService authenticationService,
                                      INotificationService notificationService,
                                      ITranslationService translationService,
                                     IDashboardQuoteService dashboardQuoteService,
                                       ILocalStringResourcesServices localStringResourcesServices) : base(workContext: workContext,
                                                                                  httpContextAccessor: httpContextAccessor,
                                                                                  authenticationService: authenticationService)
        {
            _weeklyUpdateServices = weeklyUpdateServices;
            _notificationService = notificationService;
            _videoServices = videoService;
            _imageMasterService = imageMasterService;
            _dashboardQuoteService = dashboardQuoteService;
            _translationService = translationService;
            _localStringResourcesServices = localStringResourcesServices;
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
            model.AvailableBannerImage.Add(new SelectListItem { Text = "Select BannerImage", Value = "0" });
            model.AvailableImages.Add(new SelectListItem { Text = "Select BodyImages", Value = "0" });


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
            model.AvilableQuoteSpanish.Add(new SelectListItem { Text = "Select Quote", Value = "0" });
            var AvailableQuote = _dashboardQuoteService.GetAllDashboardQuotes();
            foreach (var quote in AvailableQuote)
            {
                var quotespanish = quote.Title != null ? _translationService.TranslateLevel(quote.Title + " - " + quote.Author, key) : "";
                model.AvilableQuote.Add(new SelectListItem
                {
                    Value = quote.Id.ToString(),
                    Text = quote.Title + " - " + quote.Author,
                    Selected = quote.Id == model.QuoteId
                });

                model.AvilableQuoteSpanish.Add(new SelectListItem
                {
                    Value = quote.Id.ToString(),
                    Text = quotespanish,
                    Selected = quote.Id == model.QuoteId
                });

            }

           
           



        }

        public virtual void PrepareImageUrls(WeeklyUpdateModel model)
        {
            model.landingImageOneUrl = model.SliderOneImageId > 0 ? _imageMasterService.GetImageById(model.SliderOneImageId)?.ImageUrl : null;
            model.landingImageTwoUrl = model.SliderTwoImageId > 0 ? _imageMasterService.GetImageById(model.SliderTwoImageId)?.ImageUrl : null;
            model.landingImageThreeUrl = model.SliderThreeImageId > 0 ? _imageMasterService.GetImageById(model.SliderThreeImageId)?.ImageUrl : null;
            model.DescriptionImageUrl = model.DescriptionImageId > 0 ? _imageMasterService.GetImageById(model.DescriptionImageId)?.ImageUrl : null;
            model.BannerImageURL = model.BannerImageId > 0 ? _imageMasterService.GetImageById(model.BannerImageId)?.ImageUrl : null;
            model.BodyImageURL = model.BodyImageId > 0 ? _imageMasterService.GetImageById(model.BodyImageId)?.ImageUrl : null;

        }

        #endregion
        #region methods

        public IActionResult Create(CRM.Core.Domain.Quote type)
        {
            AddBreadcrumbs($"{type} Page", "Create", $"/Admin/WeeklyUpdate/List?typeId={(int)type}", $"/Admin/WeeklyUpdate/Create?type={type}");
            WeeklyUpdateModel model = new WeeklyUpdateModel();
            ViewBag.QuoteType = type.ToString();
            PrepareVideo(model);
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(WeeklyUpdateModel model)
        {
            AddBreadcrumbs($"{model.QuoteType} Page", "Create", $"/WeeklyUpdate/List?typeId={(int)model.QuoteType}", $"/WeeklyUpdate/Create?type={model.QuoteType}");

            if (model.QuoteType.ToString() == "Registration" || model.QuoteType.ToString() == "Login")
            {
                ModelState.Remove("SliderOneTitle"); ModelState.Remove("SliderOneDescription"); ModelState.Remove("SliderTwoTitle");
                ModelState.Remove("SliderTwoDescription"); ModelState.Remove("SliderThreeTitle"); ModelState.Remove("SliderThreeDescription");
                ModelState.Remove("LandingQuote");
            }
            if (model.QuoteType.ToString() == "Landing")
            {
                ModelState.Remove("Subtitle");
            }
            if (ModelState.IsValid)
            {
                _weeklyUpdateServices.InActiveAllActiveQuotes((int)model.QuoteId);

                var data = model.ToEntity<WeeklyUpdate>();
                _weeklyUpdateServices.InsertWeeklyUpdate(data);
                _translationService.Translate(data.Quote, model.QuoteRegistration);
                _translationService.Translate(data.Title, model.TitleRegistration);
                _translationService.Translate(data.Title, model.TitleSpanishLanding);
                _translationService.Translate(data.Subtitle, model.SubtitleSpanishLanding);
                _translationService.Translate(data.Subtitle, model.SubtitleRegistration);
                _translationService.Translate(data.VideoHeader, model.VideoHeaderRegistration);
                _notificationService.SuccessNotification("Video created successfully.");
                return RedirectToAction("List", "WeeklyUpdate", new { typeId = (int)model.QuoteId });
            }
            PrepareVideo(model);
            return View(model);
        }


        public IActionResult Edit(int id, CRM.Core.Domain.Quote type)
        {
            AddBreadcrumbs($"{type} Page", "Edit", $"/Admin/WeeklyUpdate/List?typeId={(int)type}", $"/Admin/WeeklyUpdate/Edit/{id}?type={type}");

            if (id != 0)
            {
                var data = _weeklyUpdateServices.GetWeeklyUpdateById(id);

                if (data != null)
                {
                    var model = data.ToModel<WeeklyUpdateModel>();
                    model.SpanishTitleLogin = _localStringResourcesServices.GetResourceValueByResourceNameScreen(model.Title);
                    model.SpanishSubtitleLogin = _localStringResourcesServices.GetResourceValueByResourceNameScreen(data.Subtitle);
                    model.QuoteRegistration= _localStringResourcesServices.GetResourceValueByResourceNameScreen(model.Quote);
                    model.TitleRegistration= _localStringResourcesServices.GetResourceValueByResourceNameScreen(model.Title);
                    model.SubtitleRegistration= _localStringResourcesServices.GetResourceValueByResourceNameScreen(data.Subtitle);
                    model.VideoHeaderRegistration= _localStringResourcesServices.GetResourceValueByResourceNameScreen(model.VideoHeader);
                    model.TitleSpanishLanding=_localStringResourcesServices.GetResourceValueByResourceNameScreen(model.Title);
                    model.SubtitleSpanishLanding = _localStringResourcesServices.GetResourceValueByResourceNameScreen(data.Subtitle);
                    PrepareVideo(model);
                    PrepareImageUrls(model);
                    return View(model);
                }

                return View();
            }
            return View();
        }

        [HttpPost]
        public IActionResult Edit(WeeklyUpdateModel model)
        {
            AddBreadcrumbs($"{model.QuoteType} Page", "Edit", $"/Admin/WeeklyUpdate/List?typeId={(int)model.QuoteType}", $"/Admin/WeeklyUpdate/Edit/{model.Id}?type={model.QuoteType}");
            if (model.QuoteType.ToString() == "Registration" || model.QuoteType.ToString() == "Login")
            {
                ModelState.Remove("SliderOneTitle"); ModelState.Remove("SliderOneDescription"); ModelState.Remove("SliderTwoTitle");
                ModelState.Remove("SliderTwoDescription"); ModelState.Remove("SliderThreeTitle"); ModelState.Remove("SliderThreeDescription");
                ModelState.Remove("LandingQuote");
            }
            if (model.QuoteType.ToString() == "Landing")
            {
                ModelState.Remove("Subtitle");
            }

            if (ModelState.IsValid)
            {
                if (model.IsActive == true)
                {
                    _weeklyUpdateServices.InActiveAllActiveQuotes((int)model.QuoteType);
                }
                var val = _weeklyUpdateServices.GetWeeklyUpdateById(model.Id);
                val.IsActive = model.IsActive;
                val.VideoId = model.VideoId;
                val.Title = model.Title;
                val.Subtitle = model.Subtitle;
                val.IsRandom = model.IsRandom;
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
                val.BannerImageId = model.BannerImageId;
                val.BodyImageId = model.BodyImageId;
                val.QuoteId = model.QuoteId;
                val.Quote = model.Quote;
               
                val.VideoHeader = model.VideoHeader;


                _weeklyUpdateServices.UpdateWeeklyUpdate(val);

                if (model.SpanishTitleLogin!=null) {
                _translationService.Translate(val.Title, model.SpanishTitleLogin);
                }
                if (model.SpanishSubtitleLogin != null)
                {
                    _translationService.Translate(val.Subtitle, model.SpanishSubtitleLogin);
                }
                if (model.TitleRegistration != null)
                {
                    _translationService.Translate(val.Title, model.TitleRegistration);
                }
                if (model.SubtitleRegistration != null)
                {
                    _translationService.Translate(val.Subtitle, model.SubtitleRegistration);
                }
                if (model.QuoteRegistration != null)
                {
                    _translationService.Translate(val.Quote, model.QuoteRegistration);
                }
                if (model.VideoHeaderRegistration != null)
                {
                    _translationService.Translate(val.VideoHeader, model.VideoHeaderRegistration);
                }
                if (model.TitleSpanishLanding != null)
                {
                    _translationService.Translate(val.Title, model.TitleSpanishLanding);
                }
                if (model.SubtitleSpanishLanding != null)
                {
                    _translationService.Translate(val.Subtitle, model.SubtitleSpanishLanding);
                }

               

                //_translationService.Translate(val.SliderTwoTitle, key);
                _notificationService.SuccessNotification("Video edited successfully.");
                return RedirectToAction("List", "WeeklyUpdate", new { typeId = (int)model.QuoteType });
            }
            PrepareVideo(model);
            return View(model);
        }

        public IActionResult List(int typeId)
        {
            AddBreadcrumbs($"{(CRM.Core.Domain.Quote)typeId} Page", "List", $"/Admin/WeeklyUpdate/List?typeId={typeId}", $"/Admin/WeeklyUpdate/List?typeId={typeId}");

            ViewBag.QuoteType = EnumDescription.GetDescription((CRM.Core.Domain.Quote)typeId);
            ViewBag.Quote = (CRM.Core.Domain.Quote)typeId;
            var model = new List<WeeklyUpdateModel>();
            var data = _weeklyUpdateServices.GetWeeklyUpdatesByQuoteType(typeId);
            if (data.Count() != 0)
            {
                model = data.ToList().ToModelList<WeeklyUpdate, WeeklyUpdateModel>(model);
              

                
                ViewBag.TableData = JsonConvert.SerializeObject(model);

                return View(model);
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

        #region Translation
        [HttpPost]
        public IActionResult TranslateSpanish(WeeklyUpdate weeklyupdate)
        {
            WeeklyUpdateModel model = new WeeklyUpdateModel();

            model.Title = weeklyupdate.Title != null ? _translationService.TranslateLevel(weeklyupdate.Title, key) : "";
            model.Subtitle = weeklyupdate.Subtitle != null ? _translationService.TranslateLevel(weeklyupdate.Subtitle, key) : "";
            model.Quote = weeklyupdate.Quote != null ? _translationService.TranslateLevel(weeklyupdate.Quote, key) : "";
            model.VideoHeader = weeklyupdate.VideoHeader != null ? _translationService.TranslateLevel(weeklyupdate.VideoHeader, key) : "";
            return Json(model);

        }
        [HttpPost]
        public IActionResult TranslateEnglishCreate(WeeklyUpdate weeklyUpdate)
        {
            WeeklyUpdateModel model = new WeeklyUpdateModel();
            model.Title = weeklyUpdate.Title!=null?_translationService.TranslateLevelSpanish(weeklyUpdate.Title, key):"";
            model.Subtitle = weeklyUpdate.Subtitle!=null? _translationService.TranslateLevelSpanish(weeklyUpdate.Subtitle, key) :"";
            model.Quote = weeklyUpdate.Quote!=null? _translationService.TranslateLevelSpanish(weeklyUpdate.Quote, key) :"";
            model.VideoHeader = weeklyUpdate.VideoHeader!=null ?_translationService.TranslateLevelSpanish(weeklyUpdate.VideoHeader, key):"";

            return Json(model);

        }
        [HttpPost]
        public IActionResult TranslateSpanishEditRegistration(WeeklyUpdate weeklyUpdate)
        {
            WeeklyUpdateModel model = new WeeklyUpdateModel();
            model.Title = weeklyUpdate.Title!=null? _translationService.TranslateLevel(weeklyUpdate.Title, key):"";
            model.Subtitle = weeklyUpdate.Subtitle != null ? _translationService.TranslateLevel(weeklyUpdate.Subtitle, key) : "";
            return Json(model);

        }
        
        [HttpPost]
        public IActionResult TranslateEnglishCreateLogin(WeeklyUpdate weeklyupdate)
        {
            WeeklyUpdateModel model = new WeeklyUpdateModel();

            model.Title = weeklyupdate.Title != null ? _translationService.TranslateLevel(weeklyupdate.Title, key) : "";
            model.Quote = weeklyupdate.Quote != null ? _translationService.TranslateLevel(weeklyupdate.Quote, key) : "";
            return Json(model);

        }
        [HttpPost]
        public IActionResult TranslateEnglishEditLogin(WeeklyUpdate weeklyupdate)
        {
            WeeklyUpdateModel model = new WeeklyUpdateModel();

            model.Title = weeklyupdate.Title != null ? _translationService.TranslateLevel(weeklyupdate.Title, key) : "";


            return Json(model);

        }
        [HttpPost]
        public IActionResult TranslateSpanishCreateLogin(WeeklyUpdate weeklyupdate)
        {
            WeeklyUpdateModel model = new WeeklyUpdateModel();

            model.Title = weeklyupdate.Title != null ? _translationService.TranslateLevelSpanish(weeklyupdate.Title, key) : "";
            model.Quote = weeklyupdate.Quote != null ? _translationService.TranslateLevelSpanish(weeklyupdate.Quote, key) : "";
            return Json(model);

        }
        [HttpPost]
        public IActionResult TranslateLanding(WeeklyUpdate weeklyupdate)
        {
            WeeklyUpdateModel model = new WeeklyUpdateModel();

            model.Title = weeklyupdate.Title != null ? _translationService.TranslateLevel(weeklyupdate.Title, key) : "";
            model.Subtitle = weeklyupdate.Subtitle != null ? _translationService.TranslateLevel(weeklyupdate.Subtitle, key) : "";

            return Json(model);

        }
        [HttpPost]
        public IActionResult TranslateQuoteDropSpanish(WeeklyUpdate weeklyupdate)
        {
            WeeklyUpdateModel model = new WeeklyUpdateModel();

            model.Quote = weeklyupdate.Quote != null ? _translationService.TranslateLevel(weeklyupdate.Quote, key) : "";
           

            return Json(model);

        }
        

        #endregion
    }
}

using CRM.Core;
using CRM.Core.Domain.LayoutSetups;
using CRM.Services;
using CRM.Services.Authentication;
using CRM.Services.Layoutsetup;
using CRM.Services.Localization;
using CRM.Services.Message;
using DeVeeraApp.Filters;
using DeVeeraApp.Utils;
using DeVeeraApp.ViewModels.Common;
using DeVeeraApp.ViewModels.LayoutSetups;
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
    public class LayoutSetupController : BaseController
    {
        #region fields
        private readonly ILayoutSetupService _layoutSetupService;
        private readonly INotificationService _notificationService;
        private readonly IImageMasterService _imageMasterService;
        private readonly ITranslationService _translationService;
        private readonly ILocalStringResourcesServices _localStringResourcesServices;
        public string key = "AIzaSyC2wpcQiQQ7ASdt4vcJHfmly8DwE3l3tqE";
        #endregion

        #region ctor

        public LayoutSetupController(ILayoutSetupService layoutSetupService,
                                     IImageMasterService imageMasterService,
                                     INotificationService notificationService,
                                     IWorkContext workContext,
                                     IHttpContextAccessor httpContextAccessor,
                                     IAuthenticationService authenticationService,
                                      ITranslationService translationService,
                                       ILocalStringResourcesServices localStringResourcesServices) : base(workContext: workContext,
                                                                                  httpContextAccessor: httpContextAccessor,
                                                                                  authenticationService: authenticationService)

        {
            _layoutSetupService = layoutSetupService;
            _imageMasterService = imageMasterService;
            _notificationService = notificationService;
            _translationService = translationService;
            _localStringResourcesServices = localStringResourcesServices;
        }
        #endregion

        #region Utilities
        public virtual void PrepareImages(LayoutSetupModel model)
        {
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
        public virtual void PrepareImageUrls(LayoutSetupModel model)
        {

            model.SliderOneImageUrl = model.SliderOneImageId>0 ? _imageMasterService.GetImageById(model.SliderOneImageId)?.ImageUrl:null;
            model.SliderTwoImageUrl = model.SliderTwoImageId > 0 ? _imageMasterService.GetImageById(model.SliderTwoImageId)?.ImageUrl : null;
            model.SliderThreeImageUrl = model.SliderThreeImageId > 0 ? _imageMasterService.GetImageById(model.SliderThreeImageId)?.ImageUrl : null;
            model.BannerOneImageUrl = model.BannerOneImageId > 0 ? _imageMasterService.GetImageById(model.BannerOneImageId)?.ImageUrl : null;
            model.BannerTwoImageUrl = model.BannerTwoImageId > 0 ? _imageMasterService.GetImageById(model.BannerTwoImageId)?.ImageUrl : null;
            model.DiaryHeaderImageUrl = model.DiaryHeaderImageId > 0 ? _imageMasterService.GetImageById(model.DiaryHeaderImageId)?.ImageUrl : null;
            model.CompleteRegistrationHeaderImgUrl = model.CompleteRegistrationHeaderImgId > 0 ? _imageMasterService.GetImageById(model.CompleteRegistrationHeaderImgId)?.ImageUrl : null;
            model.Link_1_BannerImageUrl= model.Link_1_BannerImageId > 0 ? _imageMasterService.GetImageById(model.Link_1_BannerImageId)?.ImageUrl : null;
            model.Link_2_BannerImageUrl = model.Link_2_BannerImageId > 0 ? _imageMasterService.GetImageById(model.Link_2_BannerImageId)?.ImageUrl : null;
            model.Link_3_BannerImageUrl = model.Link_3_BannerImageId > 0 ? _imageMasterService.GetImageById(model.Link_3_BannerImageId)?.ImageUrl : null;
            model.FooterImageUrl = model.FooterImageId > 0 ? _imageMasterService.GetImageById(model.FooterImageId)?.ImageUrl : null;
        }

        #endregion

            public IActionResult Index()
        {
            return View();
        }
        public IActionResult List()
        {
            AddBreadcrumbs("Layout Setup", "List", "/Admin/LayoutSetup/List", "/Admin/LayoutSetup/List");
            var model = new List<LayoutSetupModel>();
            var data = _layoutSetupService.GetAllLayoutSetups();
            if (data.Count() != 0)
            {
                model = data.ToList().ToModelList<LayoutSetup, LayoutSetupModel>(model);
                ViewBag.LayOut = JsonConvert.SerializeObject(model);
                return View(model);
            }
            return View(model);
        }
        public IActionResult Create()
        {
            AddBreadcrumbs("Layout Setup", "Create", "/Admin/LayoutSetup/List", "/Admin/LayoutSetup/Create");

            LayoutSetupModel model = new LayoutSetupModel();
            PrepareImages(model);
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(LayoutSetupModel model)
        {
            AddBreadcrumbs("Layout Setup", "Create", "/LayoutSetup/Create", "/LayoutSetup/Create");

            if (ModelState.IsValid)
            {

                var data = model.ToEntity<LayoutSetup>();

                _layoutSetupService.InsertLayoutSetup(data);
                _translationService.Translate(model.Title, model.ModuleSpanishTitle);
                _translationService.Translate(model.Description, model.ModuleSpanishDescription);
                _translationService.Translate(model.Location, model.LocationSpanish);
                _translationService.Translate(model.FooterDescription, model.FooterDescriptionSpanish);
                _notificationService.SuccessNotification("Layout Setup Successfully.");
                return RedirectToAction("List");
            }

            PrepareImages(model);
            return View(model);
        }

        public IActionResult Edit(int Id)
        {
            AddBreadcrumbs("Layout Setup", "Edit", $"/Admin/LayoutSetup/List", $"/Admin/LayoutSetup/Edit/{Id}");

            if (Id != 0)
            {
                var data = _layoutSetupService.GetLayoutSetupById(Id);
                if (data != null)
                {
                    var model = data.ToModel<LayoutSetupModel>();
                    model.ModuleSpanishTitle = _localStringResourcesServices.GetResourceValueByResourceName(model.Title);
                    model.ModuleSpanishDescription = _localStringResourcesServices.GetResourceValueByResourceName(model.Description);
                    model.FooterDescriptionSpanish = _localStringResourcesServices.GetResourceValueByResourceName(model.FooterDescription);
                    model.LocationSpanish = _localStringResourcesServices.GetResourceValueByResourceName(model.Location);
                    PrepareImageUrls(model);
                    return View(model);
                }

            }
            return View();
        }

        [HttpPost]
        public IActionResult Edit(LayoutSetupModel model)
        {
            AddBreadcrumbs("Layout Setup", "Edit", $"/Admin/LayoutSetup/List", $"/LayoutSetup/Edit/{model.Id}");

            if (ModelState.IsValid)
            {
                var data = _layoutSetupService.GetLayoutSetupById(model.Id);

                data.SliderOneTitle = model.SliderOneTitle;
                data.SliderOneDescription = model.SliderOneDescription;
                data.SliderOneImageId = model.SliderOneImageId;
                data.SliderTwoTitle = model.SliderTwoTitle;
                data.SliderTwoDescription = model.SliderTwoDescription;
                data.SliderTwoImageId = model.SliderTwoImageId;
                data.SliderThreeTitle = model.SliderThreeTitle;
                data.SliderThreeDescription = model.SliderThreeDescription;
                data.SliderThreeImageId = model.SliderThreeImageId;

                data.BannerOneImageId = model.BannerOneImageId;
                data.BannerTwoImageId = model.BannerTwoImageId;
                data.DiaryHeaderImageId = model.DiaryHeaderImageId;
                data.ReasonToSubmit = model.ReasonToSubmit;
                data.CompleteRegistrationHeaderImgId = model.CompleteRegistrationHeaderImgId;
                data.Link_1_BannerImageId = model.Link_1_BannerImageId;
                data.Link_2_BannerImageId = model.Link_2_BannerImageId;
                data.Link_3_BannerImageId = model.Link_3_BannerImageId;
                data.Link_1 = model.Link_1;
                data.Link_2 = model.Link_2;
                data.Link_3 = model.Link_3;
                data.IsActive = model.IsActive;
                data.Location = model.Location;
                data.Description = model.Description;
                data.PhoneNo = model.PhoneNo;
                data.FooterImageId = model.FooterImageId;


                _layoutSetupService.UpdateLayoutSetup(data);
                _translationService.Translate(model.Title, model.ModuleSpanishTitle);
                _translationService.Translate(model.Description, model.ModuleSpanishDescription);
                _translationService.Translate(model.Location, model.LocationSpanish);
                _translationService.Translate(model.FooterDescription, model.FooterDescriptionSpanish);
                _notificationService.SuccessNotification("Layout Setup Updated Successfully.");
                return RedirectToAction("List");
            }

            PrepareImageUrls(model);
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            ResponseModel response = new ResponseModel();

            if (id != 0)
            {
                var Data = _layoutSetupService.GetLayoutSetupById(id);
                if (Data == null)
                {
                    response.Success = false;
                    response.Message = "No data found";
                }
                _layoutSetupService.DeleteLayoutSetup(Data);

                response.Success = true;
            }
            else
            {
                response.Success = false;
                response.Message = "Id is 0";

            }
            return Json(response);
        }

        #region Translate
        [HttpPost]
        public IActionResult TranslateModule(LayoutSetupModel weeklyupdate)
        {
            LayoutSetupModel model = new LayoutSetupModel();

            model.Title = _translationService.TranslateLevel(weeklyupdate.Title, key);
            model.Description = _translationService.TranslateLevel(weeklyupdate.Description, key);

            return Json(model);

        }
        [HttpPost]
        public IActionResult TranslateFooter(LayoutSetupModel weeklyupdate)
        {
            LayoutSetupModel model = new LayoutSetupModel();

            model.FooterDescription = _translationService.TranslateLevel(weeklyupdate.FooterDescription, key);
            model.Location = _translationService.TranslateLevel(weeklyupdate.Location, key);

            return Json(model);

        }
        
        #endregion
    }
}

using CRM.Core;
using CRM.Core.Domain.LayoutSetups;
using CRM.Services;
using CRM.Services.Authentication;
using CRM.Services.Layoutsetup;
using CRM.Services.Message;
using DeVeeraApp.Filters;
using DeVeeraApp.Utils;
using DeVeeraApp.ViewModels.Common;
using DeVeeraApp.ViewModels.LayoutSetups;
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
    public class LayoutSetupController : BaseController
    {
        #region fields
        private readonly ILayoutSetupService _layoutSetupService;
        private readonly INotificationService _notificationService;
        private readonly IImageMasterService _imageMasterService;

        #endregion

        #region ctor

        public LayoutSetupController(ILayoutSetupService layoutSetupService,
                                     IImageMasterService imageMasterService,
                                     INotificationService notificationService,
                                     IWorkContext workContext,
                                     IHttpContextAccessor httpContextAccessor,
                                     IAuthenticationService authenticationService) : base(workContext: workContext,
                                                                                  httpContextAccessor: httpContextAccessor,
                                                                                  authenticationService: authenticationService)

        {
            _layoutSetupService = layoutSetupService;
            _imageMasterService = imageMasterService;
            _notificationService = notificationService;
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
                    PrepareImages(model);
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

                _layoutSetupService.UpdateLayoutSetup(data);
                _notificationService.SuccessNotification("Layout Setup Updated Successfully.");
                return RedirectToAction("List");
            }

            PrepareImages(model);
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
    }
}

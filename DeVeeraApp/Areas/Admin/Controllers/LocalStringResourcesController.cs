using CRM.Core;
using CRM.Core.Domain;
using CRM.Services;
using CRM.Services.Authentication;
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
using System.Collections.Generic;
using System.Linq;

namespace DeVeeraApp.Controllers
{
    [Area("Admin")]
    [AuthorizeAdmin]
    public class LocalStringResourcesController : BaseController
    {
        #region fields
        private readonly ILocalStringResourcesServices _localStringResourcesServices;
        private readonly INotificationService _notificationService;
        private readonly ILanguageService _languageService;
        private readonly ITranslationService _translationService;
        public string key = "AIzaSyC2wpcQiQQ7ASdt4vcJHfmly8DwE3l3tqE";
        #endregion

        #region ctor
        public LocalStringResourcesController(ILocalStringResourcesServices localStringResourcesServices,
                                              INotificationService notificationService,
                                              ILanguageService languageService,
                                              IWorkContext workContext,
                                              IHttpContextAccessor httpContextAccessor,
                                              ITranslationService translationService,
                                              IAuthenticationService authenticationService) : base(workContext: workContext,
                                                                                  httpContextAccessor: httpContextAccessor,
                                                                                  authenticationService: authenticationService)
        {
            _localStringResourcesServices = localStringResourcesServices;
            _notificationService = notificationService;
            _translationService = translationService;
            this._languageService = languageService;
        }

        #endregion


        #region utilities

        public virtual void PrepareLanguages(LanguageModel model)
        {

            model.AvailableLanguages.Add(new SelectListItem { Text = "Select Language", Value = "0" });
            var AvailableLanguage = _languageService.GetAllLanguages();
            foreach (var item in AvailableLanguage)
            {
                model.AvailableLanguages.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                });
            }
        }


        #endregion


        #region method

        public IActionResult List()
        {
            AddBreadcrumbs("LocalStringResources", "List", "/Admin/LocalStringResources/List", "/Admin/LocalStringResources/List");
            var model = new List<LocalStringResourceModel>();
            var data = _localStringResourcesServices.GetAllLocalStringResources();
            var staticLabel = _localStringResourcesServices.GetAllLocalStringResources().OrderBy(a => a.Id).Where(a => a.IsActive == true);
           
            if (staticLabel !=null)
            {
               model = staticLabel.ToList().ToModelList<LocaleStringResource, LocalStringResourceModel>(model);
                ViewBag.ResourcesTable = JsonConvert.SerializeObject(model);
                return View(model);
            }
            else if (data.Count() != 0)
            {
                model = data.ToList().ToModelList<LocaleStringResource, LocalStringResourceModel>(model);
                ViewBag.ResourcesTable = JsonConvert.SerializeObject(model);
            }
            return View(model);
          
        }
        public IActionResult Create()
        {
            AddBreadcrumbs("LocalStringResources", "Create", "/Admin/LocalStringResources/List", "/Admin/LocalStringResources/Create");
            var model = new LocalStringResourceModel();
            PrepareLanguages(model.Language);
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(LocalStringResourceModel model)
        {
            try
            {
                ModelState.Remove("Language.Name");
                ModelState.Remove("Language.LanguageCulture");
                ModelState.Remove("Language.UniqueSeoCode");
                if (ModelState.IsValid)
                {
                    if (_localStringResourcesServices.GetAllLocalStringResources().Where(r => r.LanguageId == model.LanguageId && r.ResourceName == model.ResourceName).FirstOrDefault() == null)
                    {
                        LocaleStringResource data = new LocaleStringResource()
                        {
                            LanguageId = 5,
                            ResourceName = model.ResourceName,
                            ResourceValue = model.ResourceValue,
                            IsActive = true,

                    };
                        _localStringResourcesServices.InsertLocalStringResource(data);
                        _notificationService.SuccessNotification("Local String Resource Inserted Successfully.");
                        return RedirectToAction(nameof(List));

                    }

                    _notificationService.ErrorNotification("Resource already present.");
                }
                PrepareLanguages(model.Language);

                return View(model);

            }
            catch
            {
                PrepareLanguages(model.Language);

                return View(model);
            }
        }

        public IActionResult Edit(int id)
        {
            AddBreadcrumbs("LocalStringResources", "Edit", "/Admin/LocalStringResources/List", $"/Admin/LocalStringResources/Edit/{id}");
            if (id != 0)
            {
                var data = _localStringResourcesServices.GetLocalStringResourceById(id);

                if (data != null)
                {
                    var model = data.ToModel<LocalStringResourceModel>();
                   // PrepareLanguages(model.Language);

                    return View(model);
                }
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult Edit(LocalStringResourceModel model)
        {
            try
            {
                ModelState.Remove("Language.Name");
                ModelState.Remove("Language.LanguageCulture");
                ModelState.Remove("Language.UniqueSeoCode");
                if (ModelState.IsValid)
                {
                    if (_localStringResourcesServices.GetAllLocalStringResources().Where(r => r.LanguageId == model.LanguageId && r.ResourceName == model.ResourceName).FirstOrDefault() == null)
                    {
                        var data = _localStringResourcesServices.GetLocalStringResourceById(model.Id);

                        if (data != null)
                        {
                            data.LanguageId = 5;
                            data.ResourceName = model.ResourceName;
                            data.ResourceValue = model.ResourceValue;
                            data.IsActive =true;
                            _localStringResourcesServices.UpdateLocalStringResource(data);
                            _notificationService.SuccessNotification("Local String Resource Inserted Successfully.");

                            return RedirectToAction(nameof(List));
                        }

                    }
                    _notificationService.ErrorNotification("Resource already present.");
                }
                PrepareLanguages(model.Language);

                return View(model);
            }
            catch
            {
                PrepareLanguages(model.Language);

                return View(model);
            }
        }

        public IActionResult Delete(int id)
        {
            ResponseModel response = new ResponseModel();

            if (id != 0)
            {
                var data = _localStringResourcesServices.GetLocalStringResourceById(id);
                if (data == null)
                {
                    response.Success = false;
                    response.Message = "No user found";
                }
                _localStringResourcesServices.DeleteLocalStringResource(data);

                response.Success = true;
            }
            else
            {
                response.Success = false;
                response.Message = "id is 0";

            }
            return Json(response);
        }
        [HttpPost]
        public IActionResult TranslatationSpanish(string ResourceName)
        {
            LocalStringResourceModel model1 = new LocalStringResourceModel();
            model1.ResourceName = _translationService.TranslateLevel(ResourceName, key);
            return Json(model1.ResourceName);
            //return Json(new { Status = "success",/* LevelSpanish = model.Title,*/ TitleSpanish = model.Subtitle, Subtitlespanish = model.Subtitle, DescriptionSpanish = model.FullDescription });
        }
        [HttpPost]
        public IActionResult TranslatationEnglish(string ResourceValue)
        {
            LocalStringResourceModel model1 = new LocalStringResourceModel();
            model1.ResourceValue = _translationService.TranslateLevelSpanish(ResourceValue, key);
            return Json(model1.ResourceValue);
            //return Json(new { Status = "success",/* LevelSpanish = model.Title,*/ TitleSpanish = model.Subtitle, Subtitlespanish = model.Subtitle, DescriptionSpanish = model.FullDescription });
        }

        #endregion
    }
}

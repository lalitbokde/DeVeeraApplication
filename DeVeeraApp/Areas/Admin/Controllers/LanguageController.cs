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
using DeVeeraApp.ViewModels.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthorizeAdmin]
    public class LanguageController : BaseController
    {
        private readonly ILanguageService _languageService;
        private readonly INotificationService _notificationService;
        private readonly ILocalizationService _localizationService;


        #region fields

        public LanguageController(ILanguageService languageService,
                                  INotificationService notificationService,
                                   IWorkContext workContext,
                               IHttpContextAccessor httpContextAccessor,
                               IAuthenticationService authenticationService,
                               ILocalizationService localizationService) : base(workContext: workContext,
                                                                                  httpContextAccessor: httpContextAccessor,
                                                                                  authenticationService: authenticationService)
        {
            _languageService = languageService;
            _notificationService = notificationService;
            _localizationService = localizationService;
        }
        #endregion


        #region methods

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Create()
        {
            AddBreadcrumbs("Language", "Create", "/Language/Create", "/Language/Create");
            return View();
        }

        


        [HttpPost]
        public IActionResult Create(LanguageModel model)
        {
            if (ModelState.IsValid)
            {
                var data = new Language
                {
                    Name = model.Name,
                    LanguageCulture = model.LanguageCulture,
                    UniqueSeoCode = model.UniqueSeoCode,
                    FlagImageFileName = model.FlagImageFileName,
                    Rtl = model.Rtl,
                    Published = model.Published,
                    DisplayOrder = model.DisplayOrder
                    
                };
                _languageService.InsertLanguage(data);
                _notificationService.SuccessNotification("Language added successfully");
                return RedirectToAction("List");
            }
            return View();
        }


        public IActionResult Edit(int id)
        {
            if(id != 0)
            {
                var data = _languageService.GetLanguageById(id);
                var model = data.ToModel<LanguageModel>();
                return View(model);
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult Edit(LanguageModel model)
        {
            if (ModelState.IsValid)
            {
                var data = _languageService.GetLanguageById(model.Id);
                data.Name = model.Name;
                data.LanguageCulture = model.LanguageCulture;
                data.UniqueSeoCode = model.UniqueSeoCode;
                data.FlagImageFileName = model.FlagImageFileName;
                data.Rtl = model.Rtl;
                data.Published = model.Published;
                data.DisplayOrder = model.DisplayOrder;
                _languageService.UpdateLanguage(data);
                _notificationService.SuccessNotification("Language updated successfully");

                return RedirectToAction("List");
            }
            return View(model);
        }


        public IActionResult List()
        {
            AddBreadcrumbs("Language", "List", "/Language/List", "/Language/List");
            var model = new List<LanguageModel>();
            var data = _languageService.GetAllLanguages();
            if(data.Count != 0)
            {
                model = data.ToList().ToModelList<Language, LanguageModel>(model);

            }
            return View(model);
        }


        public IActionResult Delete(int languageId)
        {
            ResponseModel response = new ResponseModel();

            if (languageId != 0)
            {
                var module = _languageService.GetLanguageById(languageId);

                if (module == null)
                {
                    response.Success = false;
                    response.Message = "No user found";
                }

                _languageService.DeleteLanguage(module);

                response.Success = true;
            }
            else
            {
                response.Success = false;
                response.Message = "";

            }
            return Json(response);

        }

        //public virtual List<LocaleResourceModel> PrepareLocaleResourceListModel(LocaleResourceSearchModel searchModel, Language language)
        //{
        //    if (language == null)
        //        throw new ArgumentNullException(nameof(language));

        //    //get locale resources
        //    var localeResources = _localizationService.GetAllResourceValues(language.Id, loadPublicLocales: null)
        //        .OrderBy(localeResource => localeResource.Key).AsQueryable();
        //    if (searchModel != null)
        //    {
        //        //filter locale resources
        //        //TODO: move filter to language service
        //        if (!string.IsNullOrEmpty(searchModel.SearchResourceName))
        //            localeResources = localeResources.Where(l => l.Key.ToLowerInvariant().Contains(searchModel.SearchResourceName.ToLowerInvariant()));
        //        if (!string.IsNullOrEmpty(searchModel.SearchResourceValue))
        //            localeResources = localeResources.Where(l => l.Value.Value.ToLowerInvariant().Contains(searchModel.SearchResourceValue.ToLowerInvariant()));

        //    }
        //    //prepare list model
        //    //fill in model values from the entity
        //    var localeResourceLisTmodel = localeResources.Select(localeResource => new LocaleResourceModel
        //    {
        //        LanguageId = language.Id,
        //        Id = localeResource.Value.Key,
        //        ResourceName = localeResource.Key,
        //        ResourceValue = localeResource.Value.Value
        //    }).ToList();

        //    return localeResourceLisTmodel;
        //}
        //public virtual IActionResult ResourceAdd(int languageId, LocaleResourceModel model)
        //{
        //    if (HttpContext.Session.GetInt32("isMaintenance") == null)
        //        return Logout();
        //    //if (!_permissionService.Authorize(StandardPermissionProvider.ManageLanguages))
        //    //    return AccessDeniedView();

        //    if (model.ResourceName != null)
        //        model.ResourceName = model.ResourceName.Trim();
        //    if (model.ResourceValue != null)
        //        model.ResourceValue = model.ResourceValue.Trim();

        //    if (!ModelState.IsValid)
        //    {
        //        return Json(ModelState.SerializeErrors());
        //    }

        //    var res = _localizationService.GetLocaleStringResourceByName(model.ResourceName, model.LanguageId, false);
        //    if (res == null)
        //    {
        //        //fill entity from model
        //        var resource = model.ToEntity();

        //        resource.LanguageId = languageId;
        //        //psd
        //        resource.ResourceValue = Regex.Replace(model.ResourceValue, @"\s+|\n|\t", string.Empty);

        //        _localizationService.InsertLocaleStringResource(resource);
        //    }
        //    else
        //    {
        //        return Json(string.Format(_localizationService.GetResource("Admin.Configuration.Languages.Resources.NameAlreadyExists"), model.ResourceName));
        //    }

        //    return Json(new { Result = true });
        //}

        //public virtual IActionResult DeleteLanguageResource(int id)
        //{
        //    if (HttpContext.Session.GetInt32("isMaintenance") == null)
        //        return Logout();
        //    ResponceModel responceModel = new ResponceModel();
        //    try
        //    {
        //        //Get local string resource by Id
        //        var localResourceString = _languageService.GetLocalStringResourceById(id);


        //        if (localResourceString == null)
        //            //No product found with the specified id
        //            return RedirectToAction("List");
        //        if (localResourceString != null)
        //        {
        //            //if local resource is not null then delete local resource
        //            _languageService.DeleteLocalResourceString(localResourceString);
        //            responceModel.Success = true;
        //            responceModel.Message = "Deleted.";
        //            return Json(responceModel);

        //        }
        //        responceModel.Success = false;
        //        responceModel.Message = "NotDeleted.";
        //        AddNotification(NotificationMessage.TitleError, NotificationMessage.ErrormsgDeleteCategories, NotificationMessage.TypeError);

        //        return Json(responceModel);


        //    }
        //    catch (Exception e)
        //    {

        //        return View(e);
        //    }
        //}
        //public IActionResult ResourceList(int id)
        //{
        //    if (HttpContext.Session.GetInt32("isMaintenance") == null)
        //        return Logout();
        //    LanguageModel model = new LanguageModel();
        //    var language = _languageService.GetLanguageById(id);
        //    model.LocaleResourceModelList = PrepareLocaleResourceListModel(null, language);
        //    return PartialView("_ResourceList", model.LocaleResourceModelList);
        //}


        #endregion
    }
}

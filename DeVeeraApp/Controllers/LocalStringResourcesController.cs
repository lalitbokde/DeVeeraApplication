using CRM.Core.Domain;
using CRM.Services;
using CRM.Services.Message;
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
using System.Threading.Tasks;

namespace DeVeeraApp.Controllers
{

    public class LocalStringResourcesController : Controller
    {
        #region fields
        private readonly ILocalStringResourcesServices _localStringResourcesServices;
        private readonly INotificationService _notificationService;
        private readonly ILanguageService _languageService;
        #endregion

        #region ctor
        public LocalStringResourcesController(ILocalStringResourcesServices localStringResourcesServices,
                                              INotificationService notificationService,
                                              ILanguageService languageService)
        {
            _localStringResourcesServices = localStringResourcesServices;
            _notificationService = notificationService;
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
            var model = new List<LocalStringResourceModel>();
            var data = _localStringResourcesServices.GetAllLocalStringResources();

            if (data.Count() != 0)
            {
                model = data.ToList().ToModelList<LocaleStringResource, LocalStringResourceModel>(model);
                ViewBag.ResourcesTable = JsonConvert.SerializeObject(model);
            }
            return View(model);
        }

        public IActionResult Create()
        {
            var model = new LocalStringResourceModel();
            PrepareLanguages(model.Language);
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(LocalStringResourceModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(_localStringResourcesServices.GetAllLocalStringResources().Where(r => r.LanguageId == model.LanguageId && r.ResourceName == model.ResourceName).FirstOrDefault() == null)
                    {
                        LocaleStringResource data = new LocaleStringResource()
                        {
                            LanguageId = model.LanguageId,
                            ResourceName = model.ResourceName,
                            ResourceValue = model.ResourceValue
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
            catch (Exception ex)
            {
                PrepareLanguages(model.Language);

                return View(model);
            }
        }

        public IActionResult Edit(int id)
        {
            if (id != 0)
            {
                var data = _localStringResourcesServices.GetLocalStringResourceById(id);

                if (data != null)
                {
                    var model = data.ToModel<LocalStringResourceModel>();
                    PrepareLanguages(model.Language);

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
                if (ModelState.IsValid)
                {
                    if (_localStringResourcesServices.GetAllLocalStringResources().Where(r => r.LanguageId == model.LanguageId && r.ResourceName == model.ResourceName).FirstOrDefault() == null)
                    {
                        var data = _localStringResourcesServices.GetLocalStringResourceById(model.Id);

                        if (data != null)
                        {
                            data.LanguageId = model.LanguageId;
                            data.ResourceName = model.ResourceName;
                            data.ResourceValue = model.ResourceValue;
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
            catch (Exception ex)
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


        #endregion
    }
}

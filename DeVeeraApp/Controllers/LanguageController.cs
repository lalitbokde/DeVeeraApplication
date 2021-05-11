using CRM.Core;
using CRM.Core.Domain;
using CRM.Services;
using CRM.Services.Authentication;
using CRM.Services.Message;
using DeVeeraApp.Utils;
using DeVeeraApp.ViewModels;
using DeVeeraApp.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.Controllers
{
    public class LanguageController : BaseController
    {
        private readonly ILanguageService _languageService;
        private readonly INotificationService _notificationService;


        #region fields

        public LanguageController(ILanguageService languageService,
                                  INotificationService notificationService,
                                   IWorkContext workContext,
                               IHttpContextAccessor httpContextAccessor,
                               IAuthenticationService authenticationService) : base(workContext: workContext,
                                                                                  httpContextAccessor: httpContextAccessor,
                                                                                  authenticationService: authenticationService)
        {
            _languageService = languageService;
            _notificationService = notificationService;
        }
        #endregion

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
                    LanguageName = model.LanguageName,
                    Abbreviations = model.Abbreviations
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
                data.LanguageName = model.LanguageName;
                data.Abbreviations = model.Abbreviations;
                _languageService.UpdateLanguage(data);
                _notificationService.SuccessNotification("Language updated successfully");

                return RedirectToAction("List");
            }
            return View(model);
        }


        public IActionResult List()
        {
            var model = new List<LanguageModel>();
            var data = _languageService.GetAllLanguages();
            if(data.Count != 0)
            {
                foreach(var item in data)
                {
                    model.Add(item.ToModel<LanguageModel>());
                }

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


    }
}

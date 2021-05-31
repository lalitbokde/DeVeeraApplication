using CRM.Services;
using DeVeeraApp.ViewModels;
using DeVeeraApp.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewComponents
{
    public class SelectLanguageViewComponent : ViewComponent
    {
        private readonly ILanguageService _languageService;

        public SelectLanguageViewComponent(ILanguageService languageService)
        {
            _languageService = languageService;
        }


        public IViewComponentResult Invoke(UserModel model)
        {

            model.LandingPageModel.Language.AvailableLanguages.Add(new SelectListItem { Text = "Select Language", Value = "0" });
            var AvailableLanguage = _languageService.GetAllLanguages();
            foreach (var item in AvailableLanguage)
            {
                model.LandingPageModel.Language.AvailableLanguages.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                });
            }

            return View(model);
        }

    }
}

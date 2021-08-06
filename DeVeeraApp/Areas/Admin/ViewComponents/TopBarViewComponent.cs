using CRM.Core;
using CRM.Services;
using CRM.Services.Settings;
using DeVeeraApp.ViewModels;
using DeVeeraApp.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace DeVeeraApp.Areas.Admin.ViewComponents
{
    public class TopBarViewComponent : ViewComponent
    {
        private readonly ILanguageService _languageService;
        private readonly ISettingService _settingService;
        private readonly IWorkContext _WorkContextService;

        public TopBarViewComponent(ILanguageService languageService,
                                           ISettingService settingService,
                                           IWorkContext workContext)
        {
            _languageService = languageService;
            _settingService = settingService;
            _WorkContextService = workContext;
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
                    Text = item.Name,

                });
            }

            var userLanguage = _settingService.GetAllSetting().Where(s => s.UserId == _WorkContextService.CurrentUser?.Id).FirstOrDefault();
            var guestLanguage = _settingService.GetSetting();
            if (userLanguage != null)
            {
                model.LandingPageModel.Language.Id = userLanguage.LanguageId;
            }
            else if (guestLanguage != null)
            {
                model.LandingPageModel.Language.Id = guestLanguage.LanguageId;
            }
            else
            {
                var defaultLanguage = _languageService.GetAllLanguages().Where(s => s.Name == "English").FirstOrDefault().Id;
                model.LandingPageModel.Language.Id = defaultLanguage;
            }

            return View(model);
        }

    }
}

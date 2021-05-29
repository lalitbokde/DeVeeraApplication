using CRM.Core.Domain;
using CRM.Services.Settings;
using DeVeeraApp.Filters;
using DeVeeraApp.ViewModels;
using DeVeeraApp.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthorizeAdmin]
    public class GeneralSettingController : Controller
    {
        private readonly ISettingService _settingService;


        #region ctor
        public GeneralSettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserModel model)
        {
            if(model.LandingPageModel.Language.Id != 0)
            {
                if(model.Id != 0)
                {
                    var userLanguage = _settingService.GetAllSetting().Where(s => s.UserId == model.Id).FirstOrDefault();
                    if(userLanguage != null)
                    {
                        userLanguage.UserId = model.Id;
                        userLanguage.LanguageId = model.LandingPageModel.Language.Id;
                        _settingService.UpdateSetting(userLanguage);
                    }
                    else
                    {
                        var settingData = new Setting();
                        settingData.UserId = model.Id;
                        settingData.LanguageId = model.LandingPageModel.Language.Id;
                        _settingService.InsertSetting(settingData);
                    }
                }
                else
                {
                    var guestLanguage = _settingService.GetSetting();
                    if (guestLanguage != null)
                    {
                        guestLanguage.LanguageId = model.LandingPageModel.Language.Id;
                        _settingService.UpdateSetting(guestLanguage);
                    }
                }
               
            }
            return LocalRedirect(model.LandingPageModel.Language.ReturnUrl);
        }
    }
}

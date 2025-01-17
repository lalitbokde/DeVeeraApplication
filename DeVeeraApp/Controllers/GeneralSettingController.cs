﻿using CRM.Core.Domain;
using CRM.Services.Settings;
using DeVeeraApp.ViewModels.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace DeVeeraApp.Controllers
{
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
            string SessionLangId = "0";string FirstLangaugeCheck = "0";
            if (model.LandingPageModel.Language.Id != 0)
            {
               
                if(model.Id != 0 && model.Id!=34)
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
                        var settingData = new Setting
                        {
                            UserId = model.Id,
                            LanguageId = model.LandingPageModel.Language.Id
                        };
                        _settingService.InsertSetting(settingData);
                    }
                    
                }
                else
                {
                    var guestLanguage = _settingService.GetSetting();
                    if (guestLanguage != null  && guestLanguage.UserId!=34)
                    {
                        guestLanguage.LanguageId = model.LandingPageModel.Language.Id;
                        _settingService.UpdateSetting(guestLanguage);
                        model.LandingPageModel.Language.ReturnUrl = model.LandingPageModel.Language.ReturnUrl ?? "/Home/Index";
                    }
                }

                
                TempData["LangaugeId"] = model.LandingPageModel.Language.Id;
            }

            if(model?.LandingPageModel?.Language?.ReturnUrl== "/User/UserProfile?userId=" + model?.Id)
            {
                return RedirectToAction("UserProfile","User", new { userId = model?.Id, userprofile= "userprofile" });
            }
            if (model?.LandingPageModel?.Language?.ReturnUrl == "/User/SendOTP?langId=0" || model?.LandingPageModel?.Language?.ReturnUrl == "/User/SendOTP?langId=3" || model?.LandingPageModel?.Language?.ReturnUrl == "/User/SendOTP?langId=5" || model?.LandingPageModel?.Language?.ReturnUrl == "/User/SendOTP")
            {
                return RedirectToAction("Register", "User");
            }

            if (TempData["LangaugeId"] != null)
            {
                SessionLangId = Convert.ToString(TempData["LangaugeId"]);

                FirstLangaugeCheck = Convert.ToString(TempData["LangaugeId"]);
            }

            HttpContext.Session.SetInt32(SessionLangId, Convert.ToInt32(SessionLangId));
            Response.Cookies.Append("SessionLangId", SessionLangId);

            HttpContext.Session.SetInt32(FirstLangaugeCheck, Convert.ToInt32(FirstLangaugeCheck));
            Response.Cookies.Append("FirstLangaugeCheck", FirstLangaugeCheck);

          

            return LocalRedirect(model.LandingPageModel.Language.ReturnUrl);
        }
    }
}

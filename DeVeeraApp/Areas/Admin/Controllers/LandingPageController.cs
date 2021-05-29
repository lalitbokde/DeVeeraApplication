using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DeVeeraApp.Models;
using CRM.Services;
using DeVeeraApp.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using ErrorViewModel = DeVeeraApp.Models.ErrorViewModel;
using DeVeeraApp.Utils;
using CRM.Core.Domain;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc.Localization;
using DeVeeraApp.ViewModels.User;

namespace DeVeeraApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LandingPage : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWeeklyUpdateServices _weeklyUpdateServices;
        private readonly IHtmlLocalizer<LandingPage> _htmlLocalizer;
        private readonly ILanguageService _languageService;


        #region ctor
        public LandingPage(ILogger<HomeController> logger, IWeeklyUpdateServices weeklyUpdateServices,
                           IHtmlLocalizer<LandingPage> htmlLocalizer,
                           ILanguageService languageService)
        {
            _logger = logger;
            _weeklyUpdateServices = weeklyUpdateServices;
            this._htmlLocalizer = htmlLocalizer;
            _languageService = languageService;
        }
        #endregion


        #region Utilities

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



        #region methods
        public IActionResult Index()
        {
            var model = new UserModel();

            var data = _weeklyUpdateServices.GetWeeklyUpdateByQuoteType((int)ViewModels.Quote.Landing);

            model.LandingPageModel.WeeklyUpdate = data.ToModel<WeeklyUpdateModel>();

            ViewBag.VideoUrl = data?.Video?.VideoUrl;
           
            PrepareLanguages(model.LandingPageModel.Language);
            return View(model);
        }


        [HttpPost]
        public IActionResult CultureManagement(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.Now.AddDays(30) });

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }
}

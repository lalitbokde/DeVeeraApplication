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

namespace DeVeeraApp.Controllers
{
    public class LandingPage : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWeeklyUpdateServices _weeklyUpdateServices;
        private readonly ILanguageService _languageService;


        #region ctor
        public LandingPage(ILogger<HomeController> logger, IWeeklyUpdateServices weeklyUpdateServices,
                           ILanguageService languageService)
        {
            _logger = logger;
            _weeklyUpdateServices = weeklyUpdateServices;
            _languageService = languageService;
        }
        #endregion


        #region Utilities

        public virtual void PrepareLanguages(LanguageModel model)
        {
            //prepare available url
            model.AvailableLanguages.Add(new SelectListItem { Text = "Select Language", Value = "0" });
            var AvailableLanguage = _languageService.GetAllLanguages();
            foreach (var item in AvailableLanguage)
            {
                model.AvailableLanguages.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.LanguageName
                });
            }
        }


        #endregion



        #region methods
        public IActionResult Index()
        {
            var data = _weeklyUpdateServices.GetWeeklyUpdateByQuoteType((int)ViewModels.Quote.Landing);
            ViewBag.VideoUrl = data?.Video?.VideoUrl;
            var model = new LanguageModel();
            PrepareLanguages(model);
            return View(model);
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

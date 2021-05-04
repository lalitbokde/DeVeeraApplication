using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DeVeeraApp.Models;
using CRM.Services;

namespace DeVeeraApp.Controllers
{
    public class LandingPage : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWeeklyUpdateServices _weeklyUpdateServices;
        public LandingPage(ILogger<HomeController> logger, IWeeklyUpdateServices weeklyUpdateServices)
        {
            _logger = logger;
            _weeklyUpdateServices = weeklyUpdateServices;
        }

        public IActionResult Index()
        {
            var data = _weeklyUpdateServices.GetWeeklyUpdateByQuoteType((int)ViewModels.Quote.Landing);
            ViewBag.VideoUrl = data?.Video?.VideoUrl;
            return View();
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
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DeVeeraApp.Models;
using DeVeeraApp.Filters;
using CRM.Core.Domain.Users;
using DeVeeraApp.ViewModels.User;
using DeVeeraApp.Utils;
using CRM.Services.Users;
using CRM.Services;
using DeVeeraApp.ViewModels;
using CRM.Core.Domain;

namespace DeVeeraApp.Controllers
{
    [AuthorizeAdmin]
    public class HomeController : Controller
    {
        #region fields

        private readonly ILogger<HomeController> _logger;
        private readonly IVideoServices _videoServices;
        private readonly IWeeklyUpdateServices _weeklyUpdateServices;

        #endregion


        #region ctor
        public HomeController(ILogger<HomeController> logger,
                              IVideoServices videoServices,
                              IWeeklyUpdateServices weeklyUpdateServices)
        {
            _logger = logger;
            _videoServices = videoServices;
            _weeklyUpdateServices = weeklyUpdateServices;
        }

        #endregion



        #region Method
        public IActionResult Index()
        {
            var model = new List<VideoModel>();
            var data = _videoServices.GetAllVideos();
            if(data.Count() != 0)
            {
                foreach(var item in data)
                {
                    model.Add(item.ToModel<VideoModel>());
                }

                return View(model);
            }
            return View();
        }

        public IActionResult ExistingUser(int QuoteType)
        {
            if (QuoteType != 0)
            {
               var data = _weeklyUpdateServices.GetWeeklyUpdateByQuoteType(QuoteType);

                var model = data.ToModel<WeeklyUpdateModel>();
                return View(model);

            }

            return View();
        }

        public IActionResult NewUser(int QuoteType)
        {
            if (QuoteType != 0)
            {
                var data = _weeklyUpdateServices.GetWeeklyUpdateByQuoteType(QuoteType);

                var model = data.ToModel<WeeklyUpdateModel>();
                return View(model);

            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        #endregion
    }
}

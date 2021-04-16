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
using CRM.Services.DashboardQuotes;

namespace DeVeeraApp.Controllers
{
    [AuthorizeAdmin]
    public class HomeController : Controller
    {
        #region fields

        private readonly ILogger<HomeController> _logger;
        private readonly IVideoServices _videoServices;
        private readonly IWeeklyUpdateServices _weeklyUpdateServices;
        private readonly IDashboardQuoteService _dashboardQuoteService;

        #endregion


        #region ctor
        public HomeController(ILogger<HomeController> logger,
                              IVideoServices videoServices,
                              IWeeklyUpdateServices weeklyUpdateServices,
                              IDashboardQuoteService dashboardQuoteService)
        {
            _logger = logger;
            _videoServices = videoServices;
            _weeklyUpdateServices = weeklyUpdateServices;
            _dashboardQuoteService = dashboardQuoteService;
        }

        #endregion



        #region Method
        public IActionResult Index()
        {
            var model = new DashboardQuoteModel();
            var quote = _dashboardQuoteService.GetAllDashboardQutoes().Where(a=>a.IsActive==true).FirstOrDefault();
            model.Title = quote !=null ? quote.Title : "";
            model.Author =quote != null ? quote.Author : "";

            var data = _videoServices.GetAllVideos();
            if(data.Count() != 0)
            {
                foreach(var item in data)
                {
                    model.VideoModelList.Add(item.ToModel<VideoModel>());
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


        public IActionResult UnderDevelopment()
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

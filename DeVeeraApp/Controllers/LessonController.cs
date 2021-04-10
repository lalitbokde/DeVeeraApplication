using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DeVeeraApp.Models;
using DeVeeraApp.Filters;
using CRM.Services;
using DeVeeraApp.ViewModels;
using DeVeeraApp.Utils;
using ErrorViewModel = DeVeeraApp.Models.ErrorViewModel;

namespace DeVeeraApp.Controllers
{
    [AuthorizeAdmin]
    public class LessonController : Controller
    {
        #region fields
        private readonly ILogger<LessonController> _logger;
        private readonly IVideoServices _videoServices;

        #endregion


        #region ctor
        public LessonController(ILogger<LessonController> logger,
                                IVideoServices videoServices)
        {
            _logger = logger;
            _videoServices = videoServices;
        }

        #endregion


        #region Method
        public IActionResult Index(int id)
        {
          if(id != 0)
            {
                var data = _videoServices.GetVideoById(id);
                var videoData = data.ToModel<VideoModel>();
                return View(videoData);
            }
            return RedirectToAction("Index", "Home");
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

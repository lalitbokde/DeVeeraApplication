using CRM.Services;
using CRM.Services.Message;
using DeVeeraApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.Controllers
{
    public class VideoController : Controller
    {
        #region field

        private readonly INotificationService notificationService;
        private readonly IVideoMasterService videoMasterService;

        #endregion


        #region ctor

        public VideoController(INotificationService notificationService,
                       IVideoMasterService videoMasterService)
        {
            this.notificationService = notificationService;
            this.videoMasterService = videoMasterService;
        }

        #endregion


        #region Method

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Create(VideoModel model)
        {
            return View();

        }

        public IActionResult Edit()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Edit(VideoModel model)
        {
            return View();

        }

        public IActionResult List()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return null;
        }
        #endregion


    }
}

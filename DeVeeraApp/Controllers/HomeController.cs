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
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _UserService;
        private readonly IUserPasswordService _userPasswordService;
        private readonly IVideoServices _videoServices;
        private readonly IWeeklyVideoServices _weeklyVideoServices;

        public HomeController(ILogger<HomeController> logger,
                              IUserService UserService,
                              IUserPasswordService userPasswordService,
                              IVideoServices videoServices,
                              IWeeklyVideoServices weeklyVideoServices)
        {
            _logger = logger;
            _UserService = UserService;
            _userPasswordService = userPasswordService;
            _videoServices = videoServices;
            _weeklyVideoServices = weeklyVideoServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ExistingUser()
        {
            ViewBag.lessonName = "03. Practice Presence";
            return View();
        }

        public IActionResult NewUser()
        {
            ViewBag.lessonName = "04. Positive Thinking";

            return View();
        }

        public IActionResult CreateAdminUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAdminUser(UserModel model, bool continueEditing)
        {

            if (ModelState.IsValid)
            {
                //fill entity from model
                var user = model.ToEntity<User>();
                UserPassword password = null;
                user.UserGuid = Guid.NewGuid();
                user.CreatedOnUtc = DateTime.UtcNow;
                user.LastActivityDateUtc = DateTime.UtcNow;
                user.UserRoleId = 2;
                user.Active = true;

                _UserService.InsertUser(user);

                // password
                if (!string.IsNullOrWhiteSpace(model.UserPassword.Password))
                {

                    password = new UserPassword
                    {
                        UserId = user.Id,
                        Password = model.UserPassword.Password,
                        CreatedOnUtc = DateTime.UtcNow,
                    };
                    _userPasswordService.InsertUserPassword(password);
                }
                _UserService.UpdateUser(user);


                return RedirectToAction("Index");
            }

            return View(model);

        }


        public IActionResult UploadVideos()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadVideos(VideoModel model)
        {
            if (ModelState.IsValid)
            {
                var data = model.ToEntity<Video>();
                _videoServices.InsertVideo(data);
            }
            return View();
        }

        public IActionResult UploadWeeklyVideo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadWeeklyVideo(WeeklyVideoModel model)
        {
            if (ModelState.IsValid)
            {
                var data = model.ToEntity<WeeklyVideo>();
                _weeklyVideoServices.InsertWeeklyVideo(data);
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

    }
}

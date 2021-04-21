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
using CRM.Core;
using CRM.Services.Authentication;
using Microsoft.AspNetCore.Http;
using CRM.Services.Users;

namespace DeVeeraApp.Controllers
{
    [AuthorizeAdmin]
    public class LessonController : BaseController
    {
        #region fields
        private readonly ILogger<LessonController> _logger;
        private readonly ILevelServices _levelServices;
        private readonly IUserService _userService;
        private readonly IWorkContext _workContext;

        #endregion


        #region ctor
        public LessonController(ILogger<LessonController> logger,
                                ILevelServices levelServices,
                                IUserService userService,
                                IWorkContext workContext,
                                IHttpContextAccessor httpContextAccessor,
                                IAuthenticationService authenticationService) : base(workContext: workContext,
                                                                                  httpContextAccessor: httpContextAccessor,
                                                                                  authenticationService: authenticationService)
        {
            _logger = logger;
            _levelServices = levelServices;
            _userService = userService;
            _workContext = workContext;
        }

        #endregion


        #region Method
        public IActionResult Index(int id, bool isNewUser)
        {
            AddBreadcrumbs("Level", "Index", $"/Lesson/Index/{id}", $"/Lesson/Index/{id}");

            var currentUser = _userService.GetUserById(_workContext.CurrentUser.Id);

            if (id == 0 && isNewUser == false)
            {
                var data = _levelServices.GetLevelById((int)currentUser.LastLevel);
                var videoData = data.ToModel<LevelModel>();
                return View(videoData);
            }
            else if(id == 0 && isNewUser == true)
            {
                var data = _levelServices.GetFirstRecord();
                var videoData = data.ToModel<LevelModel>();
                currentUser.LastLevel = videoData.Id;
                _userService.UpdateUser(currentUser);
                return View(videoData);
            }
            else
            {
                var data = _levelServices.GetLevelById(id);
                var videoData = data.ToModel<LevelModel>();

                currentUser.LastLevel = videoData.Id;
                _userService.UpdateUser(currentUser);
                return View(videoData);
            }
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

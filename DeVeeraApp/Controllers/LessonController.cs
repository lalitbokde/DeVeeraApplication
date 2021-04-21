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

using CRM.Services.VideoModules;
using CRM.Services.Users;


namespace DeVeeraApp.Controllers
{
    [AuthorizeAdmin]
    public class LessonController : BaseController
    {
        #region fields
        private readonly ILogger<LessonController> _logger;
        private readonly ILevelServices _levelServices;

        private readonly IModuleService _moduleServices;

        private readonly IUserService _userService;
        private readonly IWorkContext _workContext;


        #endregion


        #region ctor
        public LessonController(ILogger<LessonController> logger,
                                ILevelServices levelServices,

                                IModuleService moduleService,

                                IUserService userService,

                                IWorkContext workContext,
                                IHttpContextAccessor httpContextAccessor,
                                IAuthenticationService authenticationService) : base(workContext: workContext,
                                                                                  httpContextAccessor: httpContextAccessor,
                                                                                  authenticationService: authenticationService)
        {
            _logger = logger;
            _levelServices = levelServices;

            _moduleServices = moduleService;

            _userService = userService;
            _workContext = workContext;

        }

        #endregion


        #region Method
        public IActionResult Index(int id,int srno)
        {
            ViewBag.SrNo = srno;
            ViewBag.TotalLevels = _levelServices.GetAllLevels().Count;
            AddBreadcrumbs("Level", "Index", $"/Lesson/Index/{id}", $"/Lesson/Index/{id}");

          
                var data = _levelServices.GetLevelById(id);
                var videoData = data.ToModel<LevelModel>();
                videoData.ModuleList = _moduleServices.GetModulesByLevelId(id);
                return View(videoData);
        }

        
        public IActionResult Previous(int id, int srno)
        {
           
            var data = _levelServices.GetAllLevels().Where(a=>a.Id < id).FirstOrDefault();
            if (data != null)
            {
                return RedirectToAction("Index", new { id = data.Id, srno = srno -1 });
            }
            return RedirectToAction("Index", new { id = id, srno = srno -1 });
        }

        public IActionResult Next(int id, int srno)
        {
            var currentUser = _userService.GetUserById(_workContext.CurrentUser.Id);
          

            ViewBag.SrNo = srno;
            var data = _levelServices.GetAllLevels().Where(a => a.Id > id).FirstOrDefault();
            if (data != null)
            {
                if (data.Id > currentUser.LastLevel)
                {
                    currentUser.LastLevel = data.Id;
                    _userService.UpdateUser(currentUser);
                }
                return RedirectToAction("Index", new { id = data.Id, srno = srno +1 });
            }
            return RedirectToAction("Index", new { id = id, srno = srno +1 });
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

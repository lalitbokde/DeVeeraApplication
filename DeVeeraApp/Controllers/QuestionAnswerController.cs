using CRM.Core;
using CRM.Services;
using CRM.Services.Authentication;
using CRM.Services.VideoModules;
using DeVeeraApp.ViewModels.QuestionAnswer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.Controllers
{
    public class QuestionAnswerController : BaseController
    {
        #region fields
        private readonly ILevelServices _levelService;
        private readonly IModuleService _moduleService;
        #endregion

        #region ctor
        public QuestionAnswerController(ILevelServices levelServices,
                                        IModuleService moduleService,
                                        IWorkContext workContext,
                                        IHttpContextAccessor httpContextAccessor,
                                        IAuthenticationService authenticationService) : base(workContext: workContext,
                                                                                             httpContextAccessor: httpContextAccessor,
                                                                                             authenticationService: authenticationService)
        {
            _levelService = levelServices;
            _moduleService = moduleService;
        }
        #endregion

        #region Utilities
        public virtual void PrepareDropdown(QuestionModel model)
        {
            //prepare available url
            model.AvailableLevels.Add(new SelectListItem { Text = "Select Level", Value = "0" });
            var availableLevels = _levelService.GetAllLevels();
            foreach (var url in availableLevels)
            {
                model.AvailableLevels.Add(new SelectListItem
                {
                    Value = url.Id.ToString(),
                    Text = url.Title,
                    Selected = url.Id == model.LevelId
                });
            }

            //prepare available url
            model.AvailableModules.Add(new SelectListItem { Text = "Select Modules", Value = "0" });
            var availableModules = _moduleService.GetAllModules();
            foreach (var url in availableModules)
            {
                model.AvailableLevels.Add(new SelectListItem
                {
                    Value = url.Id.ToString(),
                    //Text = url.Title,
                    Selected = url.Id == model.ModuleId
                });
            }
        }
      
        #endregion
        #region Methods
        public IActionResult Create()
        {
            QuestionModel model = new QuestionModel();
            return View();
        }
        #endregion
    }
}

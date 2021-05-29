using CRM.Core;
using CRM.Core.Domain;
using CRM.Services.Authentication;
using CRM.Services.DashboardMenu;
using CRM.Services.Message;
using DeVeeraApp.Utils;
using DeVeeraApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardMenuController : BaseController
    {
        #region fields
        private readonly IDashboardMenuService _dashboardMenuService;
        private readonly INotificationService _notificationService;
        #endregion

        #region ctor

        public DashboardMenuController(INotificationService notificationService,
                                       IDashboardMenuService dashboardMenuService,
                                       IWorkContext workContext,
                                       IHttpContextAccessor httpContextAccessor,
                                       IAuthenticationService authenticationService) : base(workContext: workContext,
                                                                                            httpContextAccessor: httpContextAccessor,
                                                                                           authenticationService: authenticationService)
        {
            _dashboardMenuService = dashboardMenuService;
            _notificationService = notificationService;
        }

        
        #endregion
        public IActionResult Index()
        {
            AddBreadcrumbs("Dashboard Menu ", "Index", "/DashboardMenu/Index", "/DashboardMenu/Index");
            var dashboardMenu =_dashboardMenuService.GetAllDashboardMenus().FirstOrDefault();
            if (dashboardMenu != null) { 
            var model = dashboardMenu.ToModel<DashboardMenuModel>();
                return View(model);
            }
            else
            {
                return View();
            }
            
        }


        [HttpPost]
        public IActionResult Edit(DashboardMenuModel model)
        {
            AddBreadcrumbs("Dashboard Menu", "Index", "/DashboardMenu/Index", "/DashboardMenu/Index");

            if (ModelState.IsValid)
            {
                var data = model.ToEntity<DashboardMenus>();
                _dashboardMenuService.UpdateDashboardMenu(data);

                _notificationService.SuccessNotification("Dashboard Menu Edited Successfully.");

                return RedirectToAction("Index" ,"Home");
            }
            
            return View(model);
        }
    }
}

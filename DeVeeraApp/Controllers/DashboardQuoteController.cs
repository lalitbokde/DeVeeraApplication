using CRM.Core.Domain;
using DeVeeraApp.ViewModels;
using DeVeeraApp.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Services.DashboardQuotes;
using DeVeeraApp.ViewModels.Common;
using CRM.Core;
using Microsoft.AspNetCore.Http;
using CRM.Services.Authentication;
using CRM.Services.Message;

namespace DeVeeraApp.Controllers
{
    public class DashboardQuoteController : BaseController
    {
        #region fields
        private readonly IDashboardQuoteService _dashboardQuoteService;
        private readonly INotificationService _notificationService;
        #endregion
        #region ctor
        public DashboardQuoteController(IDashboardQuoteService dashboardQuoteService,
                                        IWorkContext workContext,
                                        IHttpContextAccessor httpContextAccessor,
                                        IAuthenticationService authenticationService,
                                        INotificationService notificationService) : base(workContext: workContext,
                                                                                  httpContextAccessor: httpContextAccessor,
                                                                                  authenticationService: authenticationService)
        {
            this._dashboardQuoteService = dashboardQuoteService;
            _notificationService = notificationService;
        }
        #endregion
        #region methods
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            AddBreadcrumbs("Dashboard Quote", "List", "/DashboardQuote/List", "/DashboardQuote/List");

            var model = new List<DashboardQuoteModel>();
            var data = _dashboardQuoteService.GetAllDashboardQutoes();
            if (data.Count() != 0)
            {
                foreach (var item in data)
                {
                    model.Add(item.ToModel<DashboardQuoteModel>());
                }
                return View(model);
            }
            return View();
        }
        public IActionResult Create()
        {
            AddBreadcrumbs("Dashboard Quote", "Create", "/DashboardQuote/List", "/DashboardQuote/Create");

            return View();
        }
        [HttpPost]
        public IActionResult Create(DashboardQuoteModel model)
        {
            AddBreadcrumbs("Dashboard Quote", "Create", "/DashboardQuote/List", "/DashboardQuote/Create");

            if (ModelState.IsValid)
            {
                _dashboardQuoteService.InActiveAllDashboardQuotes();

                var quote = model.ToEntity<DashboardQuote>();
                _dashboardQuoteService.InsertDashboardQutoe(quote);
                _notificationService.SuccessNotification("Dashboard quote added successfully.");
                return RedirectToAction("List");
            }
            return View();
        }

        public IActionResult Edit(int id)
        {
            AddBreadcrumbs("Dashboard Quote", "Edit", "/DashboardQuote/List", $"/DashboardQuote/Edit/{id}");

            if (id != 0)
            {
                var data = _dashboardQuoteService.GetDashboardQutoeById(id);

                if (data != null)
                {
                    var model = data.ToModel<DashboardQuoteModel>();
                    return View(model);
                }

                return View();
            }
            return View();
        }

        [HttpPost]
        public IActionResult Edit(DashboardQuoteModel model)
        {
            AddBreadcrumbs("Dashboard Quote", "Edit", "/DashboardQuote/List", $"/DashboardQuote/Edit/{model.Id}");

            if (ModelState.IsValid)
            {
                _dashboardQuoteService.InActiveAllDashboardQuotes();
                var quote = _dashboardQuoteService.GetDashboardQutoeById(model.Id);
                quote.Title = model.Title;
                quote.Author = model.Author;
                quote.IsActive = model.IsActive;
                _dashboardQuoteService.UpdateDashboardQutoe(quote);
                _notificationService.SuccessNotification("Dashboard quote edited successfully.");

                return RedirectToAction("List");
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            ResponseModel response = new ResponseModel();

            if (id != 0)
            {
                var Data = _dashboardQuoteService.GetDashboardQutoeById(id);
                if (Data == null)
                {
                    response.Success = false;
                    response.Message = "No Data found";
                }
                _dashboardQuoteService.DeleteDashboardQuote(Data);

                response.Success = true;
            }
            else
            {
                response.Success = false;
                response.Message = "No Data found";

            }
            return Json(response);
        }

        #endregion
    }
}

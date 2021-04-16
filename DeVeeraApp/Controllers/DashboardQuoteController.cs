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

namespace DeVeeraApp.Controllers
{
    public class DashboardQuoteController : Controller
    {
        #region fields
        private readonly IDashboardQuoteService _dashboardQuoteService;
        #endregion
        #region ctor
        public DashboardQuoteController(IDashboardQuoteService dashboardQuoteService)
        {
            this._dashboardQuoteService = dashboardQuoteService;
        }
        #endregion
        #region methods
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
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
            return View();
        }
        [HttpPost]
        public IActionResult Create(DashboardQuoteModel model)
        {
            if (ModelState.IsValid)
            {
                _dashboardQuoteService.InActiveAllDashboardQuotes();

                var quote = model.ToEntity<DashboardQuote>();
                _dashboardQuoteService.InsertDashboardQutoe(quote);
                return RedirectToAction("List");
            }
            return View();
        }

        public IActionResult Edit(int id)
        {
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
            if (ModelState.IsValid)
            {
                _dashboardQuoteService.InActiveAllDashboardQuotes();
                var quote = _dashboardQuoteService.GetDashboardQutoeById(model.Id);
                quote.Title = model.Title;
                quote.Author = model.Author;
                quote.IsActive = model.IsActive;
                _dashboardQuoteService.UpdateDashboardQutoe(quote);
                return RedirectToAction("List");
            }
            return View();
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

using CRM.Core.Domain;
using CRM.Services;
using DeVeeraApp.Utils;
using DeVeeraApp.ViewModels;
using DeVeeraApp.ViewModels.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.Controllers
{
    public class WeeklyUpdateController : Controller
    {

        #region fields
        private readonly IWeeklyUpdateServices _weeklyUpdateServices;

        #endregion

        #region ctor

        public WeeklyUpdateController(IWeeklyUpdateServices weeklyUpdateServices)
        {
            _weeklyUpdateServices = weeklyUpdateServices;
        }

        #endregion

        #region methods

        public IActionResult Create(string type)
        {
            ViewBag.QuoteType = type;
            return View();
        }

        [HttpPost]
        public IActionResult Create(WeeklyUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                _weeklyUpdateServices.InActiveAllActiveQuotes((int)model.QuoteType);

                var data = model.ToEntity<WeeklyUpdate>();
                _weeklyUpdateServices.InsertWeeklyUpdate(data);
                return RedirectToAction("List");
            }
            return View();
        }


        public IActionResult Edit(int id)
        {
            if (id != 0)
            {
                var data = _weeklyUpdateServices.GetWeeklyUpdateById(id);

                if (data != null)
                {
                    var model = data.ToModel<WeeklyUpdateModel>();
                    return View(model);
                }

                return View();
            }
            return View();
        }

        [HttpPost]
        public IActionResult Edit(WeeklyUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                _weeklyUpdateServices.InActiveAllActiveQuotes((int)model.QuoteType);
                
                var val = _weeklyUpdateServices.GetWeeklyUpdateById(model.Id);
                //val = model.ToEntity<WeeklyUpdate>();
                val.Title = model.Title;
                val.Subtitle = model.Subtitle;
                val.IsActive = model.IsActive;
                val.VideoURL = model.VideoURL;
                _weeklyUpdateServices.UpdateWeeklyUpdate(val);
                return RedirectToAction("List");
            }
            return View();
        }

        public IActionResult List()
        {
            var model = new List<WeeklyUpdateModel>();
            var data = _weeklyUpdateServices.GetAllWeeklyUpdates();
            if (data.Count() != 0)
            {
                foreach (var item in data)
                {
                    model.Add(item.ToModel<WeeklyUpdateModel>());
                }
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Delete(int id)
        {
            ResponseModel response = new ResponseModel();

            if (id != 0)
            {
                var Data = _weeklyUpdateServices.GetWeeklyUpdateById(id);
                if (Data == null)
                {
                    response.Success = false;
                    response.Message = "No user found";
                }
                _weeklyUpdateServices.DeleteWeeklyUpdate(Data);

                response.Success = true;
            }
            else
            {
                response.Success = false;
                response.Message = "Id is 0";

            }
            return Json(response);
        }

        #endregion

    }
}

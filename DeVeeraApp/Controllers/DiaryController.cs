using CRM.Core.Domain;

using CRM.Core.Domain.VideoModules;
using CRM.Services;
using CRM.Services.Message;
using DeVeeraApp.Utils;
using DeVeeraApp.ViewModels;
using DeVeeraApp.ViewModels.Common;
using DeVeeraApp.ViewModels.Diaries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.Controllers
{
    public class DiaryController : Controller
    {
        #region field

        private readonly INotificationService _notificationService;
        private readonly IDiaryMasterService _DiaryMasterService;

        #endregion


        #region ctor

        public DiaryController(INotificationService notificationService,
                       IDiaryMasterService DiaryMasterService)
        {
            _notificationService = notificationService;
            _DiaryMasterService = DiaryMasterService;
        }

        #endregion


        #region Method

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Create(int levelid, int moduleid)
        {
            DiaryModel model= new DiaryModel(){
                 ModuleId = moduleid,
                 LevelId = levelid
            };
            return View(model);

        }

        [HttpPost]
        public IActionResult Create(DiaryModel model)
        {
            if (ModelState.IsValid)
            {
                var data = model.ToEntity<Diary>();
                _DiaryMasterService.InsertDiary(data);
                _notificationService.SuccessNotification("Diary url added successfully.");
                return RedirectToAction("List");
            }
            return View();

        }

        public IActionResult Edit(int id)
        {
            if(id != 0)
            {
                var data = _DiaryMasterService.GetDiaryById(id);
                if(data != null)
                {
                    var model = data.ToModel<DiaryModel>();
                    return View(model);
                }
            }
            return RedirectToAction("List");

        }

        [HttpPost]
        public IActionResult Edit(DiaryModel model)
        {

            if (ModelState.IsValid)
            {
                var DiaryData = _DiaryMasterService.GetDiaryById(model.Id);
                DiaryData.Comment = model.Comment;
                _DiaryMasterService.UpdateDiary(DiaryData);
                _notificationService.SuccessNotification("Diary url updated successfully.");
                return RedirectToAction("List");
            }
            return View();

        }

        public IActionResult List()
        {
            var DiaryList = _DiaryMasterService.GetAllDiarys();
            var model = new List<DiaryModel>();
            if(DiaryList.Count != 0)
            {
                foreach(var item in DiaryList)
                {
                    model.Add(item.ToModel<DiaryModel>());
                }
                return View(model);

            }
            return RedirectToAction("Index", "Home");

        }

        public IActionResult Delete(int DiaryId)
        {
            ResponseModel response = new ResponseModel();

            if (DiaryId != 0)
            {
                var DiaryData = _DiaryMasterService.GetDiaryById(DiaryId);
                if (DiaryData == null)
                {
                    response.Success = false;
                    response.Message = "No Diary found";
                }
                _DiaryMasterService.DeleteDiary(DiaryData);

                response.Success = true;
            }
            else
            {
                response.Success = false;
                response.Message = "DiaryId is 0";

            }
            return Json(response);
        }
        #endregion


    }
}

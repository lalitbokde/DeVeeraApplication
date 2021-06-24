using CRM.Core.Domain;
using DeVeeraApp.ViewModels;
using DeVeeraApp.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Services.DashboardQuotes;
using DeVeeraApp.ViewModels.Common;
using CRM.Core;
using Microsoft.AspNetCore.Http;
using CRM.Services.Authentication;
using CRM.Services.Message;
using Microsoft.AspNetCore.Mvc.Rendering;
using CRM.Services;
using System.Data;
using OfficeOpenXml;
using Microsoft.AspNetCore.Hosting;
using DeVeeraApp.ViewModels.Response;
using Newtonsoft.Json;
using DeVeeraApp.Filters;

namespace DeVeeraApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthorizeAdmin]
    public class DashboardQuoteController : BaseController
    {
        #region fields
        private readonly IDashboardQuoteService _dashboardQuoteService;
        private readonly ILevelServices _levelService;
        private readonly INotificationService _notificationService;
        private readonly IHostingEnvironment _hostingEnvironment;
        #endregion
        #region ctor
        public DashboardQuoteController(IDashboardQuoteService dashboardQuoteService,
                                        ILevelServices levelServices,
                                        IWorkContext workContext,
                                        IHttpContextAccessor httpContextAccessor,
                                        IAuthenticationService authenticationService,
                                        INotificationService notificationService,
                                        IHostingEnvironment hostingEnvironment) : base(workContext: workContext,
                                                                                  httpContextAccessor: httpContextAccessor,
                                                                                  authenticationService: authenticationService)
        {
            this._dashboardQuoteService = dashboardQuoteService;
            _levelService = levelServices;
            _notificationService = notificationService;
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion

        #region Utilities
        public virtual void PrepareLevelDropdown(DashboardQuoteModel model)
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
        }

        #endregion
        #region methods
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            AddBreadcrumbs("Dashboard Quote", "List", "/Admin/DashboardQuote/List", "/Admin/DashboardQuote/List");

            var model = new List<DashboardQuoteModel>();
            var data = _dashboardQuoteService.GetAllDashboardQuotes();
            if (data.Count() != 0)
            {
                foreach (var item in data)
                {
                    if (item.LevelId != null && item.LevelId != 0)
                    {
                        item.Level = _levelService.GetLevelById(Convert.ToInt32(item.LevelId))?.Title;
                    }
                    model.Add(item.ToModel<DashboardQuoteModel>());
                }

                ViewBag.QuoteTable = JsonConvert.SerializeObject(model);
                return View(model);
            }
            return View(model);
        }
        public IActionResult Create()
        {
            AddBreadcrumbs("Dashboard Quote", "Create", "/Admin/DashboardQuote/List", "/Admin/DashboardQuote/Create");
            DashboardQuoteModel model = new DashboardQuoteModel();
            PrepareLevelDropdown(model);
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(DashboardQuoteModel model)
        {

            ModelState.Remove("layoutSetup.SliderOneTitle"); ModelState.Remove("layoutSetup.SliderTwoTitle"); 
            ModelState.Remove("layoutSetup.SliderThreeTitle"); ModelState.Remove("layoutSetup.ReasonToSubmit");

            if (ModelState.IsValid)
            {
                var ExistingQuote = _dashboardQuoteService.GetAllDashboardQuotes().Where(a => a.Title == model.Title && a.Author == model.Author);
                if (ExistingQuote.Count() == 0)
                {
                    var quote = model.ToEntity<DashboardQuote>();
                    if (model.IsDashboardQuote == true)
                    {
                        _dashboardQuoteService.InActiveAllDashboardQuotes();
                    }
                    _dashboardQuoteService.InsertDashboardQuote(quote);
                    _notificationService.SuccessNotification("Dashboard quote added successfully.");
                    return RedirectToAction("List");
                }
                else
                {
                    _notificationService.ErrorNotification("Quote already exists.");
                    PrepareLevelDropdown(model);
                    return View(model);
                }
            }
            PrepareLevelDropdown(model);
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            AddBreadcrumbs("Dashboard Quote", "Edit", "/Admin/DashboardQuote/List", $"/Admin/DashboardQuote/Edit/{id}");

            if (id != 0)
            {
                var data = _dashboardQuoteService.GetDashboardQuoteById(id);

                if (data != null)
                {
                    var model = data.ToModel<DashboardQuoteModel>();
                    PrepareLevelDropdown(model);
                    return View(model);
                }

                return View();
            }

            return View();
        }

        [HttpPost]
        public IActionResult Edit(DashboardQuoteModel model)
        {
            AddBreadcrumbs("Dashboard Quote", "Edit", "/Admin/DashboardQuote/List", $"/Admin/DashboardQuote/Edit/{model.Id}");
            ModelState.Remove("layoutSetup.SliderOneTitle"); ModelState.Remove("layoutSetup.SliderTwoTitle");
            ModelState.Remove("layoutSetup.SliderThreeTitle"); ModelState.Remove("layoutSetup.ReasonToSubmit");
            if (ModelState.IsValid)
            {
                var quote = _dashboardQuoteService.GetDashboardQuoteById(model.Id);
                quote.Title = model.Title;
                quote.Author = model.Author;
                quote.IsRandom = model.IsRandom;
                quote.IsWeeklyInspiringQuotes = model.IsWeeklyInspiringQuotes;
                quote.IsDashboardQuote = model.IsDashboardQuote;
                quote.LevelId = model.LevelId;
                if (model.IsDashboardQuote == true)
                {
                    _dashboardQuoteService.InActiveAllDashboardQuotes();
                }
                _dashboardQuoteService.UpdateDashboardQuote(quote);
                _notificationService.SuccessNotification("Dashboard quote edited successfully.");

                return RedirectToAction("List");
            }
            PrepareLevelDropdown(model);
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            ResponseModel response = new ResponseModel();

            if (id != 0)
            {
                var Data = _dashboardQuoteService.GetDashboardQuoteById(id);
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
        #region ImportExcel

        [Obsolete]
        public async Task<bool> UploadExcel(IFormFile file)
        {

            var FileDic = "ImportExcel";

            string FilePath = Path.Combine(_hostingEnvironment.WebRootPath, FileDic);

            if (!Directory.Exists(FilePath))
                Directory.CreateDirectory(FilePath);

            var fileName = file.FileName;

            var filePath = Path.Combine(FilePath, fileName);

            using (FileStream fs = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(fs);
            }

            return true;
        }

        [HttpPost]
        public IActionResult ImportExcel(string filename)
        {
            ResponceModel response = new ResponceModel();

            if (filename != null)
            {
                List<DashboardQuote> _QuoteList = new List<DashboardQuote>();
                List<DashboardQuote> _UniqueQuoteList = new List<DashboardQuote>();

                string ErrorMessage = "";
                int duplicatecount = 0;

                var path = Path.Combine(_hostingEnvironment.WebRootPath + "//ImportExcel", filename);

                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (ExcelPackage package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet workSheet = package.Workbook.Worksheets[1];

                        int totalColumns = workSheet.Dimension.Columns;

                        int totalRows = workSheet.Dimension.Rows;

                        var IsValidColumns = (totalColumns == 2);

                        if (!IsValidColumns) ErrorMessage += "Invalid Columns";

                        var IsEmpty = (totalRows <= 0);

                        if (IsEmpty) ErrorMessage += "No Data To Import";

                        for (int row = 2; row <= totalRows; row++)
                        {
                            if (workSheet.Cells[row, 1].Value != null && workSheet.Cells[row, 2].Value != null)
                            {
                                _QuoteList.Add(new DashboardQuote
                                {
                                    Title = workSheet.Cells[row, 1].Value.ToString().Trim(),

                                    Author = workSheet.Cells[row, 2].Value.ToString().Trim(),

                                    IsDashboardQuote = false,

                                    IsRandom = true,

                                    //Level = (workSheet.Cells[row, 4].Value != null) ? Convert.ToBoolean(workSheet.Cells[row, 4].Value.ToString().Trim()) : false,
                                });
                            }

                        }


                    }

                }

                if (_QuoteList.Count() != 0)
                {
                    foreach (var item in _QuoteList)
                    {
                        var ExistingQuote = _dashboardQuoteService.GetAllDashboardQuotes().Where(a => a.Title == item.Title && a.Author==item.Author);

                        if (ExistingQuote.Count() == 0)
                        {
                            _UniqueQuoteList.Add(new DashboardQuote
                            {
                                Title = item.Title,

                                Author = item.Author,

                                IsDashboardQuote = item.IsDashboardQuote,

                                IsRandom = item.IsRandom,

                                Level="All Level"

                            });;;
                        }
                        else
                        {
                            duplicatecount++;
                        }

                    }
                }
                else
                {
                    _notificationService.ErrorNotification("Empty File.");
                    System.IO.File.Delete(path);

                    response.Success = false;
                }

                if (_UniqueQuoteList.Count() != 0)
                {
                    foreach (var item in _UniqueQuoteList)
                    {
                        _dashboardQuoteService.InsertDashboardQuote(item);
                    }

                    _notificationService.SuccessNotification("Quotes added successfully.");

                    response.Success = true;

                }
                else
                {
                    ErrorMessage = duplicatecount != 0 ? $"{duplicatecount} Duplicate Quote in sheet." : "";
                    _notificationService.ErrorNotification(ErrorMessage);
                    System.IO.File.Delete(path);
                    response.Success = false;
                }

            }
            else
            {
                _notificationService.ErrorNotification("File Not Selected.");

                response.Success = false;

            }


            return Json(response);
        }
        public virtual IActionResult SampleExcel()
        {
            string filename = _hostingEnvironment.WebRootPath + "/ImportSample/SampleQuote.xlsx";
            byte[] fileBytes = System.IO.File.ReadAllBytes(_hostingEnvironment.WebRootPath + "/ImportSample/SampleQuote.xlsx");
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Xml, "SampleQuote.xlsx");
        }

        #endregion
    }
}

using CRM.Core;
using CRM.Core.Domain.VideoModules;
using CRM.Services;
using CRM.Services.Authentication;
using CRM.Services.Message;
using CRM.Services.QuestionsAnswer;
using CRM.Services.VideoModules;
using DeVeeraApp.Filters;
using DeVeeraApp.Utils;
using DeVeeraApp.ViewModels.Common;
using DeVeeraApp.ViewModels.QuestionAnswer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeVeeraApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthorizeAdmin]
    public class QuestionAnswerController : BaseController
    {
        #region fields
        private readonly ILevelServices _levelService;
        private readonly IModuleService _moduleService;
        private readonly IQuestionAnswerService _QuestionAnswerService;
        private readonly INotificationService _notificationService;
        private readonly IWorkContext _workContext;
        #endregion

        #region ctor
        public QuestionAnswerController(ILevelServices levelServices,
                                        IModuleService moduleService,
                                        IQuestionAnswerService questionAnswerService,
                                        INotificationService notificationService,
                                        IWorkContext WorkContextService,
                                        IHttpContextAccessor httpContextAccessor,
                                        IAuthenticationService authenticationService) : base(workContext: WorkContextService,
                                                                                             httpContextAccessor: httpContextAccessor,
                                                                                             authenticationService: authenticationService)
        {
            _levelService = levelServices;
            _moduleService = moduleService;
            _QuestionAnswerService = questionAnswerService;
            this._workContext = WorkContextService;
            _notificationService = notificationService;
        }
        #endregion

        #region Utilities
        public virtual void PrepareDropdowns(QuestionModel model)
        {
            //prepare available levels
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

            //prepare available modules
            model.AvailableModules.Add(new SelectListItem { Text = "Select Modules", Value = "0" });
            var availableModules = _moduleService.GetAllModules();
            foreach (var url in availableModules)
            {
                model.AvailableModules.Add(new SelectListItem
                {
                    Value = url.Id.ToString(),
                    Text = url.Title,
                    Selected = url.Id == model.ModuleId
                });
            }
        }

        #endregion

        #region Methods
        public IActionResult List()
        {
            AddBreadcrumbs("Questionnaire", "List", "/Admin/QuestionAnswer/List", "/Admin/QuestionAnswer/List");
            var model = new QuestionModel();
            List<QuestionModel> QuestionsList = new List<QuestionModel>();
            var data = _QuestionAnswerService.GetAllQuestions().ToList();
            if (data.Count != 0)
            {
               model.QuestionsList = data.ToModelList<Questions, QuestionModel>(QuestionsList);

            }

            return View(model);
        }
        public IActionResult Create(string pagetype)
        {
            AddBreadcrumbs("Questionnaire", "Create", "/Admin/QuestionAnswer/List", "/Admin/QuestionAnswer/Create");
            QuestionModel model = new QuestionModel();
            model.Questionarrie = pagetype;
            ViewBag.pagetype = pagetype;
            PrepareDropdowns(model);
            return View(model);
            
        }

        [HttpPost]
        public IActionResult Create(QuestionModel model)
        {
            string pagetype = Request.Form["pagetype"];
            AddBreadcrumbs("Questionnaire", "Create", "/Admin/QuestionAnswer/List", "/Admin/QuestionAnswer/Create");
            if (ModelState.IsValid)
            {
                var data = model.ToEntity<Questions>();
                data.CreatedOn = DateTime.UtcNow;

                _QuestionAnswerService.InsertQuestion(data);
                if(pagetype!= "Questionarrie")
                { 
                return RedirectToAction("Index", "Module",new {id=model.ModuleId });
                }
                else
                {
                    return RedirectToAction("List", "QuestionAnswer");
                }
            }
            PrepareDropdowns(model);
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            AddBreadcrumbs("Questionnaire", "Edit", "/Admin/QuestionAnswer/List", $"/Admin/QuestionAnswer/Edit/{id}");

            if (id != 0)
            {
                QuestionModel model = new QuestionModel();
                var data = _QuestionAnswerService.GetQuestionById(id);

                if (data != null)
                {
                    model = data.ToModel<QuestionModel>();
                    model.LevelId = data.Module.LevelId;
                    PrepareDropdowns(model);
                    return View(model);
                }

                return View();
            }

            return View();
        }

        [HttpPost]
        public IActionResult Edit(QuestionModel model)
        {
            AddBreadcrumbs("Questionnaire", "Edit", "/Admin/QuestionAnswer/List", $"/Admin/QuestionAnswer/Edit/{model.Id}");

            if (ModelState.IsValid)
            {
                var question = _QuestionAnswerService.GetQuestionById(model.Id);

                question.Question = model.Question;
                question.ModuleId = model.ModuleId;
                _QuestionAnswerService.UpdateQuestion(question);

                _notificationService.SuccessNotification("Question edited successfully.");

                return RedirectToAction("List");
            }
           
            return View(model);
        }

        [HttpGet]
        public IActionResult GetModuleByLevelId(int Id, int SelectedId)
        {
            List<SelectListItem> Modules = new List<SelectListItem>();
            //modules
            if (Id > 0) {
                var modules = _moduleService.GetModulesByLevelId(Id).ToList();
                Modules.Add(new SelectListItem { Text = "Select Module", Value = null });
                if (modules.Any())
                {
                    foreach (var s in modules)
                    {
                        Modules.Add(new SelectListItem { Text = s.Title, Value = s.Id.ToString(), Selected = (s.Id == SelectedId) });
                    }
                }
            }
            else
            {
                var modules = _moduleService.GetAllModules().ToList();
                Modules.Add(new SelectListItem { Text = "Select Module", Value = null });
                if (modules.Any())
                {
                    foreach (var s in modules)
                    {
                        Modules.Add(new SelectListItem { Text = s.Title, Value = s.Id.ToString(), Selected = (s.Id == SelectedId) });
                    }
                }
            }
            return Json(Modules);
        }

        [HttpPost]
        public IActionResult Answer(QuestionModel model)
        {          
            model.Id =Convert.ToInt32(Request.Form["Id"]);
            model.ModuleId = Convert.ToInt32(Request.Form["ModuleId"]);
            model.Answer = Request.Form["Answer"];
            int UserId = _workContext.CurrentUser.Id;

            var Question = _QuestionAnswerService.GetQuestionById(model.Id);
            if (Question.Question_Answer_Mapping.Where(a => a.UserId == UserId).ToList().Count == 0)
            {
                Question.Question_Answer_Mapping.Add(new Question_Answer_Mapping
                {
                    QuestionId = model.Id,
                    UserId = _workContext.CurrentUser.Id,
                    CreatedOn = DateTime.UtcNow,
                    Answer = model.Answer
                });

                _QuestionAnswerService.UpdateQuestion(Question);
            }

            return RedirectToAction("Index", "Module", new { id = model.ModuleId });

        }

        public IActionResult Delete(int id)
        {
            ResponseModel response = new ResponseModel();

            if (id != 0)
            {
                var Data = _QuestionAnswerService.GetQuestionById(id);
                if (Data == null)
                {
                    response.Success = false;
                    response.Message = "No Data found";
                }
                _QuestionAnswerService.DeleteQuestion(Data);

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

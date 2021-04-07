using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DeVeeraApp.Models;
using DeVeeraApp.Filters;

namespace DeVeeraApp.Controllers
{
    [AuthorizeAdmin]
    public class LessonController : Controller
    {
        private readonly ILogger<LessonController> _logger;

        public LessonController(ILogger<LessonController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int id, string lesson)
        {
            ViewBag.LessonNumber = id.ToString();
            ViewBag.lessonName = lesson;
            return View();
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
    }
}

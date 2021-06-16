using DeVeeraApp.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewComponents
{
    public class PagerViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke(PagedResultBase result)
        {
            return View("Default", result);
        }
    }
}

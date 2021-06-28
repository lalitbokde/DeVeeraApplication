using DeVeeraApp.Utils;
using Microsoft.AspNetCore.Mvc;

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

using DeVeeraApp.Utils;
using Microsoft.AspNetCore.Mvc;

namespace DeVeeraApp.Areas.Admin.ViewComponents
{
    public class PaginationViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke(PagedResultBase result)
        {
            return View("Default", result);
        }
    }
}

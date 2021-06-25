using Microsoft.AspNetCore.Mvc;

namespace DeVeeraApp.Areas.Admin.ViewComponents
{
    public class AdminMenuViewComponent : ViewComponent
    {
       

        public AdminMenuViewComponent()
        {
           
        }


        public IViewComponentResult Invoke()
        {

        
            return View();
        }

    }
}

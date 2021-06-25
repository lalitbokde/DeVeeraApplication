using Microsoft.AspNetCore.Mvc;

namespace DeVeeraApp.Areas.Admin.ViewComponents
{
    public class MobileMenuViewComponent : ViewComponent
    {
       

        public MobileMenuViewComponent()
        {
           
        }


        public IViewComponentResult Invoke()
        {

        
            return View();
        }

    }
}

using Microsoft.AspNetCore.Mvc;

namespace DeVeeraApp.Areas.Admin.ViewComponents
{
    public class TopBarViewComponent : ViewComponent
    {
       

        public TopBarViewComponent()
        {
           
        }


        public IViewComponentResult Invoke()
        {

        
            return View();
        }

    }
}

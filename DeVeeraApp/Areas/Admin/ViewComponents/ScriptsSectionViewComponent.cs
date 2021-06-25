using Microsoft.AspNetCore.Mvc;

namespace DeVeeraApp.Areas.Admin.ViewComponents
{
    public class ScriptsSectionViewComponent : ViewComponent
    {
       

        public ScriptsSectionViewComponent()
        {
           
        }


        public IViewComponentResult Invoke()
        {

        
            return View();
        }

    }
}

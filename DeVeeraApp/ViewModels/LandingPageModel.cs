using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels
{
    public class LandingPageModel : BaseEntityModel
    {
        public LandingPageModel()
        {
            WeeklyUpdate = new WeeklyUpdateModel();
            Language = new LanguageModel();
        }
        public WeeklyUpdateModel WeeklyUpdate { get; set; }
        public LanguageModel Language { get; set; }
    }
}

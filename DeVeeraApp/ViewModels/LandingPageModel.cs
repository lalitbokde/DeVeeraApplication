using DeVeeraApp.ViewModels.User;
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
            //User = new UserModel();
        }
        public WeeklyUpdateModel WeeklyUpdate { get; set; }
        public LanguageModel Language { get; set; }
        //public UserModel User { get; set; }
    }
}

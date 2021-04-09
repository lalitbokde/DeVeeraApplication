using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels
{
    public class WeeklyVideoModel : BaseEntityModel
    {
        public string Title { get; set; }
        public string VideoURL { get; set; }
        public string WeeklyText { get; set; }
    }
}

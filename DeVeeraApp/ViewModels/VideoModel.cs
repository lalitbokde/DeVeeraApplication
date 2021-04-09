using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels
{
    public class VideoModel : BaseEntityModel
    {
        public string Title { get; set; }
        public string VideoURL { get; set; }
        public string FullDescription { get; set; }
    }
}

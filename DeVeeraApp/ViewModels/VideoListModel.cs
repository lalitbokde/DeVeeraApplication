using CRM.Core.ViewModels;
using DeVeeraApp.Utils;
using DeVeeraApp.ViewModels.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels
{
    public class VideoListModel : CommonPageModel
    {
        public VideoListModel()
        {
            Video = new VideoModel();
            
        }
       
        public VideoModel Video { get; set; }

        public PagedResult<VideoViewModel> VideoListPaged { get; set; }

    }
}

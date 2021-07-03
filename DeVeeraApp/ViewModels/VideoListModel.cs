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
            Video = new List<VideoModel>();
            
        }
        public int SortTypeId { get; set; }
        public string SearchByDate { get; set; }

        [NotMapped]
        public SortType SortType
        {
            get
            {
                return (SortType)SortTypeId;
            }
            set
            {
                SortTypeId = (int)value;
            }
        }
        public List<VideoModel> Video { get; set; }

        public PagedResult<VideoViewModel> VideoListPaged { get; set; }

    }
}

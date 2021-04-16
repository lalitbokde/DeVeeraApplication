using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels
{
    public class DashboardQuoteModel:BaseEntityModel
    {
        public DashboardQuoteModel()
        {
            VideoModelList = new List<VideoModel>();
        }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }

        public bool IsActive { get; set; } = false;

      
        public IList<VideoModel> VideoModelList { get; set; }

    }
}

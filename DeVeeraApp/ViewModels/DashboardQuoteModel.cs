using Microsoft.AspNetCore.Mvc.Rendering;
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
            VideoModelList = new List<LevelModel>();
            this.AvailableLevels = new List<SelectListItem>();
        }

        public int? LevelId { get; set; }

        [Required]
        public string Title { get; set; }     
        public string Author { get; set; }
        public string Level { get; set; }

        //public bool IsActive { get; set; } = false;
        public bool IsDashboardQuote { get; set; }
        public bool IsRandom { get; set; }
        public bool IsWeeklyInspiringQuotes { get; set; }
        public IList<LevelModel> VideoModelList { get; set; }
        public IList<SelectListItem> AvailableLevels { get; set; }
    }
}

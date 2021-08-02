using CRM.Core.Domain;
using DeVeeraApp.ViewModels.LayoutSetups;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeVeeraApp.ViewModels
{
    public class DashboardQuoteModel:BaseEntityModel
    {
        public DashboardQuoteModel()
        {
            VideoModelList = new List<LevelModel>();
            this.AvailableLevels = new List<SelectListItem>();
            layoutSetup = new LayoutSetupModel();
        }

        [Range(1, int.MaxValue, ErrorMessage = "Please select the level")]
        [Required]
        public int? LevelId { get; set; }

        
        [Required(ErrorMessage ="The Quote field is Required")]
        public string Title { get; set; }
        
        [Required(ErrorMessage ="Please enter the author ")]
        public string Author { get; set; }
        public string Level { get; set; }

        //public bool IsActive { get; set; } = false;
        public bool IsDashboardQuote { get; set; }
        public bool IsRandom { get; set; }
        public bool IsWeeklyInspiringQuotes { get; set; }
        public int AutoIncrement { get; set; }

        public LayoutSetupModel layoutSetup { get; set; }
        public DashboardMenus Menus { get; set; }
        public IList<LevelModel> VideoModelList { get; set; }
        public IList<SelectListItem> AvailableLevels { get; set; }

        public int TotalLevelCount { get; set; }
        public int TotalUserCount { get; set; }
        public int TotalVisitorsCount { get; set; }
        public int TotalModuleCount { get; set; }
    }
}

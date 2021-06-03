using CRM.Core.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels
{
    public class WeeklyUpdateModel : BaseEntityModel
    {
        public WeeklyUpdateModel()
        {
            this.AvailableVideo = new List<SelectListItem>();
            AvailableImages = new List<SelectListItem>();
        }

        [Required]
        public int VideoId { get; set; }
        public string VideoUrl { get; set; }

        [Required]
        public string Title { get; set; }

       
        public string Subtitle { get; set; }

        public string VideoName { get; set; }

        [Required]
        public Quote QuoteType { get; set; }
        public bool IsActive { get; set; } = false;
        public IList<SelectListItem> AvailableVideo { get; set; }
        public List<SelectListItem> AvailableImages { get; }

        public string FileName { get; set; }

        [NotMapped]
        public int LastLevel { get; set; }

        public string SliderOneTitle { get; set; }
        public string SliderOneDescription { get; set; }
        public string SliderOneImage { get; set; }
        public string SliderTwoTitle { get; set; }
        public string SliderTwoDescription { get; set; }
        public string SliderTwoImage { get; set; }
        public string SliderThreeTitle { get; set; }
        public string SliderThreeDescription { get; set; }
        public string SliderThreeImage { get; set; }

        public string DescriptionImage { get; set; }
        public string LandingQuote { get; set; }

    }

    public enum Quote
    {
        [Description("Login")]
        Login = 1,
        [Description("Registration")]
        Registration = 2,
        [Description("Landing Page")]
        Landing = 3
    }

}

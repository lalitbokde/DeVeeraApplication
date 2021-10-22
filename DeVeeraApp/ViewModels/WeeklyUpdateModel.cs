using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeVeeraApp.ViewModels
{
    public class WeeklyUpdateModel : BaseEntityModel
    {
        public WeeklyUpdateModel()
        {
            this.AvailableVideo = new List<SelectListItem>();
            AvailableImages = new List<SelectListItem>();
            AvailableBannerImage = new List<SelectListItem>();
            AvilableQuote = new List<SelectListItem>();
            AvilableQuoteSpanish = new List<SelectListItem>();
        }

        //[Range(1, int.MaxValue, ErrorMessage = "Please select the video")]
        //[Required]
        public int VideoId { get; set; }
        public string VideoUrl { get; set; }

        
        //[Required(ErrorMessage ="Enter the title ")]
        public string Title { get; set; }

        public string landingImageOneUrl { get; set; }
        public string landingImageTwoUrl { get; set; }
        public string landingImageThreeUrl { get; set; }
        public string DescriptionImageUrl { get; set; }

        //[Required(ErrorMessage = "Enter the Quote ")]
        public string Subtitle { get; set; }
        public string SpanishSubtitleLogin { get; set; }
        
        public string VideoName { get; set; }

        //[Required]
        public Quote QuoteType { get; set; }
        public bool IsActive { get; set; } = false;
        public bool IsRandom { get; set; }
        public int BannerImageId { get; set; }
        public int? QuoteId { get; set; }
        public int BodyImageId { get; set; }
        public string BannerImageURL { get; set; }
        public string BodyImageURL { get; set; }
        public IList<SelectListItem> AvailableVideo { get; set; }
        public List<SelectListItem> AvailableImages { get; }
        public List<SelectListItem> AvailableBannerImage { get; set; }
        public List<SelectListItem> AvilableQuote { get; set; }
        public List<SelectListItem> AvilableQuoteSpanish { get; }
        public string FileName { get; set; }

        //[NotMapped]
        public int LastLevel { get; set; }


        //[NotMapped]
        public int FirstLevel { get; set; }

        //[Required(ErrorMessage ="Please enter slider 1 title")]
        public string SliderOneTitle { get; set; }

        //[Required(ErrorMessage = "Please enter slider 1 Description")]
        public string SliderOneDescription { get; set; }
        public int SliderOneImageId { get; set; }
        //[Required(ErrorMessage = "Please enter slider 2 title")]
        public string SliderTwoTitle { get; set; }
        //[Required(ErrorMessage = "Please enter slider 2 Description")]
        public string SliderTwoDescription { get; set; }
        public int SliderTwoImageId { get; set; }
        //[Required(ErrorMessage = "Please enter slider 3 title")]
        public string SliderThreeTitle { get; set; }
        //[Required(ErrorMessage = "Please enter slider 3 Description")]
        public string SliderThreeDescription { get; set; }
        public int SliderThreeImageId { get; set; }
        public int DescriptionImageId { get; set; }
        
        //[Required(ErrorMessage = "Enter the Quote ")]
        public string LandingQuote { get; set; }
        public string Quote { get; set; }
        public string VideoHeader { get; set; }
        public string SpanishTitleLogin { get; set; }
        public string SpanishQuoteLogin { get; set; }
        public string TitleRegistration { get; set; }
        public string SubtitleRegistration { get; set; }
        public string QuoteRegistration { get; set; }
        public string VideoHeaderRegistration { get; set; }
        public string SubtitleSpanishLanding { get; set; }
        public string TitleSpanishLanding { get; set; }
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
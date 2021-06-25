using System.ComponentModel;

namespace CRM.Core.Domain
{
    public class WeeklyUpdate : BaseEntity
    {
        public int VideoId { get; set; }
        public int BannerImageId { get; set; }
        public int BodyImageId { get; set; }

        public string Title { get; set; }
        public string Subtitle { get; set; }
        public Quote QuoteType { get; set; }
        public string SliderOneTitle { get; set; }
        public string SliderOneDescription { get; set; }
        public int SliderOneImageId { get; set; }
        public string SliderTwoTitle { get; set; }
        public string SliderTwoDescription { get; set; }
        public int SliderTwoImageId { get; set; }
        public string SliderThreeTitle { get; set; }
        public string SliderThreeDescription { get; set; }
        public int SliderThreeImageId { get; set; }
        public int DescriptionImageId { get; set; }
        public string LandingQuote { get; set; }
        public bool IsActive { get; set; }
        public bool IsRandom { get; set; }
        public virtual Video Video { get; set; }

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

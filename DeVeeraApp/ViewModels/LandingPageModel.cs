namespace DeVeeraApp.ViewModels
{
    public class LandingPageModel : BaseEntityModel
    {
        public LandingPageModel()
        {
            WeeklyUpdate = new WeeklyUpdateModel();
            Language = new LanguageModel();
            //User = new UserModel();
        }

        public string SliderOneImageUrl { get; set; }
        public string SliderTwoImageUrl { get; set; }
        public string SliderThreeImageUrl { get; set; }
        public string DescriptionImageUrl { get; set; }

        public WeeklyUpdateModel WeeklyUpdate { get; set; }
        public LanguageModel Language { get; set; }
        //public UserModel User { get; set; }
    }
}

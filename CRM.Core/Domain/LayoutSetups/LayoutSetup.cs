namespace CRM.Core.Domain.LayoutSetups
{
    public class LayoutSetup:BaseEntity
    {
        //User Dashboard

        public int SliderOneImageId { get; set; }
        public int SliderTwoImageId { get; set; }
        public int SliderThreeImageId { get; set; }
        public string SliderOneTitle { get; set; }
        public string SliderOneDescription { get; set; }
        public string SliderTwoTitle { get; set; }
        public string SliderTwoDescription { get; set; }
        public string SliderThreeTitle { get; set; }
        public string SliderThreeDescription { get; set; }

        //User Registration Form
        public int BannerOneImageId { get; set; }

        //User Login Form
        public int BannerTwoImageId { get; set; }

        //User Diary
        public int DiaryHeaderImageId { get; set; }

        //Complete Registration from
        public int CompleteRegistrationHeaderImgId { get; set; }
        public string ReasonToSubmit { get; set; }

        public string Link_1 { get; set; }
        public string Link_2 { get; set; }
        public string Link_3 { get; set; }

        public int Link_1_BannerImageId { get; set; }
        public int Link_2_BannerImageId { get; set; }
        public int Link_3_BannerImageId { get; set; }


    }
}

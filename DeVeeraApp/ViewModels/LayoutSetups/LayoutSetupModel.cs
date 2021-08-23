using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;



namespace DeVeeraApp.ViewModels.LayoutSetups
{
    public class LayoutSetupModel:BaseEntityModel
    {

        public LayoutSetupModel()
        {
            AvailableImages = new List<SelectListItem>();
            AvailableVideo = new List<SelectListItem>();
        }

      

        //User Dashboard
        [DataType(DataType.ImageUrl)]
        [Required(ErrorMessage ="Select slider 1 image")]

        public int SliderOneImageId { get; set; }


        [Required(ErrorMessage = "Select slider 2 image")]
        public int SliderTwoImageId { get; set; }


        [Required(ErrorMessage = "Select slider 3 image")]
        public int SliderThreeImageId { get; set; }
        public string SliderOneImageUrl { get; set; }
        public string SliderTwoImageUrl { get; set; }
        public string SliderThreeImageUrl { get; set; }

        //[Required(ErrorMessage = "Please enter slider 1 title")]
        public string SliderOneTitle { get; set; }
        public string SliderOneDescription { get; set; }
        //[Required(ErrorMessage = "Please enter slider 2 title")]
        public string SliderTwoTitle { get; set; }
        public string SliderTwoDescription { get; set; }
        //[Required(ErrorMessage = "Please enter slider 3 title")]
        public string SliderThreeTitle { get; set; }
        public string SliderThreeDescription { get; set; }
        public bool IsActive { get; set; }

        //User Registration Form
        public int BannerOneImageId { get; set; }
        public string BannerOneImageUrl { get; set; }

        //User Login Form
        public int BannerTwoImageId { get; set; }
        public string BannerTwoImageUrl { get; set; }

        //User Diary
        public int DiaryHeaderImageId { get; set; }
        public string DiaryHeaderImageUrl { get; set; }

        //Complete Registration from
        public int CompleteRegistrationHeaderImgId { get; set; }
        public string CompleteRegistrationHeaderImgUrl { get; set; }
        
        [Required(ErrorMessage ="Enter the reason")]
        public string ReasonToSubmit { get; set; }
        public List<SelectListItem> AvailableImages { get; }

        public string Link_1 { get; set; }
        public string Link_2 { get; set; }
        public string Link_3 { get; set; }

        public int Link_1_BannerImageId { get; set; }
        public int Link_2_BannerImageId { get; set; }
        public int Link_3_BannerImageId { get; set; }

        public string Link_1_BannerImageUrl { get; set; }
        public string Link_2_BannerImageUrl { get; set; }
        public string Link_3_BannerImageUrl { get; set; }

        //Module Section Header
         public string Title { get; set; }
         public string Description { get; set; }
        public string ModuleSpanishDescription { get; set; }
        public string ModuleSpanishTitle { get; set; }

        //Home
        public string HomeTitle { get; set; }
        public string HomeDescription { get; set; }
        public string HomeSubTitle { get; set; }
        public string HomeSpanishDescription { get; set; }
        public string HomeTitleSpanish { get; set; }
        public string HomeSubTitleSpanish { get; set; }

        //video
        [Required]
        public int? VideoId { get; set; }
        public int BannerImageId { get; set; }
        public string BannerImageUrl { get; set; }

        public int VideoThumbImageId { get; set; }
        public string VideoThumbImageUrl { get; set; }

        public int ShareBackgroundImageId { get; set; }
        public string ShareBackgroundImageUrl { get; set; }
        public IList<SelectListItem> AvailableVideo { get; set; }
        //Footer 
        public string FooterDescription { get; set; }
        public string FooterDescriptionSpanish { get; set; }
        public string Email{ get; set; }
     
        public string PhoneNo{ get; set; }
         public int FooterImageId { get; set; }
          public string FooterImageUrl { get; set; }
        public string Location { get; set; }
        public string LocationSpanish { get; set; }

    }
}

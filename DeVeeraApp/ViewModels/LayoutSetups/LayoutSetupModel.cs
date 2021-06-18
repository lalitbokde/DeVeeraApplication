using CRM.Core.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
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
            
        }

        //User Dashboard

        public int SliderOneImageId { get; set; }
        public int SliderTwoImageId { get; set; }
        public int SliderThreeImageId { get; set; }
        public string SliderOneImageUrl { get; set; }
        public string SliderTwoImageUrl { get; set; }
        public string SliderThreeImageUrl { get; set; }
        public string SliderOneTitle { get; set; }
        public string SliderOneDescription { get; set; }
        public string SliderTwoTitle { get; set; }
        public string SliderTwoDescription { get; set; }
        public string SliderThreeTitle { get; set; }
        public string SliderThreeDescription { get; set; }

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
        

        public string ReasonToSubmit { get; set; }
        public List<SelectListItem> AvailableImages { get; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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
        public string ReasonToSubmit { get; set; }

    }
}

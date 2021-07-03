using CRM.Core.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeVeeraApp.ViewModels
{
    public class FeelGoodStoryModel : BaseEntityModel
    {
        public FeelGoodStoryModel()
        {
            AvailableImages = new List<SelectListItem>();
        }

        [Required(ErrorMessage ="Please enter the title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter the Author")]
        public string Author { get; set; }
        [Required(ErrorMessage = "Please enter the Story")]
        public string Story { get; set; }
        public int? ImageId { get; set; }
        public string ImageUrl { get; set; }
        

        public virtual Image Image { get; set; }
        public IList<SelectListItem> AvailableImages { get; set; }

        public string Link_3_bannerImage { get; set; }
        public string Link_3_Title { get; set; }

    }
}

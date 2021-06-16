using CRM.Core.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels.Emotions
{
    public class EmotionModel:BaseEntityModel
    {
        public EmotionModel()
        {
            EmotionList = new List<EmotionModel>();
            AvailableImages = new List<SelectListItem>();
            AvailableVideo = new List<SelectListItem>();
        }

        [Required]
        public int? EmotionNo { get; set; }

        [Required]
        public int VideoId { get; set; }

        [Required]
        public int EmotionHeaderImageId { get; set; }

        [Required]
        public int EmotionBannerImageId { get; set; }

        [Required]
        public int EmotionThumbnailImageId { get; set; }

        [Required]
        public string EmotionName { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Subtitle { get; set; }

        [Required]
        public string Quote { get; set; }

        [Required]
        public string Description { get; set; }

        public string EmotionHeaderImageUrl{ get; set; }
        public string EmotionBannerImageUrl { get; set; }
        public string EmotionThumbnailImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public bool Deleted { get; set; }
        public virtual Video Video { get; set; }
        public IList<SelectListItem> AvailableVideo { get; set; }
        public IList<SelectListItem> AvailableImages { get; set; }
        public List<EmotionModel> EmotionList { get; set; }
    }
}

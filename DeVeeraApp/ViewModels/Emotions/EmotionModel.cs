using CRM.Core.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeVeeraApp.ViewModels.Emotions
{
    public class EmotionModel:BaseEntityModel
    {
        public EmotionModel()
        {
            EmotionList = new List<EmotionModel>();
            AvailableImages = new List<SelectListItem>();
            AvailableVideo = new List<SelectListItem>();
            AvilableQuote = new List<SelectListItem>();
        }

       
        [Required(ErrorMessage ="Please enter Emotion No")]
        public int? EmotionNo { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select the video")]
        [Required]
        public int VideoId { get; set; }
        [Required]
        public int? QuoteId { get; set; }

        [Required]
        public int EmotionHeaderImageId { get; set; }

        [Required]
        public int EmotionBannerImageId { get; set; }

        [Required]
        public int EmotionThumbnailImageId { get; set; }

        [StringLength(150)]
        [Required(ErrorMessage = "Please enter Emotion name")]
        public string EmotionName { get; set; }

        [StringLength(150)]
        [Required(ErrorMessage = "Please enter title")]
        public string Title { get; set; }

        [StringLength(150)]
        [Required(ErrorMessage = "Please enter sub title")]
        public string Subtitle { get; set; }

        //[Required(ErrorMessage = "Please enter Quote")]
        public string Quote { get; set; }

        
        public string Description { get; set; }
        public bool IsRandom { get; set; }
        public string EmotionHeaderImageUrl{ get; set; }
        public string EmotionBannerImageUrl { get; set; }
        public string EmotionThumbnailImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public bool Deleted { get; set; }
        public virtual Video Video { get; set; }
        public IList<SelectListItem> AvailableVideo { get; set; }
        public IList<SelectListItem> AvilableQuote { get; set; }
        public IList<SelectListItem> AvailableImages { get; set; }
        public List<EmotionModel> EmotionList { get; set; }
        
    }
}

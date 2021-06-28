using CRM.Core.Domain;
using CRM.Core.Domain.VideoModules;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CRM.Core.Domain.Emotions;

namespace DeVeeraApp.ViewModels
{
    public class LevelModel : BaseEntityModel
    {
        public LevelModel()
        {
            Modules = new Modules();
            ImageLists = new List<Image>();
            this.AvailableVideo = new List<SelectListItem>();
            AvailableImages = new List<SelectListItem>();
            this.AvailableEmotions = new List<SelectListItem>();
            SelectedModuleImg = new List<string>();
            SelectedImg = new List<string>();
            SelectedModuleImages = new List<SelectedImage>();
            ModuleList = new List<ModulesModel>();
            SelectedImages = new List<SelectedImage>();
        }

        [Required(ErrorMessage ="Enter the level")]
        public int? LevelNo { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please select the video")]
        [Required]
        public int? VideoId { get; set; }

        public int BannerImageId { get; set; }
        public string BannerImageUrl { get; set; }

        public int VideoThumbImageId { get; set; }
        public string VideoThumbImageUrl { get; set; }

        public int ShareBackgroundImageId { get; set; }
        public string ShareBackgroundImageUrl { get; set; }

        public IList<SelectedImage> SelectedImages { get; set; }
        public IList<SelectedImage> SelectedModuleImages { get; set; }

        public IList<string> SelectedImg { get; set; }
        public IList<string> SelectedModuleImg { get; set; }

        public IList<string> SelectedEmotions { get; set; }
        [Required(ErrorMessage = "Enter the title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Enter the sub title")]
        public string Subtitle { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public string Quote { get; set; }
        public string Author { get; set; }

        public string VideoUrl { get; set; }
        public string VideoName { get; set; }
        public bool Active { get; set; }

        public string FullDescription { get; set; }

       
        [Required]
        public int EmotionId { get; set; }

        public Modules Modules { get; set; }
        public Video Video { get; set; }
        public Image Image { get; set; }
        public Emotion Emotion { get; set; }
        public IList<ModulesModel> ModuleList { get; set; }
        public List<Image> ImageLists { get; set; }
        public IList<SelectListItem> AvailableVideo { get; set; }
        public IList<SelectListItem> AvailableImages { get; set; }
        public IList<SelectListItem> AvailableEmotions { get; set; }
        [NotMapped]
        public int srno { get; set; }


        [NotMapped]
        public string DiaryText { get; set; }

        [NotMapped]
        public string DiaryLatestUpdateDate { get; set; }

        public string NextImageUrl { get; set; }
        public string PrevImageUrl { get; set; }
        public string NextTitle { get; set; }
        public string PrevTitle { get; set; }


    }

   
}

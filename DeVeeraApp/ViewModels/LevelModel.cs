using CRM.Core.Domain;
using CRM.Core.Domain.VideoModules;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DeVeeraApp.ViewModels.Enum;
using System.Linq;
using System.Threading.Tasks;
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
        }

        [Required]
        public int? LevelNo { get; set; }
        public int? VideoId { get; set; }
        public IList<SelectedImage> SelectedImages { get; set; }
        public IList<SelectedImage> SelectedModuleImages { get; set; }

        public IList<string> SelectedImg { get; set; }
        public IList<string> SelectedModuleImg { get; set; }

        public IList<string> SelectedEmotions { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Subtitle { get; set; }
     
        public string Quote { get; set; }
        public string Author { get; set; }

        public string VideoUrl { get; set; }
        public string VideoName { get; set; }
        public bool Active { get; set; }

        public string FullDescription { get; set; }
        public int EmotionId { get; set; }

        public Modules Modules { get; set; }
        public Video Video { get; set; }
        public Image Image { get; set; }
        public Emotion Emotion { get; set; }
        public IList<Modules>ModuleList { get; set; }
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

    }

   
}

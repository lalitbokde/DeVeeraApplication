using CRM.Core.Domain;
using CRM.Core.Domain.VideoModules;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CRM.Core.Domain.Emotions;
using DeVeeraApp.ViewModels.LayoutSetups;
using CRM.Core.ViewModels;
using DeVeeraApp.Utils;

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
            LikeModule = new List<LikesUnlikess>();
            CommentsModule= new List<LikesUnlikess>();
            LikeComments = new List<LikesUnlikess>();
            DisLikeUser = new List<LikesUnlikess>();
            LikeModule = new List<LikesUnlikess>();
            DisLikeModule = new List<LikesUnlikess>();
            AvilableQuotelevel = new List<SelectListItem>();
        }

        [Required(ErrorMessage ="Enter the level")]
        public int? LevelNo { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please select the video")]
        [Required]
        public int? VideoId { get; set; }
        public string YoutubeVideoUrl { get; set; }


        public int  QuoteId { get; set; }

        public bool IsRandom { get; set; }
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
        public string SpanishTitle { get; set; }
       // [Required(ErrorMessage = "Enter the sub title")]
        public string SpanishSubtitle { get; set; }

        public string SpanishTitleModule { get; set; }
        public string SpanishFullDescriptionModule { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public string Quote { get; set; }
        public string Author { get; set; }
        
        public string UserName { get; set; }
        public string VideoUrl { get; set; }
        public string VideoName { get; set; }
        public bool Active { get; set; }

        public string FullDescription { get; set; }

        public string SpanishFullDescription { get; set; }
        [Required]
        public int EmotionId { get; set; }

        public Modules Modules { get; set; }
        public Video Video { get; set; }
        public Image Image { get; set; }
        public Emotion Emotion { get; set; }
        public IList<ModulesModel> ModuleList { get; set; }
        public IList<ModulesViewModel> ModuleDataList { get; set; }
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

        [NotMapped]
        public LayoutSetupModel Titles { get; set; }
        [NotMapped]
        public LayoutSetupModel Description { get; set; }

        //Like Unlike section
        public int LikeId { get; set; }

        public int DisLikeId { get; set; }

        public bool IsLike { get; set; }
        public bool IsDisLike { get; set; }
        public string Comments { get; set; }
        public bool IsNew { get; set; }
        public IList<LevelViewModel> LevelListPaged { get; set; }
        public IList<LikesUnlikess> LikeUser { get; set; }
        public IList<LikesUnlikess> LikeComments { get; set; }
        public IList<LikesUnlikess> DisLikeUser { get; set; }
        public IList<LikesUnlikess> LikeModule { get; set; }
        public IList<LikesUnlikess> DisLikeModule { get; set; }
        public List<SelectListItem> AvilableQuotelevel { get; }
        public IList<LikesUnlikess> CommentsModule { get; set; }
    }

   
}

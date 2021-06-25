using CRM.Core.Domain;
using CRM.Core.Domain.VideoModules;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeVeeraApp.ViewModels
{
    public class ModulesModel : BaseEntityModel
    {
        public ModulesModel()
        {
            QuestionsList = new List<Questions>();
            SelectedModuleImages = new List<SelectedImage>();
        }
        public string Title { get; set; }
        public int LevelId { get; set; }
        public string VideoURL { get; set; }
        public int? VideoId { get; set; }

        public IList<SelectedImage> SelectedModuleImages { get; set; }
        public string FullDescription { get; set; }
        public virtual Level Level { get; set; }

        [NotMapped]
        public virtual Video Video { get; set; }

        public int BannerImageId { get; set; }
        public string BannerImageUrl { get; set; }

        public int VideoThumbImageId { get; set; }
        public string VideoThumbImageUrl { get; set; }

        public int ShareBackgroundImageId { get; set; }
        public string ShareBackgroundImageUrl { get; set; }


        [NotMapped]
        public string DiaryText { get; set; }

        [NotMapped]
        public string DiaryLatestUpdateDate { get; set; }

        [NotMapped]
        public List<Questions> QuestionsList { get; set; }
        public string NextImageUrl { get; set; }
        public string PrevImageUrl { get; set; }
        public string NextTitle { get; set; }
        public string PrevTitle { get; set; }

        public string NextLevelUrl { get; set; }
        public string NextLeveltitle { get; set; }


    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Core.Domain.VideoModules
{
    public class Modules : BaseEntity
    {
        public int LevelId { get; set; }
        public int? VideoId { get; set; }
        public string Title { get; set; }
        public string FullDescription { get; set; }
     
        public virtual Level Level { get; set; }
        public virtual Video Video { get; set; }

        public int BannerImageId { get; set; }
        [NotMapped]
        public string BannerImageUrl { get; set; }

        public int VideoThumbImageId { get; set; }
        [NotMapped]
        public string VideoThumbImageUrl { get; set; }

        public int ShareBackgroundImageId { get; set; }
        [NotMapped]
        public string ShareBackgroundImageUrl { get; set; }

        //Like Unlike section
        public int LikeId { get; set; }

        public int DisLikeId { get; set; }
        public bool IsNew { get; set; }
        public bool IsLike { get; set; }
        public bool IsDisLike { get; set; }
        public string Comments { get; set; }

    }
}

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

    }
}

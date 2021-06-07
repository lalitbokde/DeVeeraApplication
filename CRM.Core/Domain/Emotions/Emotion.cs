using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Domain.Emotions
{
    public class Emotion: BaseEntity
    {
        public int? EmotionNo { get; set; }
        public int VideoId { get; set; }
        public int ContentImageId { get; set; }
        public int BannerImageId { get; set; }
        public int ThumbnailImageId { get; set; }
        public string EmotionName { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Quote { get; set; }
        public string Description { get; set; }  
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public bool Deleted { get; set; }
        public virtual Video Video { get; set; }
    }
}

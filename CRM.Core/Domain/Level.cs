using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Domain
{
    public class Level : BaseEntity
    {
        public int? LevelNo { get; set; }
        public int? VideoId { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        //public string Quote { get; set; }
        public string FullDescription { get; set; }
        public bool Active { get; set; }
        public EmotionType Emotions { get; set; }
        public virtual Video Video { get; set; }
    }


    public enum EmotionType
    {
        Fear = 1,
        Anger = 2,
        Lust = 3,
        Sadness = 4,
        Ego = 5,
        Happy = 6
    }
}

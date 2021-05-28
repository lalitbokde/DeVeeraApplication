using CRM.Core.Domain.Emotions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Domain
{
    public class Level : BaseEntity
    {
        private ICollection<Level_Emotion_Mapping> _Level_Emotion_Mappings;

        public int? LevelNo { get; set; }
        public int? VideoId { get; set; }
        public int EmotionId { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        //public string Quote { get; set; }
        public string FullDescription { get; set; }
        public bool Active { get; set; }
        //public virtual Emotion Emotion { get; set; }
        public virtual Video Video { get; set; }

        public virtual ICollection<Level_Emotion_Mapping> Level_Emotion_Mappings
        {
            get { return _Level_Emotion_Mappings ?? (_Level_Emotion_Mappings = new List<Level_Emotion_Mapping>()); }
            protected set { _Level_Emotion_Mappings = value; }
        }

    }


    //public enum EmotionType
    //{
    //    Fear = 1,
    //    Anger = 2,
    //    Lust = 3,
    //    Sadness = 4,
    //    Ego = 5,
    //    Happy = 6
    //}
}

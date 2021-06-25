using System;

namespace CRM.Core.Domain.Emotions
{
    public class Level_Emotion_Mapping:BaseEntity
    {
        public int LevelId { get; set; }
        public int EmotionId { get; set; }
        public bool CurrentEmotion { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public bool Deleted { get; set; }
        public virtual Emotion Emotion { get; set; }
    }
}

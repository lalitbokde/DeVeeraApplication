using System;

namespace CRM.Core.Domain.Emotions
{
    public class User_Emotion_Mapping:BaseEntity
    {
        public int UserId { get; set; }
        public int EmotionId { get; set; }
        public bool CurrentEmotion { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public bool Deleted { get; set; }
        //public virtual User User { get; set; }
        public virtual Emotion Emotion { get; set; }
    }
}

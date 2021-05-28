using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Domain.Emotions
{
    public class Emotion: BaseEntity
    {
        public int? EmotionNo { get; set; }
        public string EmotionName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public bool Deleted { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels.Emotions
{
    public class EmotionModel:BaseEntityModel
    {
        public EmotionModel()
        {
            EmotionList = new List<EmotionModel>();
        }
        public int? EmotionNo { get; set; }
        public string EmotionName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public bool Deleted { get; set; }
        public bool Remember { get; set; }
        public List<EmotionModel> EmotionList { get; set; }
    }
}

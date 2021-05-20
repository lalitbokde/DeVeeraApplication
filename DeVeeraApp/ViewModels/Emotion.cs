using DeVeeraApp.ViewModels.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels
{
    public class Emotion : BaseEntityModel
    {
        [Required(ErrorMessage ="Please select emotion.")]
        public EmotionType Emotions { get; set; }
    }

}

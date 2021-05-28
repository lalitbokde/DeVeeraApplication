using CRM.Core.Domain.Emotions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Services.Emotions
{
    public interface IEmotionService
    {
        void DeleteEmotion(Emotion emotion);
        IList<Emotion> GetAllEmotions();
        Emotion GetEmotionById(int emotionId);
        void InsertEmotion(Emotion emotion);
        void UpdateEmotion(Emotion emotion);
    }
}

using CRM.Core.Domain.Emotions;
using System.Collections.Generic;

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

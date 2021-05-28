using CRM.Core.Domain.Emotions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Services.Emotions
{
    public interface IEmotionMappingService
    {
        void DeleteEmotion_Mapping(User_Emotion_Mapping emotion);
        IList<User_Emotion_Mapping> GetAllEmotion_Mapping();
        User_Emotion_Mapping GetEmotion_MappingById(int emotionId);
        IList<User_Emotion_Mapping> GetEmotion_MappingByUserId(int UserId);
        void InsertEmotion_Mapping(User_Emotion_Mapping emotion);
        void UpdateEmotion_Mapping(User_Emotion_Mapping emotion);
        void InActiveUserEmotion(int UserId);
    }
}

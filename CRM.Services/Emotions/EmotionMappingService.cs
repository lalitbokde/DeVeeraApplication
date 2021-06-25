using CRM.Core.Domain.Emotions;
using CRM.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Services.Emotions
{
    public class EmotionMappingService:IEmotionMappingService
    {
        #region fields
        private readonly IRepository<User_Emotion_Mapping> _emotionRepository;

        #endregion

        #region ctor
        public EmotionMappingService(IRepository<User_Emotion_Mapping> emotionRepository)
        {
            _emotionRepository = emotionRepository;
        }
        #endregion


        #region Methods
        public void DeleteEmotion_Mapping(User_Emotion_Mapping emotion)
        {
            if (emotion == null)
                throw new ArgumentNullException(nameof(emotion));

            _emotionRepository.Delete(emotion);
        }

        public IList<User_Emotion_Mapping> GetAllEmotion_Mapping()
        {
            var query = from a in _emotionRepository.Table
                        orderby a.Id
                        select a;
            var emotion = query.ToList();
            return emotion;
        }

        public User_Emotion_Mapping GetEmotion_MappingById(int emotionId)
        {
            if (emotionId == 0)
                return null;

            return _emotionRepository.GetById(emotionId);
        }

        public IList<User_Emotion_Mapping> GetEmotion_MappingByUserId(int UserId)
        {
            if (UserId == 0)
                return null;
            var query = from a in _emotionRepository.Table
                        where a.UserId == UserId
                        select a;

            var data = query.ToList();

            return data;
        }

        public void InsertEmotion_Mapping(User_Emotion_Mapping emotion)
        {
            if (emotion == null)
                throw new ArgumentNullException(nameof(emotion));

            _emotionRepository.Insert(emotion);
        }

        public void UpdateEmotion_Mapping(User_Emotion_Mapping emotion)
        {
            if (emotion == null)
                throw new ArgumentNullException(nameof(emotion));

            _emotionRepository.Update(emotion);
        }

        public void InActiveUserEmotion(int UserId)
        {
            var datalist = GetEmotion_MappingByUserId(UserId).Where(a => a.CurrentEmotion == true).ToList();

            if (datalist.Count() != 0)
            {
                foreach (var item in datalist)
                {
                    item.CurrentEmotion = false;

                    _emotionRepository.Update(item);
                }

            }

        }

        #endregion
    }
}

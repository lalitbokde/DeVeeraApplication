using CRM.Core.Domain.Emotions;
using CRM.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CRM.Services.Emotions
{
    public class EmotionService:IEmotionService
    {
        #region fields
        private readonly IRepository<Emotion> _emotionRepository;

        #endregion

        #region ctor
        public EmotionService(IRepository<Emotion> emotionRepository)
        {
            _emotionRepository = emotionRepository;
        }
        #endregion


        #region Methods
        public void DeleteEmotion(Emotion emotion)
        {
            if (emotion == null)
                throw new ArgumentNullException(nameof(emotion));

            _emotionRepository.Delete(emotion);
        }

        public IList<Emotion> GetAllEmotions()
        {
            var query = from a in _emotionRepository.Table
                        orderby a.EmotionNo
                        select a;
            var emotion = query.ToList();
            return emotion;
        }

        public Emotion GetEmotionById(int emotionId)
        {
            if (emotionId == 0)
                return null;

            return _emotionRepository.GetById(emotionId);
        }

        public void InsertEmotion(Emotion emotion)
        {
            if (emotion == null)
                throw new ArgumentNullException(nameof(emotion));

            _emotionRepository.Insert(emotion);
        }

        public void UpdateEmotion(Emotion emotion)
        {
            if (emotion == null)
                throw new ArgumentNullException(nameof(emotion));

            _emotionRepository.Update(emotion);
        }

        #endregion
    }
}

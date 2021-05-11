using CRM.Core.Domain.VideoModules;
using CRM.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CRM.Services.QuestionsAnswer
{
    public class QuestionAnswerService: IQuestionAnswerService
    {
        #region fields
        private readonly IRepository<Questions> _questionsRepository;

        #endregion

        #region ctor
        public QuestionAnswerService(IRepository<Questions> questionsRepository)
        {
            _questionsRepository = questionsRepository;
        }
        #endregion


        #region Methods
        public void DeleteQuestion(Questions question)
        {
            if (question == null)
                throw new ArgumentNullException(nameof(question));

            _questionsRepository.Delete(question);
        }

        public IList<Questions> GetAllQuestions()
        {
            var query = from vdo in _questionsRepository.Table
                        orderby vdo.Id
                        select vdo;
            var questions = query.ToList();
            return questions;
        }

        public Questions GetQuestionById(int QuestionId)
        {
            if (QuestionId == 0)
                return null;

            return _questionsRepository.GetById(QuestionId);
        }


        public IList<Questions> GetQuestionsByModuleId(int ModuleId)
        {
            if (ModuleId == 0)
                return null;
            var query = from a in _questionsRepository.Table
                        where a.ModuleId == ModuleId
                        select a;

            var data = query.ToList();

            return data;
        }

        public void InsertQuestion(Questions question)
        {
            if (question == null)
                throw new ArgumentNullException(nameof(question));

            _questionsRepository.Insert(question);
        }

        public void UpdateQuestion(Questions question)
        {
            if (question == null)
                throw new ArgumentNullException(nameof(question));



            _questionsRepository.Update(question);
        }


        #endregion
    }
}

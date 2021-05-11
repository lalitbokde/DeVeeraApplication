using CRM.Core.Domain.VideoModules;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Services.QuestionsAnswer
{
    public interface IQuestionAnswerService
    {
        void DeleteQuestion(Questions question);
        IList<Questions> GetAllQuestions();
        IList<Questions> GetQuestionsByModuleId(int ModuleId);
        Questions GetQuestionById(int questionId);
        void InsertQuestion(Questions question);
        void UpdateQuestion(Questions question);
    }
}

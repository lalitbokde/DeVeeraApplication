using CRM.Core.Domain.VideoModules;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Services
{
    public interface IQuestionAnswerMappingService
    {
        IList<Question_Answer_Mapping> GetAllAnswerByUserId(int UserId);
    }
}

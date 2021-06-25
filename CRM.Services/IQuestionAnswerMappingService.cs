using CRM.Core.Domain.VideoModules;
using System.Collections.Generic;

namespace CRM.Services
{
    public interface IQuestionAnswerMappingService
    {
        IList<Question_Answer_Mapping> GetAllAnswerByUserId(int UserId);
    }
}

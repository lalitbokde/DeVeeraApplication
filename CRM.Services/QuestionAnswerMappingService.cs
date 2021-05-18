using CRM.Core.Domain.VideoModules;
using CRM.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CRM.Services
{
    public class QuestionAnswerMappingService : IQuestionAnswerMappingService
    {
        #region fields
        private readonly IRepository<Question_Answer_Mapping> _repository;
        #endregion



        #region ctor
        public QuestionAnswerMappingService(IRepository<Question_Answer_Mapping> repository)
        {
            
            _repository = repository;
        }

        #endregion


        #region Methods
        public IList<Question_Answer_Mapping> GetAllAnswerByUserId(int UserId)
        {
            if(UserId == 0)
            {
                throw new ArgumentNullException(nameof(UserId));
            }

            var data = _repository.Table.Where(a => a.UserId == UserId).ToList();
            return data;

        }

        #endregion
    }
}

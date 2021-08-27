using CRM.Core.Domain;
using CRM.Data;
using CRM.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Services.Likes
{
  public  class LikesService :ILikesService
    {
        #region fields
        private readonly IRepository<LikesUnlikess> _likesrepository;
        protected readonly dbContextCRM _dbContext;
        #endregion


        #region ctor
        public LikesService(dbContextCRM dbContext, IRepository<LikesUnlikess> likesRepository)
        {
            this._dbContext = dbContext;
            _likesrepository = likesRepository;
        }

        #endregion

        #region Method

        public IList<LikesUnlikess> GetAllLikes()
        {
            var data = _likesrepository.Table.ToList();

            return data.ToList();
        }


        public void InsertLikes(LikesUnlikess model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _likesrepository.Insert(model);
        }

        public void UpdateLikes(LikesUnlikess model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _likesrepository.Update(model);
        }
        public LikesUnlikess GetLikesByUserId(int UserId)
        {
            var data = _likesrepository.Table.Where(a => a.UserId == UserId).FirstOrDefault();
            return data;
        }
        #endregion
    }
}

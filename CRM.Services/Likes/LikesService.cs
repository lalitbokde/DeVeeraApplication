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
        public IList<LikesUnlikess> GetLikesByLevelId(int LevelId)
        {
            var data = _likesrepository.Table.Where(a => a.LevelId == LevelId && a.IsLike==true ).ToList();
            return data.ToList();
        }
        public IList<LikesUnlikess> GetCommenntsByLevelId(int LevelId)
        {
            var data = _likesrepository.Table.Where(a => a.LevelId == LevelId).ToList();
            return data.ToList();
        }
        public IList<LikesUnlikess> GetCommenntsByModuleId(int ModuleId)
        {
            var data = _likesrepository.Table.Where(a => a.ModuleId == ModuleId ).ToList();
            return data.ToList();
        }
        public IList<LikesUnlikess> GetDislikesByLevelId(int LevelId)
        {
            var data = _likesrepository.Table.Where(a => a.LevelId == LevelId && a.IsDisLike == true).ToList();
            return data.ToList();
        }
        public IList<LikesUnlikess> GetLikesByModuleId(int ModuleId)
        {
            var data = _likesrepository.Table.Where(a => a.ModuleId == ModuleId && a.IsLike == true).ToList();
            return data.ToList();
        }
        public IList<LikesUnlikess> GetDisLikesByModuleId(int ModuleId)
        {
            var data = _likesrepository.Table.Where(a => a.ModuleId == ModuleId && a.IsDisLike == true).ToList();
            return data.ToList();
        }
        public LikesUnlikess GetLikesByLevelIdandUserId(int LevelId, int UserId)
        {
            var data = _likesrepository.Table.Where(a => a.LevelId == LevelId && a.UserId == UserId).FirstOrDefault();
            return data;
        }
        public LikesUnlikess GetLikesByModuleIdandUserId(int ModuleId, int UserId)
        {
            var data = _likesrepository.Table.Where(a => a.ModuleId == ModuleId && a.UserId == UserId).FirstOrDefault();
            return data;
        }
        #endregion
    }
}

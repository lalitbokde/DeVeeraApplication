using CRM.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Services.Likes
{
    public interface ILikesService
    {

        IList<LikesUnlikess> GetAllLikes();
        void InsertLikes(LikesUnlikess model);


        void UpdateLikes(LikesUnlikess model);
        public LikesUnlikess GetLikesByUserId(int UserId);
        public IList<LikesUnlikess> GetLikesByLevelId(int LevelId);
        public IList<LikesUnlikess> GetLikesByModuleId(int ModuleId);
        public LikesUnlikess GetLikesByLevelIdandUserId(int LevelId , int UserId);
        public LikesUnlikess GetLikesByModuleIdandUserId(int ModuleId, int UserId);
    }
}
